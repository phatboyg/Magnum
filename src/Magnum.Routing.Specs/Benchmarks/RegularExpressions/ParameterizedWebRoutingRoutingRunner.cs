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
	using System.Web.Routing;
	using Extensions;


	public class ParameterizedWebRoutingRoutingRunner :
		RoutingRunner
	{
		StubHttpContextForRouting _httpContext;
		IDictionary<string, string> _matches;
		RouteCollection _routes;

		public ParameterizedWebRoutingRoutingRunner()
		{
			_routes = new RouteCollection();
			_matches = new Dictionary<string, string>();

			_httpContext = new StubHttpContextForRouting();
		}

		public void AddRoutes(IEnumerable<string> paths)
		{
			paths.Each(x => _matches.Add(x.Substring(1), x));

			_routes.Add(new Route("{controller}", new StubRouteHandler()));
		}

		public void Route(Uri uri)
		{
			_httpContext.SetRequestUrl("~" + uri.AbsolutePath);

			RouteData routeData = _routes.GetRouteData(_httpContext);

			if (routeData != null)
			{
				var s = (string)routeData.Values["controller"];
				if (_matches.ContainsKey(s))
					return;
			}

			throw new InvalidOperationException("Invalid match URI");
		}
	}
}