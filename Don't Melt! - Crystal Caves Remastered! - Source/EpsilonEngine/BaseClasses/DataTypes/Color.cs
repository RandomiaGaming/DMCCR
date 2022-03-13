//Approved 3/1/2022
namespace EpsilonEngine
{
    public struct Color
    {
        #region Public Constants
        public static readonly Color White = new Color(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
        public static readonly Color Black = new Color(byte.MinValue, byte.MinValue, byte.MinValue, byte.MaxValue);

        public static readonly Color Transparent = new Color();
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
        public byte R;
        public byte G;
        public byte B;
        public byte A;
        #endregion
        #region Public Constructors
        public Color(byte r, byte g, byte b, byte a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }
        public Color(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
            A = byte.MaxValue;
        }
        #endregion
        #region Public Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.Color({R}, {G}, {B}, {A})";
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
                return R == a.R && G == a.G && B == a.B && A == a.A;
            }
        }
        #endregion
        #region Public Operators
        public static bool operator ==(Color a, Color b)
        {
            return a.R == b.R && a.G == b.G && a.B == b.B && a.A == b.A;
        }
        public static bool operator !=(Color a, Color b)
        {
            return a.R != b.R || a.G != b.G || a.B != b.B || a.A != b.A;
        }
        #endregion
        #region Public Static Methods
        public static uint Pack(Color source)
        {
            return System.BitConverter.ToUInt32(new byte[4] { source.R, source.G, source.B, source.A }, 0);
        }
        public static Color Unpack(uint source)
        {
            byte[] sourceBytes = System.BitConverter.GetBytes(source);
            return new Color(sourceBytes[0], sourceBytes[1], sourceBytes[2], sourceBytes[3]);
        }
        #endregion
    }
}