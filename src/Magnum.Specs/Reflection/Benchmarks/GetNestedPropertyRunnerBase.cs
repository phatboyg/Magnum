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
namespace Magnum.Specs.Reflection.Benchmarks
{
	public abstract class GetNestedPropertyRunnerBase
	{
		protected A Subject;

		protected GetNestedPropertyRunnerBase()
		{
			Subject = new A
				{
					TheB = new B
						{
							TheC = new C
								{
									Value = 47
								}
						}
				};
		}


		protected class A
		{
			public B TheB { get; set; }
		}


		protected class B
		{
			public C TheC { get; set; }
		}


		protected class C
		{
			public int Value { get; set; }
		}
	}
}