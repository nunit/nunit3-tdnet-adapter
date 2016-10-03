namespace NUnitTDNet.Adapter.Examples.Expected
{
    using System;
    using NUnit.Framework;
    using TestDriven.Framework;
    using NUnitTDNet.Expected;

    public class TestCaseTests
    {
        [TestCase(1)]
        [TestCase(2)]
        [ExpectTestRun(TestRunState.Success, PassedCount = 2)]
        public void TwoTestCases(int i)
        {
        }

        [TestCase(0, 0)]
        [ExpectTestRun(TestRunState.Failure, FailedCount = 1)]
        public void TooManyArgs(int i)
        {
        }
    }
}
