namespace NUnitTDNet.Adapter.Examples.Expected
{
    using NUnit.Framework;
    using TestDriven.Framework;

    public class GenericTests
    {
        [ExpectTestRun(TestRunState.Success, PassedCount = 1)]
        [TestCase(0)]
        public void Generic<T>(T t)
        {
            Assert.That(t, Is.InstanceOf<int>());
        }
    }
}
