using System;

namespace CHLang
{
    public enum NumType : int
    {
        Int = 0,
        Double = 1,
    }

    public struct Num
    {
        public NumType Type;
        private long __bits;

        public static Num Zero => new Num(0);
        public static Num One => new Num(1);

        public int IntValue
        {
            get
            {
                if (Type == NumType.Int)
                {
                    return (int)__bits;
                }
                else
                {
                    return (int)BitConverter.Int64BitsToDouble(__bits);
                }
            }
        }

        public double DoubleValue
        {
            get
            {
                if (Type == NumType.Int)
                {
                    return __bits;
                }
                else
                {
                    return BitConverter.Int64BitsToDouble(__bits);
                }
            }
        }

        public long Memory => __bits;

        public Num(int i)
        {
            Type = NumType.Int;
            __bits = i;
        }

        public Num(double d)
        {
            Type = NumType.Double;
            __bits = BitConverter.DoubleToInt64Bits(d);
        }

        public override string ToString()
        {
            if (Type == NumType.Int)
            {
                return __bits.ToString();
            }

            return BitConverter.Int64BitsToDouble(__bits).ToString();
        }

        public static Num operator +(Num a, Num b)
        {
            if (a.Type == NumType.Int && b.Type == NumType.Int)
            {
                return new Num((int)(a.__bits + b.__bits));
            }

            return new Num(a.DoubleValue + b.DoubleValue);
        }

        public static Num operator -(Num a, Num b)
        {
            if (a.Type == NumType.Int && b.Type == NumType.Int)
            {
                return new Num((int)(a.__bits - b.__bits));
            }

            return new Num(a.DoubleValue - b.DoubleValue);
        }

        public static Num operator -(Num a)
        {
            if (a.Type == NumType.Int)
            {
                return new Num((int)-a.__bits);
            }

            return new Num(-a.DoubleValue);
        }

        public static Num operator *(Num a, Num b)
        {
            if (a.Type == NumType.Int && b.Type == NumType.Int)
            {
                return new Num((int)(a.__bits * b.__bits));
            }

            return new Num(a.DoubleValue * b.DoubleValue);
        }

        public static Num operator /(Num a, Num b)
        {
            if (a.Type == NumType.Int && b.Type == NumType.Int)
            {
                return new Num((int)(a.__bits / b.__bits));
            }

            return new Num(a.DoubleValue / b.DoubleValue);
        }

        public static Num operator %(Num a, Num b)
        {
            if (a.Type == NumType.Int && b.Type == NumType.Int)
            {
                return new Num((int)(a.__bits % b.__bits));
            }

            return new Num(a.DoubleValue % b.DoubleValue);
        }

        public static bool operator >(Num a, Num b)
        {
            return a.DoubleValue > b.DoubleValue;
        }

        public static bool operator <(Num a, Num b)
        {
            return a.DoubleValue < b.DoubleValue;
        }

        public static bool operator >=(Num a, Num b)
        {
            return a.DoubleValue >= b.DoubleValue;
        }

        public static bool operator <=(Num a, Num b)
        {
            return a.DoubleValue <= b.DoubleValue;
        }

        public static bool operator ==(Num a, Num b)
        {
            if (a.Type == b.Type)
            {
                return a.__bits == b.__bits;
            }

            return a.DoubleValue == b.DoubleValue;
        }

        public static bool operator !=(Num a, Num b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return DoubleValue.GetHashCode();
        }
    }
}