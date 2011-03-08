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
	using Magnum.Reflection;


	public class FastPropertyObjectGetPropertyRunner :
		GetPropertyRunnerBase,
		GetPropertyRunner
	{
		readonly FastProperty<MyClass> _textProperty;
		readonly FastProperty<MyClass> _valueProperty;

		public FastPropertyObjectGetPropertyRunner()
		{
			_valueProperty = new FastProperty<MyClass>(x => x.Value);
			_textProperty = new FastProperty<MyClass>(x => x.Text);
		}


		public void GetValue(int index)
		{
			int value = (int)_valueProperty.Get(Objects[index%ObjectCount]);
		}

		public void GetText(int index)
		{
			string value = (string)_textProperty.Get(Objects[index%ObjectCount]);
		}
	}
}