namespace SubCSS.Ast
{
    public sealed class UniversalSelector : BaseSelector
    {
        internal bool Equals(UniversalSelector other)
        {
            return other != null;
        }

        public override int GetHashCode()
        {
            return 1;
        }

        public override string ToCssString()
        {
            return "*";
        }

        public override void Accept(ISelectorVisitor visitor)
        {
            visitor.VisitUniversalSelector(this);
        }

        public UniversalSelector()
        {
            
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as UniversalSelector);
        }
    }
}
