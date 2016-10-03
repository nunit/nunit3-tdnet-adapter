namespace NUnitTDNet.Adapter.Examples.Expected
{
    using NUnit.Framework;
    using NUnitTDNet.Expected;

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

        // Check message doesn't end with line break.
        [Test]
        [ExpectTest(ClassName + ".AreEqual", Message = @"  Expected: 1
  But was:  <<equal 2>>")]
        public void AreEqual()
        {
            Assert.AreEqual(1, Is.EqualTo(2));
        }

    }
}
