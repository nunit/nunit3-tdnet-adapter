namespace NUnitTDNet.Adapter.Tests
{
    using System;
    using System.Reflection;
    using Fakes;
    using Examples;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Threading;

    [TestClass]
    public class NUnitConsoleTestRunnerTests
    {
        [TestMethod]
        public void RunMember_SomeTestsPass_PassedCount1()
        {
            var testRunner = new NUnitConsoleTestRunner();
            var testListener = new FakeTestListener();
            var testMethod = new ThreadStart(SomeTests.Pass).Method;
            var testAssembly = testMethod.DeclaringType.Assembly;

            testRunner.RunMember(testListener, testAssembly, testMethod);

            Assert.AreEqual(1, testListener.PassedCount, "Check one test passed.");
        }

        [TestMethod]
        public void RunMember_SomeTestsFail_FailedCount1()
        {
            var testRunner = new NUnitConsoleTestRunner();
            var testListener = new FakeTestListener();
            var testMethod = new ThreadStart(SomeTests.Fail).Method;
            var testAssembly = testMethod.DeclaringType.Assembly;

            testRunner.RunMember(testListener, testAssembly, testMethod);

            Assert.AreEqual(1, testListener.FailedCount, "Check one test failed.");
        }
    }
}
