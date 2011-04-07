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
	using Magnum.Algorithms;
	using NUnit.Framework;
	using TestFramework;


	[Scenario]
	public class When_adding_two_matching_edges_to_a_hashset
	{
		HashSet<Edge<int>> _hashset;

		[When]
		public void Adding_two_matching_edges_to_a_hashset()
		{
			_hashset = new HashSet<Edge<int>>();

			var node1 = new Node<int>(27);
			var node2 = new Node<int>(42);

			var edge1 = new Edge<int>(node1, node2, 100);
			var edge2 = new Edge<int>(node1, node2, 100);

			_hashset.Add(edge1);
			_hashset.Add(edge2);
		}


		[Then]
		public void Should_only_have_one_edge_in_the_hashset()
		{
			_hashset.Count.ShouldEqual(1);
		}

		[Test]
		public void Nodes_equality()
		{
			var node1 = new Node<int>(27);

			node1.Equals(node1).ShouldBeTrue();

			var node2 = new Node<int>(27);

			node2.Equals(node1).ShouldBeTrue();

			var node3 = new Node<int>(42);

			node3.Equals(node2).ShouldBeFalse();
		}
	}
}