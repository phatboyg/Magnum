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
	public class NotNull_Specs
	{
		[Test]
		public void Should_pass_with_no_conditions()
		{
			Order order = null;

			List<Violation> violations = _validator.Validate(order).ToList();

			Assert.AreEqual(1, violations.Count);
		}

		Validator<Order> _validator;

		[TestFixtureSetUp]
		public void Should_be_able_to_create_a_validator_for_a_class()
		{
			_validator = Validator.New<Order>(x =>
				{
					// do not allow null values
					x.NotNull();
				});
		}
	}


	[TestFixture]
	public class NotEmpty_Tests
	{
		[Test]
		public void Should_not_violate_a_valid_string()
		{
			var order = new Order
				{
					OrderId = "a"
				};

			List<Violation> violations = _validator.Validate(order).ToList();
			Assert.AreEqual(0, violations.Count);
		}

		[Test]
		public void Should_support_not_empty()
		{
			var order = new Order
				{
					OrderId = ""
				};

			List<Violation> violations = _validator.Validate(order).ToList();
			Assert.AreEqual(1, violations.Count);
			Assert.AreEqual("cannot be empty", violations[0].Message);
		}

		[Test]
		public void Should_support_not_null()
		{
			var order = new Order
				{
					OrderId = null
				};

			List<Violation> violations = _validator.Validate(order).ToList();
			Assert.AreEqual(1, violations.Count);
			Assert.AreEqual("cannot be null", violations[0].Message);
		}

		Validator<Order> _validator;

		[TestFixtureSetUp]
		public void Using_a_not_empty_and_not_null_validator_on_a_string_property()
		{
			_validator = Validator.New<Order>(x =>
				{
					x.Property(y => y.OrderId)
						.NotEmpty()
						.NotNull();
				});
		}
	}
}