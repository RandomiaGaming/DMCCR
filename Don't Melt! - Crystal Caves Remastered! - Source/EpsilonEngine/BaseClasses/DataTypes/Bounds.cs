//Approved 3/1/2022
namespace EpsilonEngine
{
    public struct Bounds
    {
        #region Public Constants
        public static readonly Bounds Zero = new Bounds();
        public static readonly Bounds One = new Bounds(0.0f, 0.0f, 1.0f, 1.0f);
        public static readonly Bounds NegativeOne = new Bounds(-1.0f, -1.0f, 0.0f, 0.0f);

        public static readonly Bounds UpRight = new Bounds(0.0f, 0.0f, 1.0f, 1.0f);
        public static readonly Bounds UpLeft = new Bounds(-1.0f, 0.0f, 0.0f, 1.0f);
        public static readonly Bounds DownRight = new Bounds(0.0f, -1.0f, 1.0f, 0.0f);
        public static readonly Bounds DownLeft = new Bounds(-1.0f, -1.0f, 0.0f, 0.0f);
        #endregion
        #region Public Variables
        public double MinX
        {
            get
            {
                return _minX;
            }
            set
            {
                if (value < _minX)
                {
                    throw new System.Exception("MinX must be less than or equal to MaxX.");
                }

                _minX = value;

                _min.X = _minX;

                Width = _maxX - _minX;

                Size = new Vector(Width, Height);
            }
        }
        public double MinY
        {
            get
            {
                return _minY;
            }
            set
            {
                if (value > _maxY)
                {
                    throw new System.Exception("MinY must be less than or equal to MaxY.");
                }

                _minY = value;

                _min.Y = _minY;

                Height = _maxY - _minY;

                Size = new Vector(Width, Height);
            }
        }
        public double MaxX
        {
            get
            {
                return _maxX;
            }
            set
            {
                if (value < _minX)
                {
                    throw new System.Exception("MaxX must be greater than or equal to MinX.");
                }

                _maxX = value;

                _max.X = _maxX;

                Width = _maxX - _minX;

                Size = new Vector(Width, Height);
            }
        }
        public double MaxY
        {
            get
            {
                return _maxY;
            }
            set
            {
                if (value < _minY)
                {
                    throw new System.Exception("MaxY must be greater than or equal to MinY.");
                }

                _maxY = value;

                _max.Y = _maxY;

                Height = _maxY - _minY;

                Size = new Vector(Width, Height);
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
                    throw new System.Exception("Min must be less than or equal to Max.");
                }

                _minX = value.X;
                _minY = value.Y;

                _min = value;

                Width = _maxX - _minX;
                Height = _maxY - _minY;

                Size = new Vector(Width, Height);
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
                    throw new System.Exception("Max must be greater than or equal to Min.");
                }

                _maxX = value.X;
                _maxY = value.Y;

                _max = value;

                Width = _maxX - _minX;
                Height = _maxY - _minY;

                Size = new Vector(Width, Height);
            }
        }
        public double Width { get; private set; }
        public double Height { get; private set; }
        public Vector Size { get; private set; }
        #endregion
        #region Private Variables
        private double _minX;
        private double _minY;
        private double _maxX;
        private double _maxY;
        private Vector _min;
        private Vector _max;
        #endregion
        #region Public Constructors
        public Bounds(double minX, double minY, double maxX, double maxY)
        {
            if (minX > maxX)
            {
                throw new System.Exception("MaxX must be greater than MinX.");
            }
            _minX = minX;
            _maxX = maxX;
            
            if (minY > maxY)
            {
                throw new System.Exception("MaxY must be greater than MinY.");
            }
            _minY = minY;
            _maxY = maxY;

            _min = new Vector(_minX, _minY);
            _max = new Vector(_maxX, _maxY);

            Width = _maxX - _minX;
            Height = _maxY - _minY;

            Size = new Vector(Width, Height);
        }
        public Bounds(Vector min, Vector max)
        {
            if (min.X > max.X || min.Y > max.Y)
            {
                throw new System.Exception("Max must be greater or equal to than Min.");
            }

            _min = min;
            _max = max;

            _minX = min.X;
            _minY = min.Y;

            _maxX = max.X;
            _maxY = max.Y;

            Width = _maxX - _minX;
            Height = _maxY - _minY;

            Size = new Vector(Width, Height);
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
            return a.X >= _minX && a.Y >= _minY && a.X <= _maxX && a.Y <= _maxY;
        }
        public bool Incapsulates(Bounds a)
        {
            return a._minX >= _minX && a._minY >= _minY && a._maxX <= _maxX && a._maxY <= _maxY;
        }
        public bool Overlaps(Bounds a)
        {
            return a._minX <= _maxX && a._minY <= _maxY && a._maxX >= _minX && a._maxY >= _minY;
        }
        #endregion
    }
}