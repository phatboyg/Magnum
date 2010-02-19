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
	using System;

	public class ConsumerConfigurator<T> :
		IConsumerConfigurator,
		IConsumerConfigurator<T>
		where T : class
	{
		private Action<ISubscriptionScope> _binder;
		private MessageConsumer<T> _messageConsumer;

		public void Bind(ISubscriptionScope scope)
		{
			_binder(scope);
		}

		public void Validate()
		{
			if (_binder == null)
			{
				throw new PipelineConfigurationException("The consumer for " + typeof (T).FullName + " was not properly configured.");
			}
		}

		public void Using(MessageConsumer<T> messageConsumer)
		{
			_messageConsumer = messageConsumer;
			_binder = x => x.Subscribe(_messageConsumer);
		}

		public void Using<TConsumer>(Func<T, Action<T>> getConsumer)
		{
			_messageConsumer = x =>
				{
					Action<T> consumer = getConsumer(x);
					if (consumer != null)
						consumer(x);
				};

			_binder = x => x.Subscribe(_messageConsumer);
		}
	}
}