namespace NUnitTDNet.Adapter.Examples.Expected
{
    using System;
    using NUnit.Framework;
    using TestDriven.Framework;

    public class TestCaseTests
    {
        [TestCase(1)]
        [TestCase(2)]
        [Expect(TestRunState.Success, PassedCount = 2)]
        public void TwoTestCases(int i)
        {
        }

        [TestCase(1, 2)]
        [Expect(TestRunState.Failure, FailedCount = 1)]
        public void TooManyArgs(int i)
        {
        }
    }
}
