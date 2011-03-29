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
namespace Magnum.Routing.Nodes
{
	using System;
	using System.Collections.Generic;
	using System.Linq;


	public abstract class DictionaryNode<TContext> :
		Node<TContext>
	{
		readonly IDictionary<string, Activation<TContext>> _values;

		protected DictionaryNode()
		{
			_values = new Dictionary<string, Activation<TContext>>();
		}

		protected void Add(string value, Activation<TContext> activation, Func<long> generateId)
		{
			Activation<TContext> existing;
			if (_values.TryGetValue(value, out existing))
			{
				var alphaNode = existing as AlphaNode<TContext>;
				if (alphaNode == null)
				{
					alphaNode = new AlphaNode<TContext>(generateId());
					alphaNode.Add(existing);
					_values[value] = alphaNode;
				}

				alphaNode.Add(activation);
				return;
			}

			_values.Add(value, activation);
		}

		protected void Next(string key, RouteContext<TContext> context, string value)
		{
			Activation<TContext> match;
			if (!_values.TryGetValue(key, out match))
				return;

			match.Activate(context, value);
		}

		public IEnumerable<T> Match<T>()
			where T : class
		{
			if (typeof(T) == GetType())
				return ThisAsEnumerable<T>();

			return NextAsEnumerable<T>();
		}

		IEnumerable<T> ThisAsEnumerable<T>()
			where T : class
		{
			yield return this as T;
		}

		IEnumerable<T> NextAsEnumerable<T>()
			where T : class
		{
			return _values.Values.SelectMany(alphaNode => alphaNode.Match<T>());
		}
	}
}