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
namespace Magnum.Specs.Workflow
{
	using System;
	using Magnum.Workflow;
	using Magnum.Workflow.Configuration;
	using NUnit.Framework;
	using Pipeline.Messages;

	[TestFixture]
	public class An_activity_that_creates_a_new_instance
	{
		[TestFixtureSetUp]
		public void Setup()
		{
			// TODO
			// workflowEngine.SetActivityFactory(() => getinstance(.....<T>))


			Workflow<ClaimWasModifiedWorkflow> workflow = WorkflowConfigurator.Define<ClaimWasModifiedWorkflow>(x =>
				{
					x.Receive<ClaimModified>(r =>
						{
							r.CanCreateInstance();

							r.Set(i => i.ClaimId, m => m.ClaimId);
							r.Set(i => i.Text, m => m.Text);

							// TODO message creation needs to track setters so that a trace can be generated
							// use of expression alone may provide enough information to track sources from instance
							r.Send(instance => new ClaimChangeAudit
								{
									OriginalClaimId = instance.ClaimId,
									User = instance.Text,
								});

							r.ForEach<int>(en =>
								{
									en.Source(v => new[] {1, 3, 5, 7});

									en.Send<ClaimChangeAudit>(bb => { });
								});
						});
				});
		}
	}

	public class ClaimWasModifiedWorkflow :
		Workflow<ClaimWasModifiedWorkflow>
	{
		public string Text { get; set; }

		public Guid ClaimId { get; set; }
	}
}