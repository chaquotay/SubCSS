namespace SubCSS.Matcher
{
    internal interface IElementConstraint
    {
        bool Violates(object o);
    }
}