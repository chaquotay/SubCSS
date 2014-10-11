using NUnit.Framework;
using SubCSS.Ast;
using SubCSS.Matcher;

namespace SubCSS.Tests.Matcher
{
    [TestFixture]
    public class ConstraintBuilderTest
    {
        [Test]
        public void TestUniversalSpecificity()
        {
            var cb = new ConstraintBuilder(null);
            cb.VisitUniversalSelector(new UniversalSelector());
            
            Assert.AreEqual(Specificity.Zero, cb.Specificity);
        }

        [Test]
        public void TestElementSpecificity()
        {
            var cb = new ConstraintBuilder(null);
            cb.VisitTypeSelector(new TypeSelector("foo"));
            
            Assert.AreEqual(Specificity.Element, cb.Specificity);
        }

        [Test]
        public void TestPseudoClassSpecificity()
        {
            var cb = new ConstraintBuilder(null);
            cb.VisitPseudoClassSelector(new PseudoClassSelector("foo"));
            
            Assert.AreEqual(Specificity.Zero.PlusAttribute(), cb.Specificity);
        }

        [Test]
        public void TestClassSpecificity()
        {
            var cb = new ConstraintBuilder(null);
            cb.VisitClassSelector(new ClassSelector("foo"));
            
            Assert.AreEqual(Specificity.Zero.PlusAttribute(), cb.Specificity);
        }
    }
}
