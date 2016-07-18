namespace NUnitTDNet.Adapter
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using TestDriven.Framework;

    /// <summary>
    /// This is a sample test runner that executes tests using the NUnit console application.
    /// It was implemented as a convenient way to compare tests run with the NUnit console
    /// to those run with TestDriven.Net. It isn't meant for production use.
    /// </summary>
    public class ConsoleTestRunner : ITestRunner
    {
        public TestRunState RunAssembly(ITestListener testListener, Assembly assembly)
        {
            return executeConsoleRunner(testListener, assembly, null);
        }

        public TestRunState RunMember(ITestListener testListener, Assembly assembly, MemberInfo member)
        {
            var where = Utilities.GetWhereForTarget(assembly, member);
            if(string.IsNullOrEmpty(where))
            {
                return TestRunState.NoTests;
            }

            return executeConsoleRunner(testListener, assembly, where);
        }

        public TestRunState RunNamespace(ITestListener testListener, Assembly assembly, string ns)
        {
            var where = Utilities.GetWhereForTarget(assembly, ns);
            return executeConsoleRunner(testListener, assembly, where);
        }

        TestRunState executeConsoleRunner(ITestListener testListener, Assembly testAssembly, string where)
        {
            var exeFile = findConsoleRunner();
            if(exeFile == null)
            {
                throw new Exception("Couldn't find NUnit.ConsoleRunner in NuGet packages.");
            }

            string assemblyFile = new Uri(testAssembly.EscapedCodeBase).LocalPath;
            string arguments = quote(assemblyFile);
            if(!string.IsNullOrEmpty(where))
            {
                arguments += " --where=" + quote(where);
            }

            arguments += " --process:InProcess";

            var startInfo = new ProcessStartInfo(exeFile, arguments);
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            startInfo.RedirectStandardOutput = true;
            var process = Process.Start(startInfo);
            process.WaitForExit();

            string stdout = process.StandardOutput.ReadToEnd();
            testListener.WriteLine(stdout, Category.Output);

            var testResult = new TestResult();
            switch(process.ExitCode)
            {
                case 0:
                    testResult.State = TestState.Passed;
                    testListener.TestFinished(testResult);
                    return TestRunState.Success;
                default:
                    testResult.State = TestState.Failed;
                    testListener.TestFinished(testResult);
                    return TestRunState.Failure;
            }
        }

        static string quote(string text)
        {
            return '"' + text + '"';
        }

        static string findConsoleRunner()
        {
            var packagesDir = findPackagesDirectory();
            if(packagesDir == null)
            {
                return null;
            }

            var dirs = Directory.GetDirectories(packagesDir, "NUnit.ConsoleRunner.*");
            foreach(string dir in dirs)
            {
                var consoleRunnerExe = Path.Combine(dir, @"tools\nunit3-console.exe");
                if(File.Exists(consoleRunnerExe))
                {
                    return consoleRunnerExe;
                }
            }

            return null;
        }

        static string findPackagesDirectory()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var dir = new Uri(assembly.EscapedCodeBase).LocalPath;

            while(true)
            {
                var packagesDir = Path.Combine(dir, "packages");
                if(Directory.Exists(packagesDir))
                {
                    return packagesDir;
                }

                dir = Path.GetDirectoryName(dir);
                if(dir == null)
                {
                    return null;
                }
            }
        }
    }
}
