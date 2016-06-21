using NUnit.Framework;
using NUnitTDNet.Adapter.Examples.Expected;

namespace NUnitTDNet.Adapter.Examples.Expected
{
    public class TestNames
    {
        const string ClassName = "NUnitTDNet.Adapter.Examples.Expected.TestNames";

        [Test]
        [ExpectTest(ClassName + ".TestName")]
        public void TestName()
        {
        }

        [TestCase(666)]
        [ExpectTest(ClassName + ".TestCase(666)")]
        public void TestCase(int i)
        {
        }

        public class InnerClass
        {
            [Test]
            [ExpectTest(ClassName + "+InnerClass.TestName")]
            public void TestName()
            {
            }
        }
    }
}

public class LooseClass
{
    [Test]
    [ExpectTest("LooseClass.TestName")]
    public void TestName()
    {
    }
}
