namespace NUnitTDNet.Adapter.Examples.Expected
{
    using System;
    using NUnit.Framework;
    using TestDriven.Framework;

    public class TestOutputLines
    {
        const string ClassName = "NUnitTDNet.Adapter.Examples.Expected.OutputLineTests";

        [Test]
        [ExpectOutputLine("Hello, World!")]
        public void HelloWorld()
        {
            Console.WriteLine("Hello, World!");
        }

        [Test]
        [ExpectOutputLine("ConsoleOutput!", Category.Output)]
        public void ConsoleOutput()
        {
            Console.WriteLine("ConsoleOutput!");
        }

        // This will be captured forwarded by TestDriven.Net's test app domain.
        //[Test]
        //[ExpectOutputLine("DebugOutput!", Category.Debug)]
        //public void DebugOutput()
        //{
        //    Trace.WriteLine("DebugOutput!");
        //}
    }
}
