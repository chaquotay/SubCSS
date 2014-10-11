namespace SubCSS.Ast
{
    public abstract class BaseSelector : ICssElement, ISelectorVisitable
    {
        public abstract string ToCssString();
        public abstract void Accept(ISelectorVisitor visitor);
    }
}