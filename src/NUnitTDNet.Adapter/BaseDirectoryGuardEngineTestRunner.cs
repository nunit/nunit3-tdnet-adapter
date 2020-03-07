using System;
using System.Reflection;
using TestDriven.Framework;

namespace NUnitTDNet.Adapter
{
    public class BaseDirectoryGuardEngineTestRunner : ITestRunner
    {
        readonly static string[] dependencies = new[]
        {
            "nunit.engine.dll",
            "nunit.engine.api.dll"
        };

        public TestRunState RunAssembly(ITestListener testListener, Assembly assembly)
        {
            using (CreateBaseDirectoryGuard())
            {
                return new EngineTestRunner().RunAssembly(testListener, assembly);
            }
        }

        public TestRunState RunMember(ITestListener testListener, Assembly assembly, MemberInfo member)
        {
            using (CreateBaseDirectoryGuard())
            {
                return new EngineTestRunner().RunMember(testListener, assembly, member);
            }
        }

        public TestRunState RunNamespace(ITestListener testListener, Assembly assembly, string ns)
        {
            using (CreateBaseDirectoryGuard())
            {
                return new EngineTestRunner().RunNamespace(testListener, assembly, ns);
            }
        }

        static BaseDirectoryGuard CreateBaseDirectoryGuard()
        {
            return new BaseDirectoryGuard(AppDomain.CurrentDomain.BaseDirectory, dependencies);
        }
    }
}
