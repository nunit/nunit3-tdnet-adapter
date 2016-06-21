namespace NUnitTDNet.Adapter.Examples.Expected
{
    using NUnit.Framework;
    using TestDriven.Framework;

    // Should we attempt to run nested classes?
    [ExpectTestRun(TestRunState.NoTests)]
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
