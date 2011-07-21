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
namespace Magnum.Routing.Engine.Nodes
{
	/// <summary>
	/// An alpha node marks the end of a branch in the left side discrimination network
	/// and starts the journey into the right side join network
	/// </summary>
	public class AlphaNode<TContext> :
		ActivationNode<TContext>,
		Activation<TContext>
	{
		readonly long _id;

		public AlphaNode(long id)
		{
			_id = id;
		}

		public void Activate(RouteContext<TContext> context, string value)
		{
			context.AddRightActivation(_id);
			context.AddAction(() => Next(context, value));
		}
	}
}