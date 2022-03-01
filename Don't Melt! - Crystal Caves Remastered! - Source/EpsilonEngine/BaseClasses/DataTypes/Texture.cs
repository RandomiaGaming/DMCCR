//Approved 2/28/2022
using System;
using System.IO;
namespace EpsilonEngine
{
    public sealed class Texture
    {
        #region Public Constants
        public const int MaxWidth = 46340;
        public const int MaxHeight = 46340;
        #endregion
        #region Public Variables
        public Game Game { get; private set; }

        public int Width { get; private set; }
        public int Height { get; private set; }
        #endregion
        #region Internal Variables
        internal Microsoft.Xna.Framework.Graphics.Texture2D _XNABase = null;
        #endregion
        #region Private Variables
        private int _heightMinusOne;
        private int _dataLength;

        private Microsoft.Xna.Framework.Color[] _XNAColorData;
        private Color[] _colorData;
        #endregion
        #region Public Constructors
        public Texture(Game game, int width, int height)
        {
            if (game is null)
            {
                throw new Exception("game cannot be null.");
            }
            Game = game;

            if (width <= 0)
            {
                throw new Exception("width must be greater than 0.");
            }
            if (width > MaxWidth)
            {
                throw new Exception("width must be less than MaxWidth.");
            }
            Width = width;

            if (height <= 0)
            {
                throw new Exception("height must be greater than 0.");
            }
            if (height > MaxHeight)
            {
                throw new Exception("height must be less than MaxHeight.");
            }
            Height = height;

            _heightMinusOne = Height - 1;

            _XNABase = new Microsoft.Xna.Framework.Graphics.Texture2D(Game.GameInterface.GraphicsDevice, Width, Height);

            _dataLength = Width * Height;

            _XNAColorData = new Microsoft.Xna.Framework.Color[_dataLength];

            _colorData = new Color[_dataLength];
        }
        public Texture(Game game, int width, int height, Color[] data)
        {
            if (game is null)
            {
                throw new Exception("game cannot be null.");
            }
            Game = game;

            if (width <= 0)
            {
                throw new Exception("width must be greater than 0.");
            }
            if (width > MaxWidth)
            {
                throw new Exception("width must be less than MaxWidth.");
            }
            Width = width;

            if (height <= 0)
            {
                throw new Exception("height must be greater than 0.");
            }
            if (height > MaxHeight)
            {
                throw new Exception("height must be less than MaxHeight.");
            }
            Height = height;

            _heightMinusOne = Height - 1;

            _XNABase = new Microsoft.Xna.Framework.Graphics.Texture2D(Game.GameInterface.GraphicsDevice, Width, Height);

            _dataLength = Width * Height;

            if (data is null)
            {
                throw new Exception("data cannot be null.");
            }
            if (data.Length != _dataLength)
            {
                throw new Exception("data.Length must be equal to width times height.");
            }

            _colorData = (Color[])data.Clone();

            _XNAColorData = new Microsoft.Xna.Framework.Color[_dataLength];

            for (int i = 0; i < _dataLength; i++)
            {
                Color color = _colorData[i];
                _XNAColorData[i] = new Microsoft.Xna.Framework.Color(color._r, color._g, color._b, color._a);
            }

            _XNABase.SetData(_XNAColorData);
        }
        public Texture(Game game, string sourceFilePath)
        {
            if (game is null)
            {
                throw new Exception("game cannot be null.");
            }
            Game = game;

            if (sourceFilePath is null)
            {
                throw new Exception("sourceFilePath cannot be null.");
            }
            if (!File.Exists(sourceFilePath))
            {
                throw new Exception("sourceFilePath does not exist.");
            }

            try
            {
                _XNABase = Microsoft.Xna.Framework.Graphics.Texture2D.FromFile(Game.GameInterface.GraphicsDevice, sourceFilePath);
            }
            catch
            {
                throw new Exception("texture could not be loaded from filePath.");
            }

            Width = _XNABase.Width;
            Height = _XNABase.Height;

            if (Width <= 0)
            {
                throw new Exception("width must be greater than 0.");
            }
            if (Width > MaxWidth)
            {
                throw new Exception("width must be less than MaxWidth.");
            }

            if (Height <= 0)
            {
                throw new Exception("height must be greater than 0.");
            }
            if (Height > MaxHeight)
            {
                throw new Exception("height must be less than MaxHeight.");
            }

            _heightMinusOne = Height - 1;

            _dataLength = Width * Height;

            _XNAColorData = new Microsoft.Xna.Framework.Color[_dataLength];

            _colorData = new Color[_dataLength];

            _XNABase.GetData(_XNAColorData);

            for (int i = 0; i < _dataLength; i++)
            {
                Microsoft.Xna.Framework.Color color = _XNAColorData[i];
                _colorData[i] = new Color(color.R, color.G, color.B, color.A);
            }
        }
        public Texture(Game game, Stream sourceStream)
        {
            if (game is null)
            {
                throw new Exception("game cannot be null.");
            }
            Game = game;

            if (sourceStream is null)
            {
                throw new Exception("sourceStream cannot be null.");
            }
            if (!sourceStream.CanRead)
            {
                throw new Exception("sourceStream must be readable.");
            }

            try
            {
                _XNABase = Microsoft.Xna.Framework.Graphics.Texture2D.FromStream(Game.GameInterface.GraphicsDevice, sourceStream);
            }
            catch
            {
                throw new Exception("texture could not be loaded from filePath.");
            }

            Width = _XNABase.Width;
            Height = _XNABase.Height;

            if (Width <= 0)
            {
                throw new Exception("width must be greater than 0.");
            }
            if (Width > MaxWidth)
            {
                throw new Exception("width must be less than MaxWidth.");
            }

            if (Height <= 0)
            {
                throw new Exception("height must be greater than 0.");
            }
            if (Height > MaxHeight)
            {
                throw new Exception("height must be less than MaxHeight.");
            }

            _heightMinusOne = Height - 1;

            _dataLength = Width * Height;

            _XNAColorData = new Microsoft.Xna.Framework.Color[_dataLength];

            _colorData = new Color[_dataLength];

            _XNABase.GetData(_XNAColorData);

            for (int i = 0; i < _dataLength; i++)
            {
                Microsoft.Xna.Framework.Color color = _XNAColorData[i];
                _colorData[i] = new Color(color.R, color.G, color.B, color.A);
            }
        }
        #endregion
        #region Public Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.Texture({Width}, {Height})";
        }
        #endregion
        #region Public Methods
        public void SetPixel(int x, int y, Color color)
        {
            if (x < 0)
            {
                throw new Exception("x must be greater than or equal to 0.");
            }
            if (x >= Width)
            {
                throw new Exception("x must be less than width.");
            }

            if (y < 0)
            {
                throw new Exception("y must be greater than or equal to 0.");
            }
            if (y >= Height)
            {
                throw new Exception("y must be less than height.");
            }

            int targetIndex = ((_heightMinusOne - y) * Width) + x;

            _colorData[targetIndex] = color;

            _XNAColorData[targetIndex] = new Microsoft.Xna.Framework.Color(color._r, color._g, color._b, color._a);
        }
        public Color GetPixel(int x, int y)
        {
            if (x < 0)
            {
                throw new Exception("x must be greater than or equal to 0.");
            }
            if (x >= Width)
            {
                throw new Exception("x must be less than width.");
            }

            if (y < 0)
            {
                throw new Exception("y must be greater than or equal to 0.");
            }
            if (y >= Height)
            {
                throw new Exception("y must be less than height.");
            }

            return _colorData[((_heightMinusOne - y) * Width) + x];
        }
        public void SetData(Color[] data)
        {
            if (data is null)
            {
                throw new Exception("data cannot be null.");
            }
            if (data.Length != _dataLength)
            {
                throw new Exception("data.Length must be equal to width times height.");
            }

            _colorData = (Color[])data.Clone();

            for (int i = 0; i < _dataLength; i++)
            {
                Color color = _colorData[i];
                _XNAColorData[i] = new Microsoft.Xna.Framework.Color(color._r, color._g, color._b, color._a);
            }
        }
        public Color[] GetData()
        {
            return (Color[])_colorData.Clone();
        }
        public void Clear(Color color)
        {
            _colorData[0] = color;

            _XNAColorData[0] = new Microsoft.Xna.Framework.Color(color._r, color._g, color._b, color._a);

            if (_dataLength == 1)
            {
                return;
            }

            int halfDataLength = _dataLength / 2;

            int i = 1;

            while (i < halfDataLength)
            {
                Array.Copy(_colorData, 0, _colorData, i, i);
                Array.Copy(_XNAColorData, 0, _XNAColorData, i, i);
                i = i * 2;
            }

            if (i != _dataLength)
            {
                Array.Copy(_colorData, 0, _colorData, i, _dataLength - i);
                Array.Copy(_XNAColorData, 0, _XNAColorData, i, _dataLength - i);
            }
        }
        public void Apply()
        {
            _XNABase.SetData(_XNAColorData);
        }
        #endregion
    }
}