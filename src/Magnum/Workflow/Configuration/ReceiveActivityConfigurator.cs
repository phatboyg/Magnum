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
namespace Magnum.Workflow.Configuration
{
	using System;
	using System.Collections.Generic;
	using System.Linq.Expressions;

	public interface ReceiveActivityConfigurator<TWorkflow, TInput>
		where TWorkflow : class
	{
		void CanCreateInstance();

		PropertyMapper<TInput, TWorkflow> Map();

		SendActivity<TWorkflow, TOutput> Send<TOutput>(Expression<Func<TWorkflow, TOutput>> factory);

		LoopActivity<TWorkflow, TElement> ForEach<TElement>(Action<LoopActivityConfigurator<TWorkflow, TElement>> loopAction);
		
		void Set(Expression<Func<TWorkflow, object>> fromAction, Expression<Func<TInput, object>> toAction);
	}

	public interface LoopActivityConfigurator<TWorkflow, TElement>
		where TWorkflow : class
	{
		void Source(Func<TWorkflow, IEnumerable<TElement>> sourceExpression);

		SendActivity<TWorkflow, TOutput> Send<TOutput>(Action<SendActivityConfigurator<TWorkflow, TOutput>> configurationAction);
	}

	public interface LoopActivity<TWorkflow, TElement>
		where TWorkflow : class
	{
	}
}