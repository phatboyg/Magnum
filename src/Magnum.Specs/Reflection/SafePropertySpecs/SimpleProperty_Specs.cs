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
	using System;
	using Magnum.Reflection;
	using NUnit.Framework;


	[TestFixture]
	public class When_accessing_a_simple_property
	{
		[Test]
		public void Should_get_the_value_if_specified()
		{
			const string expected = "123";

			var a = new A
				{
					Id = expected
				};

			string id = null;
			_getter(a, value => { id = value; });

			Assert.AreEqual(expected, id);
		}

		[Test]
		public void Should_handle_bad_expressions()
		{
			Assert.Throws<ArgumentException>(() => SafeProperty<A>.GetGetProperty(x => true));
		}

		[Test]
		public void Should_handle_method_expressions_with_exception()
		{
			Assert.Throws<ArgumentException>(() => SafeProperty<A>.GetGetProperty(x => GetString(x)));
		}

		[Test]
		public void Should_property_handle_null()
		{
			A a = null;

			const string expected = "123";

			string id = expected;
			_getter(a, value => { id = value; });

			Assert.AreEqual(expected, id);
		}

		[Test]
		public void oiuwer()
		{
			GetProperty<A, string> getter = SafeProperty<A>.GetGetProperty(x => x.TheB.Value);

			var subject = new A
				{
					TheB = new B
						{
							Value = "123"
						}
				};
			string id = null;
			getter(subject, value => { id = value; });

			Assert.AreEqual("123", id);
		}


		string GetString(A obj)
		{
			return obj.Id;
		}

		GetProperty<A, string> _getter;


		[TestFixtureSetUp]
		public void Once()
		{
			_getter = SafeProperty<A>.GetGetProperty(x => x.Id);
		}


		class A
		{
			public string Id { get; set; }

			public B TheB { get; set; }
		}


		class B
		{
			public string Value { get; set; }
		}
	}
}