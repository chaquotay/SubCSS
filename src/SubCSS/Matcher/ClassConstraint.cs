namespace SubCSS.Matcher
{
    internal class ClassConstraint : IElementConstraint
    {
        private readonly string _class;
        private readonly IObjectNavigator _navigator;

        public ClassConstraint(string @class, IObjectNavigator navigator)
        {
            _class = @class;
            _navigator = navigator;
        }

        public bool Violates(object o)
        {
            return !_navigator.IsClass(o, _class);
        }
    }
}