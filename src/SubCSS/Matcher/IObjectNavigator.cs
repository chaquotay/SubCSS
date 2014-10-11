namespace SubCSS.Matcher
{
    public interface IObjectNavigator
    {
        string GetType(object subject);
        bool IsPseudoClass(object x, string pseudoClass);
        bool IsClass(object x, string @class);
    }
}