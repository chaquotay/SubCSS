using NUnit.Framework;
using Rhino.Mocks;
using SubCSS.Ast;
using SubCSS.Matcher;

namespace SubCSS.Tests.Matcher
{
    public abstract class SelectorMatcherTestBase
    {
        [Test]
        public void TestUniversal()
        {
            var element = "foo";
            var mocks = new MockRepository();
            var nav = mocks.StrictMock<IObjectNavigator>();

            mocks.ReplayAll();

            var sm = new SelectorMatcher(nav);
            var isMatch = IsMatch(nav, element, new SimpleSelector(new UniversalSelector()));

            mocks.VerifyAll();

            Assert.IsTrue(isMatch);
        }

        [Test]
        public void TestTypeMatch()
        {
            var element = "foo";
            var mocks = new MockRepository();
            var nav = mocks.StrictMock<IObjectNavigator>();
            Expect.Call(nav.GetType(element)).Return("foo");

            mocks.ReplayAll();

            var sm = new SelectorMatcher(nav);
            var isMatch = IsMatch(nav, element, new SimpleSelector(new TypeSelector("foo")));

            mocks.VerifyAll();

            Assert.IsTrue(isMatch);
        }

        [Test]
        public void TestTypeNotMatch()
        {
            var element = "foo";
            var mocks = new MockRepository();
            var nav = mocks.StrictMock<IObjectNavigator>();
            Expect.Call(nav.GetType(element)).Return("bar");

            mocks.ReplayAll();

            var sm = new SelectorMatcher(nav);
            var isMatch = IsMatch(nav, element, new SimpleSelector(new TypeSelector("foo")));

            mocks.VerifyAll();

            Assert.IsFalse(isMatch);
        }

        [Test]
        public void TestPseudoClassMatch()
        {
            var element = "foo";
            var mocks = new MockRepository();
            var nav = mocks.StrictMock<IObjectNavigator>();
            Expect.Call(nav.IsPseudoClass(element, "quux")).Return(true);

            mocks.ReplayAll();

            var isMatch = IsMatch(nav, element, new SimpleSelector(new UniversalSelector(), new PseudoClassSelector("quux")));

            mocks.VerifyAll();

            Assert.IsTrue(isMatch);
        }

        [Test]
        public void TestPseudoClassIsNotMatch()
        {
            var element = "foo";
            var mocks = new MockRepository();
            var nav = mocks.StrictMock<IObjectNavigator>();
            Expect.Call(nav.IsPseudoClass(element, "quux")).Return(false);

            mocks.ReplayAll();

            var isMatch = IsMatch(nav, element, new SimpleSelector(new UniversalSelector(), new PseudoClassSelector("quux")));

            mocks.VerifyAll();

            Assert.IsFalse(isMatch);
        }

        [Test]
        public void TestTypeWithPseudoClassMatch()
        {
            var element = "foo";
            var mocks = new MockRepository();
            var nav = mocks.StrictMock<IObjectNavigator>();
            Expect.Call(nav.GetType(element)).Return("foo");
            Expect.Call(nav.IsPseudoClass(element, "quux")).Return(true);

            mocks.ReplayAll();

            var isMatch = IsMatch(nav, element, new SimpleSelector(new TypeSelector("foo"), new PseudoClassSelector("quux")));

            mocks.VerifyAll();

            Assert.IsTrue(isMatch);
        }

        [Test]
        public void TestClassMatch()
        {
            var element = "foo";
            var mocks = new MockRepository();
            var nav = mocks.StrictMock<IObjectNavigator>();
            Expect.Call(nav.IsClass(element, "quux")).Return(true);

            mocks.ReplayAll();

            var isMatch = IsMatch(nav, element, new SimpleSelector(new UniversalSelector(), new ClassSelector("quux")));

            mocks.VerifyAll();

            Assert.IsTrue(isMatch);
        }

        [Test]
        public void TestClassIsNotMatch()
        {
            var element = "foo";
            var mocks = new MockRepository();
            var nav = mocks.StrictMock<IObjectNavigator>();
            Expect.Call(nav.IsClass(element, "quux")).Return(false);

            mocks.ReplayAll();

            var isMatch = IsMatch(nav, element, new SimpleSelector(new UniversalSelector(), new ClassSelector("quux")));

            mocks.VerifyAll();

            Assert.IsFalse(isMatch);
        }

        [Test]
        public void TestTypeWithClassMatch()
        {
            var element = "foo";
            var mocks = new MockRepository();
            var nav = mocks.StrictMock<IObjectNavigator>();
            Expect.Call(nav.GetType(element)).Return("foo");
            Expect.Call(nav.IsClass(element, "quux")).Return(true);

            mocks.ReplayAll();

            var isMatch = IsMatch(nav, element, new SimpleSelector(new TypeSelector("foo"), new ClassSelector("quux")));

            mocks.VerifyAll();

            Assert.IsTrue(isMatch);
        }


        protected abstract bool IsMatch(IObjectNavigator nav, string element, SimpleSelector selector);
    }
}