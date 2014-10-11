namespace SubCSS.Matcher
{
    internal class NullConstraint : IElementConstraint
    {
        public bool Violates(object o)
        {
            return false;
        }
    }
}