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
	using System.Diagnostics;


	public class RunResult
	{
		public RunResult()
		{
		}

		protected RunResult(RunResult other)
		{
			Iterations = other.Iterations;
			Duration = other.Duration;
			Description = other.Description;
			SubjectType = other.SubjectType;
			BenchmarkType = other.BenchmarkType;
			RunnerType = other.RunnerType;
		}

		public int Iterations { get; set; }
		public long Duration { get; set; }

		public string Description { get; set; }

		public Type SubjectType { get; set; }

		public Type BenchmarkType { get; set; }

		public TimeSpan TimeDuration
		{
			get { return TimeSpan.FromSeconds(Duration*1d/Stopwatch.Frequency); }
		}

		public Type RunnerType { get; set; }
	}
}