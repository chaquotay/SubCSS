using System.Drawing;
using NUnit.Framework;

namespace Demo.WinForms
{
    [TestFixture]
    public class CssColorConverterTest
    {
        [Test]
        public void TestConvertHexColorString()
        {
            Assert.IsNull(CssColorConverter.ConvertFrom("#foo"));
            Assert.AreEqual(Color.FromArgb(255, 0, 255), CssColorConverter.ConvertFrom("#ff00ff"));
        }
    }
}