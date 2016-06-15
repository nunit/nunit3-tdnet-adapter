namespace NUnitTDNet.Adapter.Examples.Expected
{
    using NUnit.Framework;
    using TestDriven.Framework;

    // All test methods with same name will be run.
    // Hopefully this won't be too confusing!

    [ExpectTestRun(TestRunState.Success, PassedCount = 3)]
    public class OverloadedMethodTests
    {
        [TestCase(1)]
        [ExpectTestRun(TestRunState.Success, PassedCount = 3)]
        public void Overloaded(int i)
        {
        }

        [TestCase("foo")]
        [ExpectTestRun(TestRunState.Success, PassedCount = 3)]
        public void Overloaded(string text)
        {
        }

        [Test]
        [ExpectTestRun(TestRunState.Success, PassedCount = 3)]
        public void Overloaded()
        {
        }
    }
}
