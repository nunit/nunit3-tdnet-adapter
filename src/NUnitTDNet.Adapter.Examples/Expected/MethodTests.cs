namespace NUnitTDNet.Adapter.Examples.Expected
{
    using NUnit.Framework;
    using NUnitTDNet.Expected;
    using TestDriven.Framework;

    public class MethodTests
    {
        [Test, ExpectTestRun(TestRunState.Success, PassedCount = 1)]
        public void Pass()
        {   
        }

        [Test, ExpectTestRun(TestRunState.Failure, FailedCount = 1)]
        public void Fail()
        {
            Assert.Fail("Boom!");
        }

        [Test, ExpectTestRun(TestRunState.Failure, FailedCount = 1)]
        public void Error()
        {
            throw new System.Exception("Boom!");
        }

        [Test, ExpectTestRun(TestRunState.Success, IgnoredCount = 1)]
        public void AssertIgnore()
        {
            Assert.Ignore();
        }

        [Test, Ignore("Ignore"), ExpectTestRun(TestRunState.Success, IgnoredCount = 1)]
        public void AttributeIgnore()
        {
        }
    }
}
