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
namespace Magnum.Monads
{
	using System;
	using System.Collections.Generic;
	using Extensions;


	public struct Maybe<T> :
		IEquatable<Maybe<T>>,
		IEquatable<T>
	{
		public static readonly Maybe<T> Default = new Maybe<T>(default(T));
		public static readonly Maybe<T> Nothing;
		readonly Func<Result> _computation;

		public Maybe(Func<T> computation)
			: this()
		{
			_computation = Default.Bind(x => computation()).Computation;
		}

		public Maybe(T value)
			: this()
		{
			_computation = () => new Result
				{
					Value = value,
					HasValue = true
				};
		}

		Maybe(Func<Result> computation)
			: this()
		{
			_computation = computation;
		}

		Func<Result> Computation
		{
			get { return _computation ?? (() => new Result()); }
		}

		public T Value
		{
			get
			{
				Result result = Computation();

				if (result.Exception != null)
					throw result.Exception;

				if (result.HasValue != true)
					throw new InvalidOperationException("No value can be provided.");

				return result.Value;
			}
		}

		public bool HasValue
		{
			get { return Computation().HasValue; }
		}

		public Exception Exception
		{
			get { return Computation().Exception; }
		}

		public bool Equals(Maybe<T> other)
		{
			return Equals(other, EqualityComparer<T>.Default);
		}

		public bool Equals(T other)
		{
			return Equals(new Maybe<T>(other));
		}

		public static Maybe<T> Unsafe(Func<Maybe<T>> unsafeComputation)
		{
			return new Maybe<T>(() => unsafeComputation().Computation());
		}

		public bool Equals(Maybe<T> other, IEqualityComparer<T> comparer)
		{
			Guard.AgainstNull(comparer, "comparer");

			if (Exception != null)
				return other.Exception != null && other.Exception == Exception;

			if (other.Exception != null)
				return false;

			if (!HasValue)
				return !other.HasValue;

			if (!other.HasValue)
				return false;

			return comparer.Equals(Value, other.Value);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(obj, null))
				return false;

			if (obj is Maybe<T>)
				return Equals((Maybe<T>)obj);

			return false;
		}

		public override int GetHashCode()
		{
			if (Exception != null)
				return Exception.GetHashCode();

			if (HasValue != true)
				return -1;

			if (Value == null)
				return 0;

			return Value.GetHashCode();
		}

		public static bool operator ==(Maybe<T> left, Maybe<T> right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Maybe<T> left, Maybe<T> right)
		{
			return !(left == right);
		}

		public static bool operator ==(Maybe<T> left, T right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Maybe<T> left, T right)
		{
			return !(left == right);
		}

		public static bool operator ==(T left, Maybe<T> right)
		{
			return right.Equals(left);
		}

		public static bool operator !=(T left, Maybe<T> right)
		{
			return !(left == right);
		}

		public Maybe<TResult> Bind<TResult>(Func<T, TResult> func)
		{
			return Bind(x => new Maybe<TResult>(func(x)));
		}

		public Maybe<TResult> Bind<TResult>(Func<T, Maybe<TResult>> func)
		{
			Func<Result> computation = Computation;

			Func<Maybe<TResult>.Result> boundComputation = () =>
				{
					Result result = computation();

					if (result.Exception != null)
					{
						return new Maybe<TResult>.Result
							{
								Exception = result.Exception
							};
					}

					if (result.HasValue != true)
						return new Maybe<TResult>.Result();

					try
					{
						return func(result.Value).Computation();
					}
					catch (Exception ex)
					{
						return new Maybe<TResult>.Result
							{
								Exception = ex
							};
					}
				};

			return new Maybe<TResult>(boundComputation.Memoize());
		}

		public static implicit operator Maybe<T>(T value)
		{
			return new Maybe<T>(value);
		}

		public static explicit operator T(Maybe<T> value)
		{
			return value.Value;
		}


		struct Result
		{
			public Exception Exception;
			public bool HasValue;
			public T Value;
		}
	}


	public static class Maybe
	{
		public static Maybe<T> NotNull<T>(T value) where T : class
		{
			return Value(value).NotNull();
		}

		public static Maybe<T> NotNull<T>(T? value) where T : struct
		{
			return Value(value)
				.NotNull()
				.Select(x => x.Value);
		}

		public static Maybe<T> NotNull<T>(Func<T> computation) where T : class
		{
			return Value(computation)
				.NotNull();
		}

		public static Maybe<T> NotNull<T>(Func<T?> computation) where T : struct
		{
			return Value(computation)
				.NotNull()
				.Select(x => x.Value);
		}

		public static Maybe<T> Value<T>(T value)
		{
			return new Maybe<T>(value);
		}

		public static Maybe<T> Value<T>(Func<T> computation)
		{
			return new Maybe<T>(computation);
		}

		public static Maybe<TResult> Select<T, TResult>(this Maybe<T> self, Func<T, TResult> selector)
		{
			Guard.AgainstNull(selector, "selector");
			return self.Bind(selector);
		}

		public static Maybe<TResult> Select<T, TResult>(this Maybe<T> self, Func<T, Maybe<TResult>> selector)
		{
			Guard.AgainstNull(selector, "selector");
			return self.Bind(selector);
		}

		public static Maybe<T> NotNull<T>(this Maybe<T> self) where T : class
		{
			return self.NotNull(x => x);
		}

		public static Maybe<T?> NotNull<T>(this Maybe<T?> self) where T : struct
		{
			return self.NotNull(x => x);
		}

		public static Maybe<TResult> NotNull<T, TResult>(this Maybe<T> self, Func<T, TResult> selector) where TResult : class
		{
			Guard.AgainstNull(selector, "selector");
			return self
				.Select(selector)
				.Where(x => x != null);
		}

		public static Maybe<TResult?> NotNull<T, TResult>(this Maybe<T> self, Func<T, TResult?> selector)
			where TResult : struct
		{
			Guard.AgainstNull(selector, "selector");
			return self
				.Select(selector)
				.Where(x => x.HasValue);
		}

		public static Maybe<T> Where<T>(this Maybe<T> self, Func<T, bool> predicate)
		{
			Guard.AgainstNull(predicate, "predicate");
			return self.Bind(x => predicate(x) ? x : Maybe<T>.Nothing);
		}

		public static Maybe<T> Unless<T>(this Maybe<T> self, Func<T, bool> predicate)
		{
			Guard.AgainstNull(predicate, "predicate");
			return self.Bind(x => predicate(x) ? Maybe<T>.Nothing : x);
		}

		public static T Return<T>(this Maybe<T> self)
		{
			return self.Return(default(T));
		}

		public static T Return<T>(this Maybe<T> self, T @default)
		{
			return self
				.OnException(@default)
				.OnNothing(@default)
				.Value;
		}

		public static Maybe<T> Do<T>(this Maybe<T> self, Action<T> action)
		{
			Guard.AgainstNull(action, "action");
			return self.Bind(x =>
				{
					action(x);
					return x;
				});
		}

		public static Maybe<T> Assign<T>(this Maybe<T> self, ref T target)
		{
			if (self.HasValue)
				target = self.Value;

			return self;
		}

		public static Maybe<T> OnNothing<T>(this Maybe<T> self, T value)
		{
			return self.OnNothing(() => value);
		}

		public static Maybe<T> OnNothing<T>(this Maybe<T> self, Func<T> valueFactory)
		{
			return self.OnNothing(() => Value(valueFactory));
		}

		public static Maybe<T> OnNothing<T>(this Maybe<T> self, Func<Maybe<T>> valueFactory)
		{
			Guard.AgainstNull(valueFactory, "valueFactory");
			return self.When(x => x.HasValue != true, x => valueFactory());
		}

		public static Maybe<T> OnException<T>(this Maybe<T> self, T value)
		{
			return self.OnException(x => Value(value));
		}

		public static Maybe<T> OnException<T>(this Maybe<T> self, Action<Exception> handler)
		{
			Guard.AgainstNull(handler, "handler");
			return self.When(x => x.Exception != null, x =>
				{
					handler(x.Exception);
					return x;
				});
		}

		public static Maybe<T> OnException<T>(this Maybe<T> self, Func<Exception, Maybe<T>> handler)
		{
			Guard.AgainstNull(handler, "handler");
			return self.When(x => x.Exception != null, x => handler(x.Exception));
		}

		public static Maybe<T> ThrowIfException<T>(this Maybe<T> self)
		{
			return self.ThrowIfException(typeof(Exception));
		}

		public static Maybe<T> ThrowIfException<T>(this Maybe<T> self, Type exceptionType)
		{
			Guard.AgainstNull(exceptionType, "exceptionType");

			Func<Maybe<T>> boundComputation = () =>
				{
					if (self.Exception != null && exceptionType.IsAssignableFrom(self.Exception.GetType()))
						throw self.Exception;

					return self;
				};

			return Maybe<T>.Unsafe(boundComputation.Memoize());
		}

		public static Maybe<T> With<T, TSelected>(this Maybe<T> self, Func<T, TSelected> selector, Action<TSelected> action)
		{
			Guard.AgainstNull(selector, "selector");
			Guard.AgainstNull(action, "action");

			return With(self, x => Value(selector(x)), action);
		}

		public static Maybe<T> With<T, TSelected>(this Maybe<T> self, Func<T, Maybe<TSelected>> selector,
		                                          Action<TSelected> action)
		{
			Guard.AgainstNull(selector, "selector");
			Guard.AgainstNull(action, "action");

			return self.Bind(x =>
				{
					selector(x)
						.Do(action)
						.Return();

					return x;
				});
		}

		public static Maybe<T> When<T>(this Maybe<T> self, Func<Maybe<T>, bool> predicate,
		                               Func<Maybe<T>, Maybe<T>> computation)
		{
			Guard.AgainstNull(predicate, "predicate");
			Guard.AgainstNull(computation, "computation");

			return Value(self)
				.Select(predicate)
				.Select(x => x ? computation(self) : self);
		}

		public static Maybe<T> When<T>(this Maybe<T> self, Maybe<T> value, Func<T, Maybe<T>> computation)
		{
			Guard.AgainstNull(computation, "computation");
			return self.When(x => x.Equals(value), x => x.Bind(computation));
		}

		public static Maybe<T> When<T>(this Maybe<T> self, Maybe<T> value, Action<T> action)
		{
			Guard.AgainstNull(action, "action");
			return self.When(value, x =>
				{
					action(x);
					return x;
				});
		}

		public static Maybe<T> When<T>(this Maybe<T> self, Func<Maybe<T>, bool> predicate, Maybe<T> result)
		{
			Guard.AgainstNull(predicate, "predicate");
			return self.When(predicate, x => result);
		}

		public static Maybe<T> When<T>(this Maybe<T> self, Maybe<T> value, Maybe<T> result)
		{
			return self.When(value, x => result);
		}

		public static Maybe<T> Run<T>(this Maybe<T> self)
		{
			return self.HasValue ? self : self;
		}
	}


	public static class ExtensionsToMaybe
	{
		public static Maybe<T> ToMaybe<T>(this T value)
		{
			return new Maybe<T>(value);
		}

		public static Maybe<V> SelectMany<T, U, V>(this Maybe<T> m, Func<T, Maybe<U>> k, Func<T, U, V> s)
		{
			return m.SelectMany(x => k(x).SelectMany(y => s(x, y).ToMaybe()));
		}

		public static Maybe<U> SelectMany<T, U>(this Maybe<T> m, Func<T, Maybe<U>> k)
		{
			return !m.HasValue ? Maybe<U>.Nothing : k(m.Value);
		}
	}
}