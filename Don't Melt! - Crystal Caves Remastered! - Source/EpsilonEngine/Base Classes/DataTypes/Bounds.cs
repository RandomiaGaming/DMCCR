using System;
namespace EpsilonEngine
{
    public struct Bounds
    {
        #region Public Constants
        public static readonly Bounds Zero = new Bounds(0.0f, 0.0f, 0.0f, 0.0f);
        public static readonly Bounds One = new Bounds(0.0f, 0.0f, 1.0f, 1.0f);
        public static readonly Bounds NegativeOne = new Bounds(-1.0f, -1.0f, 0.0f, 0.0f);

        public static readonly Bounds UpRight = new Bounds(0.0f, 0.0f, 1.0f, 1.0f);
        public static readonly Bounds UpLeft = new Bounds(-1.0f, 0.0f, 0.0f, 1.0f);
        public static readonly Bounds DownRight = new Bounds(0.0f, -1.0f, 1.0f, 0.0f);
        public static readonly Bounds DownLeft = new Bounds(-1.0f, -1.0f, 0.0f, 0.0f);
        #endregion
        #region Public Variables
        public float MinX
        {
            get
            {
                return _minX;
            }
            set
            {
                if (value < _minX)
                {
                    throw new Exception("MinX must be less than or equal to MaxX.");
                }

                _minX = value;

                _min.X = _minX;

                _width = _maxX - _minX;

                _size.X = _width;
            }
        }
        public float MinY
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

                _height = _maxY - _minY;

                _size.Y = _height;
            }
        }
        public float MaxX
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

                _width = _maxX - _minX;

                _size.X = _width;
            }
        }
        public float MaxY
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

                _height = _maxY - _minY;

                _size.Y = _height;
            }
        }
        public Vector Min
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

                _width = _maxX - _minX;
                _height = _maxY - _minY;

                _size.X = _width;
                _size.Y = _height;
            }
        }
        public Vector Max
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

                _width = _maxX - _minX;
                _height = _maxY - _minY;

                _size.X = _width;
                _size.Y = _height;
            }
        }
        public float Width
        {
            get
            {
                return _width;
            }
        }
        public float Height
        {
            get
            {
                return _height;
            }
        }
        public Vector Size
        {
            get
            {
                return _size;
            }
        }
        #endregion
        #region Internal Variables
        internal float _minX;
        internal float _minY;
        internal float _maxX;
        internal float _maxY;
        internal Vector _min;
        internal Vector _max;
        internal float _width;
        internal float _height;
        internal Vector _size;
        #endregion
        #region Public Constructors
        public Bounds(float minX, float minY, float maxX, float maxY)
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

            _min = new Vector(_minX, _minY);
            _max = new Vector(_maxX, _maxY);

            _width = _maxX - _minX;
            _height = _maxY - _minY;

            _size = new Vector(_width, _height);
        }
        public Bounds(Vector min, Vector max)
        {
            if (min.X > max.X || min.Y > max.Y)
            {
                throw new Exception("Max must be greater or equal to than Min.");
            }

            _minX = min.X;
            _minY = min.Y;

            _maxX = max.X;
            _maxY = max.Y;

            _min = new Vector(_minX, _minY);
            _max = new Vector(_maxX, _maxY);

            _width = _maxX - _minX;
            _height = _maxY - _minY;

            _size = new Vector(_width, _height);
        }
        #endregion
        #region Public Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.Bounds({_minX}, {_minY}, {_maxX}, {_maxY})";
        }
        public override bool Equals(object obj)
        {
            if (obj is null || obj.GetType() != typeof(Bounds))
            {
                return false;
            }
            else
            {
                Bounds a = (Bounds)obj;
                return _minX == a._minX && _minY == a._minY && _maxX == a._maxX && _maxY == a._maxY;
            }
        }
        #endregion
        #region Public Operators
        public static bool operator ==(Bounds a, Bounds b)
        {
            return a._minX == b._minX && a._minY == b._minY && a._maxX == b._maxX && a._maxY == b._maxY;
        }
        public static bool operator !=(Bounds a, Bounds b)
        {
            return a._minX != b._minX || a._minY != b._minY || a._maxX != b._maxX || a._maxY != b._maxY;
        }
        #endregion
        #region Public Methods
        public bool Incapsulates(Vector a)
        {
            return a._x >= _minX && a._x <= _maxX && a._y >= _minY && a._y <= _maxY;
        }
        public bool Incapsulates(Bounds a)
        {
            return a._maxY <= _maxY && a._minY >= _minY && a._maxX <= _maxX && a._minX >= _minX;
        }
        public bool Overlaps(Bounds a)
        {
            return _maxX >= a._minX && _minX <= a._maxX && _maxY >= a._minY && _minY <= a._maxY;
        }
        #endregion
    }
}