using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SubCSS
{
    struct Quantity
    {
        public bool Equals(Quantity other)
        {
            return _value == other._value && _unit == other._unit;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_value.GetHashCode()*397) ^ (int) _unit;
            }
        }

        private readonly decimal _value;
        private readonly Unit _unit;

        public Quantity(decimal value, Unit unit)
        {
            _value = value;
            _unit = unit;
        }

        public override bool Equals(object obj)
        {
            return obj is Quantity && Equals((Quantity)obj);
        }

        public override string ToString()
        {
            return base.ToString(); // TODO
        }
    }

    enum Unit
    {
        Unknown,
        Point,
        Pixel,
        Centimeter,
        None
    }
}
