namespace NUnitTDNet.Adapter.Examples.Expected
{
    using TestDriven.Framework;

    [Expect(TestRunState.NoTests)]
    public class ExpectClass
    {
        [Expect(TestRunState.NoTests)]
        public static void ExpectMethod()
        {
        }

        [Expect(TestRunState.NoTests)]
        public class ExpectNestedClass
        {
            [Expect(TestRunState.NoTests)]
            public static void ExpectMethod()
            {
            }
        }
    }
}
