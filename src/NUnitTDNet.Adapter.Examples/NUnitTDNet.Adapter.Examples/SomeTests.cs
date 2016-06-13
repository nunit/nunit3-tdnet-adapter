namespace NUnitTDNet.Adapter.Examples
{
    using NUnit.Framework;

    public class SomeTests
    {
        [Test]
        public static void Pass()
        {
            Assert.That(2 + 2, Is.EqualTo(4));
        }

        [Test]
        public static void Fail()
        {
            Assert.That(2 + 2, Is.EqualTo(5));
        }

        [Test]
        public static void Ignore()
        {
            Assert.Ignore();
        }
    }
}
