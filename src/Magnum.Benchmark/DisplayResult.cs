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


	public class DisplayResult :
		RunResult
	{
		public DisplayResult(RunResult self, RunResult best)
			: base(self)
		{
			DurationDifference = self.Duration - best.Duration;

			var bestPerIteration = Math.Round((decimal)best.Duration/best.Iterations, 8);
			var selfPerIteration = Math.Round((decimal)Duration/Iterations, 8);
			PercentageDifference = bestPerIteration != 0m ? Math.Round(selfPerIteration/bestPerIteration, 6) : 0m;
		}

		public long DurationDifference { get; set; }

		public decimal PercentageDifference { get; set; }

		public decimal DurationPerIteration
		{
			get { return Math.Round((decimal)Duration/Iterations, 4); }
		}

		public TimeSpan TimeDifference
		{
			get { return TimeSpan.FromSeconds(DurationDifference*1d/Stopwatch.Frequency); }
		}
	}
}