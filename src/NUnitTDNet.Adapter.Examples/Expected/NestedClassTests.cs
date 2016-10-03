namespace NUnitTDNet.Adapter.Examples.Expected
{
    using NUnit.Framework;
    using NUnitTDNet.Expected;
    using TestDriven.Framework;

    [ExpectTestRun(TestRunState.Success, PassedCount = 1)]
    public class NestedClassTests
    {
        [ExpectTestRun(TestRunState.Success, PassedCount = 1)]
        public class NestedClass
        {
            [Test, ExpectTestRun(TestRunState.Success, PassedCount = 1)]
            public void Pass()
            {
            }
        }
    }
}
