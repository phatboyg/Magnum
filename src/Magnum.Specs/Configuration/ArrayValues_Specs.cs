namespace Magnum.Specs.Configuration
{
    using Magnum.Configuration;
    using NUnit.Framework;
    using TestFramework;


    [Scenario]
    [Explicit("Failing test we need to look at")]
    public class When_retrieving_an_array_value
    {
        ConfigurationBinder _binder;
        ClassWithArray _configuration;

        [When]
        public void A_configuration_object_is_bound()
        {
            const string json = @"{ Value: [47,22] }";

            _binder = ConfigurationBinderFactory.New(x => { x.AddJson(json); });

            _configuration = _binder.Bind<ClassWithArray>();
        }

        [Then]
        public void Should_have_name_value()
        {
            var i = _configuration.Value[0];
            i.ShouldEqual(47);
        }


        public class ClassWithArray
        {
            public int[] Value { get; set; }
        }


    }
}