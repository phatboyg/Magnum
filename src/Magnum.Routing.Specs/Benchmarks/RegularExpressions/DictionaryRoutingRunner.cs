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
	using System.Linq;


	public class DictionaryRoutingRunner :
		RoutingRunner
	{
		IDictionary<string, string> _routes;

		public DictionaryRoutingRunner()
		{
			_routes = new Dictionary<string, string>();
		}

		public void AddRoutes(IEnumerable<string> paths)
		{
			_routes = paths.ToDictionary(x => x, x => x);
		}

		public void Route(Uri uri)
		{
			string absolutePath = uri.AbsolutePath;

			if (_routes.ContainsKey(absolutePath))
				return;

			throw new InvalidOperationException("Invalid match URI");
		}
	}
}