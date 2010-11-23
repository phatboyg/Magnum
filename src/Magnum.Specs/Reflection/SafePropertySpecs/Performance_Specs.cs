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
namespace Magnum.Specs.Reflection.SafePropertySpecs
{
	using System;
	using System.Diagnostics;
	using System.Linq.Expressions;
	using Magnum.Reflection;
	using NUnit.Framework;


	[TestFixture, Explicit]
	public class Performance_Specs
	{
		[Test]
		public void Regular_access_method()
		{
			var subject = new A
				{
					TheB = new B
						{
							TheC = new C
								{
									Value = 47
								}
						}
				};

			Expression<Func<A, int>> direct = x => x.TheB.TheC.Value;
			Func<A, int> compiled = direct.Compile();

			int iterations = _iterations;

			Trace.WriteLine("Regular method");
			RunTest(() => { compiled(subject); }, iterations, 3);
		}

		[Test]
		public void Safe_property_method()
		{
			var subject = new A
				{
					TheB = new B
						{
							TheC = new C
								{
									Value = 47
								}
						}
				};

			GetProperty<A, int> accessor = SafeProperty<A>.GetGetProperty(x => x.TheB.TheC.Value);

			int iterations = _iterations;

			Trace.WriteLine("Safe method");
			RunTest(() => { accessor(subject, x => { }); }, iterations, 3);
		}

		const int _iterations = 10000000;

		void RunTest(Action test, int iterations, int loops)
		{
			for (int i = 0; i < 100; i++)
				test();

			long totalTime = 0L;

			for (int j = 0; j < loops; j++)
			{
				Stopwatch timer = Stopwatch.StartNew();

				for (int i = 0; i < iterations; i++)
					test();

				timer.Stop();

				totalTime += timer.ElapsedMilliseconds;
			}

			Trace.WriteLine("Elapsed time: " + totalTime/loops + "ms");
		}


		class A
		{
			public B TheB { get; set; }
		}


		class B
		{
			public C TheC { get; set; }
		}


		class C
		{
			public int Value { get; set; }
		}
	}
}