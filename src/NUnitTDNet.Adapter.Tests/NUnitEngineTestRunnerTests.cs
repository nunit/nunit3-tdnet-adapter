namespace NUnitTDNet.Adapter.Tests
{
    using System;
    using System.Reflection;
    using Fakes;
    using Examples;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Threading;
    using TestDriven.Framework;
    [TestClass]
    public class NUnitEngineTestRunnerTests
    {
        ITestRunner testRunner;

        [TestInitialize]
        public void CreateTestRunner()
        {
            testRunner = new NUnitEngineTestRunner();
        }

        [TestMethod]
        public void RunMember_SomeTestsPass_PassedCount1()
        {
            var testListener = new FakeTestListener();
            var testMethod = new ThreadStart(SomeTests.Pass).Method;
            var testAssembly = testMethod.DeclaringType.Assembly;

            testRunner.RunMember(testListener, testAssembly, testMethod);

            Assert.AreEqual(1, testListener.PassedCount, "Check one test passed.");
        }

        [TestMethod]
        public void RunMember_SomeTestsFail_FailedCount1()
        {
            var testListener = new FakeTestListener();
            var testMethod = new ThreadStart(SomeTests.Fail).Method;
            var testAssembly = testMethod.DeclaringType.Assembly;

            testRunner.RunMember(testListener, testAssembly, testMethod);

            Assert.AreEqual(1, testListener.FailedCount, "Check one test failed.");
        }

        [TestMethod]
        public void RunMember_SomeTestsIgnore_IgnoreCount1()
        {
            var testListener = new FakeTestListener();
            var testMethod = new ThreadStart(SomeTests.Ignore).Method;
            var testAssembly = testMethod.DeclaringType.Assembly;

            testRunner.RunMember(testListener, testAssembly, testMethod);

            Assert.AreEqual(1, testListener.IgnoredCount, "Check one test was ignored.");
        }

        [TestMethod]
        public void RunMember_TwoPassingTests_IgnoreCount1()
        {
            var testListener = new FakeTestListener();
            var testClass = typeof(TwoPassingTests);
            var testAssembly = testClass.Assembly;

            testRunner.RunMember(testListener, testAssembly, testClass);

            Assert.AreEqual(2, testListener.PassedCount, "Check both passed.");
        }

        [TestMethod]
        public void RunMember_SomeTestCases_PassAndFail()
        {
            var testListener = new FakeTestListener();
            var type = typeof(SomeTestCases);
            var testMethod = type.GetMethod("PassAndFail");
            var testAssembly = type.Assembly;

            testRunner.RunMember(testListener, testAssembly, testMethod);

            Assert.AreEqual(1, testListener.PassedCount, "Check 1 passed");
            Assert.AreEqual(1, testListener.FailedCount, "Check 1 failed");
        }
    }
}
