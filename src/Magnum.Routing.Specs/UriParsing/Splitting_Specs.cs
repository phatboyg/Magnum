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
namespace Magnum.Routing.Specs.UriParsing
{
	using Model;
	using NUnit.Framework;
	using TestFramework;


	[TestFixture]
	public class When_converting_a_parameterized_pattern_to_a_route_definition
	{
		const string Pattern = "{controller}/{action}/{id}";

		Route _route;
		UrlPattern _url;

		[TestFixtureSetUp]
		public void Setup()
		{
			_url = new UrlPattern(Pattern);
			_route = _url.ToRouteDefinition();
		}

		[Test]
		public void Should_have_a_matching_pattern_string()
		{
			_route.Url.ShouldEqual(Pattern);
		}

		[Test]
		public void Should_have_three_parameters()
		{
			_route.Parameters.Count.ShouldEqual(3);
		}

		[Test]
		public void Should_have_the_right_three_parameters()
		{
			_route.Parameters.Has("controller").ShouldBeTrue();
			_route.Parameters.Has("action").ShouldBeTrue();
			_route.Parameters.Has("id").ShouldBeTrue();
		}
	}

	[TestFixture]
	public class Specifying_duplicate_parameter_names
	{
		[Test]
		public void Should_throw_an_exception()
		{
			const string pattern = "{controller}/{controller}";

			var url = new UrlPattern(pattern);

			Assert.Throws<RoutingException>(() => url.ToRouteDefinition());
		}
	}

	[TestFixture]
	public class Specifying_an_incomplete_parameter_name
	{
		[Test]
		public void Should_throw_an_exception()
		{
			const string pattern = "{controller}/{controller";

			var url = new UrlPattern(pattern);

			Assert.Throws<RoutingException>(() => url.ToRouteDefinition());
		}
	}

	[TestFixture]
	public class Specifying_a_separator_in_the_parameter_name
	{
		[Test]
		public void Should_throw_an_exception()
		{
			const string pattern = "{controller/controller}";

			var url = new UrlPattern(pattern);

			Assert.Throws<RoutingException>(() => url.ToRouteDefinition());
		}
	}
}