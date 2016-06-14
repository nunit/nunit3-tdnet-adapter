namespace NUnitTDNet.Adapter.Tests
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TestDriven.Framework;
    using Fakes;
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

            if (expectAttribute.TestRunState != null)
            {
                string message = string.Format("Checking 'TestRunState' for " + name);
                Assert.AreEqual(expectAttribute.TestRunState, testRunState, message);
            }

            if (expectAttribute.PassedCount >= 0)
            {
                string message = string.Format("Checking 'PassedCount' for " + name);
                Assert.AreEqual(expectAttribute.PassedCount, testListener.PassedCount, message);
            }

            if (expectAttribute.IgnoredCount >= 0)
            {
                string message = string.Format("Checking 'IgnoredCount' for " + name);
                Assert.AreEqual(expectAttribute.IgnoredCount, testListener.IgnoredCount, message);
            }

            if (expectAttribute.FailedCount >= 0)
            {
                string message = string.Format("Checking 'FailedCount' for " + name);
                Assert.AreEqual(expectAttribute.FailedCount, testListener.FailedCount, message);
            }
        }
    }
}
