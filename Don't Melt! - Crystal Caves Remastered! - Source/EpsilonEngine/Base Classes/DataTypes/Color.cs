using System;
namespace EpsilonEngine
{
    public struct Color
    {
        #region Public Constants
        public static readonly Color White = new Color(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
        public static readonly Color Black = new Color(byte.MinValue, byte.MinValue, byte.MinValue, byte.MaxValue);

        public static readonly Color Transparent = new Color(byte.MinValue, byte.MinValue, byte.MinValue, byte.MinValue);
        public static readonly Color TransparentWhite = new Color(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MinValue);
        public static readonly Color TransparentBlack = new Color(byte.MinValue, byte.MinValue, byte.MinValue, byte.MinValue);

        public static readonly Color Red = new Color(byte.MaxValue, byte.MinValue, byte.MinValue, byte.MaxValue);
        public static readonly Color Yellow = new Color(byte.MaxValue, byte.MaxValue, byte.MinValue, byte.MaxValue);
        public static readonly Color Green = new Color(byte.MinValue, byte.MaxValue, byte.MinValue, byte.MaxValue);
        public static readonly Color Aqua = new Color(byte.MinValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
        public static readonly Color Blue = new Color(byte.MinValue, byte.MinValue, byte.MaxValue, byte.MaxValue);
        public static readonly Color Pink = new Color(byte.MaxValue, byte.MinValue, byte.MaxValue, byte.MaxValue);

        public static readonly Color SoftRed = new Color(byte.MaxValue, 150, 150, byte.MaxValue);
        public static readonly Color SoftYellow = new Color(byte.MaxValue, byte.MaxValue, 150, byte.MaxValue);
        public static readonly Color SoftGreen = new Color(150, byte.MaxValue, 150, byte.MaxValue);
        public static readonly Color SoftAqua = new Color(150, byte.MaxValue, byte.MaxValue, byte.MaxValue);
        public static readonly Color SoftBlue = new Color(150, 150, byte.MaxValue, byte.MaxValue);
        public static readonly Color SoftPink = new Color(byte.MaxValue, 150, byte.MaxValue, byte.MaxValue);
        #endregion
        #region Public Variables
        public byte R
        {
            get
            {
                return _r;
            }
            set
            {
                _r = value;
            }
        }
        public byte G
        {
            get
            {
                return _g;
            }
            set
            {
                _g = value;
            }
        }
        public byte B
        {
            get
            {
                return _b;
            }
            set
            {
                _b = value;
            }
        }
        public byte A
        {
            get
            {
                return _a;
            }
            set
            {
                _a = value;
            }
        }
        #endregion
        #region Internal Variables
        internal byte _r;
        internal byte _g;
        internal byte _b;
        internal byte _a;
        #endregion
        #region Public Constructors
        public Color(byte r, byte g, byte b, byte a)
        {
            _r = r;
            _g = g;
            _b = b;
            _a = a;
        }
        public Color(byte r, byte g, byte b)
        {
            _r = r;
            _g = g;
            _b = b;
            _a = byte.MaxValue;
        }
        #endregion
        #region Public Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.Color({_r}, {_g}, {_b}, {_a})";
        }
        public override bool Equals(object obj)
        {
            if (obj is null || obj.GetType() != typeof(Color))
            {
                return false;
            }
            else
            {
                Color a = (Color)obj;
                return _r == a._r && _g == a._g && _b == a._b && _a == a._a;
            }
        }
        #endregion
        #region Public Operators
        public static bool operator ==(Color a, Color b)
        {
            return a._r == b._r && a._g == b._g && a._b == b._b && a._a == b._a;
        }
        public static bool operator !=(Color a, Color b)
        {
            return a._r != b._r || a._g != b._g || a._b != b._b || a._a != b._a;
        }
        #endregion
        #region Public Static Methods
        public static uint Pack(Color source)
        {
            return BitConverter.ToUInt32(new byte[4] { source._r, source._g, source._b, source._a }, 0);
        }
        public static Color Unpack(uint source)
        {
            byte[] sourceBytes = BitConverter.GetBytes(source);
            return new Color(sourceBytes[0], sourceBytes[1], sourceBytes[2], sourceBytes[3]);
        }
        #endregion
    }
}