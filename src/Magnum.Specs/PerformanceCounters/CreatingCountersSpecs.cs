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
namespace Magnum.Specs.PerformanceCounters
{
    using System.Diagnostics;
    using Magnum.Extensions;
    using Magnum.PerformanceCounters;
    using NUnit.Framework;
    using TestFramework;


    public abstract class PerformanceCounterBase
    {
        public void PerformanceCounterExists(string categoryName, string counterName)
        {
            Assert.IsTrue(PerformanceCounterCategory.CounterExists(counterName, categoryName),
                          "Performance Counter '{0}\\{1}' doesn't exist".FormatWith(counterName, categoryName));
        }

        public void PerformaneCategoryExists(string categoryName)
        {
            Assert.IsTrue(PerformanceCounterCategory.Exists(categoryName),
                          "Performance Category '{0}' doesn't exist".FormatWith(categoryName));
        }
    }


    [TestFixture]
    public class CreatingCountersSpecs
    {
        [Test]
        public void CanCreateCategoryAndCounter()
        {
            CounterRepository cr = CounterRepositoryConfigurator.New(cfg => { cfg.Register<MagnumTestCounters>(); });
            var counters = cr.GetCounter<MagnumTestCounters>("_default");
            cr.Dispose();
        }
    }
}