using System;
using System.Reflection;
using TestDriven.Framework;

namespace NUnitTDNet.Adapter
{
    // Fix for NUnit3TestAdapter copying `nunit.engine` and `nunit.engine.api`
    // into the output directory. Move them out of the way while TestDriven.NET
    // is executing tests. See:
    // https://github.com/jcansdale/TestDriven.Net-Issues/issues/153
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
