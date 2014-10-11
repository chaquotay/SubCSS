namespace SubCSS.Ast
{
    public interface ISelectorVisitable
    {
        void Accept(ISelectorVisitor visitor);
    }
}