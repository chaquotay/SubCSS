using System.Collections.Generic;
using SubCSS.Ast;

namespace SubCSS.Matcher
{
    internal class StyleSheetMatcher
    {
        private readonly StyleSheet _styleSheet;
        private readonly IObjectNavigator _navigator;
        private readonly SelectorMatcher _selectorMatcher;

        public StyleSheetMatcher(StyleSheet styleSheet, IObjectNavigator navigator)
        {
            _styleSheet = styleSheet;
            _navigator = navigator;
            _selectorMatcher = new SelectorMatcher(navigator);
        }

        public IDictionary<string, object> CollectRules(object subject)
        {
            var properties = new Dictionary<string, object>();

            // TODO: check CSS specificity
            foreach (var ruleSet in _styleSheet.RuleSets)
            {
                if (_selectorMatcher.IsSubject(ruleSet.Selector, subject))
                {
                    foreach (var property in ruleSet.Properties)
                    {
                        properties[property.Name] = property.Value;
                    }
                }
            }

            return properties;
        }
    }
}
