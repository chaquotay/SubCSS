namespace SubCSS.Ast
{
    public class PseudoClassSelector : RestrictingSelector
    {
        protected bool Equals(PseudoClassSelector other)
        {
            return string.Equals(Name, other.Name);
        }

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }

        public override string ToCssString()
        {
            return ":" + Name;
        }

        public override void Accept(ISelectorVisitor visitor)
        {
            visitor.VisitPseudoClassSelector(this);
        }

        private readonly string _name;

        internal PseudoClassSelector(string name)
        {
            _name = name;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as PseudoClassSelector);
        }

        public string Name
        {
            get { return _name; }
        }
    }
}