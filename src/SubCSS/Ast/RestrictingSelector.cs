namespace SubCSS.Ast
{
    public abstract class RestrictingSelector : ICssElement, ISelectorVisitable
    {
        public abstract string ToCssString();
        public abstract void Accept(ISelectorVisitor visitor);
    }
}