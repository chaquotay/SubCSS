using System;

namespace SubCSS.Ast
{
    public class TypeSelector : BaseSelector
    {
        protected bool Equals(TypeSelector other)
        {
            return other!=null && string.Equals(_type, other._type);
        }

        public override int GetHashCode()
        {
            return (_type != null ? _type.GetHashCode() : 0);
        }

        public override string ToCssString()
        {
            return _type;
        }

        public override void Accept(ISelectorVisitor visitor)
        {
            visitor.VisitTypeSelector(this);
        }

        private readonly string _type;

        public TypeSelector(string type)
        {
            if(string.IsNullOrEmpty(type))
                throw new ArgumentNullException("type");

            _type = type;
        }

        public string Type
        {
            get { return _type; }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as TypeSelector);
        }

        public override string ToString()
        {
            return _type;
        }
    }
}