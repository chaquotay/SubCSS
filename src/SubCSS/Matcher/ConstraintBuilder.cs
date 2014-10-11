using System.Collections.Generic;
using System.Linq;
using SubCSS.Ast;

namespace SubCSS.Matcher
{
    internal class ConstraintBuilder : ISelectorVisitor
    {
        private readonly IObjectNavigator _navigator;
        private readonly List<IElementConstraint> _constraints = new List<IElementConstraint>();
        private Specificity _specificity = Specificity.Zero;

        public ConstraintBuilder(IObjectNavigator navigator)
        {
            _navigator = navigator;
        }

        public Specificity Specificity
        {
            get { return _specificity; }
        }

        public void VisitUniversalSelector(UniversalSelector us)
        {
            
        }

        public void VisitTypeSelector(TypeSelector ts)
        {
            _constraints.Add(new TypeConstraint(ts.Type, _navigator));
            _specificity = Specificity.Element;
        }

        public void VisitSimpleSelector(SimpleSelector ss)
        {
            ss.BaseSelector.Accept(this);
            foreach (var r in ss.RestrictingSelectors)
            {
                r.Accept(this);
            }
        }

        public void VisitPseudoClassSelector(PseudoClassSelector pcs)
        {
            _constraints.Add(new PseudoClassConstraint(pcs.Name, _navigator));
            _specificity = _specificity.PlusAttribute();
        }

        public void VisitClassSelector(ClassSelector cs)
        {
            var x = System.TimeSpan.MaxValue.Add(System.TimeSpan.Zero);
            _constraints.Add(new ClassConstraint(cs.Name, _navigator));
            _specificity = _specificity.PlusAttribute();
        }

        public IElementConstraint ToConstraint()
        {
            if (_constraints.Any())
            {
                if (_constraints.Count == 1)
                {
                    return _constraints.First();
                }
                else
                {
                    return new MultiConstraint(_constraints);
                }
            }
            else
            {
                return new NullConstraint();
            }
        }
    }
}