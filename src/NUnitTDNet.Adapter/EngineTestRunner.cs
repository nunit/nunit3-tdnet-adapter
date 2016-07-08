namespace NUnitTDNet.Adapter
{
    // We use an alias so that we don't accidentally make
    // references to engine internals, except for creating
    // the engine object in the CreateTestEngine method.
    extern alias ENG;
    using TestEngineClass = ENG::NUnit.Engine.TestEngine;

    using NUnit.Engine;
    using System;
    using System.Reflection;
    using System.Xml;
    using TDF = TestDriven.Framework;

    /// <summary>
    /// Execute tests using a private copy of the 'nunit.engine' assembly. Tests will
    /// be executed in a process and app domain created by TestDriven.Net.
    /// </summary>
    public class EngineTestRunner : TDF.ITestRunner
    {
        public TDF.TestRunState RunAssembly(TDF.ITestListener testListener, Assembly assembly)
        {
            var testPath = new Uri(assembly.EscapedCodeBase).LocalPath;
            return run(testListener, assembly, testPath);
        }

        public TDF.TestRunState RunMember(TDF.ITestListener testListener, Assembly assembly, MemberInfo member)
        {
            string testPath = Utilities.GetTestPath(member);
            return run(testListener, assembly, testPath);
        }

        public TDF.TestRunState RunNamespace(TDF.ITestListener testListener, Assembly assembly, string ns)
        {
            var testPath = ns;
            if(string.IsNullOrEmpty(ns))
            {
                testPath = new Uri(assembly.EscapedCodeBase).LocalPath;
            }

            return run(testListener, assembly, testPath);
        }

        TDF.TestRunState run(TDF.ITestListener testListener, Assembly testAssembly, string testPath)
        {
            using (var engine = new TestEngineClass())
            {
                string assemblyFile = new Uri(testAssembly.EscapedCodeBase).LocalPath;
                TestPackage package = new TestPackage(assemblyFile);

                package.AddSetting("ProcessModel", "InProcess");
                package.AddSetting("DomainUsage", "None");

                ITestRunner runner = engine.GetRunner(package);

                var filterService = engine.Services.GetService<ITestFilterService>();
                ITestFilterBuilder builder = filterService.GetTestFilterBuilder();
                builder.AddTest(testPath);

                var filter = builder.GetFilter();

                var totalTests = runner.CountTestCases(filter);
                if (totalTests == 0)
                {
                    return TDF.TestRunState.NoTests;
                }

                var testRunnerName = getTestRunnerName(testAssembly);
                var eventHandler = new TestEventListener(testListener, totalTests, testRunnerName);

                XmlNode result = runner.Run(eventHandler, filter);
                return eventHandler.TestRunState;
            }
        }

        static string getTestRunnerName(Assembly testRunnerName)
        {
            foreach(var assemblyName in testRunnerName.GetReferencedAssemblies())
            {
                if (assemblyName.Name == "nunit.framework")
                {
                    return getTestRunnerName(assemblyName);
                }
            }

            return "NUnit - Unknown Version";
        }

        static string getTestRunnerName(AssemblyName assemblyName)
        {
            var version = assemblyName.Version;
            return string.Format("NUnit {0}.{1}.{2}", version.Major, version.Minor, version.MajorRevision);
        }


        public class TestEventListener : ITestEventListener
        {
            TDF.ITestListener testListener;
            int totalTests;
            string testRunnerName;

            public TDF.TestRunState TestRunState
            {
                get; private set;
            }

            public TestEventListener(TDF.ITestListener testListener, int totalTests, string testRunnerName)
            {
                this.testListener = testListener;
                this.totalTests = totalTests;
                this.testRunnerName = testRunnerName;
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
                    testResult.TestRunnerName = testRunnerName;

                    testResult.Name = element.GetAttribute("fullname");

                    var message = element.SelectSingleNode("//message");
                    if (message != null)
                    {
                        var text = trimNewLine(message.InnerText);
                        testResult.Message = text;
                    }

                    var stackTrace = element.SelectSingleNode("//stack-trace");
                    if (stackTrace != null)
                    {
                        testResult.StackTrace = stackTrace.InnerText;
                    }

                    var output = element.SelectSingleNode("//output");
                    if (output != null)
                    {
                        var text = trimNewLine(output.InnerText);
                        testListener.WriteLine(text, TDF.Category.Output);
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
                        case "Inconclusive":
                            testResult.State = TDF.TestState.Ignored;
                            break;
                        default:
                            testListener.WriteLine("Unknown 'result': " + result + "\n" + report, TDF.Category.Error);
                            testResult.State = TDF.TestState.Failed;
                            break;
                    }

                    testListener.TestFinished(testResult);
                }
            }

            static string trimNewLine(string text)
            {
                var newLine = Environment.NewLine;
                if (text.EndsWith(newLine))
                {
                    text = text.Substring(0, text.Length - newLine.Length);
                }

                return text;
            }
        }
    }
}
