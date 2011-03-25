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
namespace Magnum.Routing
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Conditions;


	public class MagnumRoutingEngine :
		RoutingEngine
	{
		RouteCondition _conditions;

		public MagnumRoutingEngine()
		{
			_conditions = new RootRouteCondition();
		}

		public void Route<T>(T context, Uri uri, Action<RouteMatch> callback)
		{
			var routeContext = new RouteContextImpl<T>(context, uri);

			_conditions.Activate(routeContext, uri.PathAndQuery);

			RouteMatch matched = routeContext.Matches.FirstOrDefault();
			if (matched == null)
				return;

			callback(matched);
		}

		public IEnumerable<T> Match<T>() 
			where T : class
		{
			return _conditions.Match<T>();
		}

		public void AddActivation(Activation activation)
		{
			_conditions.AddActivation(activation);
		}
	}
}