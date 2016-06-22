namespace NUnitTDNet.Adapter.Examples.Expected
{
    using NUnit.Framework;
    using TestDriven.Framework;

    [ExpectTestRun(TestRunState.Success, PassedCount = 1, IgnoredCount = 1)]
    public class ExplicitTests
    {
        [ExpectTestRun(TestRunState.Failure, FailedCount = 1)]
        [Test, Explicit]
        public void ExplicitTest()
        {
            Assert.Fail();
        }

        [Test]
        public void Test() { }
    }

    namespace ExplicitNamespace
    {
        [ExpectTestRun(TestRunState.Success, Namespace = true, PassedCount = 1, IgnoredCount = 1)]
        public class NS { }

        [ExpectTestRun(TestRunState.Success, PassedCount = 1)]
        [Explicit, TestFixture]
        public class ExplicitFixture
        {
            [Test, ExpectTestRun(TestRunState.Success, PassedCount = 1)]
            public void Test()
            {
            }
        }

        public class NormalTest
        {
            [Test]
            public void Test()
            {
            }
        }
    }
}
