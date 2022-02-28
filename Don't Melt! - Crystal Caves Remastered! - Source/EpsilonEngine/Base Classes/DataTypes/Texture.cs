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
        public Game Game
        {
            get
            {
                return _game;
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
        #endregion
        #region Internal Variables
        internal Game _game = null;

        internal int _width = 0;
        internal int _height = 0;
        internal int _heightMinusOne = 0;
        internal int _dataLength = 0;

        internal Microsoft.Xna.Framework.Graphics.Texture2D _XNABase = null;
        internal Microsoft.Xna.Framework.Rectangle _XNADrawRectangle = new Microsoft.Xna.Framework.Rectangle();
        internal Microsoft.Xna.Framework.Color[] _XNAColorData = null;

        internal Color[] _colorData = null;
        #endregion
        #region Constructors
        public Texture(Game game, int width, int height)
        {
            if (game is null)
            {
                throw new Exception("game cannot be null.");
            }
            _game = game;

            if (width <= 0)
            {
                throw new Exception("width must be greater than 0.");
            }
            if (width > MaxWidth)
            {
                throw new Exception("width must be less than MaxWidth.");
            }
            _width = width;

            if (height <= 0)
            {
                throw new Exception("height must be greater than 0.");
            }
            if (height > MaxHeight)
            {
                throw new Exception("height must be less than MaxHeight.");
            }
            _height = height;

            _heightMinusOne = _height - 1;

            _XNADrawRectangle = new Microsoft.Xna.Framework.Rectangle(0, 0, _width, _height);

            _XNABase = new Microsoft.Xna.Framework.Graphics.Texture2D(_game.GameInterface.GraphicsDevice, _width, _height);

            _dataLength = _width * _height;

            _XNAColorData = new Microsoft.Xna.Framework.Color[_dataLength];

            _colorData = new Color[_dataLength];
        }
        public Texture(Game game, int width, int height, Color[] data)
        {
            if (game is null)
            {
                throw new Exception("game cannot be null.");
            }
            _game = game;

            if (width <= 0)
            {
                throw new Exception("width must be greater than 0.");
            }
            if (width > MaxWidth)
            {
                throw new Exception("width must be less than MaxWidth.");
            }
            _width = width;

            if (height <= 0)
            {
                throw new Exception("height must be greater than 0.");
            }
            if (height > MaxHeight)
            {
                throw new Exception("height must be less than MaxHeight.");
            }
            _height = height;

            _heightMinusOne = _height - 1;

            _XNADrawRectangle = new Microsoft.Xna.Framework.Rectangle(0, 0, _width, _height);

            _XNABase = new Microsoft.Xna.Framework.Graphics.Texture2D(_game.GameInterface.GraphicsDevice, _width, _height);

            _dataLength = _width * _height;

            if(data is null)
            {
                throw new Exception("data cannot be null.");
            }
            if(data.Length != _dataLength)
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
            _game = game;

            if(sourceFilePath is null)
            {
                throw new Exception("sourceFilePath cannot be null.");
            }
            if (!File.Exists(sourceFilePath))
            {
                throw new Exception("sourceFilePath does not exist.");
            }

            try
            {
                _XNABase = Microsoft.Xna.Framework.Graphics.Texture2D.FromFile(_game.GameInterface.GraphicsDevice, sourceFilePath);
            }
            catch
            {
                throw new Exception("texture could not be loaded from filePath.");
            }

            _width = _XNABase.Width;
            _height = _XNABase.Height;

            if (_width <= 0)
            {
                throw new Exception("width must be greater than 0.");
            }
            if (_width > MaxWidth)
            {
                throw new Exception("width must be less than MaxWidth.");
            }

            if (_height <= 0)
            {
                throw new Exception("height must be greater than 0.");
            }
            if (_height > MaxHeight)
            {
                throw new Exception("height must be less than MaxHeight.");
            }

            _heightMinusOne = _height - 1;

            _XNADrawRectangle = new Microsoft.Xna.Framework.Rectangle(0, 0, _width, _height);

            _dataLength = _width * _height;

            _XNAColorData = new Microsoft.Xna.Framework.Color[_dataLength];

            _colorData = new Color[_dataLength];

            _XNABase.GetData(_XNAColorData);

            for (int i = 0; i < _dataLength; i++)
            {
                Microsoft.Xna.Framework.Color color = _XNAColorData[i];
                _colorData[i]._r = color.R;
                _colorData[i]._g = color.G;
                _colorData[i]._b = color.B;
                _colorData[i]._a = color.A;
            }

            _XNABase.SetData(_XNAColorData);
        }
        public Texture(Game game, Stream sourceStream)
        {
            if (game is null)
            {
                throw new Exception("game cannot be null.");
            }
            _game = game;

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
                _XNABase = Microsoft.Xna.Framework.Graphics.Texture2D.FromStream(_game.GameInterface.GraphicsDevice, sourceStream);
            }
            catch
            {
                throw new Exception("texture could not be loaded from filePath.");
            }

            _width = _XNABase.Width;
            _height = _XNABase.Height;

            if (_width <= 0)
            {
                throw new Exception("width must be greater than 0.");
            }
            if (_width > MaxWidth)
            {
                throw new Exception("width must be less than MaxWidth.");
            }

            if (_height <= 0)
            {
                throw new Exception("height must be greater than 0.");
            }
            if (_height > MaxHeight)
            {
                throw new Exception("height must be less than MaxHeight.");
            }

            _heightMinusOne = _height - 1;

            _XNADrawRectangle = new Microsoft.Xna.Framework.Rectangle(0, 0, _width, _height);

            _dataLength = _width * _height;

            _XNAColorData = new Microsoft.Xna.Framework.Color[_dataLength];

            _colorData = new Color[_dataLength];

            _XNABase.GetData(_XNAColorData);

            for (int i = 0; i < _dataLength; i++)
            {
                Microsoft.Xna.Framework.Color color = _XNAColorData[i];
                _colorData[i]._r = color.R;
                _colorData[i]._g = color.G;
                _colorData[i]._b = color.B;
                _colorData[i]._a = color.A;
            }

            _XNABase.SetData(_XNAColorData);
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
            if (x >= _width)
            {
                throw new Exception("x must be less than width.");
            }

            if (y < 0)
            {
                throw new Exception("y must be greater than or equal to 0.");
            }
            if (y >= _height)
            {
                throw new Exception("y must be less than height.");
            }

            int targetIndex = ((_heightMinusOne - y) * _width) + x;

            _colorData[targetIndex] = color;

            _XNAColorData[targetIndex] = new Microsoft.Xna.Framework.Color(color._r, color._g, color._b, color._a);
        }
        public Color GetPixel(int x, int y)
        {
            if (x < 0)
            {
                throw new Exception("x must be greater than or equal to 0.");
            }
            if (x >= _width)
            {
                throw new Exception("x must be less than width.");
            }

            if (y < 0)
            {
                throw new Exception("y must be greater than or equal to 0.");
            }
            if (y >= _height)
            {
                throw new Exception("y must be less than height.");
            }

            return _colorData[((_heightMinusOne - y) * _width) + x];
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

            _XNAColorData = new Microsoft.Xna.Framework.Color[_dataLength];

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