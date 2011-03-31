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
namespace Magnum.Routing.Engine
{
	using System;
	using System.Collections.Generic;
	using Nodes;


	public class MagnumRoutingEngine<TContext> :
		RoutingEngine<TContext>
	{
		readonly Func<TContext, Uri> _getUri;
		readonly Activation<TContext> _network;

		public MagnumRoutingEngine(Func<TContext, Uri> getUri)
		{
			_getUri = getUri;
			_network = new RootNode<TContext>();
		}

		public void Route(TContext context, Action<RouteMatch<TContext>> callback)
		{
			Uri uri = _getUri(context);

			var routeContext = new RouteContextImpl<TContext>(context, uri);

			_network.Activate(routeContext, uri.PathAndQuery);

			routeContext.Resolve();

			RouteMatch<TContext> matched = routeContext.Match;
			if (matched == null)
				return;

			callback(matched);
		}

		public IEnumerable<T> Match<T>()
			where T : class
		{
			return _network.Match<T>();
		}
	}
}