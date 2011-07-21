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
namespace Magnum.Routing.Specs
{
	using System;
	using NUnit.Framework;
	using TestFramework;


	[TestFixture]
	public class RoutingFactory_Specs
	{
		[Test]
		public void Using_the_factory_dsl_to_create_a_routing_engine()
		{
			RoutingEngine<Context> engine = RoutingEngineFactory.New<Context>(x =>
				{
					// specify the Uri accessor from the context type
					x.Uri(context => context.Uri);

					x.Route("version").To(context => { });

					//x.Route<int, int, int>((year, month, day) => "archive/{year}/{month}/{day}");

					//					x.Route("archive/{year}/{month}/{day}", r =>
					//						{
					//							r.Default("year", DateTime.Now.Year);
					//
					//							r.Constrain("year").To<int>(1970, 2099);
					//							r.Constrain("month").To<int>(1, 12);
					//							r.Constrain("day").To<int>()
					//								.Where(k => k >= 1)
					//								.Where((y,m,d) => k <= )
					//							r.Default("month", DateTime.Now.Month);
					//							r.Default("day", DateTime.Now.Day);
					//
					//						});
				});

			engine.ShouldNotBeNull();

			/*
			Route reportRoute = new Route("{locale}/{year}", new ReportRouteHandler());
    reportRoute.Defaults = new RouteValueDictionary { { "locale", "en-US" }, { "year", DateTime.Now.Year.ToString() } };
    reportRoute.Constraints = new RouteValueDictionary { { "locale", "[a-z]{2}-[a-z]{2}" }, { "year", @"\d{4}" } };
    reportRoute.DataTokens = new RouteValueDictionary { { "format", "short" } };
    routes.Add(reportRoute);
			 * */
		}


		class Context
		{
			public Uri Uri { get; set; }
		}
	}
}