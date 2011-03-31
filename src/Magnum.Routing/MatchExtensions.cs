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
namespace Magnum.Routing
{
	using System.Collections.Generic;
	using System.Linq;
	using Engine;
	using Engine.Nodes;


	public static class MatchExtensions
	{
		public static IEnumerable<T> Match<T>(this IEnumerable<Activation> activations)
			where T : class
		{
			return activations.SelectMany(activation => activation.Match<T>());
		}

		public static void Add<T>(this RoutingEngine<T> engine, Activation<T> activation)
			where T : class
		{
			RootNode<T> match = engine.Match<RootNode<T>>().FirstOrDefault();
			if (match == null)
				return;

			match.Add(activation);
		}
	}
}