namespace NUnitTDNet.Expected
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Collections;

    public class ExpectAttributeExplorer : IEnumerable<ExpectEntry>
    {
        IEnumerable<ExpectEntry> expectEntries;

        public ExpectAttributeExplorer(Assembly testAssembly)
        {
            expectEntries = getExpectEntries(testAssembly);
        }

        public IEnumerator<ExpectEntry> GetEnumerator()
        {
            return expectEntries.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return expectEntries.GetEnumerator();
        }

        public ExpectEntry FindExpectEntry(string name)
        {
            foreach(var expectEntry in this)
            {
                if(expectEntry.Name == name)
                {
                    return expectEntry;
                }
            }

            return null;
        }

        static IEnumerable<ExpectEntry> getExpectEntries(Assembly testAssembly)
        {
            var expectEntryList = new List<ExpectEntry>();
            var memberVisitor = new MemberVisitor((MemberInfo member) =>
            {
                var expectAttributes = member.GetCustomAttributes(typeof(ExpectAttribute), false);
                foreach (ExpectAttribute expectAttribute in expectAttributes)
                {
                    string name = GetName(member);
                    var expectEntry = new ExpectEntry(name, testAssembly, member, expectAttribute);
                    expectEntryList.Add(expectEntry);
                }
            });
            memberVisitor.VisitAssembly(testAssembly);
            return expectEntryList;
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
