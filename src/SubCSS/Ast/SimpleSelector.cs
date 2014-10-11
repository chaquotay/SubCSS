using System.Collections.Generic;
using System.Linq;

namespace SubCSS.Ast
{
    public class SimpleSelector : ISelector
    {
        protected bool Equals(SimpleSelector other)
        {
            return Equals(BaseSelector, other.BaseSelector) && EqualsRestr(other.RestrictingSelectors);
        }

        private bool EqualsRestr(ICollection<RestrictingSelector> restrictingSelectors)
        {
            if (RestrictingSelectors.Count != restrictingSelectors.Count)
                return false;

            if (RestrictingSelectors.Count == 0)
                return true;

            if (RestrictingSelectors.Count == 1)
            {
                return Equals(RestrictingSelectors.ElementAt(0), restrictingSelectors.ElementAt(0));
            }

            // TODO: how to compare restricting selectors with different orders?
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((BaseSelector != null ? BaseSelector.GetHashCode() : 0)*397) ^ (RestrictingSelectors != null ? RestrictingSelectors.GetHashCode() : 0);
            }
        }

        public void Accept(ISelectorVisitor visitor)
        {
            visitor.VisitSimpleSelector(this);
        }

        public string ToCssString()
        {
            return BaseSelector.ToCssString() +
                   string.Join("", (from x in RestrictingSelectors select x.ToCssString()));
        }

        public override string ToString()
        {
            return ToCssString();
        }

        private readonly BaseSelector _baseSelector;
        private readonly List<RestrictingSelector> _restrictingSelectors;

        public SimpleSelector(BaseSelector baseSelector, params RestrictingSelector[] restrictingSelectors)
            : this(baseSelector, restrictingSelectors.ToList())
        {
            
        }

        public SimpleSelector(BaseSelector baseSelector, IEnumerable<RestrictingSelector> restrictingSelectors)
        {
            _baseSelector = baseSelector;
            _restrictingSelectors = restrictingSelectors.ToList();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as SimpleSelector);
        }

        public BaseSelector BaseSelector
        {
            get { return _baseSelector; }
        }

        public List<RestrictingSelector> RestrictingSelectors
        {
            get { return _restrictingSelectors; }
        }
    }
}