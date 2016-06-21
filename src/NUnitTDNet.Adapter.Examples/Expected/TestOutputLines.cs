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

        // NOTE: Can't send to Category.Error because NUnit sends output/error to same place.
        //[Test]
        //[ExpectOutputLine("ConsoleError!", Category.Error)]
        //public void ConsoleError()
        //{
        //    Console.WriteLine("ConsoleError!");
        //}

        [Test]
        [ExpectOutputLine(null)]
        public void WriteNothing()
        {
        }

        [Test]
        [ExpectOutputLine(null)]
        public void WriteNull()
        {
            Console.Write((string)null);
        }

        [Test]
        [ExpectOutputLine(null)]
        public void WriteEmptyString()
        {
            Console.Write("");
        }

        [Test]
        [ExpectOutputLine("")]
        public void WriteLine()
        {
            Console.WriteLine();
        }

        [Test]
        [ExpectOutputLine("")]
        public void WriteLineNull()
        {
            Console.WriteLine((string)null);
        }

        [Test]
        [ExpectOutputLine("Write!")]
        public void Write()
        {
            Console.Write("Write!");
        }

        [Test]
        [ExpectOutputLine("666")]
        public void Number()
        {
            Console.WriteLine(666);
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
