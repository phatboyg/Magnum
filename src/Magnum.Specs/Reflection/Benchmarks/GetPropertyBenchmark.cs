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
namespace Magnum.Specs.Reflection.Benchmarks
{
	using System.Collections.Generic;


	public class GetPropertyBenchmark :
		Benchmark<GetPropertyRunner>
	{
		public IEnumerable<int> Iterations
		{
			get { return new[] {1000000}; }
		}

		public void WarmUp(GetPropertyRunner instance)
		{
			instance.GetValue(0);
			instance.GetText(0);
		}

		public void Shutdown(GetPropertyRunner instance)
		{
		}

		public void Run(GetPropertyRunner instance, int iterationCount)
		{
			for (int i = 0; i < iterationCount; i++)
			{
				instance.GetValue(i);
				instance.GetText(i);
			}
		}
	}
}