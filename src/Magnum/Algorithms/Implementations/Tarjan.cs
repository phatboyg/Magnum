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
	using System;
	using System.Collections.Generic;


	public class Tarjan<T>
	{
		int _index;
		IList<IList<Node<T>>> _scc;
		Stack<Node<T>> _stack;

		public Tarjan()
		{
			_index = 0;
			_scc = new List<IList<Node<T>>>();
			_stack = new Stack<Node<T>>();
		}

		public IList<IList<Node<T>>> Run(T vx, AdjacencyList<T> list)
		{
			Node<T> v = list.GetNode(vx);

			return Run(v, list);
		}

		IList<IList<Node<T>>> Run(Node<T> v, AdjacencyList<T> list)
		{
			v.Index = _index;
			v.LowLink = _index;
			_index++;

			_stack.Push(v);

			foreach (Edge<T> edge in list[v.Value])
			{
				Node<T> n = edge.To;
				if (n.Index == -1)
				{
					Run(n, list);
					v.LowLink = Math.Min(v.LowLink, n.LowLink);
				}
				else if (_stack.Contains(n))
					v.LowLink = Math.Min(v.LowLink, n.Index);
			}

			if (v.LowLink == v.Index)
			{
				Node<T> n;
				IList<Node<T>> component = new List<Node<T>>();
				do
				{
					n = _stack.Pop();
					component.Add(n);
				}
				while (!v.Equals(n));

				if(component.Count != 1 || !v.Equals(component[0]))
					_scc.Add(component);
			}

			return _scc;
		}
	}
}