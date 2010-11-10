namespace Magnum.Specs.PerformanceCounters
{
    using System.Diagnostics;
    using Magnum.PerformanceCounters;
    using NUnit.Framework;

    [TestFixture]
    public class DeletingCountersSpecs
    {
        [Test]
        public void DeleteCatagory()
        {
            var cr = new CounterRepository();
            var ccfg = new CategoryConfiguration();
            ccfg.Name = "MagnumTest";
            ccfg.Help = "Help Me Rhonda";
            ccfg.Counters.Add(new PerformanceCounterConfiguration("Test Counter", "Test Help", PerformanceCounterType.NumberOfItems32));
            cr.RegisterCategory(ccfg);

            cr.RemoveCatagory("MagnumTest");
        }
    }
}