using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SubCSS.Ast
{
    internal class RuleSet
    {
        protected bool Equals(RuleSet other)
        {
            if (!Equals(_selector, other._selector))
                return false;

            if (_properties.Count() != other.Properties.Count())
                return false;

            var xe = _properties.GetEnumerator();
            var ye = other.Properties.GetEnumerator();

            while (xe.MoveNext() && ye.MoveNext())
            {
                if (!Equals(xe.Current, ye.Current))
                    return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                // TODO: incorporate hashcodes from each property
                return ((_selector != null ? _selector.GetHashCode() : 0)*397) ^ (_properties != null ? _properties.GetHashCode() : 0);
            }
        }

        private readonly ISelector _selector;
        private readonly IEnumerable<Property> _properties;

        public RuleSet(ISelector selector, IEnumerable<Property> properties)
        {
            _selector = selector;
            _properties = properties;
        }

        public RuleSet(ISelector selector, params Property[] properties)
            : this(selector, properties.ToList())
        {
            
        }
        
        internal RuleSet(BaseSelector selector, params Property[] properties)
            : this(new SimpleSelector(selector), properties.ToList())
        {
            
        }

        public ISelector Selector
        {
            get { return _selector; }
        }

        public IEnumerable<Property> Properties
        {
            get { return _properties; }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(_selector);
            sb.AppendLine(" {");

            foreach (var prop in _properties)
            {
                sb.Append("\t");
                sb.AppendLine(prop.ToString());
            }

            sb.AppendLine("}");
            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as RuleSet);
        }
    }
}