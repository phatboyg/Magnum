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
namespace Magnum.Specs.Activator.Benchmarks
{
	using System;
	using Classes;


	public class NewActivatorRunner :
		ActivatorRunner
	{
		public void DefaultConstructor(int iterations)
		{
			for (int i = 0; i < iterations; i++)
			{
				var item = new ClassWithDefaultConstructor();
			}
		}

		public void SingleArgumentConstructor(int iterations)
		{
			for (int i = 0; i < iterations; i++)
			{
				var item = new ClassWithOneConstructorArg(47);
			}
		}

		public void TwoArgumentConstructor(int iterations)
		{
			for (int i = 0; i < iterations; i++)
			{
				var item = new ClassWithTwoConstructorArgs(47, "The Name");
			}
		}

		public void MultipleArgumentConstructor(int iterations)
		{
			for (int i = 0; i < iterations; i++)
			{
				var item = new ClassWithMultipleConstructorArgs(47, "The Name", 123, "Description");
			}
		}

		public void GenericArgumentConstructor(int iterations)
		{
			var argument = new SuperConstrainedClass(CombGuid.Generate());

			for (int i = 0; i < iterations; i++)
			{
				var obj = new ClassWithAConstrainedGenericArgument<SuperConstrainedClass,Guid>(argument);
			}
		}
	}
}