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
namespace Magnum.Specs.Reflection.SafePropertySpecs
{
	using Magnum.Reflection;
	using NUnit.Framework;


	[TestFixture]
	public class When_using_extension_methods_to_get_values
	{
		[Test]
		public void Should_return_default_value_for_null_object_on_value_type_references()
		{
			GetProperty<A, int> getValue = SafeProperty<A>.GetGetProperty(x => x.Id);

			const int expected = 0;

			A obj = null;

			int value = getValue.Get(obj);

			Assert.AreEqual(expected, value);
		}

		[Test]
		public void Should_return_the_nested_value()
		{
			GetProperty<A, string> getValue = SafeProperty<A>.GetGetProperty(x => x.TheB.Value);

			const string expected = "123";

			var obj = new A
				{
					TheB = new B
						{
							Value = expected
						}
				};

			string value = getValue.Get(obj);

			Assert.AreEqual(expected, value);
		}

		[Test]
		public void Should_return_the_value_for_value_types()
		{
			GetProperty<A, int> getValue = SafeProperty<A>.GetGetProperty(x => x.Id);

			const int expected = 123;

			var obj = new A
				{
					Id = expected
				};

			int value = getValue.Get(obj);

			Assert.AreEqual(expected, value);
		}

		[Test]
		public void Should_work_for_reference_types()
		{
			GetProperty<A, string> getValue = SafeProperty<A>.GetGetProperty(x => x.Value);

			const string expected = "123";

			var obj = new A
				{
					Value = expected
				};

			string value = getValue.Get(obj);

			Assert.AreEqual(expected, value);
		}


		class A
		{
			public string Value { get; set; }
			public int Id { get; set; }

			public B TheB { get; set; }
		}


		class B
		{
			public string Value { get; set; }
		}
	}
}