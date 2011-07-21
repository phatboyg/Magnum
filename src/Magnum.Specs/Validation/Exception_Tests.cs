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
	using Magnum.Validation;
	using Model;
	using NUnit.Framework;


	[TestFixture]
	public class Exception_Tests
	{
		[Test]
		public void Should_support_not_null()
		{
			Order order = null;

			Assert.Throws<ViolationException>(() => _validator.Validate(order).ThrowIfInvalid());
		}

		Validator<Order> _validator;

		[TestFixtureSetUp]
		public void Using_a_not_empty_and_not_null_validator_on_a_string_property()
		{
			_validator = Validator.New<Order>(x => { x.NotNull(); });
		}
	}
}