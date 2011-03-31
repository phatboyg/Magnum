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
namespace Magnum.Routing.Builders
{
	using System;
	using System.Collections.Generic;
	using System.Linq.Expressions;
	using Engine;


	public class MagnumRoutingEngineBuilder<TContext> :
		RoutingEngineBuilder<TContext>
		where TContext : class
	{
		readonly Func<TContext, Uri> _getUri;
		readonly IList<RouteBuilder<TContext>> _routeBuilders;

		public MagnumRoutingEngineBuilder(Expression<Func<TContext, Uri>> getUri)
		{
			_getUri = getUri.Compile();
			_routeBuilders = new List<RouteBuilder<TContext>>();
		}

		public void AddRouteBuilder(RouteBuilder<TContext> routeBuilder)
		{
			_routeBuilders.Add(routeBuilder);
		}

		public RoutingEngine<TContext> Build()
		{
			var engine = new MagnumRoutingEngine<TContext>(_getUri);

			foreach (var routeBuilder in _routeBuilders)
				routeBuilder.Build(engine);

			return engine;
		}
	}
}