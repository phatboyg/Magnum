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
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Magnum.StateMachine;
using Magnum.Visualizers.StateMachine;

#if STRONG_NAME
[assembly: InternalsVisibleTo("Magnum.RulesEngine.Specs, PublicKey=002400000480000094000000060200000024000052534131000400000100010077a2fd2aa44b91c158fe6024be4fbd4562e3f53b2e574db07d32ee84177923babd1281ccaf1d020d125697f0ef82ea30a1e9f83fc219778f958998c6e11c547919b6c1a5eed5b01ec6fbd2ba05c77fb605bdaa068d214337ccdcfb6819449348e80a4805a478ab54a16f118136624e63197b7eda38db504b94dc0963730ce3bd")]
#else
[assembly: InternalsVisibleTo("Magnum.RulesEngine.Specs")]
#endif

[assembly: DebuggerVisualizer(typeof(StateMachineDebugVisualizer), typeof(StateMachineVisualizerObjectSource),
    Description = "State Machine Visualizer",
    Target = typeof(StateMachine<>))]