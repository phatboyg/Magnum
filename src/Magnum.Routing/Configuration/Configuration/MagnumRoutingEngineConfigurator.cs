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
namespace Magnum.Routing.Configuration
{
	using System;
	using System.Collections.Generic;
	using System.Linq.Expressions;
	using System.Reflection;
	using Builders;


	public class MagnumRoutingEngineConfigurator<TContext> :
		RoutingEngineConfigurator<TContext>
		where TContext : class
	{
		IList<RoutingEngineBuilderConfigurator<TContext>> _configurators;
		Expression<Func<TContext, Uri>> _getUri;

		public MagnumRoutingEngineConfigurator()
		{
			_configurators = new List<RoutingEngineBuilderConfigurator<TContext>>();
		}

		public void Uri(Expression<Func<TContext, Uri>> getUri)
		{
			_getUri = getUri;
		}

		public void AddConfigurator(RoutingEngineBuilderConfigurator<TContext> configurator)
		{
			_configurators.Add(configurator);
		}

		public void Validate()
		{
			ValidateGetUri();

			foreach (var configurator in _configurators)
				configurator.Validate();
		}

		public RoutingEngine<TContext> Create()
		{
			Validate();

			var builder = new MagnumRoutingEngineBuilder<TContext>(_getUri);

			foreach (var configurator in _configurators)
				configurator.Configure(builder);

			// apply configurators to the builder at this point

			return builder.Build();
		}

		void ValidateGetUri()
		{
			if (_getUri == null)
				throw new RoutingConfigurationException("The Uri access method was not specified");

			var body = _getUri.Body as MemberExpression;
			if (body == null)
				throw new RoutingConfigurationException("The Uri accessor must be a member expression: " + _getUri);

			if (body.Expression != _getUri.Parameters[0])
				throw new RoutingConfigurationException("The Uri access expression must target the lambda argument: " + _getUri);

			if (body.Member.MemberType != MemberTypes.Property && body.Member.MemberType != MemberTypes.Field)
				throw new RoutingConfigurationException("The Uri access expression must be a property or a field: " + _getUri);
		}
	}
}