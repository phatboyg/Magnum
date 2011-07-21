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
	using Magnum.Extensions;
	using Magnum.Validation;
	using Model;
	using NUnit.Framework;


	[TestFixture]
	public class When_validating_datetime_fields
	{
		[Test]
		public void Should_pass_with_no_conditions()
		{
			var order = new Order();

			List<Violation> violations = _validator.Validate(order).ToList();

			Assert.AreEqual(1, violations.Count);
			Assert.AreEqual("must be within the past month", violations[0].Message);
			Assert.AreEqual("Order.OrderDate", violations[0].Key);
			Assert.AreEqual("Order.OrderDate must be within the past month", violations[0].ToString());
		}

		Validator<Order> _validator;

		[TestFixtureSetUp]
		public void Should_be_able_to_create_a_validator_for_a_class()
		{
			_validator = Validator.New<Order>(x =>
				{
					x.Property(y => y.OrderDate)
						.WithinPast(30.Days());
				});
		}
	}


	[TestFixture]
	public class When_validating_datetime_fields_really_long
	{
		[Test]
		public void Should_pass_with_no_conditions()
		{
			var order = new Order();

			List<Violation> violations = _validator.Validate(order).ToList();

			Assert.AreEqual(1, violations.Count);
			Assert.AreEqual("must be within the past 1 year, 2 months, 1 week, 3 days", violations[0].Message);
			Assert.AreEqual("Order.OrderDate", violations[0].Key);
			Assert.AreEqual("Order.OrderDate must be within the past 1 year, 2 months, 1 week, 3 days", violations[0].ToString());
		}

		Validator<Order> _validator;

		[TestFixtureSetUp]
		public void Should_be_able_to_create_a_validator_for_a_class()
		{
			_validator = Validator.New<Order>(x =>
				{
					x.Property(y => y.OrderDate)
						.WithinPast(435.Days());
				});
		}
	}
}