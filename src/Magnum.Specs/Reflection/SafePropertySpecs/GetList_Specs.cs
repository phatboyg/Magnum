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
	using System.Collections.Generic;
	using Magnum.Reflection;
	using NUnit.Framework;


	[TestFixture]
	public class When_accessing_a_property_in_a_list
	{
		[Test]
		public void Should_return_nothing_if_list_entry_is_null()
		{
			GetProperty<A, int> getter = SafeProperty<A>.GetGetProperty(x => x.TheBs[0].Value);

			var obj = new A
				{
					TheBs = new List<B>
						{
							null
						}
				};
			Assert.AreEqual(1, obj.TheBs.Count);

			int value = getter.Get(obj);

			Assert.AreEqual(0, value);
		}

		[Test]
		public void Should_return_nothing_if_list_is_empty()
		{
			GetProperty<A, int> getter = SafeProperty<A>.GetGetProperty(x => x.TheBs[0].Value);

			var obj = new A
				{
					TheBs = new List<B>()
				};

			int value = getter.Get(obj);

			Assert.AreEqual(0, value);
		}

		[Test]
		public void Should_return_nothing_if_list_is_null()
		{
			GetProperty<A, int> getter = SafeProperty<A>.GetGetProperty(x => x.TheBs[0].Value);

			var obj = new A();

			int value = getter.Get(obj);

			Assert.AreEqual(0, value);
		}

		[Test]
		public void Should_return_nothing_if_object_is_null()
		{
			GetProperty<A, int> getter = SafeProperty<A>.GetGetProperty(x => x.TheBs[0].Value);

			A obj = null;

			int value = getter.Get(obj);

			Assert.AreEqual(0, value);
		}

		[Test]
		public void Should_return_the_default_if_array_value_missing()
		{
			GetProperty<A, int> getter = SafeProperty<A>.GetGetProperty(x => x.Values[0]);

			var obj = new A();

			int value = getter.Get(obj);

			Assert.AreEqual(0, value);
		}

		[Test]
		public void Should_return_the_value_if_it_exists()
		{
			GetProperty<A, int> getter = SafeProperty<A>.GetGetProperty(x => x.TheBs[0].Value);

			var obj = new A
				{
					TheBs = new List<B>
						{
							new B
								{
									Value = 27
								}
						}
				};

			int value = getter.Get(obj);

			Assert.AreEqual(27, value);
		}


		class A
		{
			public IList<B> TheBs { get; set; }
			public IList<int> Values { get; set; }
		}


		class B
		{
			public int Value { get; set; }
		}
	}
}