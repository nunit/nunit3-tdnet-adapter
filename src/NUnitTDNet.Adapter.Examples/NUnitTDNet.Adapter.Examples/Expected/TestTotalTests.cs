namespace NUnitTDNet.Adapter.Examples.Expected
{
    using NUnit.Framework;

    [ExpectTest(ClassName + ".OneTest", TotalTests = 3)]
    public class TestTotalTests
    {
        const string ClassName = "NUnitTDNet.Adapter.Examples.Expected.TestTotalTests";

        [Test]
        [ExpectTest(ClassName + ".OneTest", TotalTests = 1)]
        public void OneTest()
        {
        }

        [TestCase(1)]
        [TestCase(2)]
        [ExpectTest(ClassName + ".TwoTestCases(1)", TotalTests = 2)]
        public void TwoTestCases()
        {
        }
    }
}
