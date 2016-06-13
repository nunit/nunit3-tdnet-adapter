namespace NUnitTDNet.Adapter.Tests
{
    using System;
    using Fakes;
    using Examples;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class NUnitTestRunnerTests
    {
        [TestMethod]
        public void RunMember_OnePassingTest_PassedCount1()
        {
            var testRunner = new NUnitTestRunner();
            var testListener = new FakeTestListener();
            var testClass = typeof(OnePassingTest);

            testRunner.RunMember(testListener, testClass.Assembly, testClass);

            Assert.AreEqual(1, testListener.PassedCount, "Check one test passed.");
        }
    }
}
