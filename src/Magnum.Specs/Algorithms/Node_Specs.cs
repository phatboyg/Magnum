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
	using Magnum.Algorithms.Implementations;
	using TestFramework;


	[Scenario]
	public class When_comparing_nodes
	{
		[Then]
		public void Two_identical_references_should_match()
		{
			var node1 = new DependencyGraphNode<string>(1, "A");
			DependencyGraphNode<string> node2 = node1;

			node2.ShouldEqual(node1);
		}

		[Then]
		public void Two_different_references_should_not_be_equal()
		{
			var node1 = new DependencyGraphNode<string>(1, "A");
			var node2 = new DependencyGraphNode<string>(1, "A");

			node2.ShouldNotEqual(node1);
		}
	}
}