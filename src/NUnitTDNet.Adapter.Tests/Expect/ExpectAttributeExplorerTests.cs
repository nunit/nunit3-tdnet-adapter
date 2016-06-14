namespace NUnitTDNet.Adapter.Tests
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NUnitTDNet.Adapter.Examples;
    using System.Reflection;
    using NUnitTDNet.Adapter.Examples.Expected;
    using System.Threading;
    using System.Xml;

    [TestClass]
    public class ExpectAttributeExplorerTests
    {
        [TestMethod]
        public void GetMember_ExpectClass_Exists()
        {
            var expectClass = typeof(ExpectClass);
            var name = ExpectAttributeExplorer.GetName(expectClass);

            var member = ExpectAttributeExplorer.GetMember(name);

            Assert.AreEqual(expectClass, member);
        }

        [TestMethod]
        public void GetMember_ExpectMethod_Exists()
        {
            var expectMethod = new ThreadStart(ExpectClass.ExpectMethod).Method;
            var name = ExpectAttributeExplorer.GetName(expectMethod);

            var member = ExpectAttributeExplorer.GetMember(name);

            Assert.AreEqual(expectMethod, member);
        }

        [TestMethod]
        public void GetMember_ExpectNestedClass_Exists()
        {
            var expectClass = typeof(ExpectClass.ExpectNestedClass);
            var name = ExpectAttributeExplorer.GetName(expectClass);

            var member = ExpectAttributeExplorer.GetMember(name);

            Assert.AreEqual(expectClass, member);
        }

        [TestMethod]
        public void GetMember_ExpectMethodInNestedType_Exists()
        {
            var expectMethod = new ThreadStart(ExpectClass.ExpectNestedClass.ExpectMethod).Method;
            var name = ExpectAttributeExplorer.GetName(expectMethod);

            var member = ExpectAttributeExplorer.GetMember(name);

            Assert.AreEqual(expectMethod, member);
        }

        [TestMethod]
        public void GetExpectAttribute_Class_Exists()
        {
            var expectClass = typeof(ExpectClass);
            var name = ExpectAttributeExplorer.GetName(expectClass);

            var attribute = ExpectAttributeExplorer.GetExpectAttribute(name);

            Assert.IsNotNull(attribute);
        }

        [TestMethod]
        public void ExpectXml_Class_Exists()
        {
            var expectClass = typeof(ExpectClass);
            var name = ExpectAttributeExplorer.GetName(expectClass);
            var xpath = string.Format("/Expects/Expect[@Name='{0}']", name);
            var expectXml = string.Format(@"<Expect Name=""{0}"" />", name);

            var xmlDoc = new XmlDocument();
            xmlDoc.Load(ExpectAttributeExplorer.XmlFile);
            var node = xmlDoc.SelectSingleNode(xpath);

            Assert.IsNotNull(node);
            Assert.AreEqual(expectXml, node.OuterXml);
        }
    }
}
