namespace NUnitTDNet.Adapter.Examples.Expected
{
    using NUnitTDNet.Expected;
    using TestDriven.Framework;

    [ExpectTestRun(TestRunState.NoTests)]
    public class AdHocTests
    {
        [ExpectTestRun(TestRunState.NoTests)]
        public void MethodNoTestAttribute()
        {
        }

        [ExpectTestRun(TestRunState.NoTests)]
        public string TargetProperty
        {
            get; set;
        }

        [ExpectTestRun(TestRunState.NoTests)]
        public string TargetField;
    }
}
