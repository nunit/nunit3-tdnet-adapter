namespace NUnitTDNet.Adapter.Examples
{
    using NUnit.Framework;

    public class OnePassingTest
    {
        [Test]
        public void Passing()
        {
            Assert.That(2 + 2, Is.EqualTo(4));
        }
    }
}
