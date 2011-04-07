namespace Magnum.Specs.Algorithms
{
	using System.Collections.Generic;
	using System.Diagnostics;
	using Magnum.Algorithms;
	using TestFramework;


	[Scenario]
	public class Sorting_a_dependency_graph_using_tarjan
	{
		[Then]
		public void Should_return_the_proper_graph()
		{
			var node1 = new Node<int>(27);
			var node2 = new Node<int>(42);
			var node3 = new Node<int>(69);
			var node4 = new Node<int>(99);

			var adjacencyList = new AdjacencyList<int>();

			adjacencyList.AddEdge(node1, node2, 1);
			adjacencyList.AddEdge(node1, node3, 1);
			adjacencyList.AddEdge(node2, node3, 1);
			adjacencyList.AddEdge(node4, node1, 1);

			var tarjan = new Tarjan<int>();
			IList<IList<Node<int>>> result = tarjan.Run(node1, adjacencyList);

			foreach (var first in result)
			{
				foreach (var node in first)
				{
					Trace.WriteLine(node.Value);
				}

				Trace.WriteLine("done");
			}

			if (result.Count > 0)
				return;

			var sort = new TopologicalSort<int>(adjacencyList);

			var sorted = sort.Sort();

			Trace.WriteLine("Sorted");

			foreach (var node in sorted)
			{
				Trace.WriteLine(node.Value);
			}

		}
		
	}
}