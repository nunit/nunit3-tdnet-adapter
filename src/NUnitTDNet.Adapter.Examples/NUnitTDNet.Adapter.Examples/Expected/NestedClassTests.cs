namespace NUnitTDNet.Adapter.Examples.Expected
{
    using NUnit.Framework;
    using TestDriven.Framework;

    // Should we attempt to run nested classes?
    [Expect(TestRunState.NoTests)]
    public class NestedClassTests
    {
        [Expect(TestRunState.Success, PassedCount = 1)]
        public class NestedClass
        {
            [Test, Expect(TestRunState.Success, PassedCount = 1)]
            public void Pass()
            {
            }
        }
    }
}
