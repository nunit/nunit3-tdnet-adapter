namespace NUnitTDNet.Adapter.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NUnitTDNet.Adapter.Examples.Expected;
    using System.Threading;

    [TestClass]
    public class ExpectAttributeExplorerTests
    {
        static ExpectAttributeExplorer explorer;

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            explorer = new ExpectAttributeExplorer(typeof(ExpectClass).Assembly);
        }

        [TestMethod]
        public void FindExpectEntry_ExpectClass_Exists()
        {
            var expectClass = typeof(ExpectClass);
            var name = ExpectAttributeExplorer.GetName(expectClass);

            var expectEntry = explorer.FindExpectEntry(name);

            Assert.AreEqual(expectClass, expectEntry.Member);
        }

        [TestMethod]
        public void FindExpectEntry_ExpectMethod_Exists()
        {
            var expectMethod = new ThreadStart(ExpectClass.ExpectMethod).Method;
            var name = ExpectAttributeExplorer.GetName(expectMethod);
            var explorer = new ExpectAttributeExplorer(expectMethod.DeclaringType.Assembly);

            var expectEntry = explorer.FindExpectEntry(name);

            Assert.AreEqual(expectMethod, expectEntry.Member);
        }

        [TestMethod]
        public void FindExpectEntry_ExpectNestedClass_Exists()
        {
            var expectClass = typeof(ExpectClass.ExpectNestedClass);
            var name = ExpectAttributeExplorer.GetName(expectClass);

            var expectEntry = explorer.FindExpectEntry(name);

            Assert.AreEqual(expectClass, expectEntry.Member);
        }

        [TestMethod]
        public void FindExpectEntry_ExpectMethodInNestedType_Exists()
        {
            var expectMethod = new ThreadStart(ExpectClass.ExpectNestedClass.ExpectMethod).Method;
            var name = ExpectAttributeExplorer.GetName(expectMethod);

            var expectEntry = explorer.FindExpectEntry(name);

            Assert.AreEqual(expectMethod, expectEntry.Member);
        }

        [TestMethod]
        public void FindExpectEntry_Class_Exists()
        {
            var expectClass = typeof(ExpectClass);
            var name = ExpectAttributeExplorer.GetName(expectClass);

            var expectEntry = explorer.FindExpectEntry(name);

            Assert.IsNotNull(expectEntry.ExpectAttribute);
        }
    }
}
