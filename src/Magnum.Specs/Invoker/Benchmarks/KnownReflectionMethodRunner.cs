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
	using System.Reflection;
	using Classes;


	public class KnownReflectionMethodRunner :
		MethodRunner
	{
		MethodInfo _method;
		ClassWithGenericMethods _target;

		public KnownReflectionMethodRunner()
		{
			_target = new ClassWithGenericMethods();

			_method = typeof(ClassWithGenericMethods)
				.GetMethod("TwoGenericArguments")
				.MakeGenericMethod(typeof(string), typeof(DateTime));
		}

		public void CallGenericMethod(int iterationCount)
		{
			object name = "Name";
			object when = DateTime.Now;

			var args = new[] {name, when};

			for (int i = 0; i < iterationCount; i++)
				_method.Invoke(_target, args);
		}
	}
}