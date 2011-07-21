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
namespace Magnum.Specs.Invoker.Benchmarks
{
	using System;
	using Classes;
	using Magnum.Reflection;


	public class FastInvokerMethodRunner :
		MethodRunner
	{
		ClassWithGenericMethods _target;

		public FastInvokerMethodRunner()
		{
			_target = new ClassWithGenericMethods();
		}

		public void CallGenericMethod(int iterationCount)
		{
			DateTime now = DateTime.Now;

			for (int i = 0; i < iterationCount; i++)
				_target.FastInvoke("TwoGenericArguments", "Name", now);
		}
	}
}