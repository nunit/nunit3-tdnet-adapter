namespace NUnitTDNet.Adapter.Tests.Expect
{
    using Examples.Expected;
    using System.Reflection;

    public class ExpectEntry
    {
        public string Name
        {
            get; private set;
        }

        public Assembly TestAssembly
        {
            get; private set;
        }

        public MemberInfo Member
        {
            get; private set;
        }

        public ExpectAttribute ExpectAttribute
        {
            get; private set;
        }

        public ExpectEntry(string name, Assembly testAssembly, MemberInfo member, ExpectAttribute expectAttribute)
        {
            Name = name;
            TestAssembly = testAssembly;
            Member = member;
            ExpectAttribute = expectAttribute;
        }
    }
}
