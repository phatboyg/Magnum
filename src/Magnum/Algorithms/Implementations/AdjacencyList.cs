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
namespace Magnum.Algorithms.Implementations
{
	using System.Collections.Generic;


	public class AdjacencyList<T>
	{
		IDictionary<Node<T>, HashSet<Edge<T>>> _adjacencies;
		NodeList<T> _nodeList;
		

		public AdjacencyList()
		{
			_nodeList = new NodeList<T>();
			_adjacencies = new Dictionary<Node<T>, HashSet<Edge<T>>>();
		}


		public void AddEdge(T source, T target, int weight)
		{
			Node<T> sourceNode = _nodeList[source];
			Node<T> targetNode = _nodeList[target];

			AddEdge(sourceNode, targetNode, weight);
		}

		void AddEdge(Node<T> source, Node<T> target, int weight)
		{
			HashSet<Edge<T>> edges;
			if (!_adjacencies.TryGetValue(source, out edges))
			{
				edges = new HashSet<Edge<T>>();
				_adjacencies.Add(source, edges);
			}

			edges.Add(new Edge<T>(source, target, weight));
		}

		public HashSet<Edge<T>> this[T index]
		{
			get
			{
				var indexNode = _nodeList[index];

				HashSet<Edge<T>> edges;
				if (_adjacencies.TryGetValue(indexNode, out edges))
					return edges;

				return new HashSet<Edge<T>>();
			}
		}

		public void ReverseEdge(Edge<T> edge)
		{
			HashSet<Edge<T>> edges;
			if (_adjacencies.TryGetValue(edge.From, out edges))
				edges.Remove(edge);

			AddEdge(edge.To, edge.From, edge.Weight);
		}

		public void ReverseList()
		{
			_adjacencies = Reverse()._adjacencies;
		}

		public AdjacencyList<T> Reverse()
		{
			AdjacencyList<T> result = new AdjacencyList<T>();
			foreach (var adjacency in _adjacencies.Values)
			{
				foreach (var edge in adjacency)
				{
					result.AddEdge(edge.To, edge.From, edge.Weight);
				}
			}

			return result;
		}

		public ICollection<Node<T>> GetSourceNodes()
		{
			return _adjacencies.Keys; 
		}

		public Node<T> GetNode(T key)
		{
			return _nodeList[key];
		}
	}
}