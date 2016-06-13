namespace NUnitTDNet.Adapter.Tests.Fakes
{
    using System;
    using TestDriven.Framework;

    class FakeTestListener : ITestListener
    {
        public int PassedCount
        {
            get; private set;
        }

        public int FailedCount
        {
            get; private set;
        }

        public int IgnoredCount
        {
            get; private set;
        }

        public void TestFinished(TestResult summary)
        {
            switch(summary.State)
            {
                case TestState.Passed:
                    PassedCount++;
                    break;
                case TestState.Failed:
                    FailedCount++;
                    break;
                case TestState.Ignored:
                    IgnoredCount++;
                    break;
            }
        }

        public void TestResultsUrl(string resultsUrl)
        {
        }

        public void WriteLine(string text, Category category)
        {
        }
    }
}
