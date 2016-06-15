namespace NUnitTDNet.Adapter.Examples.Expected
{
    using NUnit.Framework;
    using TestDriven.Framework;

    [ExpectTestRun(TestRunState.Failure, FailedCount = 1)]
    public class FailClass
    {
        [Test]
        public void Fail()
        {
            Assert.Fail("Boom!");
        }
    }

    [ExpectTestRun(TestRunState.Success, PassedCount = 1)]
    public class PassClass
    {
        [Test]
        public void Pass()
        {
        }
    }

    [ExpectTestRun(TestRunState.Failure, PassedCount = 1, FailedCount = 1)]
    public class PassFailClass
    {
        [Test]
        public void Pass()
        {
        }

        [Test]
        public void Fail()
        {
            Assert.Fail("Boom!");
        }
    }

    [ExpectTestRun(TestRunState.Success, IgnoredCount = 1)]
    public class AssertIgnoreClass
    {
        [Test]
        public void Ignore()
        {
            Assert.Ignore();
        }
    }

    [ExpectTestRun(TestRunState.Success, IgnoredCount = 1)]
    public class AttributeIgnoreClass
    {
        [Test, Ignore("Ignore me!")]
        public void Ignore()
        {
        }
    }
}
