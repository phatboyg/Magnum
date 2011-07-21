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
namespace Magnum.Routing.Specs.Benchmarks.RegularExpressions
{
	using System;
	using System.Collections.Generic;


	public class SmallPageSetRoutingBenchmark :
		Benchmark<RoutingRunner>
	{
		public IEnumerable<int> Iterations
		{
			get { return new[] {5000, 100000}; }
		}

		public void WarmUp(RoutingRunner instance)
		{
			instance.AddRoutes(GenerateRoutes());

			instance.Route(new Uri("http://localhost/aaaaaaaa"));
		}

		public void Shutdown(RoutingRunner instance)
		{
		}

		public void Run(RoutingRunner instance, int iterationCount)
		{
			var uri = new Uri("http://localhost/pppppppp");

			for (int i = 0; i < iterationCount; i++)
				instance.Route(uri);
		}

		static IEnumerable<string> GenerateRoutes()
		{
			for (char c = 'a'; c <= 'z'; c++)
			{
				string s = @"/" + new string(c, 8);

				yield return s;
			}
		}
	}
}