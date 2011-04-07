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


	/// <summary>
	/// Maintains a list of nodes for a given set of instances of T
	/// </summary>
	/// <typeparam name="T">The type encapsulated in the node</typeparam>
	public class NodeList<T>
	{
		readonly IList<Node<T>> _nodes;
		readonly NodeTable<T> _nodeTable;

		public NodeList()
		{
			_nodes = new List<Node<T>>();
			_nodeTable = new NodeTable<T>();
		}

		public NodeList(int capacity)
		{
			_nodes = new List<Node<T>>(capacity);
			_nodeTable = new NodeTable<T>(capacity);
		}

		/// <summary>
		/// Retrieve the index for a given key
		/// </summary>
		/// <param name="key">The key</param>
		/// <returns>The index</returns>
		public int Index(T key)
		{
			int index = _nodeTable[key];

			if (index <= _nodes.Count)
				return index;

			var node = new Node<T>(index, key);
			_nodes.Add(node);

			return index;
		}

		/// <summary>
		/// Retrieves the node for the given key
		/// </summary>
		/// <param name="key">The key</param>
		/// <returns>The unique node that relates to the specified key</returns>
		public Node<T> this[T key]
		{
			get
			{
				return _nodes[Index(key) - 1];
			}
		}
	}
}