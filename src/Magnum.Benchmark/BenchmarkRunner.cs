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
	using System.Diagnostics;
	using System.Linq;
	using Reflection;


	public interface BenchmarkRunner
	{
		IEnumerable<RunResult> Run();
	}


	public class BenchmarkRunner<T> :
		BenchmarkRunner
	{
		readonly Type _benchmarkType;
		readonly Type[] _subjectTypes;
		Benchmark<T> _benchmark;
		int _runCount = 3;

		public BenchmarkRunner(Type benchmarkType, Type[] subjectTypes)
		{
			_benchmarkType = benchmarkType;
			_subjectTypes = subjectTypes;

			_benchmark = (Benchmark<T>)FastActivator.Create(_benchmarkType);
		}

		public IEnumerable<RunResult> Run()
		{
			foreach (int loopCount in _benchmark.Iterations)
			{
				foreach (Type subjectType in _subjectTypes)
				{
					IEnumerable<RunResult> metrics = RunMeasurement(subjectType, loopCount);

					var best = metrics.OrderByDescending(x => x.Duration)
						.First();

					yield return best;
				}
			}
		}


		IEnumerable<RunResult> RunMeasurement(Type subjectType, int loopCount)
		{
			var metrics = new List<RunResult>();

			for (int i = 0; i < _runCount; i++)
			{
				GC.Collect();
				var subject = (T)FastActivator.Create(subjectType);

				long duration = Measure(subject, loopCount);

				if (subject is IDisposable)
					((IDisposable)subject).Dispose();

				metrics.Add(new RunResult
					{
						Description = subjectType.Name,
						BenchmarkType = _benchmarkType,
						RunnerType = typeof(T),
						SubjectType = subjectType,
						Iterations = loopCount,
						Duration = duration,
					});
			}

			return metrics;
		}

		long Measure(T subject, int iteration)
		{
			_benchmark.WarmUp(subject);

			GC.Collect();

			long begin = Stopwatch.GetTimestamp();

			_benchmark.Run(subject, iteration);

			long end = Stopwatch.GetTimestamp();

			long ticksTaken = (end - begin);

			return ticksTaken;
		}
	}
}