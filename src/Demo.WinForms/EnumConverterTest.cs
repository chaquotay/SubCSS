using System.Windows.Forms;
using NUnit.Framework;

namespace Demo.WinForms
{
    [TestFixture]
    public class EnumConverterTest
    {
        [Test]
        public void TestAlign()
        {
            var a = EnumConverter.ConvertFrom<HorizontalAlignment>("center");
            Assert.AreEqual(HorizontalAlignment.Center, a);
        }
    }
}