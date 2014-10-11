using System;
using NUnit.Framework;

namespace SubCSS.Matcher
{
    public struct Specificity : IComparable<Specificity>, IComparable
    {
        public bool Equals(Specificity other)
        {
            return _inline == other._inline && _id == other._id && _attribute == other._attribute && _elementType == other._elementType;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = _inline;
                hashCode = (hashCode*397) ^ _id;
                hashCode = (hashCode*397) ^ _attribute;
                hashCode = (hashCode*397) ^ _elementType;
                return hashCode;
            }
        }

        private readonly int _inline;
        private readonly int _id;
        private readonly int _attribute;
        private readonly int _elementType;

        public static readonly Specificity Zero = new Specificity();

        private Specificity(int inline, int id, int attribute, int elementType)
        {
            _inline = inline;
            _id = id;
            _attribute = attribute;
            _elementType = elementType;
        }

        public int CompareTo(Specificity other)
        {
            if (Equals(other))
                return 0;

            var result = 0;
            
            if(result == 0)
                result = _inline.CompareTo(other._inline);

            if (result == 0)
                result = _id.CompareTo(other._id);

            if (result == 0)
                result = _attribute.CompareTo(other._attribute);

            if (result == 0)
                result = _elementType.CompareTo(other._elementType);

            return result;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public static readonly Specificity Element = new Specificity(0,0,0,1);
        public static readonly Specificity Inline = new Specificity(1,0,0,0);
        public int CompareTo(object obj)
        {
            if (obj is Specificity)
            {
                return CompareTo((Specificity) obj);
            }

            throw new ArgumentException();
        }

        public override string ToString()
        {
            return string.Format("({0},{1},{2},{3})", _inline, _id, _attribute, _elementType);
        }

        public Specificity PlusAttribute()
        {
            return new Specificity(_inline, _id, _attribute+1, _elementType);
        }

        public Specificity PlusId()
        {
            return new Specificity(_inline, _id+1, _attribute, _elementType);
        }
    }

    [TestFixture]
    public class SpecificityTest
    {
        [Test]
        public void TestZero()
        {
            Assert.AreEqual(Specificity.Zero, Specificity.Zero);
        }

        [Test]
        public void TestElementTypeIsMoreSpecificThanZero()
        {
            Assert.Greater(Specificity.Element, Specificity.Zero);
            Assert.Less(Specificity.Zero, Specificity.Element);
        }

        [Test]
        public void TestAttributeIsMoreSpecificThanElementType()
        {
            Assert.Greater(Specificity.Zero.PlusAttribute(), Specificity.Element);
            Assert.Less(Specificity.Element, Specificity.Zero.PlusAttribute());
        }

        [Test]
        public void TestIdIsMoreSpecificThanAttribute()
        {
            Assert.Greater(Specificity.Zero.PlusId(), Specificity.Zero.PlusAttribute());
            Assert.Less(Specificity.Zero.PlusAttribute(), Specificity.Zero.PlusId());
        }

        [Test]
        public void TestInlineIsMoreSpecificThanId()
        {
            Assert.Greater(Specificity.Inline, Specificity.Zero.PlusId());
            Assert.Less(Specificity.Zero.PlusId(), Specificity.Inline);
        }
    }
}
