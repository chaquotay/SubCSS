using NUnit.Framework;

namespace Demo.WinForms
{
    [TestFixture]
    public class PropertyNameTranslatorTest
    {
        [Test]
        public void TranslateEmptyWord()
        {
            Assert.AreEqual("", PropertyNameTranslator.CssToClr(""));
            Assert.AreEqual(null, PropertyNameTranslator.CssToClr(null));
        }

        [Test]
        public void TranslateSimpleWord()
        {
            Assert.AreEqual("Foo", PropertyNameTranslator.CssToClr("foo"));
        }

        [Test]
        public void TranslateComplexWord()
        {
            Assert.AreEqual("FooBar", PropertyNameTranslator.CssToClr("foo-bar"));
            Assert.AreEqual("FooBarBaz", PropertyNameTranslator.CssToClr("foo-bar-baz"));
        }
    }
}