namespace NUnitTDNet.Adapter.Tests
{
    using System;
    using System.Reflection;
    using Fakes;
    using Examples;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Threading;
    using TestDriven.Framework;
    using System.IO;

    [TestClass]
    public class EngineTestRunnerTests
    {
        static string tempDir;

        // Create a temp direcoty and clean up after previous run.
        // Files will be locked when compiled assemblies are loaded.
        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            tempDir = getUniqueDirectoryForType(typeof(EngineTestRunnerTests));
            Directory.CreateDirectory(tempDir);
            foreach(var file in Directory.GetFiles(tempDir))
            {
                try
                {
                    File.Delete(file);
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        static string getUniqueDirectoryForType(Type type)
        {
            var localPath = new Uri(type.Assembly.EscapedCodeBase).LocalPath;
            var dir = Path.GetDirectoryName(localPath);
            return Path.Combine(dir, type.FullName);
        }

        [TestMethod]
        public void RunMember_ReferencesNUnitFramework_CheckTestRunnerName()
        {
            var assemblyName = AssemblyName.GetAssemblyName("nunit.framework.dll");
            var expectedTestRunnerName = getFriendlyName(assemblyName);
            var testMethod = new ThreadStart(SomeTests.Pass).Method;
            var testAssembly = testMethod.DeclaringType.Assembly;
            var testName = testMethod.DeclaringType.FullName + "." + testMethod.Name;
            var testListener = new FakeTestListener();
            var testRunner = createTestRunner();

            testRunner.RunMember(testListener, testAssembly, testMethod);

            var testResult = testListener.GetTestResult(testName);
            Assert.AreEqual(expectedTestRunnerName, testResult.TestRunnerName, "Check TestRunnerName.");
            
        }

        static string getFriendlyName(AssemblyName assemblyName)
        {
            var version = assemblyName.Version;
            return string.Format("NUnit {0}.{1}.{2}", version.Major, version.Minor, version.MajorRevision);
        }

        [TestMethod]
        public void RunMember_SomeTestsPass_PassedCount1()
        {
            var testListener = new FakeTestListener();
            var testMethod = new ThreadStart(SomeTests.Pass).Method;
            var testAssembly = testMethod.DeclaringType.Assembly;
            var testRunner = createTestRunner();

            testRunner.RunMember(testListener, testAssembly, testMethod);

            Assert.AreEqual(1, testListener.PassedCount, "Check one test passed.");
        }

        [TestMethod]
        public void RunMember_SomeTestsFail_FailedCount1()
        {
            var testListener = new FakeTestListener();
            var testMethod = new ThreadStart(SomeTests.Fail).Method;
            var testAssembly = testMethod.DeclaringType.Assembly;
            var testRunner = createTestRunner();

            testRunner.RunMember(testListener, testAssembly, testMethod);

            Assert.AreEqual(1, testListener.FailedCount, "Check one test failed.");
        }

        [TestMethod]
        public void RunMember_SomeTestsIgnore_IgnoreCount1()
        {
            var testListener = new FakeTestListener();
            var testMethod = new ThreadStart(SomeTests.Ignore).Method;
            var testAssembly = testMethod.DeclaringType.Assembly;
            var testRunner = createTestRunner();

            testRunner.RunMember(testListener, testAssembly, testMethod);

            Assert.AreEqual(1, testListener.IgnoredCount, "Check one test was ignored.");
        }

        [TestMethod]
        public void RunMember_TwoPassingTests_IgnoreCount1()
        {
            var testListener = new FakeTestListener();
            var testClass = typeof(TwoPassingTests);
            var testAssembly = testClass.Assembly;
            var testRunner = createTestRunner();

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
            var testRunner = createTestRunner();

            testRunner.RunMember(testListener, testAssembly, testMethod);

            Assert.AreEqual(1, testListener.PassedCount, "Check 1 passed");
            Assert.AreEqual(1, testListener.FailedCount, "Check 1 failed");
        }

        [TestMethod]
        public void RunAssembly_WithNoTests_NoTests()
        {
            var testListener = new FakeTestListener();
            var testFile = Path.Combine(tempDir, "no-tests.dll");
            var assemblyReferences = new[] { "nunit.framework.dll" };
            var source = "class NoTests {}";
            var assemblyFile = CompilerUtilities.Compile(testFile, assemblyReferences, source);
            var testAssembly = Assembly.LoadFrom(testFile);
            var testRunner = createTestRunner();

            var state = testRunner.RunAssembly(testListener, testAssembly);

            Assert.AreEqual(TestRunState.NoTests, state, "check no tests were found");
        }

        [TestMethod]
        public void RunAssembly_WithOneTest_RanOneTest()
        {
            var testListener = new FakeTestListener();
            var testFile = Path.Combine(tempDir, "one-test.dll");
            var assemblyReferences = new[] { "nunit.framework.dll" };
            var expectedName = "TestClass.TestMethod";
            var source = @"
using NUnit.Framework;
public class TestClass
{
    [Test]
    public void TestMethod() {}
}";
            var assemblyFile = CompilerUtilities.Compile(testFile, assemblyReferences, source);
            var testAssembly = Assembly.LoadFrom(testFile);
            var testRunner = createTestRunner();

            var state = testRunner.RunAssembly(testListener, testAssembly);

            var testNames = testListener.GetTestNames();
            Assert.AreEqual(1, testNames.Count, "Check 1 ran.");
            Assert.IsTrue(testNames.Contains(expectedName), "Check for test name: " + expectedName);
            Assert.AreEqual(TestRunState.Success, state, "Check for success.");
        }

        [TestMethod]
        public void RunNamespace_EmptyNamespace_RanOneTest()
        {
            var testListener = new FakeTestListener();
            var testFile = Path.Combine(tempDir, "empty-namespace.dll");
            var assemblyReferences = new[] { "nunit.framework.dll" };
            var expectedName = "TestClass.TestMethod";
            var source = @"
using NUnit.Framework;
public class TestClass
{
    [Test]
    public void TestMethod() {}
}";
            var assemblyFile = CompilerUtilities.Compile(testFile, assemblyReferences, source);
            var testAssembly = Assembly.LoadFrom(testFile);
            var testRunner = createTestRunner();

            var state = testRunner.RunNamespace(testListener, testAssembly, "");

            var testNames = testListener.GetTestNames();
            Assert.AreEqual(1, testNames.Count, "Check 1 ran.");
            Assert.IsTrue(testNames.Contains(expectedName), "Check for test name: " + expectedName);
            Assert.AreEqual(TestRunState.Success, state, "Check for success.");
        }

        [TestMethod]
        public void RunNamespace_TargetNamespace_RanOneTest()
        {
            var testListener = new FakeTestListener();
            var testFile = Path.Combine(tempDir, "target-namespace.dll");
            var assemblyReferences = new[] { "nunit.framework.dll" };
            var ns = "TargetNamespace";
            var expectedName = ns + ".TestClass.TestMethod";
            var source = @"
using NUnit.Framework;
namespace {ns}
{
    public class TestClass
    {
        [Test]
        public void TestMethod() { }
    }
}

public class OutsideNamespace
{
    [Test]
    public void Test() { }
}
".Replace("{ns}", ns);
            var assemblyFile = CompilerUtilities.Compile(testFile, assemblyReferences, source);
            var testAssembly = Assembly.LoadFrom(testFile);
            var testRunner = createTestRunner();

            var state = testRunner.RunNamespace(testListener, testAssembly, ns);

            var testNames = testListener.GetTestNames();
            Assert.AreEqual(1, testNames.Count, "Check 1 ran.");
            Assert.IsTrue(testNames.Contains(expectedName), "Check for test name: " + expectedName);
            Assert.AreEqual(TestRunState.Success, state, "Check for success.");
        }

        ITestRunner createTestRunner()
        {
            return new EngineTestRunner();
        }
    }
}
