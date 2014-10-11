using System.Collections.Generic;

namespace SubCSS.Ast
{
    internal class StyleSheet
    {
        private readonly IEnumerable<RuleSet> _ruleSets;

        public StyleSheet(IEnumerable<RuleSet> ruleSets)
        {
            _ruleSets = ruleSets;
        }

        public IEnumerable<RuleSet> RuleSets
        {
            get { return _ruleSets; }
        }
    }
}