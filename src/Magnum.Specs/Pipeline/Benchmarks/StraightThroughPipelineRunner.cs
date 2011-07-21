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
namespace Magnum.Specs.Pipeline.Benchmarks
{
	using System.Threading;
	using Magnum.Pipeline;
	using Magnum.Pipeline.Segments;
	using Messages;


	public class StraightThroughPipelineRunner :
		PipelineRunner
	{
		readonly Pipe _input;
		readonly ClaimModified _message;
		int _count;

		public StraightThroughPipelineRunner()
		{
			Pipe consumer = PipeSegment.Consumer<ClaimModified>(m => Interlocked.Increment(ref _count));

			_input = PipeSegment.Input(consumer);

			_message = new ClaimModified();
		}

		public void Reset()
		{
			_count = 0;
		}

		public void SendMessage()
		{
			_input.Send(_message);
		}
	}
}