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
	public class Accessing_an_array
	{
		[Test]
		public void Should_return_default_if_item_missing()
		{
			GetProperty<A, int> getter = SafeProperty<A>.GetGetProperty(x => x.TheBs[0].Value);

			A obj = null;

			int value = getter.Get(obj);

			Assert.AreEqual(0, value);
		}

		[Test]
		public void Should_return_default_if_the_array_is_empty()
		{
			GetProperty<A, int> getter = SafeProperty<A>.GetGetProperty(x => x.TheBs[0].Value);

			var obj = new A
				{
					TheBs = new B[] {}
				};

			int value = getter.Get(obj);

			Assert.AreEqual(0, value);
		}

		[Test]
		public void Should_return_default_if_the_array_is_null()
		{
			GetProperty<A, int> getter = SafeProperty<A>.GetGetProperty(x => x.TheBs[0].Value);

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
					TheBs = new[]
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


		[Test]
		public void Should_return_value()
		{
			GetProperty<A, int> getter = SafeProperty<A>.GetGetProperty(x => x.TheBs[0].Value);

			var obj = new A
				{
					TheBs = new[]
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
			public B[] TheBs { get; set; }
			public int[] Values { get; set; }
		}


		class B
		{
			public int Value { get; set; }
		}
	}
}