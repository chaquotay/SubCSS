using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SubCSS.Ast;
using SubCSS.Matcher;

namespace SubCSS
{
    public class StyleSheets
    {
        private readonly List<RuleSet> _ruleSets = new List<RuleSet>();

        public void Load(string cssFile)
        {
            var css = File.ReadAllText(cssFile);
            LoadCss(css);
        }

        public void LoadCss(string css)
        {
            var parsedCss = Parser.SubCssParser.Parse(css);
            _ruleSets.AddRange(parsedCss.RuleSets);
        }

        public Dictionary<string, object> QueryProperties(object o, IObjectNavigator navigator)
        {
            var properties = new Properties();

            var sm = new SelectorMatcher(navigator);
            foreach (var ruleSet in _ruleSets)
            {
                var spec = sm.GetSpecificity(ruleSet.Selector, o);
                if (spec.HasValue)
                {
                    foreach (var property in ruleSet.Properties)
                    {
                        properties.AddProperty(property.Name, property.Value, spec.Value);
                    }
                }
            }

            return properties.ToDictionary();
        }
    }

    public class Properties
    {
        private readonly Dictionary<string, Tuple<object, Specificity>> _entries = new Dictionary<string, Tuple<object, Specificity>>();

        public bool AddProperty(string name, object value, Specificity specificity)
        {
            var added = false;
            Tuple<object, Specificity> existingValue = null;
            if (_entries.TryGetValue(name, out existingValue))
            {
                if (existingValue.Item2.CompareTo(specificity) <= 0)
                {
                    _entries[name] = new Tuple<object, Specificity>(value, specificity);
                    added = true;
                }
            }
            else
            {
                _entries.Add(name, new Tuple<object, Specificity>(value, specificity));
                added = true;
            }

            return added;
        }

        public Dictionary<string, object> ToDictionary()
        {
            return _entries.ToDictionary(x => x.Key, x => x.Value.Item1);
        }
    }
}
