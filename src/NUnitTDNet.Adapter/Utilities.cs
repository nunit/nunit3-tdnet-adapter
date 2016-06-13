namespace NUnitTDNet.Adapter
{
    using System;
    using System.Reflection;

    class Utilities
    {
        public static string GetTestPath(MemberInfo member)
        {
            if (member is Type)
            {
                Type type = (Type)member;
                return type.FullName;
            }

            if (member is MethodInfo)
            {
                MethodInfo methodInfo = (MethodInfo)member;
                return methodInfo.DeclaringType.FullName + "." + methodInfo.Name;
            }

            throw new Exception("Member type not supported: " + member.GetType());
        }
    }
}
