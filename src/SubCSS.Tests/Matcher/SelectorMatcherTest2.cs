using NUnit.Framework;
using SubCSS.Ast;
using SubCSS.Matcher;

namespace SubCSS.Tests.Matcher
{
    [TestFixture]
    public class SelectorMatcherTest2 : SelectorMatcherTestBase
    {
        protected override bool IsMatch(IObjectNavigator nav, string element, SimpleSelector selector)
        {
            var sm = new SelectorMatcher(nav);
            var isMatch = sm.IsSubjectConstraint(selector, element);
            return isMatch;
        }

    }
}