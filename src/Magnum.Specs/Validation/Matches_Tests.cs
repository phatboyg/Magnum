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
	using Magnum.Validation.Advanced;
	using Model;
	using NUnit.Framework;


	[TestFixture]
	public class Matches_Tests
	{
		[Test]
		public void Should_accept_a_value_with_valid_characters()
		{
			var order = new Order
				{
					OrderId = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
				};

			List<Violation> violations = _validator.Validate(order).ToList();
			Assert.AreEqual(0, violations.Count);
		}

		[Test]
		public void Should_fail_with_a_null_value()
		{
			var order = new Order();

			List<Violation> violations = _validator.Validate(order).ToList();
			Assert.AreEqual(1, violations.Count);
			Assert.AreEqual("did not match expression: ^[a-zA-Z]{1,40}$", violations[0].Message);
		}

		[Test]
		public void Should_not_match_a_value_with_invalid_characters()
		{
			var order = new Order
				{
					OrderId = "DEX20101001001"
				};

			List<Violation> violations = _validator.Validate(order).ToList();
			Assert.AreEqual(1, violations.Count);
			Assert.AreEqual("did not match expression: ^[a-zA-Z]{1,40}$", violations[0].Message);
		}

		Validator<Order> _validator;

		[TestFixtureSetUp]
		public void Using_a_matches_expression()
		{
			_validator = Validator.New<Order>(x =>
				{
					x.Property(y => y.OrderId)
						.Matches("^[a-zA-Z]{1,40}$")
						.SingleLine()
						.NotEmpty();
				});
		}
	}
}