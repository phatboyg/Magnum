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
namespace Magnum.Specs.StateMachine
{
    using System;
    using Magnum.StateMachine;

    [Serializable]
    public abstract class AbstractStateMachine<TOne, TTwo, TSub> : StateMachine<TSub>
        where TOne : class
        where TTwo : class
        where TSub : StateMachine<TSub>
    {
        protected static void Definition()
        {
            Initially(When(AnEvent).Then((x, y) => { }).TransitionTo(Intermediary));
            During(Intermediary, When(AnEvent).Then((x, y) => { }));
            During(Intermediary, When(TheFinalEvent).Then((x, y) => { }).TransitionTo(Completed));
        }

        public static State Initial { get; set; }
        public static State Intermediary { get; set; }
        public static State Completed { get; set; }

        public static Event<TOne> AnEvent { get; set; }
        public static Event<TTwo> TheFinalEvent { get; set; }
    }


    [Serializable]
    public class SubclassedStateMachine : AbstractStateMachine<string, object, SubclassedStateMachine>
    {
        static SubclassedStateMachine()
        {
            Define(Definition);
        }

        public static State SubclassState { get; set; }
        public static Event<string> SubclassEvent { get; set; }
    }
}