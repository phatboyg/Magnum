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

	public interface IConsumerConfigurator
	{
		void Bind(ISubscriptionScope scope);
		void Validate();
	}

	public interface IConsumerConfigurator<T>
		where T : class
	{
		/// <summary>
		/// Specifies a delegate that will consume the message when available
		/// </summary>
		/// <param name="consumerAction"></param>
		void Using(MessageConsumer<T> consumerAction);

		/// <summary>
		/// Specifies a handler that will review the message and return the appropriate action to consume the message
		/// </summary>
		/// <typeparam name="TConsumer"></typeparam>
		/// <param name="getConsumer"></param>
		void Using<TConsumer>(Func<T, Action<T>> getConsumer);
	}
}