namespace NUnitTDNet.Adapter.Examples.Expected
{
    using NUnit.Framework;

    public class TestStackTraces
    {
        const string ClassName = "NUnitTDNet.Adapter.Examples.Expected.TestStackTraces";

        [Test]
        [ExpectTest(ClassName + ".AtMethodIn", StackTraceStartsWith = "at " + ClassName + ".AtMethodIn() in ")]
        public void AtMethodIn()
        {
            Assert.Fail("Boom!");
        }

        [Test] // Stack trace should only include user methods.
        [ExpectTest(ClassName + ".FileLineReturn", StackTraceEndsWith = "\\TestStackTraces.cs:line 20\r\n")]
        public void FileLineReturn()
        {
            Assert.Fail("Boom!"); // Check this is line 20 in Visual Studio!
        }
    }
}
