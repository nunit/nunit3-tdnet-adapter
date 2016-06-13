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

    public class NUnitEngineTestRunner : TDF.ITestRunner
    {
        public TDF.TestRunState RunAssembly(TDF.ITestListener testListener, Assembly assembly)
        {
            using (new NUnitEngineResolver())
            {
                return run(testListener, assembly, null);
            }
        }

        public TDF.TestRunState RunMember(TDF.ITestListener testListener, Assembly assembly, MemberInfo member)
        {
            using (new NUnitEngineResolver())
            {
                string testPath = Utilities.GetTestPath(member);
                return run(testListener, assembly, testPath);
            }
        }

        public TDF.TestRunState RunNamespace(TDF.ITestListener testListener, Assembly assembly, string ns)
        {
            using (new NUnitEngineResolver())
            {
                return run(testListener, assembly, ns);
            }
        }

        TDF.TestRunState run(TDF.ITestListener testListener, Assembly testAssembly, string testPath)
        {
            var eventHandler = new TestEventListener(testListener);

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

                XmlNode result = runner.Run(eventHandler, filter);
            }

            return eventHandler.TestRunState;
        }

        public class TestEventListener : ITestEventListener
        {
            TDF.ITestListener testListener;

            public TDF.TestRunState TestRunState
            {
                get; private set;
            }

            public TestEventListener(TDF.ITestListener testListener)
            {
                this.testListener = testListener;
            }

            public void OnTestEvent(string report)
            {
                if (report.StartsWith("<test-case "))
                {
                    var doc = new XmlDocument();
                    doc.LoadXml(report);
                    var fullname = doc.DocumentElement.GetAttribute("fullname");
                    var methodname = doc.DocumentElement.GetAttribute("methodname");
                    var result = doc.DocumentElement.GetAttribute("result");

                    var testResult = new TDF.TestResult();
                    switch (result)
                    {
                        case "Failed":
                            testResult.State = TDF.TestState.Failed;
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
