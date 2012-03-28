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
    using System;
    using System.Collections.Generic;
    using Magnum.Reflection;
    using TestFramework;


    [Scenario]
    public class When_initializing_a_dynamic_object_implementation
    {
        DateTime _dateTimeValue = DateTime.UtcNow;
        int _intValue = 42;
        string _stringValue = "Johnson";
        ISubject _subject;

        [When]
        public void Initializing_a_dynamic_object_implementation()
        {
            _subject = (ISubject)typeof(ISubject).InitializeProxy(new
                {
                    IntValue = _intValue,
                    StringValue = _stringValue,
                    BooleanValue = true,
                    DateTimeValue = _dateTimeValue,
                });
        }

        [Then]
        public void Should_set_a_value_type_properly()
        {
            _subject.IntValue.ShouldEqual(_intValue);
        }

        [Then]
        public void Should_set_a_string_value()
        {
            _subject.StringValue.ShouldEqual(_stringValue);
        }

        [Then]
        public void Should_set_a_boolean_value()
        {
            _subject.BooleanValue.ShouldBeTrue();
        }

        [Then]
        public void Should_set_a_nullable_value_type()
        {
            _subject.DateTimeValue.HasValue.ShouldBeTrue();
            _subject.DateTimeValue.Value.ShouldEqual(_dateTimeValue);
        }


        public interface ISubject
        {
            int IntValue { get; }
            string StringValue { get; }
            bool BooleanValue { get; }
            DateTime? DateTimeValue { get; }
        }
    }

	[Scenario]
	public class When_initializing_with_dictionary_with_subclass_in_property
	{
		const string SoundOfSilence = "";
		const int Age = 24;

		ISubject _subject;

		[When]
		public void Initializing_a_dynamic_object_proxy()
		{
			_subject = (ISubject)typeof(ISubject).InitializeProxy(new Dictionary<string, object>
				{
					{"SoundOfSilence", SoundOfSilence},
					{"SpokenBy.Age", Age}
				});
		}

		[Then]
		public void Should_set_value_val()
		{
			_subject
				.SoundOfSilence
				.ShouldEqual("");
		}

		[Then]
		public void Should_set_SpokenBy()
		{
			_subject.SpokenBy.ShouldNotBeNull();
		}

		[Then]
		public void Should_set_age()
		{
			_subject.SpokenBy.Age
				.ShouldBeEqualTo(Age);
		}

		public interface ISubject
		{
			string SoundOfSilence { get; }
			Person SpokenBy { get; }
		}


		public class Person
		{
			public int Age { get; set; }
		}
	}

	[Scenario]
	public class When_initializing_a_dynamic_object_with_subclass_in_property
	{
		const ulong AnswerToEverything = 42ul;
		const uint Mystery = uint.MaxValue;

		ISubject _subject;

		[When]
		public void Initializing_a_dynamic_object_proxy()
		{
			_subject = (ISubject)typeof(ISubject).InitializeProxy(new
				{
					AnswerToEverything,
					DarkHole = new { Mystery }
				});
		}

		[Then]
		public void Should_set_value_val()
		{
			_subject
				.AnswerToEverything
				.ShouldEqual(AnswerToEverything);
		}

		[Then]
		public void Should_not_set_null_subclass()
		{
			_subject
				.DarkHole.ShouldNotBeNull();
		}

		[Then]
		public void Should_set_subclass_value_vally()
		{
			_subject
				.DarkHole.Mystery
				.ShouldBeEqualTo(Mystery);
		}

		public interface ISubject
		{
			ulong AnswerToEverything { get; }
			DarkHoles DarkHole { get; }
		}

		public class DarkHoles
		{
			public uint Mystery { get; set; }
		}
	}
}