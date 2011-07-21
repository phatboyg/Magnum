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
namespace Validation.Tests
{
	using System.Collections.Generic;
	using System.Linq;
	using Magnum.Specs.Validation.Model;
	using Magnum.Validation;
	using NUnit.Framework;


	[TestFixture]
	public class Property_Specs
	{
		[Test]
		public void Should_pass_with_no_conditions()
		{
			var order = new Order();

			List<Violation> violations = _validator.Validate(order).ToList();

			Assert.AreEqual(1, violations.Count);
			Assert.AreEqual("cannot be null", violations[0].Message);
			Assert.AreEqual("Order.OrderId", violations[0].Key);
			Assert.AreEqual("Order.OrderId cannot be null", violations[0].ToString());
		}

		Validator<Order> _validator;

		[TestFixtureSetUp]
		public void Should_be_able_to_create_a_validator_for_a_class()
		{
			_validator = Validator.New<Order>(x =>
				{
					x.Property(y => y.OrderId)
						.NotNull()
						.NotEmpty();
				});
		}
	}


	[TestFixture]
	public class Property_Specs2
	{
		[Test]
		public void Should_match_the_empty_string_value()
		{
			var order = new Order();
			order.OrderId = "";

			List<Violation> violations = _validator.Validate(order).ToList();

			Assert.AreEqual(1, violations.Count);
			Assert.AreEqual("cannot be empty", violations[0].Message);
			Assert.AreEqual("Order.OrderId", violations[0].Key);
			Assert.AreEqual("Order.OrderId cannot be empty", violations[0].ToString());
		}

		Validator<Order> _validator;

		[TestFixtureSetUp]
		public void Should_call_the_chained_validator_on_the_property()
		{
			_validator = Validator.New<Order>(x =>
				{
					x.Property(y => y.OrderId)
						.NotNull()
						.NotEmpty();
				});
		}
	}
}