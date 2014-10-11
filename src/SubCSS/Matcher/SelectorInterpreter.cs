using SubCSS.Ast;

namespace SubCSS.Matcher
{
    internal class SelectorInterpreter : ISelectorVisitor
    {
        private readonly object _subject;
        private readonly IObjectNavigator _navigator;
        // TODO: calculate CSS specificity of match
        private bool _isSubject = true;

        public SelectorInterpreter(object subject, IObjectNavigator navigator)
        {
            _subject = subject;
            _navigator = navigator;
        }

        public bool IsSubject
        {
            get { return _isSubject; }
        }

        public void VisitUniversalSelector(UniversalSelector us)
        {
        }

        public void VisitTypeSelector(TypeSelector ts)
        {
            var type = ts.Type;
            var actualType = _navigator.GetType(_subject);
            _isSubject = Equals(type, actualType);
        }

        public void VisitSimpleSelector(SimpleSelector ss)
        {
            ss.BaseSelector.Accept(this);

            foreach (var extra in ss.RestrictingSelectors)
            {
                if (!_isSubject)
                    return;

                extra.Accept(this);
            }
        }

        public void VisitPseudoClassSelector(PseudoClassSelector pcs)
        {
            _isSubject = _navigator.IsPseudoClass(_subject, pcs.Name);
        }

        public void VisitClassSelector(ClassSelector cs)
        {
            _isSubject = _navigator.IsClass(_subject, cs.Name);
        }
    }
}