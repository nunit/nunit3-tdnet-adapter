namespace NUnitTDNet.Adapter.Examples.Expected
{
    using System;
    using NUnit.Framework;
    using NUnitTDNet.Expected;
    using TestDriven.Framework;

    public class OneTimeTests
    {
        public class OneTimeSetUpWriteLine
        {
            [OneTimeSetUp]
            public void OneTimeSetUp()
            {
                Console.WriteLine("__OneTimeSetUp__");
            }

            [Test]
            [ExpectOutputLine("__OneTimeSetUp__")]
            public void Test()
            {
            }
        }

        public class OneTimeTearDownWriteLine
        {
            [OneTimeTearDown]
            public void OneTimeTearDown()
            {
                Console.WriteLine("__OneTimeTearDown__");
            }

            [Test]
            [ExpectOutputLine("__OneTimeTearDown__")]
            public void Test()
            {
            }
        }

        public class OneTimeSetUpError
        {
            [OneTimeSetUp]
            public void OneTimeSetUp()
            {
                throw new Exception("Boom!");
            }

            [Test] // Failed fixture set up will appear as extra failed test.
            [ExpectTest("NUnitTDNet.Adapter.Examples.Expected.OneTimeTests+OneTimeSetUpError", TestState.Failed)]
            public void Test()
            {
            }

            [Test]
            [ExpectTest("NUnitTDNet.Adapter.Examples.Expected.OneTimeTests+OneTimeSetUpError.Test2", TestState.Failed)]
            public void Test2()
            {
            }
        }

        public class OneTimeTearDownError
        {
            [OneTimeTearDown]
            public void OneTimeTearDown()
            {
                throw new Exception("Boom!");
            }

            [Test] // Failed fixture tear down will appear as extra failed test.
            [ExpectTest("NUnitTDNet.Adapter.Examples.Expected.OneTimeTests+OneTimeTearDownError", TestState.Failed)]
            public void Test()
            {
            }

            [Test] // Test passed even though clean up failed.
            [ExpectTest("NUnitTDNet.Adapter.Examples.Expected.OneTimeTests+OneTimeTearDownError.Test2", TestState.Passed)]
            public void Test2()
            {
            }
        }

        // Failing [OneTimeSetUp] / [OneTimeTearDown] appear as 1 fialing test (plus the 2 tests).
        [ExpectTestRun(TestRunState.Failure, FailedCount = 3)]
        public class OneTimeSetUpAndTearDownError
        {
            [OneTimeSetUp]
            public void OneTimeSetUp()
            {
                throw new Exception("OneTimeSetUp");
            }

            [OneTimeTearDown]
            public void OneTimeTearDown()
            {
                throw new Exception("OneTimeTearDown");
            }

            [Test] // Set up and tear down fail but appear as one extra failed test.
            [ExpectTest("NUnitTDNet.Adapter.Examples.Expected.OneTimeTests+OneTimeSetUpAndTearDownError", TestState.Failed)]
            public void Test()
            {
            }

            [Test] // Failed fixture tear down will appear as extra failed test.
            [ExpectTest("NUnitTDNet.Adapter.Examples.Expected.OneTimeTests+OneTimeSetUpAndTearDownError.Test2", TestState.Failed)]
            public void Test2()
            {
            }
        }
    }

    namespace SetUpFixtureNamespace
    {
        [SetUpFixture]
        public class SetUpFixture
        {
            [OneTimeSetUp]
            public void OneTimeSetUp()
            {
                throw new Exception("OneTimeSetUp");
            }
        }

        public class OneTimeSetUpError
        {
            [Test] // Failed fixture set up will appear as extra failed test.
            [ExpectTest("NUnitTDNet.Adapter.Examples.Expected.SetUpFixtureNamespace.SetUpFixture", TestState.Failed)]
            public void Test()
            {
            }

            [Test]
            [ExpectTest("NUnitTDNet.Adapter.Examples.Expected.SetUpFixtureNamespace.OneTimeSetUpError.Test2", TestState.Failed)]
            public void Test2()
            {
            }
        }
    }
}
