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
namespace Magnum.Routing.Specs.AspNetRouting
{
	using System.Web.Routing;
	using NUnit.Framework;


	[TestFixture]
	public class Routing_to_a_controller_action
	{
		[Test]
		public void Should_work_for_no_arguments()
		{
			var context = new StubHttpContextForRouting(requestUrl: "~/aaaaaaaa");
			var routes = new RouteCollection();

			routes.Add(new Route("{controller}", new StubRouteHandler()));

			RouteData routeData = routes.GetRouteData(context);

			// Assert
			Assert.IsNotNull(routeData);
			Assert.AreEqual("aaaaaaaa", routeData.Values["controller"]);
//			Assert.AreEqual("Index", routeData.Values["action"]);
		}
	}
}