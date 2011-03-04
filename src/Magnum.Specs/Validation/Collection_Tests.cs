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
	public class Collection_Tests
	{
		[Test]
		public void Should_catch_a_null_element_in_the_list()
		{
			var order = new Order();
			order.Items = new List<OrderItem>();
			order.Items.Add(null);

			List<Violation> violations = _validator.Validate(order).ToList();

			Assert.AreEqual(1, violations.Count);
			Assert.AreEqual("cannot be null", violations[0].Message);
			Assert.AreEqual("Order.Items[0]", violations[0].Key);
			Assert.AreEqual("Order.Items[0] cannot be null", violations[0].ToString());
		}

		[Test]
		public void Should_catch_an_empty_collection()
		{
			var order = new Order();
			order.Items = new List<OrderItem>();

			List<Violation> violations = _validator.Validate(order).ToList();

			Assert.AreEqual(1, violations.Count);
			Assert.AreEqual("cannot be empty", violations[0].Message);
			Assert.AreEqual("Order.Items", violations[0].Key);
			Assert.AreEqual("Order.Items cannot be empty", violations[0].ToString());
		}

		[Test]
		public void Should_pass_a_valid_object()
		{
			var order = new Order();
			order.Items = new List<OrderItem>();
			order.Items.Add(new OrderItem());

			List<Violation> violations = _validator.Validate(order).ToList();

			Assert.AreEqual(0, violations.Count);
		}

		Validator<Order> _validator;

		[TestFixtureSetUp]
		public void Validating_a_property_that_is_a_collection()
		{
			Validator<OrderItem> itemValidator = Validator.New<OrderItem>(x => { x.NotNull(); });

			_validator = Validator.New<Order>(x =>
				{
					x.Property(y => y.Items)
						.NotNull()
						.NotEmpty()
						.ValidateWith(itemValidator);
				});
		}
	}
}