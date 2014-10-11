namespace SubCSS.Ast
{
    public interface ISelectorVisitor
    {
        void VisitUniversalSelector(UniversalSelector us);
        void VisitTypeSelector(TypeSelector ts);
        void VisitSimpleSelector(SimpleSelector ss);
        void VisitPseudoClassSelector(PseudoClassSelector pcs);
        void VisitClassSelector(ClassSelector cs);
    }
}