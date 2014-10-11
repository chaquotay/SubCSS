namespace SubCSS.Matcher
{
    internal class TypeConstraint : IElementConstraint
    {
        private readonly string _type;
        private readonly IObjectNavigator _navigator;

        public TypeConstraint(string type, IObjectNavigator navigator)
        {
            _type = type;
            _navigator = navigator;
        }

        public bool Violates(object o)
        {
            var type = _navigator.GetType(o);
            return !Equals(type, _type);
        }
    }
}