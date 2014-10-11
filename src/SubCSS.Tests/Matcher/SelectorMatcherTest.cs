using NUnit.Framework;
using SubCSS.Ast;
using SubCSS.Matcher;

namespace SubCSS.Tests.Matcher
{
    [TestFixture]
    public class SelectorMatcherTest : SelectorMatcherTestBase
    {
        protected override bool IsMatch(IObjectNavigator nav, string element, SimpleSelector selector)
        {
            var sm = new SelectorMatcher(nav);
            var isMatch = sm.IsSubject(selector, element);
            return isMatch;
        }

    }
}