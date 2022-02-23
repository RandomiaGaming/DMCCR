using System;
namespace EpsilonEngine
{
    internal sealed class UnsafeRectangle
    {
        #region Constants
        public static readonly UnsafeRectangle Zero = new UnsafeRectangle(0, 0, 0, 0);
        public static readonly UnsafeRectangle One = new UnsafeRectangle(0, 0, 1, 1);
        public static readonly UnsafeRectangle NegativeOne = new UnsafeRectangle(-1, -1, 0, 0);

        public static readonly UnsafeRectangle UpRight = new UnsafeRectangle(0, 0, 1, 1);
        public static readonly UnsafeRectangle UpLeft = new UnsafeRectangle(-1, 0, 0, 1);
        public static readonly UnsafeRectangle DownRight = new UnsafeRectangle(0, -1, 1, 0);
        public static readonly UnsafeRectangle DownLeft = new UnsafeRectangle(-1, -1, 0, 0);
        #endregion
        #region Properties
        public int MinX { get; set; } = 0;
        public int MinY { get; set; } = 0;
        public int MaxX { get; set; } = 0;
        public int MaxY { get; set; } = 0;
        #endregion
        #region Constructors
        public UnsafeRectangle(int minX, int minY, int maxX, int maxY)
        {
            MinX = minX;
            MaxX = maxX;
            MinY = minY;
            MaxY = maxY;
        }
        public UnsafeRectangle(Point min, Point max)
        {
            MinX = min.X;
            MinY = min.Y;
            MaxX = max.X;
            MaxY = max.Y;
        }
        public UnsafeRectangle(Microsoft.Xna.Framework.Rectangle source)
        {
            MinX = source.Left;
            MinY = source.Top;
            MaxX = source.Right;
            MaxY = source.Bottom;
        }
        #endregion
        #region Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.UnsafeRectangle({MinX}, {MinY}, {MaxX}, {MaxY})";
        }
        public override bool Equals(object obj)
        {
            if (obj is null || obj.GetType() != typeof(UnsafeRectangle))
            {
                return false;
            }
            else
            {
                return this == (UnsafeRectangle)obj;
            }
        }
        public static bool operator ==(UnsafeRectangle a, UnsafeRectangle b)
        {
            return (a.MinX == b.MinX) && (a.MinY == b.MinY) && (a.MaxX == b.MaxX) && (a.MaxY == b.MaxY);
        }
        public static bool operator !=(UnsafeRectangle a, UnsafeRectangle b)
        {
            return !(a == b);
        }
        #endregion
        #region Methods
        public static bool Incapsulates(UnsafeRectangle a, Point b)
        {
            return b.X >= a.MinX && b.X <= a.MaxX && b.Y >= a.MinY && b.Y <= a.MaxY;
        }
        public bool Incapsulates(Point a)
        {
            return Incapsulates(this, a);
        }
        public static bool Incapsulates(UnsafeRectangle a, UnsafeRectangle b)
        {
            return b.MaxY <= a.MaxY && b.MinY >= a.MinY && b.MaxX <= a.MaxX && b.MinX >= a.MinX;
        }
        public bool Incapsulates(UnsafeRectangle a)
        {
            return Incapsulates(this, a);
        }
        public static bool Overlaps(UnsafeRectangle a, UnsafeRectangle b)
        {
            return a.MaxX >= b.MinX && a.MinX <= b.MaxX && a.MaxY >= b.MinY && a.MinY <= b.MaxY;
        }
        public bool Overlaps(UnsafeRectangle a)
        {
            return Overlaps(this, a);
        }
        public static Microsoft.Xna.Framework.Rectangle ToXNA(UnsafeRectangle source)
        {
            return new Microsoft.Xna.Framework.Rectangle(source.MinX, source.MaxY, source.MaxX - source.MinX + 1, source.MaxY - source.MinY + 1);
        }
        public Microsoft.Xna.Framework.Rectangle ToXNA()
        {
            return ToXNA(this);
        }
        public static Rectangle FromXNA(Microsoft.Xna.Framework.Rectangle source)
        {
            return new Rectangle(source);
        }
        #endregion
    }
}