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
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Magnum.Algorithms;
	using NUnit.Framework;
	using TestFramework;


	[Scenario]
	public class When_items_are_added_to_a_dependency_graph
	{
		IEnumerable<string> _dependencyOrder;

		[When]
		public void Items_are_added_to_a_dependency_graph()
		{
			var dependencyGraph = new DependencyGraph<string>();

			dependencyGraph.Add("A", "B");
			dependencyGraph.Add("B", "C");
			dependencyGraph.Add("C", "D");

			_dependencyOrder = dependencyGraph.GetItemsInDependencyOrder();
		}

		[Then]
		public void Items_should_be_in_dependent_order()
		{
			_dependencyOrder.ShouldEqual(new[] { "D", "C", "B", "A" });
		}
	}

	[Scenario]
	public class Adding_unrelated_items_to_a_dependency_graph
	{
		DependencyGraph<string> _dependencyGraph;

		[When]
		public void Items_are_added_to_a_dependency_graph()
		{
			_dependencyGraph = new DependencyGraph<string>();

			_dependencyGraph.Add("A", "B");
			_dependencyGraph.Add("B", "C");
			_dependencyGraph.Add("D", "E");
		}

		[Then]
		public void Items_should_be_in_dependent_order()
		{
			_dependencyGraph.GetItemsInDependencyOrder("A")
				.ShouldEqual(new[] { "C", "B", "A" });
		}

		[Then]
		public void Other_items_should_also_be_in_order()
		{
			_dependencyGraph.GetItemsInDependencyOrder("D")
				.ShouldEqual(new[] { "E", "D" });
		}

		[Then]
		public void Items_without_dependencies_should_be_present()
		{
			_dependencyGraph.GetItemsInDependencyOrder("E")
				.ShouldEqual(new[] { "E" });
		}
	}

	[Scenario]
	public class When_adding_a_disconnected_loop_to_the_graph
	{
		DependencyGraph<string> _dependencyGraph;

		[When]
		public void Adding_a_disconnected_loop_to_the_graph()
		{
			_dependencyGraph = new DependencyGraph<string>();

			_dependencyGraph.Add("A", "B");
			_dependencyGraph.Add("B", "C");
			_dependencyGraph.Add("D", "E");
			_dependencyGraph.Add("E", "F");
			_dependencyGraph.Add("F", "D");
		}

		[Then]
		public void An_exception_should_be_thrown()
		{
			Assert.Throws<InvalidOperationException>(() => _dependencyGraph.GetItemsInDependencyOrder());
		}
	}

	[Scenario]
	public class When_complex_items_are_added_to_a_dependency_graph
	{
		IEnumerable<string> _dependencyOrder;

		[When]
		public void Items_are_added_to_a_dependency_graph()
		{
			var dependencyGraph = new DependencyGraph<string>();

			dependencyGraph.Add("A", "B");
			dependencyGraph.Add("B", "C");
			dependencyGraph.Add("B", "D");
			dependencyGraph.Add("C", "E");
			dependencyGraph.Add("C", "F");
			dependencyGraph.Add("C", "G");
			dependencyGraph.Add("D", "H");
			dependencyGraph.Add("D", "I");
			dependencyGraph.Add("D", "J");
			dependencyGraph.Add("D", "K");
			dependencyGraph.Add("H", "C");

			_dependencyOrder = dependencyGraph.GetItemsInDependencyOrder();
		}

		[Then]
		public void Items_should_be_in_dependent_order()
		{
			_dependencyOrder.ShouldEqual(new[] { "E", "F", "G", "C", "H", "I", "J", "K", "D", "B", "A" });
		}
	}

	[Scenario]
	public class When_a_dependency_generates_a_loop
	{
		DependencyGraph<string> _dependencyGraph;

		[When]
		public void Items_are_added_to_a_dependency_graph()
		{
			_dependencyGraph = new DependencyGraph<string>();

			_dependencyGraph.Add("A", "B");
			_dependencyGraph.Add("B", "C");
			_dependencyGraph.Add("C", "D");
			_dependencyGraph.Add("D", "B");
		}

		[Then]
		public void An_exception_should_be_thrown()
		{
			Assert.Throws<InvalidOperationException>(() => _dependencyGraph.GetItemsInDependencyOrder());
		}
	}

	[Scenario]
	public class When_there_is_a_huge_dependency_chain
	{
		DependencyGraph<int> _dependencyGraph;

		[When]
		public void There_is_a_huge_dependency_chain()
		{
			_dependencyGraph = new DependencyGraph<int>();

			for (int i = 0; i < 1000; i++)
			{
				_dependencyGraph.Add(i, i + 1);
			}
		}

		[Then]
		public void Items_should_be_in_dependent_order()
		{
			_dependencyGraph.GetItemsInDependencyOrder().ShouldEqual(Enumerable.Range(0,1001).Reverse());
		}
	}
}