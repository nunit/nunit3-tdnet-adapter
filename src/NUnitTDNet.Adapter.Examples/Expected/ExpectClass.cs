namespace NUnitTDNet.Adapter.Examples.Expected
{
    using NUnitTDNet.Expected;
    using TestDriven.Framework;

    [ExpectTestRun(TestRunState.NoTests)]
    public class ExpectClass
    {
        [ExpectTestRun(TestRunState.NoTests)]
        public static void ExpectMethod()
        {
        }

        [ExpectTestRun(TestRunState.NoTests)]
        public class ExpectNestedClass
        {
            [ExpectTestRun(TestRunState.NoTests)]
            public static void ExpectMethod()
            {
            }
        }
    }
}
