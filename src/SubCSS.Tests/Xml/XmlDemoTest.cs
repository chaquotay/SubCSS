using System;
using System.Xml;
using NUnit.Framework;
using SubCSS.Matcher;
using SubCSS.Parser;

namespace SubCSS.Tests.Xml
{
    [TestFixture]
    public class XmlDemoTest
    {
        [Test]
        public void TestXmlElement()
        {
            var doc = LoadXml("<a />");
            var node = doc.DocumentElement;

            Assert.IsTrue(IsMatch(node, "a"));
        }

        [Test]
        public void TestNotXmlElement()
        {
            var doc = LoadXml("<a />");
            var node = doc.DocumentElement;

            Assert.IsFalse(IsMatch(node, "b"));
        }

        [Test]
        public void TestUniversal()
        {
            var doc = LoadXml("<a />");
            var node = doc.DocumentElement;

            Assert.IsTrue(IsMatch(node, "*"));
        }

        [Test]
        public void TestClassElement()
        {
            var doc = LoadXml("<a class='foo'/>");
            var node = doc.DocumentElement;

            Assert.IsTrue(IsMatch(node, ".foo"));
        }

        [Test]
        public void TestNotClassElement()
        {
            var doc = LoadXml("<a class='foo'/>");
            var node = doc.DocumentElement;

            Assert.IsFalse(IsMatch(node, ".bar"));
        }

        [Test]
        public void TestPseudoClassElement()
        {
            var doc = LoadXml("<a pseudo='foo'/>");
            var node = doc.DocumentElement;

            Assert.IsTrue(IsMatch(node, ":foo"));
        }

        [Test]
        public void TestNotPseudoClassElement()
        {
            var doc = LoadXml("<a pseudo='foo'/>");
            var node = doc.DocumentElement;

            Assert.IsFalse(IsMatch(node, ":bar"));
        }

        [Test]
        public void TestComplex1()
        {
            var doc = LoadXml("<a pseudo='bar' class='foo'/>");
            var node = doc.DocumentElement;

            Assert.IsTrue(IsMatch(node, "a.foo:bar"));
        }
        
        [Test]
        public void TestComplex1NoClassMatch()
        {
            var doc = LoadXml("<a pseudo='bar' class='foo1'/>");
            var node = doc.DocumentElement;

            Assert.IsFalse(IsMatch(node, "a.foo:bar"));
        }

        [Test]
        public void TestComplex1NoPseudoClassMatch()
        {
            var doc = LoadXml("<a pseudo='bar1' class='foo'/>");
            var node = doc.DocumentElement;

            Assert.IsFalse(IsMatch(node, "a.foo:bar"));
        }

        [Test]
        public void TestComplex1NoTypeMatch()
        {
            var doc = LoadXml("<b pseudo='bar' class='foo'/>");
            var node = doc.DocumentElement;

            Assert.IsFalse(IsMatch(node, "a.foo:bar"));
        }

        private static bool IsMatch(XmlElement node, string selector)
        {
            var sel = SubCssParser.ParseSelector(selector);
            var sm = new SelectorMatcher(new XmlNavigator());
            var isMatch = sm.IsSubjectConstraint(sel, node);
            return isMatch;
        }

        private static XmlDocument LoadXml(string xml)
        {
            var doc = new XmlDocument();
            doc.LoadXml(xml);
            return doc;
        }
    }

    [TestFixture]
    public class XmlStyleSheetsDemoTest
    {
        [Test]
        public void TestFoo()
        {
            var styleSheets = new StyleSheets();
            styleSheets.LoadCss("a { foo: u; quux: y; }");
            styleSheets.LoadCss("a { foo: v }");
            styleSheets.LoadCss("* { bar: w }");
            styleSheets.LoadCss("b { bar: www }");
            styleSheets.LoadCss(":quux { baz: x }");
            styleSheets.LoadCss(":p { baz: x }");
            styleSheets.LoadCss(".c { blubb: z }");
            styleSheets.LoadCss(".cc { blubb: zzz }");

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<a class='c' pseudo='p'/>");

            var props = styleSheets.QueryProperties(xmlDoc.DocumentElement, new XmlNavigator());

            Assert.AreEqual(5, props.Count);
            Assert.AreEqual("v", props["foo"]);
            Assert.AreEqual("w", props["bar"]);
            Assert.AreEqual("y", props["quux"]);
            Assert.AreEqual("x", props["baz"]);
            Assert.AreEqual("z", props["blubb"]);
        }
    }

    internal class XmlNavigator : IObjectNavigator
    {
        public string GetType(object subject)
        {
            return ((XmlElement) subject).Name;
        }

        public bool IsPseudoClass(object x, string pseudoClass)
        {
            var e = (XmlElement) x;
            var pseudo = e.GetAttribute("pseudo");
            return Equals(pseudoClass, pseudo);
        }

        public bool IsClass(object x, string @class)
        {
            var e = (XmlElement)x;
            var clazz = e.GetAttribute("class");
            return Equals(@class, clazz);
        }
    }
}
