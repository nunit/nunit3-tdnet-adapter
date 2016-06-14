namespace NUnitTDNet.Adapter.Examples.Expected
{
    using NUnit.Framework;
    using TestDriven.Framework;

    // All test methods with same name will be run.
    // Hopefully this won't be too confusing!

    [Expect(TestRunState.Success, PassedCount = 3)]
    public class OverloadedMethodTests
    {
        [TestCase(1)]
        [Expect(TestRunState.Success, PassedCount = 3)]
        public void Overloaded(int i)
        {
        }

        [TestCase("foo")]
        [Expect(TestRunState.Success, PassedCount = 3)]
        public void Overloaded(string text)
        {
        }

        [Test]
        [Expect(TestRunState.Success, PassedCount = 3)]
        public void Overloaded()
        {
        }
    }
}
