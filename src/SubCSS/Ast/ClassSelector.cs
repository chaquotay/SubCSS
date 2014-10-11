namespace SubCSS.Ast
{
    public class ClassSelector : RestrictingSelector
    {
        protected bool Equals(ClassSelector other)
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
            visitor.VisitClassSelector(this);
        }

        private readonly string _name;

        internal ClassSelector(string name)
        {
            _name = name;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ClassSelector);
        }

        public string Name
        {
            get { return _name; }
        }
    }
}