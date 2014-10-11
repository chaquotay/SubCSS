using System.Collections.Generic;
using System.Linq;

namespace SubCSS.Matcher
{
    internal class MultiConstraint : IElementConstraint
    {
        private readonly List<IElementConstraint> _constraints;

        public MultiConstraint(IEnumerable<IElementConstraint>  constraints)
        {
            _constraints = constraints.ToList();
        }

        public bool Violates(object o)
        {
            return !_constraints.TrueForAll(c => !c.Violates(o));
        }
    }
}