//Approved 09/22/2022
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
        internal Microsoft.Xna.Framework.Graphics.Texture2D _XNATexture;
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
            _XNATexture = new Microsoft.Xna.Framework.Graphics.Texture2D(Game.GameInterface.GraphicsDevice, Width, Height);
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
            if (data is null)
            {
                throw new System.Exception("data cannot be null.");
            }
            if (data.Length != width * height)
            {
                throw new System.Exception("data.Length must be equal to width times height.");
            }
            _XNATexture = new Microsoft.Xna.Framework.Graphics.Texture2D(Game.GameInterface.GraphicsDevice, Width, Height);
            _XNATexture.SetData<Color>(data);
        }
        public Texture(Game game, int width, int height, byte[] data)
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
            if (data is null)
            {
                throw new System.Exception("data cannot be null.");
            }
            if (data.Length != (width * height) << 2)
            {
                throw new System.Exception("data.Length must be equal to width times height times 4.");
            }
            _XNATexture = new Microsoft.Xna.Framework.Graphics.Texture2D(Game.GameInterface.GraphicsDevice, Width, Height);
            _XNATexture.SetData<byte>(data);
        }
        public Texture(Game game, int width, int height, System.IO.Stream data)
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
            if (data is null)
            {
                throw new System.Exception("data cannot be null.");
            }
            int dataBytesLength = (width * height) << 2;
            if (data.Position - data.Length != dataBytesLength)
            {
                throw new System.Exception("Starting at data.Position there must be at least width times height times 4 bytes of data before the end of the stream.");
            }
            _XNATexture = new Microsoft.Xna.Framework.Graphics.Texture2D(Game.GameInterface.GraphicsDevice, Width, Height);
            byte[] dataBytes = new byte[dataBytesLength];
            data.Read(dataBytes, 0, dataBytesLength);
            _XNATexture.SetData<byte>(dataBytes);
        }

        public Texture(Game game, string filePath)
        {
            if (game is null)
            {
                throw new System.Exception("game cannot be null.");
            }
            Game = game;
            if (filePath is null)
            {
                throw new System.Exception("filePath cannot be null.");
            }
            if(filePath is "")
            {
                throw new System.Exception("filePath cannot be empty.");
            }
            if (!System.IO.File.Exists(filePath))
            {
                throw new System.Exception("filePath does not exist.");
            }
            Microsoft.Xna.Framework.Graphics.Texture2D xnaBase;
            try
            {
                xnaBase = Microsoft.Xna.Framework.Graphics.Texture2D.FromFile(Game.GameInterface.GraphicsDevice, filePath);
            }
            catch
            {
                throw new System.Exception("texture could not be loaded from filePath.");
            }
            if(xnaBase is null)
            {
                throw new System.Exception("texture could not be loaded from filePath.");
            }
            _XNATexture = xnaBase;
            if (xnaBase.Width <= 0)
            {
                throw new System.Exception("width must be greater than 0.");
            }
            Width = xnaBase.Width;
            if (xnaBase.Height <= 0)
            {
                throw new System.Exception("height must be greater than 0.");
            }
            Height = xnaBase.Height;
        }
        public Texture(Game game, byte[] encodedBytes)
        {
            if (game is null)
            {
                throw new System.Exception("game cannot be null.");
            }
            Game = game;
            if (encodedBytes is null)
            {
                throw new System.Exception("encodedBytes cannot be null.");
            }
            if (encodedBytes.Length is 0)
            {
                throw new System.Exception("encodedBytes cannot be empty.");
            }
            System.IO.MemoryStream stream = new System.IO.MemoryStream(encodedBytes);
            Microsoft.Xna.Framework.Graphics.Texture2D xnaBase;
            try
            {
                xnaBase = Microsoft.Xna.Framework.Graphics.Texture2D.FromStream(Game.GameInterface.GraphicsDevice, stream);
            }
            catch
            {
                throw new System.Exception("texture could not be loaded from bytes.");
            }
            if (xnaBase is null)
            {
                throw new System.Exception("texture could not be loaded from bytes.");
            }
            _XNATexture = xnaBase;
            if (xnaBase.Width <= 0)
            {
                throw new System.Exception("width must be greater than 0.");
            }
            Width = xnaBase.Width;
            if (xnaBase.Height <= 0)
            {
                throw new System.Exception("height must be greater than 0.");
            }
            Height = xnaBase.Height;
        }
        public Texture(Game game, System.IO.Stream encodedStream)
        {
            if (game is null)
            {
                throw new System.Exception("game cannot be null.");
            }
            Game = game;
            if (encodedStream is null)
            {
                throw new System.Exception("encodedStream cannot be null.");
            }
            if (!encodedStream.CanRead)
            {
                throw new System.Exception("encodedStream must be readable.");
            }
            Microsoft.Xna.Framework.Graphics.Texture2D xnaBase;
            try
            {
                xnaBase = Microsoft.Xna.Framework.Graphics.Texture2D.FromStream(Game.GameInterface.GraphicsDevice, encodedStream);
            }
            catch
            {
                throw new System.Exception("texture could not be loaded from stream.");
            }
            if (xnaBase is null)
            {
                throw new System.Exception("texture could not be loaded from stream.");
            }
            _XNATexture = xnaBase;
            if (xnaBase.Width <= 0)
            {
                throw new System.Exception("width must be greater than 0.");
            }
            Width = xnaBase.Width;
            if (xnaBase.Height <= 0)
            {
                throw new System.Exception("height must be greater than 0.");
            }
            Height = xnaBase.Height;
        }
        #endregion
        #region Public Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.Texture({Width}, {Height})";
        }
        #endregion
    }
}