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
namespace Magnum.Specs.Validation
{
	using System.Collections.Generic;
	using System.Linq;
	using Magnum.Validation;
	using Model;
	using NUnit.Framework;


	[TestFixture]
	public class NestedValidator_Tests
	{
		[Test]
		public void Should_accept_a_valid_customer_value()
		{
			var order = new Order
				{
					Customer = new Customer
						{
							Name = "a"
						}
				};

			List<Violation> violations = _validator.Validate(order).ToList();

			Assert.AreEqual(0, violations.Count);
		}

		[Test]
		public void Should_catch_the_null_customer_name()
		{
			var order = new Order
				{
					Customer = new Customer()
				};

			List<Violation> violations = _validator.Validate(order).ToList();

			Assert.AreEqual(1, violations.Count);
			Assert.AreEqual("cannot be null", violations[0].Message);
			Assert.AreEqual("Order.Customer.Name", violations[0].Key);
			Assert.AreEqual("Order.Customer.Name cannot be null", violations[0].ToString());
		}

		[Test]
		public void Should_catch_the_null_property()
		{
			var order = new Order();

			List<Violation> violations = _validator.Validate(order).ToList();

			Assert.AreEqual(1, violations.Count);
			Assert.AreEqual("cannot be null", violations[0].Message);
			Assert.AreEqual("Order.Customer", violations[0].Key);
			Assert.AreEqual("Order.Customer cannot be null", violations[0].ToString());
		}

		Validator<Order> _validator;

		[TestFixtureSetUp]
		public void Specifying_a_nested_validator_for_a_property()
		{
			Validator<Customer> customerValidator = Validator.New<Customer>(x =>
				{
					x.NotNull();
					x.Property(y => y.Name)
						.NotNull()
						.NotEmpty();
				});

			_validator = Validator.New<Order>(x =>
				{
					x.Property(y => y.Customer)
						.ValidateWith(customerValidator);
				});
		}
	}
}