using NUnitTDNet.Adapter;
using TestDriven.Framework;

// Tell TestDriven.Net to use this test runner when targeting tests in this assembly.

[assembly: CustomTestRunner(typeof(EngineTestRunner))]
//[assembly: CustomTestRunner(typeof(NUnitConsoleTestRunner))]
