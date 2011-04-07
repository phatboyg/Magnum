namespace Magnum.Algorithms
{
	using System;
	using System.Collections.Generic;
	using System.Linq;


	public class TopologicalSort<T>
		where T : IComparable<T>
	{
		readonly AdjacencyList<T> _list;
		int _count;
		HashSet<Node<T>> _inbound;
		IDictionary<Node<T>, int> _result;
		

		public TopologicalSort(AdjacencyList<T> list)
		{
			_list = list;
			_inbound = new HashSet<Node<T>>();
			_result = new Dictionary<Node<T>, int>();
		}

		public IEnumerable<Node<T>> Sort()
		{
			foreach (var node in _list.GetSourceNodes())
			{
				if(!node.Visited)
					Sort(node);
			}

			return _result.OrderBy(x => x.Value).Select(x => x.Key);
		}

		void Sort(Node<T> node)
		{
			node.Visited = true;
			foreach (var edge in _list[node])
			{
				if(!edge.To.Visited)
					Sort(edge.To);
			}

			_result[node] = _count++;
		}
	}
}