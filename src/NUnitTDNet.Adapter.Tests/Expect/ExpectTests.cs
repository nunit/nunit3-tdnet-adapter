namespace NUnitTDNet.Adapter.Tests
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TestDriven.Framework;
    using Fakes;
    using Examples.Expected;

    [TestClass]
    public class ExpectTests
    {
        ITestRunner testRunner;

        [TestInitialize]
        public void CreateTestRunner()
        {
            testRunner = new NUnitEngineTestRunner();
        }

        public TestContext TestContext
        {
            get; set;
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
                   "|DataDirectory|\\" + ExpectAttributeExplorer.XmlFile,
                   "Expect", DataAccessMethod.Sequential)]
        public void ExpectAttributeBasedAssertions()
        {
            var name = (string)TestContext.DataRow["Name"];
            var testAssembly = ExpectAttributeExplorer.TestAssembly;
            var member = ExpectAttributeExplorer.GetMember(name);
            var expectAttribute = ExpectAttributeExplorer.GetExpectAttribute(name);

            var testListener = new FakeTestListener();
            var testRunState = testRunner.RunMember(testListener, testAssembly, member);

            if (expectAttribute is ExpectTestRunAttribute)
            {
                // Checks for all tests.
                var expectTestRunAttribute = (ExpectTestRunAttribute)expectAttribute;
                {
                    string message = string.Format("Checking 'TestRunState' for " + name);
                    Assert.AreEqual(expectTestRunAttribute.TestRunState, testRunState, message);
                }

                if (expectTestRunAttribute.PassedCount >= 0)
                {
                    string message = string.Format("Checking 'PassedCount' for " + name);
                    Assert.AreEqual(expectTestRunAttribute.PassedCount, testListener.PassedCount, message);
                }

                if (expectTestRunAttribute.IgnoredCount >= 0)
                {
                    string message = string.Format("Checking 'IgnoredCount' for " + name);
                    Assert.AreEqual(expectTestRunAttribute.IgnoredCount, testListener.IgnoredCount, message);
                }

                if (expectTestRunAttribute.FailedCount >= 0)
                {
                    string message = string.Format("Checking 'FailedCount' for " + name);
                    Assert.AreEqual(expectTestRunAttribute.FailedCount, testListener.FailedCount, message);
                }
            }

            if (expectAttribute is ExpectTestAttribute)
            {
                // Checks for specific test.
                var expectTestAttribute = (ExpectTestAttribute)expectAttribute;

                string expectName = expectTestAttribute.Name;
                var summary = testListener.GetTestResult(expectName);
                if (summary == null)
                {
                    foreach (string testName in testListener.GetTestNames())
                    {
                        Console.WriteLine(testName);
                    }

                    string message = string.Format("Looking up test with name: " + expectName);
                    Assert.IsNotNull(summary, message);
                }

            }
        }
    }
}
