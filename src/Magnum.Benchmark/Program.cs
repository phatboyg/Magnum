// Copyright 2007-2010 The Apache Software Foundation.
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace Magnum.Benchmark
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using Extensions;
	using Reflection;
	using TypeScanning;


	class Program
	{
		static void Main(string[] argv)
		{
			if (argv.Length == 0)
			{
				Console.WriteLine("usage: <assembly name> [filter]");
				return;
			}

			Assembly specs = Assembly.Load(argv[0]);

			string filter = null;
			if (argv.Length >= 2)
				filter = argv[1];

			specs.GetTypes()
				.Where(type => type.Implements(typeof(Benchmark<>)) && type.IsConcrete())
				.Select(type => new { Type = type, InputType = type.GetDeclaredGenericArguments().Single() })
				.Where(type => filter == null || type.InputType.Name.Contains(filter))
				.OrderBy(x => x.InputType.Name)
				.Each(benchmark => {

						Type[] subjectTypes = specs.GetTypes()
							.Where(type => type.Implements(benchmark.InputType))
							.Where(type => type.IsConcrete())
							.OrderBy(x => x.Name)
							.ToArray();

						var runner = (BenchmarkRunner)FastActivator.Create(typeof(BenchmarkRunner<>),
																		   new[] { benchmark.InputType },
																		   new object[] { benchmark.Type, subjectTypes });

						IEnumerable<RunResult> results = runner.Run();

						Display(results);
					});
		}

		static void Display(IEnumerable<RunResult> results)
		{
			results.GroupBy(x => x.Iterations)
				.Each(DisplayGroupResults);
		}

		static void DisplayGroupResults(IGrouping<int, RunResult> group)
		{
			Console.WriteLine("Benchmark {0}, Runner {1}, {2} iterations", group.First().BenchmarkType.Name,
			                  group.First().RunnerType.Name, group.Key);

			Console.WriteLine();
			Console.WriteLine("{0,-30}{1,-14}{2,-12}{3,-10}{4}", "Implementation", "Duration", "Difference", "Each",
			                  "Multiplier");
			Console.WriteLine(new string('=', 78));

			IOrderedEnumerable<RunResult> ordered = group.OrderBy(x => x.Duration);

			RunResult best = ordered.First();


			ordered.Select(x => new DisplayResult(x, best))
				.Each(DisplayResult);

			Console.WriteLine();
		}

		static void DisplayResult(DisplayResult result)
		{
			string testSubject = result.SubjectType.Name.Replace(result.RunnerType.Name, "");

			Console.WriteLine("{0,-30}{1,-14}{2,-12}{3,-10}{4}", testSubject, result.TimeDuration.ToFriendlyString(),
			                  result.TimeDifference.ToFriendlyString(), result.DurationPerIteration,
			                  result.PercentageDifference > 1m ? result.PercentageDifference.ToString("F2") + "x" : "");
		}
	}
}