namespace NUnitTDNet.Adapter.Examples.Expected
{
    using NUnit.Framework;
    using NUnitTDNet.Expected;
    using TestDriven.Framework;

    [ExpectTestRun(TestRunState.Failure, FailedCount = 1, PassedCount = 1)]
    public abstract class AbstractTests
    {
        bool pass;

        public AbstractTests(bool pass)
        {
            this.pass = pass;
        }

        [Test]
        public void Test()
        {
            Assert.IsTrue(pass);
        }
    }

    [ExpectTestRun(TestRunState.Success, PassedCount = 1)]
    public class PassConcreteTests : AbstractTests
    {
        public PassConcreteTests() : base(true)
        {
        }
    }

    [ExpectTestRun(TestRunState.Failure, FailedCount = 1)]
    public class FailConcreteTests : AbstractTests
    {
        public FailConcreteTests() : base(false)
        {
        }
    }

    // static classes are also marked as abstract
    [ExpectTestRun(TestRunState.Success, PassedCount = 1)]
    public static class StaticClassTests
    {
        [Test]
        public static void Test()
        {
        }
    }

    [ExpectTestRun(TestRunState.Success, PassedCount = 1)]
    public class OuterClassWithAbstractTests
    {
        public abstract class AbstractTests
        {
            [Test]
            public void Test()
            {
            }
        }

        public class PassConcreteTests : AbstractTests
        {
            public PassConcreteTests()
            {
            }
        }
    }

    [ExpectTestRun(TestRunState.Success, PassedCount = 1)]
    public abstract class AbstractClassWithInnerImplementations
    {
        [Test]
        public void Test()
        {
        }

        public class PassConcreteTests : AbstractClassWithInnerImplementations
        {
        }
    }

    [ExpectTestRun(TestRunState.NoTests)]
    public abstract class AbstractTestsWithNoConcreateImplementation
    {
        [ExpectTestRun(TestRunState.NoTests)]
        [Test]
        public void Test()
        {
        }
    }

    public abstract class AbstractClassTargetMethod
    {
        [ExpectTestRun(TestRunState.Success, PassedCount = 1)]
        [Test]
        public void Test()
        {
        }

        public class PassConcreteTests : AbstractClassTargetMethod
        {
        }
    }
}
