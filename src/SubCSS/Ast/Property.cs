using System.Collections;
using System.Linq;

namespace SubCSS.Ast
{
    internal class Property
    {
        protected bool Equals(Property other)
        {
            return string.Equals(_name, other._name) && ValueEquals(_value, other._value) && _important.Equals(other._important);
        }

        private bool ValueEquals(object x, object y)
        {
            if (x is IEnumerable && y is IEnumerable)
            {
                return Enumerable.SequenceEqual(((IEnumerable) x).OfType<object>(), ((IEnumerable) y).OfType<object>());
            }

            return Equals(x, y);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (_name != null ? _name.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (_value != null ? _value.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ _important.GetHashCode();
                return hashCode;
            }
        }

        private readonly string _name;
        private readonly object _value;
        private readonly bool _important;

        public Property(string name, object value, bool important)
        {
            _name = name;
            _value = value;
            _important = important;
        }

        public string Name
        {
            get { return _name; }
        }

        public object Value
        {
            get { return _value; }
        }

        public bool Important
        {
            get { return _important; }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Property);
        }

        public override string ToString()
        {
            return _name + ": " + _value + (_important ? " !important" : "");
        }
    }
}