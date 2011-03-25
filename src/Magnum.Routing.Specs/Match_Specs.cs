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
	using System.Linq;
	using Conditions;
	using NUnit.Framework;
	using TestFramework;


	[TestFixture]
	public class Matching_a_segment_condition
	{
		MagnumRoutingEngine _engine;

		[TestFixtureSetUp]
		public void Given_an_existing_segment_condition()
		{
			_engine = new MagnumRoutingEngine();

			var segmentRouteCondition = new SegmentRouteCondition(0);
			segmentRouteCondition.AddActivation(new EqualRouteCondition("version"));

			_engine.AddActivation(segmentRouteCondition);
		}

		[Test]
		public void Should_find_the_existing_segment_condition()
		{
			_engine.Match<SegmentRouteCondition>()
				.Where(x => x.Position == 0)
				.Any()
				.ShouldBeTrue();
		}

		[Test]
		public void Should_find_the_existing_equal_condition()
		{
			_engine.Match<SegmentRouteCondition>()
				.Where(x => x.Position == 0)
				.Match<EqualRouteCondition>()
				.Where(x => x.Value == "version")
				.Any()
				.ShouldBeTrue();
		}
	}
}