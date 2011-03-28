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
	using System.Text.RegularExpressions;


	public class RegularExpressionRoutingRunner :
		RoutingRunner
	{
		IList<Regex> _routes;

		public RegularExpressionRoutingRunner()
		{
			_routes = new List<Regex>();
		}

		public void AddRoutes(IEnumerable<string> paths)
		{
			_routes = paths.Select(x => new Regex("^" + x + "$")).ToList();
		}

		public void Route(Uri uri)
		{
			string absolutePath = uri.AbsolutePath;

			for (int i = 0; i < _routes.Count; i++)
			{
				Match match = _routes[i].Match(absolutePath);
				if (match.Success)
					return;
			}

			throw new InvalidOperationException("Invalid match URI");
		}
	}
}