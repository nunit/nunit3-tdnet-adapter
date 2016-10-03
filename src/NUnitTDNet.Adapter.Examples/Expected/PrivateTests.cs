namespace NUnitTDNet.Adapter.Examples.Expected
{
    using NUnit.Framework;
    using NUnitTDNet.Expected;
    using TestDriven.Framework;

    [ExpectTestRun(TestRunState.Failure, FailedCount = 1)]
    internal class NonPublicTests
    {
        [Test]
        [ExpectTest(typeof(NonPublicTests), nameof(PrivateTest), TestState.Failed, Message = "Method is not public")]
        private void PrivateTest()
        {
        }
    }
}
