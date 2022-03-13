//Approved 3/1/2022
namespace EpsilonEngine
{
    public sealed class Texture
    {
        #region Public Variables
        public readonly Game Game;

        public readonly int Width;
        public readonly int Height;
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
                throw new System.Exception("game cannot be null.");
            }
            Game = game;

            if (width <= 0)
            {
                throw new System.Exception("width must be greater than 0.");
            }
            Width = width;

            if (height <= 0)
            {
                throw new System.Exception("height must be greater than 0.");
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
                throw new System.Exception("game cannot be null.");
            }
            Game = game;

            if (width <= 0)
            {
                throw new System.Exception("width must be greater than 0.");
            }
            Width = width;

            if (height <= 0)
            {
                throw new System.Exception("height must be greater than 0.");
            }
            Height = height;

            _heightMinusOne = Height - 1;

            _XNABase = new Microsoft.Xna.Framework.Graphics.Texture2D(Game.GameInterface.GraphicsDevice, Width, Height);

            _dataLength = Width * Height;

            if (data is null)
            {
                throw new System.Exception("data cannot be null.");
            }
            if (data.Length != _dataLength)
            {
                throw new System.Exception("data.Length must be equal to width times height.");
            }

            _colorData = (Color[])data.Clone();

            _XNAColorData = new Microsoft.Xna.Framework.Color[_dataLength];

            for (int i = 0; i < _dataLength; i++)
            {
                Color color = _colorData[i];
                _XNAColorData[i] = new Microsoft.Xna.Framework.Color(color.R, color.G, color.B, color.A);
            }

            _XNABase.SetData(_XNAColorData);
        }
        public Texture(Game game, string sourceFilePath)
        {
            if (game is null)
            {
                throw new System.Exception("game cannot be null.");
            }
            Game = game;

            if (sourceFilePath is null)
            {
                throw new System.Exception("sourceFilePath cannot be null.");
            }
            if (!System.IO.File.Exists(sourceFilePath))
            {
                throw new System.Exception("sourceFilePath does not exist.");
            }

            try
            {
                _XNABase = Microsoft.Xna.Framework.Graphics.Texture2D.FromFile(Game.GameInterface.GraphicsDevice, sourceFilePath);
            }
            catch
            {
                throw new System.Exception("texture could not be loaded from filePath.");
            }

            Width = _XNABase.Width;
            Height = _XNABase.Height;

            if (Width <= 0)
            {
                throw new System.Exception("width must be greater than 0.");
            }

            if (Height <= 0)
            {
                throw new System.Exception("height must be greater than 0.");
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
        public Texture(Game game, System.IO.Stream sourceStream)
        {
            if (game is null)
            {
                throw new System.Exception("game cannot be null.");
            }
            Game = game;

            if (sourceStream is null)
            {
                throw new System.Exception("sourceStream cannot be null.");
            }
            if (!sourceStream.CanRead)
            {
                throw new System.Exception("sourceStream must be readable.");
            }

            try
            {
                _XNABase = Microsoft.Xna.Framework.Graphics.Texture2D.FromStream(Game.GameInterface.GraphicsDevice, sourceStream);
            }
            catch
            {
                throw new System.Exception("texture could not be loaded from filePath.");
            }

            Width = _XNABase.Width;
            Height = _XNABase.Height;

            if (Width <= 0)
            {
                throw new System.Exception("width must be greater than 0.");
            }

            if (Height <= 0)
            {
                throw new System.Exception("height must be greater than 0.");
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
                throw new System.Exception("x must be greater than or equal to 0.");
            }
            if (x >= Width)
            {
                throw new System.Exception("x must be less than width.");
            }

            if (y < 0)
            {
                throw new System.Exception("y must be greater than or equal to 0.");
            }
            if (y >= Height)
            {
                throw new System.Exception("y must be less than height.");
            }

            int targetIndex = ((_heightMinusOne - y) * Width) + x;

            _colorData[targetIndex] = color;

            _XNAColorData[targetIndex] = new Microsoft.Xna.Framework.Color(color.R, color.G, color.B, color.A);
        }
        public Color GetPixel(int x, int y)
        {
            if (x < 0)
            {
                throw new System.Exception("x must be greater than or equal to 0.");
            }
            if (x >= Width)
            {
                throw new System.Exception("x must be less than width.");
            }

            if (y < 0)
            {
                throw new System.Exception("y must be greater than or equal to 0.");
            }
            if (y >= Height)
            {
                throw new System.Exception("y must be less than height.");
            }

            return _colorData[((_heightMinusOne - y) * Width) + x];
        }
        public void SetData(Color[] data)
        {
            if (data is null)
            {
                throw new System.Exception("data cannot be null.");
            }
            if (data.Length != _dataLength)
            {
                throw new System.Exception("data.Length must be equal to width times height.");
            }

            _colorData = (Color[])data.Clone();

            for (int i = 0; i < _dataLength; i++)
            {
                Color color = _colorData[i];
                _XNAColorData[i] = new Microsoft.Xna.Framework.Color(color.R, color.G, color.B, color.A);
            }
        }
        public Color[] GetData()
        {
            return (Color[])_colorData.Clone();
        }
        public void Clear(Color color)
        {
            _colorData[0] = color;

            _XNAColorData[0] = new Microsoft.Xna.Framework.Color(color.R, color.G, color.B, color.A);

            if (_dataLength == 1)
            {
                return;
            }

            int halfDataLength = _dataLength / 2;

            int i = 1;

            while (i < halfDataLength)
            {
                System.Array.Copy(_colorData, 0, _colorData, i, i);
                System.Array.Copy(_XNAColorData, 0, _XNAColorData, i, i);
                i = i * 2;
            }

            if (i != _dataLength)
            {
                System.Array.Copy(_colorData, 0, _colorData, i, _dataLength - i);
                System.Array.Copy(_XNAColorData, 0, _XNAColorData, i, _dataLength - i);
            }
        }
        public void Apply()
        {
            _XNABase.SetData(_XNAColorData);
        }
        #endregion
        #region Internal Methods
        public void SetPixelUnsafe(int x, int y, Color color)
        {
            int targetIndex = ((_heightMinusOne - y) * Width) + x;

            _colorData[targetIndex] = color;

            _XNAColorData[targetIndex] = new Microsoft.Xna.Framework.Color(color.R, color.G, color.B, color.A);
        }
        public Color GetPixelUnsafe(int x, int y)
        {
            return _colorData[((_heightMinusOne - y) * Width) + x];
        }
        public void SetDataUnsafe(Color[] data)
        {
            _colorData = data;

            for (int i = 0; i < _dataLength; i++)
            {
                Color color = _colorData[i];
                _XNAColorData[i] = new Microsoft.Xna.Framework.Color(color.R, color.G, color.B, color.A);
            }
        }
        public Color[] GetDataUnsafe()
        {
            return _colorData;
        }
        #endregion
    }
}