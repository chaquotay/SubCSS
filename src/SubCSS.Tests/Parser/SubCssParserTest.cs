using System.Collections;
using System.Linq;
using NUnit.Framework;
using SubCSS.Ast;
using SubCSS.Parser;

namespace SubCSS.Tests.Parser
{
    [TestFixture]
    public class SubCssParserTest
    {
        [Test]
        public void ParseSimple()
        {
            var x = SubCssParser.Parse("foo {bar: a}");

            Assert.IsNotNull(x);
            Assert.AreEqual(1, x.RuleSets.Count());
            var rule = x.RuleSets.First();
            Assert.AreEqual(new SimpleSelector(new TypeSelector("foo")), rule.Selector);

            var properties = rule.Properties;
            Assert.IsNotNull(properties);
            properties = properties.ToList();
            Assert.AreEqual(1, properties.Count());
            Assert.AreEqual(new Property("bar", "a", false), properties.First());
        }

        [Test]
        public void ParseSimple2()
        {
            var x = SubCssParser.Parse("foo {bar: a; baz: b}");
        }

        [Test]
        public void ParseSimple3()
        {
            var x = SubCssParser.Parse("foo {bar: a}\r\nfoo2 {bar2: a2}");
            Assert.AreEqual(2, x.RuleSets.Count());
            Assert.AreEqual(new RuleSet(new TypeSelector("foo"), new Property("bar", "a", false)), x.RuleSets.ElementAt(0));
            Assert.AreEqual(new RuleSet(new TypeSelector("foo2"), new Property("bar2", "a2", false)), x.RuleSets.ElementAt(1));
        }

        [Test]
        public void ParseComment()
        {
            var x = SubCssParser.Parse("/***** TEST ****/");
            Assert.AreEqual(0, x.RuleSets.Count());
        }


        [Test]
        public void ParseAny()
        {
            var x = SubCssParser.Parse("* {bar: a}");
            Assert.AreEqual(1, x.RuleSets.Count());
            Assert.AreEqual(new RuleSet(new UniversalSelector(), new Property("bar", "a", false)), x.RuleSets.ElementAt(0));
        }

        [Test]
        public void ParsePseudoClass()
        {
            var x = SubCssParser.Parse("foo:quux {bar: a}");
            Assert.AreEqual(1, x.RuleSets.Count());
            Assert.AreEqual(new RuleSet(new SimpleSelector(new TypeSelector("foo"), new PseudoClassSelector("quux")), new Property("bar", "a", false)), x.RuleSets.ElementAt(0));
        }

        [Test]
        public void ParseUniversalPseudoClass()
        {
            var x = SubCssParser.Parse("*:quux {bar: a}");
            Assert.AreEqual(1, x.RuleSets.Count());
            Assert.AreEqual(new RuleSet(new SimpleSelector(new UniversalSelector(), new PseudoClassSelector("quux")), new Property("bar", "a", false)), x.RuleSets.ElementAt(0));
        }

        [Test]
        public void ParseUniversalPseudoClassWithoutWildcard()
        {
            var x = SubCssParser.Parse(":quux {bar: a}");
            Assert.AreEqual(1, x.RuleSets.Count());
            Assert.AreEqual(new RuleSet(new SimpleSelector(new UniversalSelector(), new PseudoClassSelector("quux")), new Property("bar", "a", false)), x.RuleSets.ElementAt(0));
        }

        [Test]
        public void ParseUniversalClass()
        {
            var x = SubCssParser.Parse("*.quux {bar: a}");
            Assert.AreEqual(1, x.RuleSets.Count());
            Assert.AreEqual(new RuleSet(new SimpleSelector(new UniversalSelector(), new ClassSelector("quux")), new Property("bar", "a", false)), x.RuleSets.ElementAt(0));
        }

        [Test]
        public void ParseUniversalClassWithoutWildcard()
        {
            var x = SubCssParser.Parse(".quux {bar: a}");
            Assert.AreEqual(1, x.RuleSets.Count());
            Assert.AreEqual(new RuleSet(new SimpleSelector(new UniversalSelector(), new ClassSelector("quux")), new Property("bar", "a", false)), x.RuleSets.ElementAt(0));
        }

        [Test]
        public void ParsePropertyWithHyphen()
        {
            var x = SubCssParser.Parse(".quux {bar-baz: a}");
            Assert.AreEqual(1, x.RuleSets.Count());
            Assert.AreEqual(new RuleSet(new SimpleSelector(new UniversalSelector(), new ClassSelector("quux")), new Property("bar-baz", "a", false)), x.RuleSets.ElementAt(0));
        }

        [Test]
        public void ParseUnits()
        {
            var x = SubCssParser.Parse("* {bar: 42pt; baz: 12px;}");
            Assert.AreEqual(1, x.RuleSets.Count());
            //Assert.AreEqual(new RuleSet(new SimpleSelector(new UniversalSelector()), new Property("bar", new Quantity(42, Unit.Point), false), new Property("baz", new Quantity(12, Unit.Pixel), false)), x.RuleSets.ElementAt(0));
            Assert.AreEqual(new RuleSet(new SimpleSelector(new UniversalSelector()), new Property("bar", "42pt", false), new Property("baz", "12px", false)), x.RuleSets.ElementAt(0));
        }

        [Test]
        public void ParseText()
        {
            var x = SubCssParser.Parse("* {bar: \"baz\"}");
            Assert.AreEqual(1, x.RuleSets.Count());
            Assert.AreEqual(new RuleSet(new SimpleSelector(new UniversalSelector()), new Property("bar", "baz", false)), x.RuleSets.ElementAt(0));
        }

        [Test]
        public void ParseTextList()
        {
            var x = SubCssParser.Parse("* {bar: \"baz\",quux}");
            Assert.AreEqual(1, x.RuleSets.Count());
            Assert.AreEqual(new RuleSet(new SimpleSelector(new UniversalSelector()), new Property("bar", new ArrayList { "baz", "quux" }, false)), x.RuleSets.ElementAt(0));
        }

        [Test]
        public void ParseColor()
        {
            var x = SubCssParser.Parse("* {bar: #ff00ff }");
            Assert.AreEqual(1, x.RuleSets.Count());
            Assert.AreEqual(new RuleSet(new SimpleSelector(new UniversalSelector()), new Property("bar", "#ff00ff", false)), x.RuleSets.ElementAt(0));
        }
    }
}
