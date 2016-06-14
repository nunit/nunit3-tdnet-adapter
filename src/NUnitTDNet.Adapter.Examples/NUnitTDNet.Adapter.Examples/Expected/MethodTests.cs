namespace NUnitTDNet.Adapter.Examples.Expected
{
    using NUnit.Framework;
    using TestDriven.Framework;

    public class MethodTests
    {
        [Test, Expect(TestRunState.Success, PassedCount = 1)]
        public void Pass()
        {
        }

        [Test, Expect(TestRunState.Failure, FailedCount = 1)]
        public void Fail()
        {
            Assert.Fail("Boom!");
        }

        [Test, Expect(TestRunState.Success, IgnoredCount = 1)]
        public void AssertIgnore()
        {
            Assert.Ignore();
        }

        [Test, Ignore("Ignore"), Expect(TestRunState.Success, IgnoredCount = 1)]
        public void AttributeIgnore()
        {
        }
    }
}
