namespace NUnitTDNet.Adapter
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    class Utilities
    {
        public static string[] GetTestPaths(MemberInfo member)
        {
            if (member is Type)
            {
                var targetType = (Type)member;
                var types = includeNestedTypes(targetType);
                var testPathList = new List<string>();
                foreach (var type in types)
                {
                    testPathList.Add(type.FullName);
                }

                return testPathList.ToArray();
            }

            if (member is MethodInfo)
            {
                MethodInfo methodInfo = (MethodInfo)member;
                var testPath = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;
                var testPaths = new string[] { testPath };
                return testPaths;
            }

            throw new Exception("Member type not supported: " + member.GetType());
        }

        static Type[] includeNestedTypes(Type type)
        {
            var types = new List<Type>();
            includeNestedTypes(types, type);
            return types.ToArray();
        }

        static void includeNestedTypes(List<Type> types, Type type)
        {
            types.Add(type);
            foreach (var nestedType in type.GetNestedTypes())
            {
                includeNestedTypes(types, nestedType);
            }
        }
    }
}
