namespace NUnitTDNet.Adapter.Tests.Fakes
{
    using System;
    using System.Collections.Generic;
    using TestDriven.Framework;

    public class FakeTestListener : ITestListener
    {
        public FakeTestListener()
        {
            OutputLines = new List<Tuple<string, Category>>();
        }

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
            var line = new Tuple<string, Category>(text, category);
            OutputLines.Add(line);
        }

        public List<Tuple<string, Category>> OutputLines
        {
            get; private set;
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
