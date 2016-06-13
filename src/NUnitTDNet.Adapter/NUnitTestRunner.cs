namespace NUnitTDNet.Adapter
{
    using System;
    using System.Reflection;
    using TestDriven.Framework;

    public class NUnitTestRunner : ITestRunner
    {
        public TestRunState RunAssembly(ITestListener testListener, Assembly assembly)
        {
            throw new NotImplementedException();
        }

        public TestRunState RunMember(ITestListener testListener, Assembly assembly, MemberInfo member)
        {
            throw new NotImplementedException();
        }

        public TestRunState RunNamespace(ITestListener testListener, Assembly assembly, string ns)
        {
            throw new NotImplementedException();
        }
    }
}
