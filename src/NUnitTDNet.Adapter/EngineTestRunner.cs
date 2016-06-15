namespace NUnitTDNet.Adapter
{
    using NUnit.Common;
    using NUnit.Engine;
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Xml;
    using TDF = TestDriven.Framework;

    public class EngineTestRunner : TDF.ITestRunner
    {
        public TDF.TestRunState RunAssembly(TDF.ITestListener testListener, Assembly assembly)
        {
            using (new EngineResolver())
            {
                return run(testListener, assembly, null);
            }
        }

        public TDF.TestRunState RunMember(TDF.ITestListener testListener, Assembly assembly, MemberInfo member)
        {
            using (new EngineResolver())
            {
                string testPath = Utilities.GetTestPath(member);
                return run(testListener, assembly, testPath);
            }
        }

        public TDF.TestRunState RunNamespace(TDF.ITestListener testListener, Assembly assembly, string ns)
        {
            using (new EngineResolver())
            {
                return run(testListener, assembly, ns);
            }
        }

        TDF.TestRunState run(TDF.ITestListener testListener, Assembly testAssembly, string testPath)
        {
            // Can't find find TestEngine when not on AppDomain.BaseDirectory or registry.
            //using (ITestEngine engine = TestEngineActivator.CreateInstance(false))

            using (ITestEngine engine = new TestEngine())
            {
                string assemblyFile = new Uri(testAssembly.EscapedCodeBase).LocalPath;

                TestPackage package = new TestPackage(assemblyFile);
                package.AddSetting(PackageSettings.ProcessModel, "InProcess");
                package.AddSetting(PackageSettings.DomainUsage, "None");

                ITestRunner runner = engine.GetRunner(package);

                var filterService = engine.Services.GetService<ITestFilterService>();
                ITestFilterBuilder builder = filterService.GetTestFilterBuilder();
                builder.AddTest(testPath);
                var filter = builder.GetFilter();

                var totalTests = runner.CountTestCases(filter);
                if(totalTests == 0)
                {
                    return TDF.TestRunState.NoTests;
                }

                var eventHandler = new TestEventListener(testListener, totalTests);

                XmlNode result = runner.Run(eventHandler, filter);
                return eventHandler.TestRunState;
            }
        }

        public class TestEventListener : ITestEventListener
        {
            TDF.ITestListener testListener;
            int totalTests;

            public TDF.TestRunState TestRunState
            {
                get; private set;
            }

            public TestEventListener(TDF.ITestListener testListener, int totalTests)
            {
                this.testListener = testListener;
                this.totalTests = totalTests;
                TestRunState = TDF.TestRunState.Success;
            }

            public void OnTestEvent(string report)
            {
                var doc = new XmlDocument();
                doc.LoadXml(report);
                var element = doc.DocumentElement;

                if (element != null && element.Name == "test-case")
                {
                    var testCaseElement = doc.DocumentElement;

                    var testResult = new TDF.TestResult();
                    testResult.TotalTests = totalTests;

                    testResult.Name = element.GetAttribute("fullname");

                    var message = element.SelectSingleNode("//message");
                    if (message != null)
                    {
                        testResult.Message = message.InnerText;
                    }

                    var stackTrace = element.SelectSingleNode("//stack-trace");
                    if (stackTrace != null)
                    {
                        testResult.StackTrace = stackTrace.InnerText;
                    }

                    var result = element.GetAttribute("result");
                    switch (result)
                    {
                        case "Failed":
                            testResult.State = TDF.TestState.Failed;
                            TestRunState = TDF.TestRunState.Failure;
                            break;
                        case "Passed":
                            testResult.State = TDF.TestState.Passed;
                            break;
                        case "Skipped":
                            testResult.State = TDF.TestState.Ignored;
                            break;
                        default:
                            // what else?
                            Console.WriteLine(report);
                            testResult.State = TDF.TestState.Failed;
                            break;
                    }
                    testListener.TestFinished(testResult);
                }
            }
        }
    }
}
