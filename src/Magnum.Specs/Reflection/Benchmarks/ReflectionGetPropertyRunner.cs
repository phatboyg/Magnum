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
	using System.Reflection;
	using Magnum.Extensions;


	public class ReflectionGetPropertyRunner :
		GetPropertyRunnerBase,
		GetPropertyRunner
	{
		readonly PropertyInfo _textProperty;
		readonly PropertyInfo _valueProperty;

		public ReflectionGetPropertyRunner()
		{
			_valueProperty = ExtensionsToExpression.GetMemberPropertyInfo<MyClass, int>(x => x.Value);
			_textProperty = ExtensionsToExpression.GetMemberPropertyInfo<MyClass, string>(x => x.Text);
		}


		public void GetValue(int index)
		{
			object value = _valueProperty.GetValue(Objects[index%ObjectCount], null);
		}

		public void GetText(int index)
		{
			object value = _textProperty.GetValue(Objects[index%ObjectCount], null);
		}
	}
}