﻿// Copyright 2007-2008 The Apache Software Foundation.
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

namespace Magnum.Specs.Reflection
{
    using System.Linq;
    using Magnum.Reflection;
    using NUnit.Framework;

    interface IFoo
    {
        string Text { get; set; }
        int Number { get; }
        object Object { set; }
    }

    interface IBar
    {
        float Float { get; set; }
    }

    interface IBaz : IBar
    {
        double Double { get; set; }
    }

    interface IComplexInterface : IFoo, IBaz
    {
        char Char { get; set; }
    }

    [TestFixture]
    public class ExtensionsToGenericArguments_Specs
    {
        [Test]
        public void ShouldReturnAllPublicPropertiesForInterface()
        {
            var expected = new[] { "Text", "Number", "Object" };
            var actual = typeof(IFoo).GetAllProperties().Select(p => p.Name).ToArray();

            Assert.That(actual, Is.EquivalentTo(expected));
        }

        [Test]
        public void ShouldReturnAllPublicPropertiesForAllInterfacesInHierarchy()
        {
            var expected = new[] { "Text", "Number", "Object", "Float", "Double", "Char" };
            var actual = typeof(IComplexInterface).GetAllProperties().Select(p => p.Name).ToArray();

            Assert.That(actual, Is.EquivalentTo(expected));
        }
    }
}
