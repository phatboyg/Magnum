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
namespace Magnum.Routing.Internals
{
	using System;
	using System.Collections.Generic;


	interface Cache<TKey, TValue> :
		IEnumerable<TValue>
	{
		MissingValueProvider<TKey, TValue> MissingValueProvider { set; }
		ValueAddedCallback<TKey, TValue> ValueAddedCallback { set; }
		KeySelector<TKey, TValue> KeySelector { get; set; }

		TValue this[TKey key] { get; set; }
		TValue Get(TKey key);
		TValue Get(TKey key, MissingValueProvider<TKey, TValue> missingValueProvider);

		bool Has(TKey key);
		bool HasValue(TValue value);
		
		void Add(TKey key, TValue value);
		void AddValue(TValue value);
		void Remove(TKey key);
		void RemoveValue(TValue value);
		void Clear();

		void Fill(IEnumerable<TValue> values);

		void Each(Action<TValue> callback);
		void Each(Action<TKey, TValue> callback);
		bool Exists(Predicate<TValue> predicate);
		bool Find(Predicate<TValue> predicate, out TValue result);
		TKey[] GetAllKeys();
		TValue[] GetAll();

		bool WithValue(TKey key, Action<TValue> callback);
		TResult WithValue<TResult>(TKey key, Func<TValue, TResult> callback, TResult defaultValue = default(TResult));
	}
}