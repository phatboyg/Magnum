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
	using Nodes;


	public class RoutingEngineRoutingRunner :
		RoutingRunner
	{
		MagnumRoutingEngine<Uri> _engine;
		EqualNode<Uri> _equal;
		long _id = 1;
		SegmentNode<Uri> _segment;
		SequentialNodeIdGenerator _idGenerator;

		public RoutingEngineRoutingRunner()
		{
			_idGenerator = new SequentialNodeIdGenerator();

			_engine = new MagnumRoutingEngine<Uri>(x => x);

			_segment = new SegmentNode<Uri>(1);
			_engine.Match<RootNode<Uri>>().Single().Add(_segment);

			_equal = new EqualNode<Uri>(() => _id++);
			_segment.Add(_equal);
		}

		public void AddRoutes(IEnumerable<string> paths)
		{
			foreach (string path in paths)
			{
				var route = new RouteImpl<Uri>();

				var joinNode = new JoinNode<Uri>(_id++, new ConstantNode<Uri>());
				joinNode.Add(route);

				var alpha = new AlphaNode<Uri>(_id++);
				alpha.Add(joinNode);

				_equal.Add(path.Substring(1), alpha);
			}
		}

		public void Route(Uri uri)
		{
			bool routed = false;
			_engine.Route(uri, x => { routed = true; });

			if (routed == false)
				throw new InvalidOperationException("Unrouted message");
		}
	}
}