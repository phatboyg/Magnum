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
namespace Magnum.Routing.Conditions
{
	using System.Collections.Generic;
	using System.Linq;


	public abstract class Condition
	{
		readonly IList<Activation> _activations;

		protected Condition()
		{
			_activations = new List<Activation>();
		}

		protected void Next(RouteContext context, string value)
		{
			foreach (Activation activation in _activations)
				activation.Activate(context, value);
		}

		public void AddActivation(Activation activation)
		{
			_activations.Add(activation);
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
			return _activations.SelectMany(activation => activation.Match<T>());
		}
	}
}