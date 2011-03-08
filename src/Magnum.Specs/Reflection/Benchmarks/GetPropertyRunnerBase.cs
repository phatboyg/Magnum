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
	public abstract class GetPropertyRunnerBase
	{
		protected const int ObjectCount = 100;
		protected MyClass[] Objects;

		protected GetPropertyRunnerBase()
		{
			Objects = new MyClass[ObjectCount];
			for (int i = 0; i < ObjectCount; i++)
			{
				Objects[i] = new MyClass
					{
						Value = i,
						Text = i.ToString()
					};
			}
		}


		protected class MyClass
		{
			public int Value { get; set; }
			public string Text { get; set; }
		}
	}
}