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

	public static class WorkflowConfigurator
	{
		public static Workflow<TWorkflow> Define<TWorkflow>(Action<IWorkflowConfigurator<TWorkflow>> configuratorAction)
			where TWorkflow : class
		{
			var configurator = new WorkflowConfigurator<TWorkflow>();

			configuratorAction(configurator);

			return configurator.CreateWorkflow();
		}
	}

	public class WorkflowConfigurator<TWorkflow> :
		IWorkflowConfigurator<TWorkflow>
		where TWorkflow : class
	{
		public Workflow<TWorkflow> CreateWorkflow()
		{
			throw new NotImplementedException();
		}

		public ReceiveActivity<TWorkflow, TMessage> Receive<TMessage>(Action<TWorkflow, TMessage> receiveAction) where TMessage : class
		{
			throw new NotImplementedException();
		}

		public ReceiveActivity<TWorkflow, TMessage> Receive<TMessage>(Action<ReceiveActivityConfigurator<TWorkflow, TMessage>> configurationAction) where TMessage : class
		{
			throw new NotImplementedException();
		}

		public SendActivity<TWorkflow, TMessage> Send<TMessage>(Func<TWorkflow, TMessage> sendAction) where TMessage : class
		{
			throw new NotImplementedException();
		}

		public SendActivity<TWorkflow, TMessage> Send<TMessage>(Action<TWorkflow, TMessage> sendAction) where TMessage : class, new()
		{
			throw new NotImplementedException();
		}
	}
}