namespace NUnitTDNet.Adapter.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NUnitTDNet.Adapter.Examples;
    using NUnitTDNet.Adapter.Examples.Expected;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using System.Threading;

    [TestClass]
    public class ExpectAttributeExplorer
    {
        public const string XmlFile = "Expect.xml";

        public static TextWriter writer; 

        [AssemblyInitialize]
        public static void FindExpectAttributes(TestContext testContext)
        {
            using (writer = new StreamWriter(XmlFile))
            {
                writer.WriteLine("<Expects>");
                TestAssembly = typeof(ExpectAttribute).Assembly;
                var memberVisitor = new MemberVisitor(visitAttributes);
                memberVisitor.VisitAssembly(TestAssembly);
                writer.WriteLine("</Expects>");
            }
        }

        public static Assembly TestAssembly
        {
            get; private set;
        }

        public static Dictionary<string, MemberInfo> memberDictionary = new Dictionary<string, MemberInfo>();
        public static Dictionary<string, ExpectAttribute> expectAttributeDictionary = new Dictionary<string, ExpectAttribute>();

        static void visitAttributes(MemberInfo member)
        {
            var expectAttributes = member.GetCustomAttributes(typeof(ExpectAttribute), false);
            foreach (ExpectAttribute expectAttribute in expectAttributes)
            {
                string name = GetName(member);
                memberDictionary.Add(name, member);
                expectAttributeDictionary.Add(name, expectAttribute);
                writer.WriteLine(string.Format("    <Expect Name='{0}' />", name));
            }
        }

        public static string GetName(MemberInfo member)
        {
            var declaringType = member.DeclaringType;
            if(declaringType == null)
            {
                return member.ToString();
            }

            return declaringType.FullName + "." + member.ToString();
        }

        public static MemberInfo GetMember(string name)
        {
            return memberDictionary[name];
        }

        public static ExpectAttribute GetExpectAttribute(string name)
        {
            return expectAttributeDictionary[name];
        }

        class MemberVisitor
        {
            public delegate void VisitDelegate(MemberInfo member);

            VisitDelegate visit;

            public MemberVisitor(VisitDelegate visit)
            {
                this.visit = visit;
            }

            public void VisitAssembly(Assembly assembly)
            {
                foreach (Type type in assembly.GetExportedTypes())
                {
                    visitType(type);
                }
            }

            void visitType(Type type)
            {
                foreach (MemberInfo childMember in type.GetMembers())
                {
                    visitMember(childMember);
                }

                visit(type);
            }

            void visitMember(MemberInfo member)
            {
                // already visited
                if (member is Type)
                {
                    return;
                }

                visit(member);
            }
        }
    }
}
