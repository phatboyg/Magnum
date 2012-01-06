namespace Magnum.Specs.Activator
{
    using Magnum.Reflection;
    using NUnit.Framework;
    using TestFramework;


    [TestFixture]
    public class When_a_class_has_no_default_constructor
    {
        [Test]
        public void Should_be_able_to_create_one()
        {
            var subject = FastActivator<Subject>.Create();

            subject.ShouldNotBeNull();
        }


        [Test]
        public void Should_be_able_to_create_a_generic_type()
        {
            var subject = FastActivator.Create(typeof(GenericSubject<>), new[] {typeof(int)});

            subject.ShouldNotBeNull();
        }

        class Subject
        {
            public Subject(string value)
            {
                Value = value;
            }

            public string Value { get; private set; }
        }


        class GenericSubject<T>
        {
            public GenericSubject(T value)
            {
                Value = value;
            }

            public T Value { get; private set; }
        }
    }
}
