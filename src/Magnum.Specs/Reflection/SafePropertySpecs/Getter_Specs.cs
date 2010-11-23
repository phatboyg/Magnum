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
	public class Getting_a_property_value
	{
		[Test]
		public void Should_retrieve_a_property_value_when_present()
		{
			GetProperty<A, string> getValue = SafeProperty<A>.GetGetProperty(x => x.TheB.TheC.Value);

			var subject = new A
				{
					TheB = new B
						{
							TheC = new C
								{
									Value = "123"
								}
						}
				};

			Assert.AreEqual("123", getValue.Get(subject));
		}

		[Test]
		public void Should_return_null_when_the_subject_is_null()
		{
			GetProperty<A, string> getValue = SafeProperty<A>.GetGetProperty(x => x.TheB.TheC.Value);

			A subject = null;

			Assert.AreEqual(null, getValue.Get(subject));
		}

		[Test]
		public void Should_return_null_when_the_subject_property_is_null()
		{
			GetProperty<A, string> getValue = SafeProperty<A>.GetGetProperty(x => x.TheB.TheC.Value);

			var subject = new A();

			Assert.AreEqual(null, getValue.Get(subject));
		}


		class A
		{
			public B TheB { get; set; }
		}


		class B
		{
			public C TheC { get; set; }
		}


		class C
		{
			public string Value { get; set; }
		}
	}
}