namespace NUnitTDNet.Adapter.Examples.Expected
{
    using NUnit.Framework;

    public class TestMessages
    {
        const string ClassName = "NUnitTDNet.Adapter.Examples.Expected.TestMessages";

        [Test]
        [ExpectTest(ClassName + ".Fail", Message = "Boom!")]
        public void Fail()
        {
            Assert.Fail("Boom!");
        }

        [Test]
        [ExpectTest(ClassName + ".AssertIgnore", Message = "AssertIgnore!")]
        public void AssertIgnore()
        {
            Assert.Ignore("AssertIgnore!");
        }

        [Test, Ignore("AttributeIgnore!")]
        [ExpectTest(ClassName + ".AttributeIgnore", Message = "AttributeIgnore!")]
        public void AttributeIgnore()
        {
        }
    }
}
