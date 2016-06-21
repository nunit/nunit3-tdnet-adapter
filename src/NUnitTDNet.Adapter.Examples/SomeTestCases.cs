using NUnit.Framework;

namespace NUnitTDNet.Adapter.Examples
{
    /// <summary>
    /// 
    /// </summary>
    public class SomeTestCases
    {
        [TestCase(1)]
        [TestCase(2)]
        public static void PassAndFail(int i)
        {
            Assert.That(i, Is.EqualTo(1));
        }
    }
}
