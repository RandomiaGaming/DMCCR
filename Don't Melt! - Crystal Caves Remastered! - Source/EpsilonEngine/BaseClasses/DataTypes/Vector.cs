//Approved 07/09/2022
namespace EpsilonEngine
{
    public struct Vector
    {
        #region Public Constants
        public static readonly Vector Zero = new Vector();
        public static readonly Vector One = new Vector(1.0f, 1.0f);
        public static readonly Vector NegativeOne = new Vector(-1.0f, -1.0f);

        public static readonly Vector Up = new Vector(0.0f, 1.0f);
        public static readonly Vector Down = new Vector(0.0f, -1.0f);
        public static readonly Vector Right = new Vector(1.0f, 0.0f);
        public static readonly Vector Left = new Vector(-1.0f, 0.0f);

        public static readonly Vector UpRight = new Vector(1.0f, 1.0f);
        public static readonly Vector UpLeft = new Vector(-1.0f, 1.0f);
        public static readonly Vector DownRight = new Vector(1.0f, -1.0f);
        public static readonly Vector DownLeft = new Vector(-1.0f, -1.0f);
        #endregion
        #region Public Varialbes
        public double X;
        public double Y;
        #endregion
        #region Public Constructors
        public Vector(double x, double y)
        {
            if(x == double.NaN || x == double.NegativeInfinity || x == double.PositiveInfinity)
            {
                throw new System.Exception("x must be a real number.");
            }
            X = x;
            if (y == double.NaN || y == double.NegativeInfinity || y == double.PositiveInfinity)
            {
                throw new System.Exception("y must be a real number.");
            }
            Y = y;
        }
        #endregion
        #region Public Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.Vector({X}, {Y})";
        }
        public override bool Equals(object obj)
        {
            if (obj is null || obj.GetType() != typeof(Vector))
            {
                return false;
            }
            Vector a = (Vector)obj;
            return X == a.X && Y == a.Y;
        }
        #endregion
        #region Public Operators
        public static bool operator ==(Vector a, Vector b)
        {
            return a.X == b.X && a.Y == b.Y;
        }
        public static bool operator !=(Vector a, Vector b)
        {
            return a.X != b.X || a.Y != b.Y;
        }
        public static Vector operator +(Vector a, Vector b)
        {
            a.X += b.X;
            a.Y += b.Y;
            return a;
        }
        public static Vector operator -(Vector a, Vector b)
        {
            a.X -= b.X;
            a.Y -= b.Y;
            return a;
        }
        public static Vector operator *(Vector a, Vector b)
        {
            a.X *= b.X;
            a.Y *= b.Y;
            return a;
        }
        public static Vector operator /(Vector a, Vector b)
        {
            a.X /= b.X;
            a.Y /= b.Y;
            return a;
        }
        public static Vector operator +(Vector a, int b)
        {
            a.X += b;
            a.Y += b;
            return a;
        }
        public static Vector operator -(Vector a, int b)
        {
            a.X -= b;
            a.Y -= b;
            return a;
        }
        public static Vector operator *(Vector a, int b)
        {
            a.X *= b;
            a.Y *= b;
            return a;
        }
        public static Vector operator /(Vector a, int b)
        {
            a.X /= b;
            a.Y /= b;
            return a;
        }
        public static Vector operator -(Vector a)
        {
            a.X = -a.X;
            a.Y = -a.Y;
            return a;
        }
        public static explicit operator Vector(Point source)
        {
            return new Vector(source.X, source.Y);
        }
        #endregion
    }
}