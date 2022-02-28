namespace EpsilonEngine
{
    public struct Point
    {
        #region Public Constants
        public static readonly Point Zero = new Point(0, 0);
        public static readonly Point One = new Point(1, 1);
        public static readonly Point NegativeOne = new Point(-1, -1);

        public static readonly Point Up = new Point(0, 1);
        public static readonly Point Down = new Point(0, -1);
        public static readonly Point Right = new Point(1, 0);
        public static readonly Point Left = new Point(-1, 0);

        public static readonly Point UpRight = new Point(1, 1);
        public static readonly Point UpLeft = new Point(-1, 1);
        public static readonly Point DownRight = new Point(1, -1);
        public static readonly Point DownLeft = new Point(-1, -1);
        #endregion
        #region Public Varialbes
        public int X
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
        public int Y
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
        internal int _x;
        internal int _y;
        #endregion
        #region Public Constructors
        public Point(int x, int y)
        {
            _x = x;
            _y = y;
        }
        public Point(Vector source)
        {
            _x = (int)source.X;
            _y = (int)source.Y;
        }
        #endregion
        #region Public Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.Point({_x}, {_y})";
        }
        public override bool Equals(object obj)
        {
            if (obj is null || obj.GetType() != typeof(Point))
            {
                return false;
            }
            else
            {
                Point a = (Point)obj;
                return _x == a._x && _y == a._y;
            }
        }
        #endregion
        #region Public Operators
        public static bool operator ==(Point a, Point b)
        {
            return a._x == b._x && a._y == b._y;
        }
        public static bool operator !=(Point a, Point b)
        {
            return a._x != b._x || a._y != b._y;
        }
        public static Point operator +(Point a, Point b)
        {
            a._x += b._x;
            a._y += b._y;
            return a;
        }
        public static Point operator -(Point a, Point b)
        {
            a._x -= b._x;
            a._y -= b._y;
            return a;
        }
        public static Point operator *(Point a, Point b)
        {
            a._x *= b._x;
            a._y *= b._y;
            return a;
        }
        public static Point operator /(Point a, Point b)
        {
            a._x /= b._x;
            a._y /= b._y;
            return a;
        }
        public static Point operator +(Point a, int b)
        {
            a._x += b;
            a._y += b;
            return a;
        }
        public static Point operator -(Point a, int b)
        {
            a._x -= b;
            a._y -= b;
            return a;
        }
        public static Point operator *(Point a, int b)
        {
            a._x *= b;
            a._y *= b;
            return a;
        }
        public static Point operator /(Point a, int b)
        {
            a._x /= b;
            a._y /= b;
            return a;
        }
        public static Point operator -(Point a)
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