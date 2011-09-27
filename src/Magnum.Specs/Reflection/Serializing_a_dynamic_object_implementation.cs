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
namespace Magnum.Specs.Reflection
{
    using System.Diagnostics;
    using Magnum.Reflection;
    using Newtonsoft.Json;
    using TestFramework;


    [Scenario]
    public class Serializing_a_dynamic_object_implementation
    {
        string _stringValue = "Johnson";
        ISubject _subject;

        [When]
        public void Initializing_a_dynamic_object_implementation()
        {
            _subject = (ISubject)typeof(ISubject).InitializeProxy(new
                {
                    Value = _stringValue,
                });
        }

        [Then]
        public void Should_set_a_string_value()
        {
            _subject.Value.ShouldEqual(_stringValue);
        }

        [Then]
        public void Should_be_serializable()
        {
            string json = JsonConvert.SerializeObject(_subject);

            Trace.WriteLine(json);
        }


        public interface ISubject
        {
            string Value { get; }
        }
    }
}