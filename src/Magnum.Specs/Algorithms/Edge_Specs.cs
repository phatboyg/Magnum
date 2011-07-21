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
namespace Magnum.Specs.Algorithms
{
	using System.Collections.Generic;
	using Magnum.Algorithms.Implementations;
	using TestFramework;


	[Scenario]
	public class When_adding_two_matching_edges_to_a_hashset
	{
		HashSet<Edge<int, Node<int>>> _hashset;
		NodeList<int, Node<int>> _nodeList;

		[When]
		public void Adding_two_matching_edges_to_a_hashset()
		{
			_hashset = new HashSet<Edge<int, Node<int>>>();
			_nodeList = new NodeList<int, Node<int>>((index, value) => new Node<int>(index, value));

			Node<int> node1 = _nodeList[27];
			Node<int> node2 = _nodeList[42];

			var edge1 = new Edge<int, Node<int>>(node1, node2, 100);
			var edge2 = new Edge<int, Node<int>>(node1, node2, 100);

			_hashset.Add(edge1);
			_hashset.Add(edge2);
		}


		[Then]
		public void Should_only_have_one_edge_in_the_hashset()
		{
			_hashset.Count.ShouldEqual(1);
		}
	}
}