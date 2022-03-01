//Approved 2/28/2022
using System;
namespace EpsilonEngine
{
    public struct Rectangle
    {
        #region Public Constants
        public static readonly Rectangle Zero = new Rectangle();
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

                Width = _maxX - _minX + 1;

                Size = new Point(Width, Height);
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

                Height = _maxY - _minY + 1;

                Size = new Point(Width, Height);
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

                Width = _maxX - _minX + 1;

                Size = new Point(Width, Height);
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

                Height = _maxY - _minY + 1;

                Size = new Point(Width, Height);
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

                _min = value;

                Width = _maxX - _minX + 1;
                Height = _maxY - _minY + 1;

                Size = new Point(Width, Height);
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

                _max = value;

                Width = _maxX - _minX + 1;
                Height = _maxY - _minY + 1;

                Size = new Point(Width, Height);
            }
        }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public Point Size { get; private set; }
        #endregion
        #region Private Variables
        private int _minX;
        private int _minY;
        private int _maxX;
        private int _maxY;
        private Point _min;
        private Point _max;
        #endregion
        #region Public Constructors
        public Rectangle(int minX, int minY, int maxX, int maxY)
        {
            if (minX > maxX)
            {
                throw new Exception("maxX must be greater than minX.");
            }
            _minX = minX;
            _maxX = maxX;

            if (minY > maxY)
            {
                throw new Exception("maxY must be greater than minY.");
            }
            _minY = minY;
            _maxY = maxY;

            _min = new Point(_minX, _minY);
            _max = new Point(_maxX, _maxY);

            Width = _maxX - _minX + 1;
            Height = _maxY - _minY + 1;

            Size = new Point(Width, Height);
        }
        public Rectangle(Point min, Point max)
        {
            if (min.X > max.X || min.Y > max.Y)
            {
                throw new Exception("max must be greater or equal to than min.");
            }

            _min = min;
            _max = max;

            _minX = _min.X;
            _minY = _min.Y;

            _maxX = _max.X;
            _maxY = _max.Y;

            Width = _maxX - _minX + 1;
            Height = _maxY - _minY + 1;

            Size = new Point(Width, Height);
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
            return a._x >= _minX && a._y >= _minY && a._x <= _maxX && a._y <= _maxY;
        }
        public bool Incapsulates(Rectangle a)
        {
            return a._minX >= _minX && a._minY >= _minY && a._maxX <= _maxX && a._maxY <= _maxY;
        }
        public bool Overlaps(Rectangle a)
        {
            return a._minX <= _maxX && a._minY <= _maxY && a._maxX >= _minX && a._maxY >= _minY;
        }
        #endregion
    }
}