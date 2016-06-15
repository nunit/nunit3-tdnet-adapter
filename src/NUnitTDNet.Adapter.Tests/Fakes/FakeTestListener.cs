namespace NUnitTDNet.Adapter.Tests.Fakes
{
    using System;
    using System.Collections.Generic;
    using TestDriven.Framework;

    class FakeTestListener : ITestListener
    {
        Dictionary<string, TestResult> testResultDictionary = new Dictionary<string, TestResult>();

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
            if (summary.Name != null)
            {
                testResultDictionary[summary.Name] = summary;
            }

            switch (summary.State)
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

        public TestResult GetTestResult(string name)
        {
            TestResult testResult;
            testResultDictionary.TryGetValue(name, out testResult);
            return testResult;
        }

        public ICollection<string> GetTestNames()
        {
            return testResultDictionary.Keys;
        }
    }
}
