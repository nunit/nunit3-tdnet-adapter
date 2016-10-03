namespace NUnitTDNet.Adapter.Examples
{
    using System;
    using NUnit.Framework;

    public class OneTimeSetUpWithOutput
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Console.WriteLine("__OneTimeSetUp__");
        }

        [Test]
        public void Test() { }
    }

    public class OneTime
    {
        public class SetUpWithOutput
        {
            [OneTimeSetUp]
            public void OneTimeSetUp()
            {
                Console.WriteLine("__OneTimeSetUp__");
            }

            [Test]
            public void Test() { }
        }

        public class TearDownWithOutput
        {
            [OneTimeTearDown]
            public void OneTimeTearDown()
            {
                Console.WriteLine("__OneTimeTearDown__");
            }

            [Test]
            public void Test() { }
        }

        public class SetUpThrowsNotImplementedException
        {
            [OneTimeSetUp]
            public void OneTimeSetUp()
            {

                throw new NotImplementedException();
            }

            [Test]
            public void Test1() { }

            [Test]
            public void Test2() { }
        }
    }
}