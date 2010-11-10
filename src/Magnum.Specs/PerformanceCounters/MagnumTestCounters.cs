namespace Magnum.Specs.PerformanceCounters
{
    using Magnum.PerformanceCounters;


    public class MagnumTestCounters :
        CounterCategory
    {
        public Counter ConsumerThreadCount { get; set; }
    }
}