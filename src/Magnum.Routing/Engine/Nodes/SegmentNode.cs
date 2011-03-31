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
	/// Matches a positional segment in the URI if it exists, and passes to the next condition
	/// </summary>
	public class SegmentNode<TContext> :
		ActivationNode<TContext>,
		Activation<TContext>
	{
		readonly int _position;

		public SegmentNode(int position)
		{
			_position = position;
		}

		public int Position
		{
			get { return _position; }
		}

		public void Activate(RouteContext<TContext> context, string value)
		{
			string segmentValue = context.Segment(_position);
			if (segmentValue == null)
				return;

			Next(context, segmentValue);
		}
	}
}