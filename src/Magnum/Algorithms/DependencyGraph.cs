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
namespace Magnum.Algorithms
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Implementations;


	public class DependencyGraph<T>
	{
		AdjacencyList<T> _adjacencyList;

		public DependencyGraph()
		{
			_adjacencyList = new AdjacencyList<T>();
		}

		public void Add(T source, T target)
		{
			_adjacencyList.AddEdge(source, target, 0);
		}

		public IEnumerable<T> GetItemsInDependencyOrder()
		{
			EnsureGraphIsAcyclic();

			var sort = new TopologicalSort<T>(_adjacencyList);
			return sort.Sort();
		}

		void EnsureGraphIsAcyclic()
		{
			var tarjan = new Tarjan<T>();
			IList<IList<Node<T>>> cycles = tarjan.Run(_adjacencyList.GetSourceNodes().First().Value, _adjacencyList);

			if (cycles.Count == 0)
				return;

			throw new InvalidOperationException("The dependency graph cannot have any cycles");
		}
	}
}