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
namespace Magnum.Specs.Caching
{
    using System;
    using Magnum.Caching;
    using NUnit.Framework;


    [TestFixture]
    public class Using_a_generic_type_cache
    {
        GenericTypeCache<IGeneric> _cache;

        [TestFixtureSetUp]
        public void Setup()
        {
            _cache = new GenericTypeCache<IGeneric>(typeof(GenericClass<>));
        }

        [Test]
        public void Should_properly_construct_and_close_a_generic_type()
        {
            Assert.AreEqual(typeof(int), _cache[typeof(int)].SpecializedType);
        }

        [Test]
        public void Should_properly_construct_and_close_another_generic_type()
        {
            _cache.Add(typeof(string), new GenericClass<string>());

            Assert.AreEqual(typeof(string), _cache[typeof(string)].SpecializedType);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Should_throw_an_exception_on_duplicate_add()
        {
            var x = _cache[typeof(bool)];

            _cache.Add(typeof(bool), new GenericClass<bool>());
        }


        interface IGeneric
        {
            Type SpecializedType { get; }
        }


        interface IGeneric<T> :
            IGeneric
        {
        }


        class GenericClass<T> :
            IGeneric<T>
        {
            public Type SpecializedType
            {
                get { return typeof(T); }
            }
        }
    }
}