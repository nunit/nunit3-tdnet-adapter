namespace NUnitTDNet.Adapter.Examples.Expected
{
    using NUnit.Framework;
    using NUnitTDNet.Expected;
    using System;
    using TestDriven.Framework;

    [ExpectTestRun(TestRunState.Success, PassedCount = 1)]
    [TestFixture(typeof(string))]
    public class GenericTestFixtures<T>
    {
        [Test]
        public void IsTypeOfString()
        {
            Assert.That(typeof(T), Is.EqualTo(typeof(string)));
        }
    }

    [ExpectTestRun(TestRunState.Success, PassedCount = 1)]
    [TestFixture(typeof(string), typeof(Uri))]
    public class GenericTestFixtures2<T,U>
    {
        [Test]
        public void Test()
        {
        }
    }

    [TestFixture(typeof(string))]
    public class TestsInGenericFixtures<T>
    {
        [Test, ExpectTestRun(TestRunState.Success, PassedCount = 1)]
        public void Test()
        {
        }
    }
}
