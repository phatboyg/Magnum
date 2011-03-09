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
namespace Magnum.Specs.Pipeline.Benchmarks
{
	using System;
	using System.Collections.Generic;
	using System.Threading;
	using Magnum.Concurrency;
	using Magnum.Extensions;


	public class MultithreadedPipelineBenchmark :
		Benchmark<PipelineRunner>
	{
		public IEnumerable<int> Iterations
		{
			get { return new[] {1000000}; }
		}

		public void WarmUp(PipelineRunner instance)
		{
			instance.SendMessage();
			instance.Reset();
		}

		public void Shutdown(PipelineRunner instance)
		{
		}

		public void Run(PipelineRunner instance, int iterationCount)
		{
			var complete = new ManualResetEvent(false);

			var latch = new CountdownLatch(10, () => complete.Set());

			for (int i = 0; i < 10; i++)
			{
				ThreadPool.QueueUserWorkItem(x =>
					{
						for (int j = 0; j < iterationCount/10; j++)
							instance.SendMessage();

						latch.CountDown();
					});
			}

			complete.WaitOne(5.Minutes());
		}
	}
}