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
	public class When_valiating_a_string_starts_with
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
			Assert.AreEqual("did not start with: ABC", violations[0].Message);
		}

		[Test]
		public void Should_not_match_a_value_with_invalid_characters()
		{
			var order = new Order
				{
					OrderId = "123"
				};

			List<Violation> violations = _validator.Validate(order).ToList();
			Assert.AreEqual(1, violations.Count);
			Assert.AreEqual("did not start with: ABC", violations[0].Message);
		}

		Validator<Order> _validator;

		[TestFixtureSetUp]
		public void Using_a_matches_expression()
		{
			_validator = Validator.New<Order>(x =>
				{
					x.Property(y => y.OrderId)
						.StartsWith("ABC");
				});
		}
	}


	[TestFixture]
	public class When_valiating_a_string_contains
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
			Assert.AreEqual("did not contain: LMNOP", violations[0].Message);
		}

		[Test]
		public void Should_not_match_a_value_with_invalid_characters()
		{
			var order = new Order
				{
					OrderId = "123"
				};

			List<Violation> violations = _validator.Validate(order).ToList();
			Assert.AreEqual(1, violations.Count);
			Assert.AreEqual("did not contain: LMNOP", violations[0].Message);
		}

		Validator<Order> _validator;

		[TestFixtureSetUp]
		public void Using_a_matches_expression()
		{
			_validator = Validator.New<Order>(x =>
				{
					x.Property(y => y.OrderId)
						.Contains("LMNOP");
				});
		}
	}


	[TestFixture]
	public class When_valiating_a_string_ends_with
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
			Assert.AreEqual("did not end with: XYZ", violations[0].Message);
		}

		[Test]
		public void Should_not_match_a_value_with_invalid_characters()
		{
			var order = new Order
				{
					OrderId = "123"
				};

			List<Violation> violations = _validator.Validate(order).ToList();
			Assert.AreEqual(1, violations.Count);
			Assert.AreEqual("did not end with: XYZ", violations[0].Message);
		}

		Validator<Order> _validator;

		[TestFixtureSetUp]
		public void Using_a_matches_expression()
		{
			_validator = Validator.New<Order>(x =>
				{
					x.Property(y => y.OrderId)
						.EndsWith("XYZ");
				});
		}
	}
}