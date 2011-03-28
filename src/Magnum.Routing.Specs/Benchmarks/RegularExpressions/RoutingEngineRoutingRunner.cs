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
	using Conditions;
	using Extensions;


	public class RoutingEngineRoutingRunner :
		RoutingRunner
	{
		MagnumRoutingEngine<Uri> _engine;
		EqualRouteCondition _equal;
		SegmentRouteCondition _segment;

		public RoutingEngineRoutingRunner()
		{
			_engine = new MagnumRoutingEngine<Uri>();

			_segment = new SegmentRouteCondition(1);
			_engine.AddActivation(_segment);

			_equal = new EqualRouteCondition();
			_segment.AddActivation(_equal);
		}

		public void AddRoutes(IEnumerable<string> paths)
		{
			paths.Select(x => x.Substring(1)).Each(x => { _equal.Add(x); });
		}

		public void Route(Uri uri)
		{
			_engine.Route(uri, uri, x => { });
		}
	}
}