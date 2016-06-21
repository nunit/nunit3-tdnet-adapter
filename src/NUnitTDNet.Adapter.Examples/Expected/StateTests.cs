namespace NUnitTDNet.Adapter.Examples.Expected
{
    using NUnit.Framework;
    using TestDriven.Framework;

    public class StateTests
    {
        const string ClassName = "NUnitTDNet.Adapter.Examples.Expected.StateTests";

        [Test]
        [ExpectTest(ClassName + ".Pass", TestState.Passed)]
        public void Pass()
        {
        }

        [Test]
        [ExpectTest(ClassName + ".Fail", TestState.Failed)]
        public void Fail()
        {
            Assert.Fail("Boom!");
        }

        [Test, Ignore("for reasons")]
        [ExpectTest(ClassName + ".AttributeIgnore", TestState.Ignored)]
        public void AttributeIgnore()
        {
        }

        [Test]
        [ExpectTest(ClassName + ".AssertIgnore", TestState.Ignored)]
        public void AssertIgnore()
        {
            Assert.Ignore("for reasons");
        }
    }
}
