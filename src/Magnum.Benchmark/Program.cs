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
	using Benchmarking;
	using Extensions;
	using Reflection;
	using TypeScanning;


	class Program
	{
		static void Main()
		{
			Assembly specs = Assembly.Load("Magnum.Specs");

			specs.GetTypes()
				.Where(type => type.Implements(typeof(Benchmark<>)))
				.Each(benchmarkType =>
					{
						Type inputType = benchmarkType.GetDeclaredGenericArguments().Single();

						Type[] subjectTypes = specs.GetTypes()
							.Where(type => type.Implements(inputType))
							.Where(type => type.IsConcrete())
							.ToArray();

						var runner = (BenchmarkRunner)FastActivator.Create(typeof(BenchmarkRunner<>),
						                                                   new[] {inputType},
						                                                   new object[] {benchmarkType, subjectTypes});

						var results = runner.Run();

						DisplayResults(results);
					});
		}

		static void DisplayResults(IEnumerable<RunResult> results)
		{
			results.GroupBy(x => x.Iterations)
				.Each(group =>
					{
						Console.WriteLine("{0}, {1} iterations", group.First().BenchmarkType, group.Key);
						Console.WriteLine(new string('=', 78));

						group.OrderBy(x => x.Duration).Each(result =>
							{
								Console.WriteLine("{0,-30} {1,-10}", result.SubjectType.Name, result.Duration.TotalMilliseconds);
							});

						Console.WriteLine();
					});

		}
	}
}