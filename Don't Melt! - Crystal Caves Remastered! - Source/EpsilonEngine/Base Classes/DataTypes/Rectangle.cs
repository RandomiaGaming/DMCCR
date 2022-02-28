using System;
namespace EpsilonEngine
{
    public struct Rectangle
    {
        #region Public Constants
        public static readonly Rectangle Zero = new Rectangle(0, 0, 0, 0);
        public static readonly Rectangle One = new Rectangle(0, 0, 1, 1);
        public static readonly Rectangle NegativeOne = new Rectangle(-1, -1, 0, 0);

        public static readonly Rectangle UpRight = new Rectangle(0, 0, 1, 1);
        public static readonly Rectangle UpLeft = new Rectangle(-1, 0, 0, 1);
        public static readonly Rectangle DownRight = new Rectangle(0, -1, 1, 0);
        public static readonly Rectangle DownLeft = new Rectangle(-1, -1, 0, 0);
        #endregion
        #region Public Variables
        public int MinX
        {
            get
            {
                return _minX;
            }
            set
            {
                if (value > _minX)
                {
                    throw new Exception("MinX must be less than or equal to MaxX.");
                }

                _minX = value;

                _min.X = _minX;

                _width = _maxX - _minX + 1;

                _size.X = _width;
            }
        }
        public int MinY
        {
            get
            {
                return _minY;
            }
            set
            {
                if (value > _maxY)
                {
                    throw new Exception("MinY must be less than or equal to MaxY.");
                }

                _minY = value;

                _min.Y = _minY;

                _height = _maxY - _minY + 1;

                _size.Y = _height;
            }
        }
        public int MaxX
        {
            get
            {
                return _maxX;
            }
            set
            {
                if (value < _minX)
                {
                    throw new Exception("MaxX must be greater than or equal to MinX.");
                }

                _maxX = value;

                _max.X = _maxX;

                _width = _maxX - _minX + 1;

                _size.X = _width;
            }
        }
        public int MaxY
        {
            get
            {
                return _maxY;
            }
            set
            {
                if (value < _minY)
                {
                    throw new Exception("MaxY must be greater than or equal to MinY.");
                }

                _maxY = value;

                _max.Y = _maxY;

                _height = _maxY - _minY + 1;

                _size.Y = _height;
            }
        }
        public Point Min
        {
            get
            {
                return _min;
            }
            set
            {
                if (value.X > _maxX || value.Y > _maxY)
                {
                    throw new Exception("Min must be less than or equal to Max.");
                }

                _minX = value.X;
                _minY = value.Y;

                _min.X = _minX;
                _min.Y = _minY;

                _width = _maxX - _minX + 1;
                _height = _maxY - _minY + 1;

                _size.X = _width;
                _size.Y = _height;
            }
        }
        public Point Max
        {
            get
            {
                return _max;
            }
            set
            {
                if (value.X < _minX || value.Y < _minY)
                {
                    throw new Exception("Max must be greater than or equal to Min.");
                }

                _maxX = value.X;
                _maxY = value.Y;

                _max.X = _minX;
                _max.Y = _minY;

                _width = _maxX - _minX + 1;
                _height = _maxY - _minY + 1;

                _size.X = _width;
                _size.Y = _height;
            }
        }
        public int Width
        {
            get
            {
                return _width;
            }
        }
        public int Height
        {
            get
            {
                return _height;
            }
        }
        public Point Size
        {
            get
            {
                return _size;
            }
        }
        #endregion
        #region Internal Variables
        internal int _minX;
        internal int _minY;
        internal int _maxX;
        internal int _maxY;
        internal Point _min;
        internal Point _max;
        internal int _width;
        internal int _height;
        internal Point _size;
        #endregion
        #region Public Constructors
        public Rectangle(int minX, int minY, int maxX, int maxY)
        {
            if (minX > maxX)
            {
                throw new Exception("MaxX must be greater than MinX.");
            }
            _minX = minX;
            _maxX = maxX;

            if (minY > maxY)
            {
                throw new Exception("MaxY must be greater than MinY.");
            }
            _minY = minY;
            _maxY = maxY;

            _min = new Point(_minX, _minY);
            _max = new Point(_maxX, _maxY);

            _width = _maxX - _minX + 1;
            _height = _maxY - _minY + 1;

            _size = new Point(_width, _height);
        }
        public Rectangle(Point min, Point max)
        {
            if (min.X > max.X || min.Y > max.Y)
            {
                throw new Exception("Max must be greater or equal to than Min.");
            }

            _minX = min.X;
            _minY = min.Y;

            _maxX = max.X;
            _maxY = max.Y;

            _min = new Point(_minX, _minY);
            _max = new Point(_maxX, _maxY);

            _width = _maxX - _minX + 1;
            _height = _maxY - _minY + 1;

            _size = new Point(_width, _height);
        }
        #endregion
        #region Public Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.Rectangle({_minX}, {_minY}, {_maxX}, {_maxY})";
        }
        public override bool Equals(object obj)
        {
            if (obj is null || obj.GetType() != typeof(Rectangle))
            {
                return false;
            }
            else
            {
                Rectangle a = (Rectangle)obj;
                return _minX == a._minX && _minY == a._minY && _maxX == a._maxX && _maxY == a._maxY;
            }
        }
        #endregion
        #region Public Operators
        public static bool operator ==(Rectangle a, Rectangle b)
        {
            return a._minX == b._minX && a._minY == b._minY && a._maxX == b._maxX && a._maxY == b._maxY;
        }
        public static bool operator !=(Rectangle a, Rectangle b)
        {
            return a._minX != b._minX || a._minY != b._minY || a._maxX != b._maxX || a._maxY != b._maxY;
        }
        #endregion
        #region Public Methods
        public bool Incapsulates(Point a)
        {
            return a._x >= _minX && a._x <= _maxX && a._y >= _minY && a._y <= _maxY;
        }
        public bool Incapsulates(Rectangle a)
        {
            return a._maxY <= _maxY && a._minY >= _minY && a._maxX <= _maxX && a._minX >= _minX;
        }
        public bool Overlaps(Rectangle a)
        {
            return _maxX >= a._minX && _minX <= a._maxX && _maxY >= a._minY && _minY <= a._maxY;
        }
        #endregion
    }
}