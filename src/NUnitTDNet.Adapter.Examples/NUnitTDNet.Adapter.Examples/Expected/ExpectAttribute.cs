namespace NUnitTDNet.Adapter.Examples.Expected
{
    using System;
    using TestDriven.Framework;

    public class ExpectAttribute : Attribute
    {
        public ExpectAttribute()
        {
        }

        public ExpectAttribute(TestRunState testRunState)
        {
            TestRunState = testRunState;
        }

        public TestRunState? TestRunState
        {
            get; private set;
        }

        public int PassedCount
        {
            get; set;
        }

        public int IgnoredCount
        {
            get; set;
        }

        public int FailedCount
        {
            get; set;
        }
    }
}
