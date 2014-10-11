using System.Drawing;
using NUnit.Framework;

namespace Demo.WinForms
{
    [TestFixture]
    public class CssFontTest
    {
        [Test]
        public void TestFamily()
        {
            var baseFont = new Font("Arial", 11, FontStyle.Italic);

            Assert.AreEqual(baseFont, CssFont.ApplyFamily(baseFont, "Garbage"));
            Assert.AreEqual(new Font("Segoe UI", 11, FontStyle.Italic), CssFont.ApplyFamily(baseFont, "Segoe UI"));
        }

        [Test]
        public void TestSize()
        {
            var baseFont = new Font("Arial", 11, FontStyle.Italic);

            Assert.AreEqual(baseFont, CssFont.ApplySize(baseFont, "tez"));
            Assert.AreEqual(new Font("Arial", 8.5f, FontStyle.Italic), CssFont.ApplySize(baseFont, "8.5"));
        }
    }
}