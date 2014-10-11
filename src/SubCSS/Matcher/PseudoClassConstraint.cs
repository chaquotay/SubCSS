namespace SubCSS.Matcher
{
    internal class PseudoClassConstraint : IElementConstraint
    {
        private readonly string _pseudoClass;
        private readonly IObjectNavigator _navigator;

        public PseudoClassConstraint(string pseudoClass, IObjectNavigator navigator)
        {
            _pseudoClass = pseudoClass;
            _navigator = navigator;
        }

        public bool Violates(object o)
        {
            return !_navigator.IsPseudoClass(o, _pseudoClass);
        }
    }
}