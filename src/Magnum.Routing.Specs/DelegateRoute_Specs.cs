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
namespace Magnum.Routing.Specs
{
	using NUnit.Framework;
	using TestFramework;


	[Scenario]
	[Explicit("Not yet implemented")]
	public class When_a_delegate_route_is_bound
	{
		bool _called;
		//RoutingEngine<int> _router;

		[Given]
		public void A_delegate_route()
		{
			_called = false;

			//_router.Bind("/version", x => _called = true);
		}

		[Then]
		public void The_delegate_should_be_invoked()
		{
			_called.ShouldBeTrue();
		}
	}
}