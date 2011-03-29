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
namespace Magnum.Routing.Configuration
{
	using System;
	using Builders;


	public class RouteConfiguratorImpl<TContext> :
		RouteConfigurator<TContext>,
		RoutingEngineBuilderConfigurator<TContext>
		where TContext : class
	{
		readonly string _path;
		Func<RouteBuilder<TContext>> _builderFactory;

		public RouteConfiguratorImpl(string path)
		{
			_path = path;
		}

		public void Configure(RoutingEngineBuilder<TContext> builder)
		{
			var routeBuilder = _builderFactory();

			// again, need to run configurators against the route for defaults, constraints, etc.
			// depending upon the builder, many of these may be handled via types and default arguments

			// not sure, but I think I want the navigation of the routes to be common
			builder.AddRouteBuilder(routeBuilder);
		}

		public void Validate()
		{
			if (_builderFactory == null)
				throw new RoutingConfigurationException("No route builder was specified for route: " + _path);
		}

		public void UseBuilder(Func<RouteBuilder<TContext>> builderFactory)
		{
			_builderFactory = builderFactory;
		}
	}
}