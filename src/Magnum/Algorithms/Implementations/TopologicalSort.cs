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


	public class TopologicalSort<T>
	{
		readonly AdjacencyList<T> _list;
		readonly IList<T> _results;

		public TopologicalSort(AdjacencyList<T> list)
		{
			_list = list;
			_results = new List<T>();
		}

		public IEnumerable<T> Sort()
		{
			foreach (var node in _list.GetSourceNodes())
			{
				if (!node.Visited)
					Sort(node.Value);
			}

			return _results;
		}

		void Sort(T node)
		{
			_list.GetNode(node).Visited = true;
			foreach (var edge in _list[node])
			{
				if (!edge.To.Visited)
					Sort(edge.To.Value);
			}

			_results.Add(node);
		}
	}
}