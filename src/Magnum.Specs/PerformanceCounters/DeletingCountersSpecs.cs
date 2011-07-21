namespace Magnum.Specs.PerformanceCounters
{
    using Magnum.PerformanceCounters;
    using NUnit.Framework;

    [TestFixture]
    [Explicit]
    public class DeletingCountersSpecs
    {
        [Test]
        public void DeleteCatagoryViaString()
        {
            using (CounterRepository cr = CounterRepositoryConfigurator.New(cfg => { cfg.Register<MagnumTestCounters>(); }))
            {
                var counters = cr.GetCounter<MagnumTestCounters>("_default");
            }

            using (var cr = new CounterRepository())
            {
                cr.RemoveCategory("MagnumTestCounters");
            }
        }

        [Test]
        public void DeleteCategoryViaType()
        {
            using (CounterRepository cr = CounterRepositoryConfigurator.New(cfg => { cfg.Register<MagnumTestCounters>(); }))
            {
                var counters = cr.GetCounter<MagnumTestCounters>("_default");
            }

            using (var cr = new CounterRepository())
            {
                cr.RemoveCategory<MagnumTestCounters>();
            }
        }
    }
}