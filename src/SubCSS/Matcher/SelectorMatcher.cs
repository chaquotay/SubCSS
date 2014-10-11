using SubCSS.Ast;

namespace SubCSS.Matcher
{
    internal class SelectorMatcher
    {
        private readonly IObjectNavigator _navigator;

        public SelectorMatcher(IObjectNavigator navigator)
        {
            _navigator = navigator;
        }

        public bool IsSubject(ISelector selector, object x)
        {
            var interpreter = new SelectorInterpreter(x, _navigator);
            selector.Accept(interpreter);
            return interpreter.IsSubject;
        }

        public bool IsSubjectConstraint(ISelector selector, object x)
        {
            return GetSpecificity(selector, x).HasValue;
        }

        public Specificity? GetSpecificity(ISelector selector, object x)
        {
            var interpreter = new ConstraintBuilder(_navigator);
            selector.Accept(interpreter);
            var constraints = interpreter.ToConstraint();
            if (constraints.Violates(x))
            {
                return null;
            }
            else
            {
                return interpreter.Specificity;
            }
        }

    }
}