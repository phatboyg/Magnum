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
namespace Magnum.Specs.Monads
{
	using System;
	using Magnum.Monads;
	using NUnit.Framework;


	[TestFixture]
	public class MaybeTests
	{
		[Test]
		public void AccessingValueOnAValueReturnsExpectedValue()
		{
			Assert.IsNull(new Maybe<string>((string)null).Value);
			Assert.AreEqual("Hello, World!", new Maybe<string>("Hello, World!").Value);
		}

		[Test]
		public void AccessingValueOnNothingThrowsException()
		{
			Assert.Throws<InvalidOperationException>(() => Console.WriteLine(Maybe<string>.Nothing.Value));
		}

		[Test]
		public void Assign_AssignsValueToReferenceIfHasValue()
		{
			string rawValue = "Hello World!";

			string refString = null;
			int refInt = 0;

			Maybe<string>.Nothing
				.Assign(ref refString)
				.Select(x => x.Length)
				.Assign(ref refInt);

			Assert.IsNull(refString);
			Assert.AreEqual(0, refInt);

			Maybe.Value(rawValue)
				.Assign(ref refString)
				.Select(x => x.Length)
				.Assign(ref refInt);

			Assert.AreEqual(rawValue, refString);
			Assert.AreEqual(rawValue.Length, refInt);
		}

		[Test]
		public void Assign_ExecutesImmediately()
		{
			bool executed = false;
			Maybe.Value(true)
				.Assign(ref executed);

			Assert.IsTrue(executed);
		}

		[Test]
		public void Bind_ThatImplicitlyThrowsException_ExceptionReturnsCorrectly()
		{
			Maybe<int> results = Maybe<int>
				.Default
				.Bind(x => 7/x);

			Assert.IsInstanceOf<DivideByZeroException>(results.Exception);
		}

		[Test]
		public void Bind_ThatImplicitlyThrowsException_ValueRethrowsException()
		{
			Maybe<int> results = Maybe<int>
				.Default
				.Bind(x => 7/x);

			Assert.Throws<DivideByZeroException>(() => { int x = results.Value; });
		}

		[Test]
		public void Bind_ThatReturnsMaybe_ValueReturnsCorrectly()
		{
			Maybe<int> results = Maybe<int>.Default
				.Bind(x => new Maybe<int>(x + 7))
				.Bind(x => new Maybe<int>(x*6));

			Assert.AreEqual(42, results.Value);
		}

		[Test]
		public void Bind_ThatReturnsScalar_ValueReturnsCorrectly()
		{
			Maybe<int> results = Maybe<int>.Default
				.Bind(x => x + 7)
				.Bind(x => x*6);

			Assert.AreEqual(42, results.Value);
		}

		[Test]
		public void Bind_WhenExceptionOccurs_DoesNotExecuteRemainingComputations()
		{
			bool executed = false;

			Maybe<bool> results = Maybe<int>
				.Default
				.Bind(x => 7/x)
				.Bind(x => executed = true);

			Assert.Throws<DivideByZeroException>(() => { bool x = results.Value; });
			Assert.IsFalse(executed);
		}

		[Test]
		public void Bind_WhenNothing_DoesNotExecuteRemaningComputations()
		{
			bool executed = false;

			Maybe<bool> results = Maybe<int>
				.Default
				.Bind(x => x + 7)
				.Bind(x => Maybe<int>.Nothing)
				.Bind(x => executed = true);

			Assert.IsFalse(results.HasValue);
			Assert.IsFalse(executed);
		}

		[Test]
		public void BoxedMaybeEqualsBehavesCorrectly()
		{
			object left = new Maybe<int>(7);
			object right = new Maybe<int>(7);

			Assert.IsTrue(left.Equals(right));
			Assert.IsFalse(left.Equals(null));
			Assert.IsFalse(left.Equals(7));
		}

		[Test]
		public void DefaultValueEqualsNothing()
		{
			Maybe<int> val = default(Maybe<int>);
			Assert.IsTrue(val == Maybe<int>.Nothing);
		}

		[Test]
		public void DefaultValueHasNothing()
		{
			Maybe<int> val = default(Maybe<int>);
			Assert.IsFalse(val.HasValue);
		}

		[Test]
		public void Do_CallsActionIfHasValueIsTrue()
		{
			bool didExecute = false;

			Maybe<string> value = Maybe<string>.Nothing
				.Do(x => didExecute = true);

			Assert.IsFalse(value.HasValue);
			Assert.IsFalse(didExecute);

			value = Maybe.Value("Hello World!")
				.Do(x => didExecute = true);

			Assert.IsTrue(value.HasValue);
			Assert.AreEqual("Hello World!", value.Value);
			Assert.IsTrue(didExecute);
		}

		[Test]
		public void Do_DefersExecutionUntilEvaluated()
		{
			bool executed = false;
			Maybe<string> value = Maybe.Value(() =>
				{
					executed = true;
					return "42";
				})
				.Do(x => executed = true);

			Assert.IsFalse(executed);

			Assert.AreEqual("42", value.Value);
			Assert.IsTrue(executed);
		}

		[Test]
		public void EqualsUsesNonGenericEqualsOfUnderlyingValueIfGenericIsNotAvailable()
		{
			var left = new Maybe<DayOfWeek>(DayOfWeek.Friday);
			var right = new Maybe<DayOfWeek>(DayOfWeek.Friday);

			Assert.IsTrue(left == right);
		}

		[Test]
		public void Equals_WithDifferentException_ReturnsFalse()
		{
			Maybe<int> first = Maybe<int>
				.Default
				.Bind(x => 7/x);

			Maybe<int> second = Maybe<int>
				.Default
				.Bind(ThrowsException);

			Assert.IsFalse(first.Equals(second));
		}

		[Test]
		public void Equals_WithOnlyOneException_ReturnsFalse()
		{
			Maybe<int> result = Maybe<int>
				.Default
				.Bind(x => 7/x);

			Assert.IsFalse(Maybe<int>.Default.Equals(result));
		}

		[Test]
		public void Equals_WithSameException_ReturnsTrue()
		{
			Maybe<int> results = Maybe<int>
				.Default
				.Bind(x => 7/x);

			Assert.IsTrue(results.Equals(results));
		}

		[Test]
		public void GetHashCodeReturnsNegativeOneForNothing()
		{
			Assert.AreEqual(-1, Maybe<int>.Nothing.GetHashCode());
		}

		[Test]
		public void GetHashCodeReturnsUnderlyingHashCodeWhenHasValue()
		{
			int val = 42;
			Assert.AreEqual(val.GetHashCode(), new Maybe<int>(val).GetHashCode());
		}

		[Test]
		public void GetHashCode_WithException_ReturnsExceptionsHashCode()
		{
			Maybe<int> result = Maybe<int>
				.Default
				.Bind(x => 7/x);

			Assert.AreEqual(result.Exception.GetHashCode(), result.GetHashCode());
		}

		[Test]
		public void HasValueOnAValueReturnsTrue()
		{
			Assert.IsTrue(new Maybe<string>("").HasValue);
		}

		[Test]
		public void HasValueOnNothingReturnsFalse()
		{
			Assert.IsFalse(Maybe<string>.Nothing.HasValue);
		}

		[Test]
		public void NotNull_WithNonNullReferenceType_ReturnsValue()
		{
			Maybe<string> value = Maybe.NotNull("Hello World!");

			Assert.IsTrue(value.HasValue);
			Assert.AreEqual("Hello World!", value.Value);
		}

		[Test]
		public void NotNull_WithNonNullValueType_ReturnsValue()
		{
			Maybe<int> value = Maybe.NotNull((int?)42);

			Assert.IsTrue(value.HasValue);
			Assert.AreEqual(42, value.Value);
		}

		[Test]
		public void NotNull_WithNullReferenceType_ReturnsNothing()
		{
			Assert.IsFalse(Maybe.NotNull((string)null).HasValue);
		}

		[Test]
		public void NotNull_WithNullValueType_ReturnsNothing()
		{
			Assert.IsFalse(Maybe.NotNull((int?)null).HasValue);
		}

		[Test]
		public void NotNull_WithReferenceType_DefersExecutionUntilEvaluated()
		{
			bool executed = false;
			Maybe<string> value = Maybe.NotNull(() =>
				{
					executed = true;
					return "42";
				});

			Assert.IsFalse(executed);

			Assert.AreEqual("42", value.Value);
			Assert.IsTrue(executed);
		}

		[Test]
		public void NotNull_WithValueType_DefersExecutionUntilEvaluated()
		{
			bool executed = false;
			Maybe<int> value = Maybe.NotNull(() =>
				{
					executed = true;
					return (int?)42;
				});

			Assert.IsFalse(executed);

			Assert.AreEqual(42, value.Value);
			Assert.IsTrue(executed);
		}

		[Test]
		public void NullValueDoesNotEqualNothing()
		{
			Assert.IsTrue(new Maybe<string>((string)null) != Maybe<string>.Nothing);
		}

		[Test]
		public void OnException_ContinuesWithNewValue()
		{
			Func<string, string> throwException = x => { throw new InvalidOperationException(); };

			Maybe<int> value = Maybe<string>.Default
				.Bind(throwException)
				.Select(x => x.Length)
				.Where(x => x > 10)
				.OnException(42);

			Assert.IsTrue(value.HasValue);
			Assert.AreEqual(42, value.Value);
		}

		[Test]
		public void OnException_DefersExecutionUntilEvaluated()
		{
			bool executed = false;
			Maybe<string> value = Maybe.Value(() =>
				{
					executed = true;
					return "42";
				})
				.OnException("Hello")
				.OnException(x => executed = true);

			Assert.IsFalse(executed);

			Assert.AreEqual("42", value.Value);
			Assert.IsTrue(executed);
		}

		[Test]
		public void OnException_WhenHandlerThrowsException_HandlerExceptionPropagates()
		{
			Func<string, string> throwException = x => { throw new InvalidOperationException(); };

			Func<Exception, Maybe<int>> handler = x => { throw new NullReferenceException(); };

			Maybe<int> value = Maybe<string>.Default
				.Bind(throwException)
				.Select(x => x.Length)
				.Where(x => x > 10)
				.OnException(handler);

			Assert.IsInstanceOf<NullReferenceException>(value.Exception);
		}

		[Test]
		public void Return_UnwrapsValueIfItHasAvalue()
		{
			string rawValue = "Hello World!";
			string value = Maybe
				.NotNull(rawValue)
				.Return("{default}");

			Assert.AreEqual(rawValue, value);

			value = Maybe<string>.Nothing
				.Return("{default}");

			Assert.AreEqual("{default}", value);
		}

		[Test]
		public void Select_DefersExecutionUntilEvaluated()
		{
			bool executed = false;
			Maybe<int> value = Maybe.Value(() =>
				{
					executed = true;
					return "42";
				})
				.Select(x => x.Length);

			Assert.IsFalse(executed);

			Assert.AreEqual(2, value.Value);
			Assert.IsTrue(executed);
		}

		[Test]
		public void Select_ReturnsSelectedValueInMaybe()
		{
			string rawValue = "Hello World!";

			Maybe<int> value = Maybe
				.Value(rawValue)
				.Select(x => x.Length);

			Assert.IsTrue(value.HasValue);
			Assert.AreEqual(rawValue.Length, value.Value);
		}

		[Test]
		public void Select_WhenSelectorReturnsMaybe_DefersExecutionUntilEvaluated()
		{
			bool executed = false;
			Maybe<int> value = Maybe.Value(() =>
				{
					executed = true;
					return "42";
				})
				.Select(x => Maybe<int>.Nothing);

			Assert.IsFalse(executed);

			Assert.IsFalse(value.HasValue);
			Assert.IsTrue(executed);
		}

		[Test]
		public void ThrowIfException_DefersExecutionUntilEvaluated()
		{
			bool executed = false;
			Maybe<int> value = Maybe.Value<int>(() =>
				{
					executed = true;
					throw new InvalidOperationException();
				})
				.ThrowIfException();

			Assert.IsFalse(executed);

			Assert.Throws<InvalidOperationException>(() => { int notAssigned = value.Value; });
			Assert.IsTrue(executed);
		}

		[Test]
		public void TwoMaybesWithSameValueAreEqual()
		{
			var left = new Maybe<int>(7);
			var right = new Maybe<int>(7);

			Assert.IsTrue(left == right);
		}

		[Test]
		public void Unless_DefersExecutionUntilEvaluated()
		{
			bool executed = false;
			Maybe<string> value = Maybe.Value(() =>
				{
					executed = true;
					return "42";
				})
				.Unless(x => x.Length == 2);

			Assert.IsFalse(executed);

			Assert.IsFalse(value.HasValue);
			Assert.IsTrue(executed);
		}

		[Test]
		public void Unless_ReturnsValueIfPredicateReturnsFalse()
		{
			string rawValue = "Hello World!";

			Maybe<string> value = Maybe
				.Value(rawValue)
				.Unless(x => x.Length == rawValue.Length - 1);

			Assert.IsTrue(value.HasValue);
			Assert.AreEqual(rawValue, value.Value);

			value = Maybe
				.Value(rawValue)
				.Unless(x => x.Length == rawValue.Length);

			Assert.IsFalse(value.HasValue);
		}

		[Test]
		public void Value_DefersExecutionUntilEvaluated()
		{
			bool executed = false;
			Maybe<int> value = Maybe.Value(() =>
				{
					executed = true;
					return 42;
				});

			Assert.IsFalse(executed);

			Assert.AreEqual(42, value.Value);
			Assert.IsTrue(executed);
		}

		[Test]
		public void Value_ReturnsValueWrapedInMaybe()
		{
			Maybe<string> value = Maybe.Value("Hello World!");

			Assert.IsTrue(value.HasValue);
			Assert.AreEqual("Hello World!", value.Value);

			value = Maybe.Value((string)null);

			Assert.IsTrue(value.HasValue);
			Assert.IsNull(value.Value);
		}

		[Test]
		public void When_PredicateIsFalse_UsesOriginalValue()
		{
			Maybe<string> value = Maybe.Value("Hello")
				.When("Goodbye", "World");

			Assert.IsTrue(value.HasValue);
			Assert.AreEqual("Hello", value.Value);
		}

		[Test]
		public void When_PredicateIsTrue_UsesComputation()
		{
			Maybe<string> value = Maybe.Value("Hello")
				.When("Hello", "World");

			Assert.IsTrue(value.HasValue);
			Assert.AreEqual("World", value.Value);
		}

		[Test]
		public void Where_DefersExecutionUntilEvaluated()
		{
			bool executed = false;
			Maybe<string> value = Maybe.Value(() =>
				{
					executed = true;
					return "42";
				})
				.Where(x => x.Length == 2);

			Assert.IsFalse(executed);

			Assert.AreEqual("42", value.Value);
			Assert.IsTrue(executed);
		}

		[Test]
		public void Where_ReturnsValueIfPredicateReturnsTrue()
		{
			string rawValue = "Hello World!";

			Maybe<string> value = Maybe
				.Value(rawValue)
				.Where(x => x.Length == rawValue.Length);

			Assert.IsTrue(value.HasValue);
			Assert.AreEqual(rawValue, value.Value);

			value = Maybe
				.Value(rawValue)
				.Where(x => x.Length == rawValue.Length - 1);

			Assert.IsFalse(value.HasValue);
		}

		[Test]
		public void With_WhenHasValue_Executes()
		{
			int checkValue = 0;

			string value = Maybe.Value("Hello")
				.With(x => x.Length, x => checkValue = x)
				.Return();

			Assert.AreEqual("Hello", value);
			Assert.AreEqual(5, checkValue);
		}

		static int ThrowsException(int x)
		{
			throw new InvalidOperationException();
		}
	}
}