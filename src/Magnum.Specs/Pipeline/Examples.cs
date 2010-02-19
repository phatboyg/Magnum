// Copyright 2007-2008 The Apache Software Foundation.
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
namespace Magnum.Specs.Pipeline
{
	using Consumers;
	using Magnum.Actors;
	using Magnum.Pipeline;
	using Messages;
	using NUnit.Framework;
	using TestFramework;

	[TestFixture]
	public class Examples
	{
		[TearDown]
		public void Teardown()
		{
			if (_scope != null)
			{
				_scope.Dispose();
				_scope = null;
			}

			_pipe = null;
		}

		private Pipe _pipe;
		private ISubscriptionScope _scope;
		private Future<ClaimModified> _delegateConsumer;
		private SingleMessageConsumer _singleConsumer;
		private MultipleMessageConsumer _multiConsumer;

		[TestFixtureSetUp]
		public void Setup()
		{
			_pipe = _pipe.New();

			_delegateConsumer = new Future<ClaimModified>();

			_singleConsumer = new SingleMessageConsumer();

			_multiConsumer = new MultipleMessageConsumer();

			_scope = _pipe.Subscribe(x =>
				{
					x.Consume<ClaimModified>()
						.Using(_delegateConsumer.Complete);

					x.Consume<ClaimModified>()
						.Using<SingleMessageConsumer>(p => _singleConsumer.Consume);

					x.ConsumeAll(_multiConsumer);
				});

			_pipe.Send(new ClaimModified());
		}

		[Test]
		public void Should_allow_the_delegate_syntax()
		{
			_delegateConsumer.IsAvailable().ShouldBeTrue();
		}

		[Test]
		public void Should_allow_the_explicit_message_type_syntax()
		{
			_singleConsumer.ClaimModifiedCalled.IsAvailable().ShouldBeTrue();
		}

		[Test]
		public void Should_allow_the_implicit_interface_discovery_syntax()
		{
			_multiConsumer.ClaimModifiedCalled.IsAvailable().ShouldBeTrue();
		}
	}
}