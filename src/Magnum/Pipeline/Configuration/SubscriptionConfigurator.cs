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
namespace Magnum.Pipeline.Configuration
{
	using System.Collections.Generic;

	public class SubscriptionConfigurator :
		ISubscriptionConfigurator
	{
		private readonly IList<IConsumerConfigurator> _consumers = new List<IConsumerConfigurator>();
		private readonly Pipe _pipe;

		public SubscriptionConfigurator(Pipe pipe)
		{
			_pipe = pipe;
		}

		public IConsumerConfigurator<T> Consume<T>()
			where T : class
		{
			var configurator = new ConsumerConfigurator<T>();

			_consumers.Add(configurator);
			return configurator;
		}

		public void Validate()
		{
			_consumers.Each(consumer => consumer.Validate());
		}

		public void ConsumeAll<TConsumer>(TConsumer consumer)
			where TConsumer : class
		{
			var configurator = new ConsumeAllConfigurator<TConsumer>(consumer);

			_consumers.Add(configurator);
		}

		public ISubscriptionScope Bind()
		{
			var scope = new SubscriptionScope(_pipe);

			_consumers.Each(consumer => { consumer.Bind(scope); });
			return scope;
		}
	}
}