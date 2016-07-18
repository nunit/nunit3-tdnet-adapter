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

        static void Main()
        {
            new ExpectXmlWriter().Write(XmlFile);
        }

        class ExpectXmlWriter
        {
            public TextWriter writer;

            public void Write(string xmlFile)
            {
                using (writer = new StreamWriter(XmlFile))
                {
                    writer.WriteLine("<Expects>");
                    TestAssembly = typeof(ExpectAttribute).Assembly;
                    var memberVisitor = new MemberVisitor(visitor);
                    memberVisitor.VisitAssembly(TestAssembly);
                    writer.WriteLine("</Expects>");
                }
            }

            void visitor(MemberInfo member)
            {
                var expectAttributes = member.GetCustomAttributes(typeof(ExpectAttribute), false);
                foreach (ExpectAttribute expectAttribute in expectAttributes)
                {
                    string name = GetName(member);
                    writer.WriteLine(string.Format("    <Expect Name='{0}' />", name));
                }
            }
        }

        [AssemblyInitialize]
        public static void FindExpectAttributes(TestContext testContext)
        {
            TestAssembly = typeof(ExpectAttribute).Assembly;
            var memberVisitor = new MemberVisitor(visitAttributes);
            memberVisitor.VisitAssembly(TestAssembly);
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
            }
        }

        public static string GetName(MemberInfo member)
        {
            var reflectedType = member.ReflectedType;
            if (reflectedType == null)
            {
                return member.ToString();
            }

            return reflectedType.FullName + "." + member.ToString();
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
                foreach (Type type in assembly.GetTypes())
                {
                    visitType(type);
                }
            }

            void visitType(Type type)
            {
                var bindingFlags = BindingFlags.DeclaredOnly; // only include directly targeted members
                bindingFlags |=  BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
                foreach (MemberInfo childMember in type.GetMembers(bindingFlags))
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
