namespace EpsilonEngine
{
    public struct Vector
    {
        #region Public Constants
        public static readonly Vector Zero = new Vector(0.0f, 0.0f);
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
        public float X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
            }
        }
        public float Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
        }
        #endregion
        #region Internal Variables
        internal float _x;
        internal float _y;
        #endregion
        #region Public Constructors
        public Vector(float x, float y)
        {
            _x = x;
            _y = y;
        }
        public Vector(Point source)
        {
            _x = source.X;
            _y = source.Y;
        }
        #endregion
        #region Public Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.Vector({_x}, {_y})";
        }
        public override bool Equals(object obj)
        {
            if (obj is null || obj.GetType() != typeof(Vector))
            {
                return false;
            }
            else
            {
                Vector a = (Vector)obj;
                return _x == a._x && _y == a._y;
            }
        }
        #endregion
        #region Public Operators
        public static bool operator ==(Vector a, Vector b)
        {
            return a._x == b._x && a._y == b._y;
        }
        public static bool operator !=(Vector a, Vector b)
        {
            return a._x != b._x || a._y != b._y;
        }
        public static Vector operator +(Vector a, Vector b)
        {
            a._x += b._x;
            a._y += b._y;
            return a;
        }
        public static Vector operator -(Vector a, Vector b)
        {
            a._x -= b._x;
            a._y -= b._y;
            return a;
        }
        public static Vector operator *(Vector a, Vector b)
        {
            a._x *= b._x;
            a._y *= b._y;
            return a;
        }
        public static Vector operator /(Vector a, Vector b)
        {
            a._x /= b._x;
            a._y /= b._y;
            return a;
        }
        public static Vector operator +(Vector a, int b)
        {
            a._x += b;
            a._y += b;
            return a;
        }
        public static Vector operator -(Vector a, int b)
        {
            a._x -= b;
            a._y -= b;
            return a;
        }
        public static Vector operator *(Vector a, int b)
        {
            a._x *= b;
            a._y *= b;
            return a;
        }
        public static Vector operator /(Vector a, int b)
        {
            a._x /= b;
            a._y /= b;
            return a;
        }
        public static Vector operator -(Vector a)
        {
            a._x = -a._x;
            a._y = -a._y;
            return a;
        }
        public static explicit operator Vector(Point source)
        {
            return new Vector(source._x, source._y);
        }
        #endregion
    }
}