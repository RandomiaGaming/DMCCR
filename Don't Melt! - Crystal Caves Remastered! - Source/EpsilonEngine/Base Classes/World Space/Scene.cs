using System;
using System.Reflection;
using System.Collections.Generic;
namespace EpsilonEngine
{
    public class Scene
    {
        #region Public Variables
        public Game Game { get; private set; } = null;

        public bool Destroyed { get; private set; } = false;

        public bool Rendering { get; private set; } = false;

        public bool OverridesUpdate { get; private set; } = false;
        public int UpdatePriority { get; private set; } = 0;
        public bool OverridesRender { get; private set; } = false;
        public int RenderPriority { get; private set; } = 0;

        public int CameraPositionX
        {
            get
            {
                return _cameraPositionX;
            }
            set
            {
                _cameraPositionX = value;
                _cameraPosition.X = value;

                CameraMovePump.Invoke();
            }
        }
        public int CameraPositionY
        {
            get
            {
                return _cameraPositionY;
            }
            set
            {
                _cameraPositionY = value;
                _cameraPosition.Y = value;

                CameraMovePump.Invoke();
            }
        }
        public Point CameraPosition
        {
            get
            {
                return _cameraPosition;
            }
            set
            {
                _cameraPositionX = value.X;
                _cameraPositionY = value.Y;
                _cameraPosition.X = value.X;
                _cameraPosition.Y = value.Y;

                CameraMovePump.Invoke();
            }
        }

        public ushort RenderWidth { get; private set; } = 256;
        public ushort RenderHeight { get; private set; } = 144;
        public float RenderAspectRatio { get; private set; } = 16f / 9f;

        public Color BackgroundColor
        {
            get
            {
                return _backgroundColor;
            }
            set
            {
                _backgroundColor = value;
                _XNABackgroundColorCache = _backgroundColor.ToXNA();
            }
        }
        #endregion
        #region Internal Variables
        internal OrderedPump RenderPump = new OrderedPump();

        internal UnorderedPump CameraMovePump = new UnorderedPump();

        internal Microsoft.Xna.Framework.Graphics.SpriteBatch XNASpriteBatch = null;

        internal Microsoft.Xna.Framework.Graphics.RenderTarget2D XNARenderTarget = null;
        #endregion
        #region Private Variables
        private List<GameObject> _gameObjects = new List<GameObject>();
        private GameObject[] _gameObjectCache = new GameObject[0];
        private bool _gameObjectValidateQued = false;

        private List<SceneManager> _sceneManagers = new List<SceneManager>();
        private SceneManager[] _sceneManagerCache = new SceneManager[0];
        private bool _sceneManagerValidateQued = false;

        private int _cameraPositionX = 0;
        private int _cameraPositionY = 0;
        private Point _cameraPosition = new Point(0, 0);

        private Microsoft.Xna.Framework.Color _XNABackgroundColorCache = new Microsoft.Xna.Framework.Color(byte.MinValue, byte.MinValue, byte.MinValue, byte.MinValue);
        private Color _backgroundColor = new Color(byte.MinValue, byte.MinValue, byte.MinValue, byte.MinValue);

        private Microsoft.Xna.Framework.Vector2 _XNAReusableDrawPosition = new Microsoft.Xna.Framework.Vector2(0, 0);
        private Microsoft.Xna.Framework.Color _XNAReusableDrawColor = new Microsoft.Xna.Framework.Color(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
        #endregion
        #region Constructors
        public Scene(Game game, ushort renderWidth, ushort renderHeight, int drawPriority)
        {
            if (game is null)
            {
                throw new Exception("game cannot be null.");
            }

            Game = game;

            if (renderWidth <= 0)
            {
                throw new Exception("renderWidth must be greater than 0.");
            }
            RenderWidth = renderWidth;

            if (renderHeight <= 0)
            {
                throw new Exception("renderHeight must be greater than 0.");
            }
            RenderHeight = renderHeight;

            RenderAspectRatio = RenderWidth / (float)RenderHeight;

            XNARenderTarget = new Microsoft.Xna.Framework.Graphics.RenderTarget2D(Game.GameInterface.XNAGraphicsDevice, renderWidth, renderHeight);

            XNASpriteBatch = new Microsoft.Xna.Framework.Graphics.SpriteBatch(Game.GameInterface.XNAGraphicsDevice);

            Game.AddScene(this);

            Type thisType = GetType();

            MethodInfo updateMethod = thisType.GetMethod("Update", BindingFlags.NonPublic | BindingFlags.Instance);
            if (updateMethod.DeclaringType != typeof(SceneManager))
            {
                PumpPriorityAttribute pumpPriorityAttribute = updateMethod.GetCustomAttribute<PumpPriorityAttribute>();
                if (pumpPriorityAttribute is not null)
                {
                    UpdatePriority = pumpPriorityAttribute.Priority;
                }
                Game.UpdatePump.RegisterPumpEventUnsafe(Update, UpdatePriority);
                OverridesUpdate = true;
            }

            MethodInfo renderMethod = thisType.GetMethod("Render", BindingFlags.NonPublic | BindingFlags.Instance);
            if (renderMethod.DeclaringType != typeof(SceneManager))
            {
                PumpPriorityAttribute pumpPriorityAttribute = updateMethod.GetCustomAttribute<PumpPriorityAttribute>();
                if (pumpPriorityAttribute is not null)
                {
                    RenderPriority = pumpPriorityAttribute.Priority;
                }
                RenderPump.RegisterPumpEventUnsafe(Render, RenderPriority);
                OverridesRender = true;
            }

            Game.RenderPump.RegisterPumpEventUnsafe(RenderScene);

            Game.DrawPump.RegisterPumpEventUnsafe(Draw, drawPriority);
        }
        #endregion
        #region Public Methods
        public void DrawTextureWorldSpace(Texture texture, Point position, Color color)
        {
            if (!Rendering)
            {
                throw new Exception("cannot draw texture because scene is not rendering.");
            }

            if (texture is null)
            {
                throw new Exception("texture cannot be null.");
            }
            
            DrawTextureWorldSpaceUnsafe(texture.XNABase, position.X, position.Y, color.R, color.B, color.B, color.A);
        }
        public void DrawTextureScreenSpace(Texture texture, Point position, Color color)
        {
            if (!Rendering)
            {
                throw new Exception("cannot draw texture because scene is not rendering.");
            }

            if (texture is null)
            {
                throw new Exception("texture cannot be null.");
            }
            
            DrawTextureScreenSpaceUnsafe(texture.XNABase, position.X, position.Y, color.R, color.B, color.B, color.A);
        }

        public void Destroy()
        {
            if (Destroyed)
            {
                throw new Exception("scene has already been destroyed.");
            }

            foreach (SceneManager sceneManager in _sceneManagerCache)
            {
                sceneManager.Destroy();
            }

            foreach (GameObject gameObject in _gameObjectCache)
            {
                gameObject.Destroy();
            }

            Game.RemoveScene(this);

            _sceneManagers = null;
            _sceneManagerCache = null;
            
            _gameObjects = null;
            _gameObjectCache = null;

            Game = null;

            Destroyed = true;
        }

        public SceneManager GetSceneManager(int index)
        {
            if (index < 0 || index >= _sceneManagerCache.Length)
            {
                throw new Exception("index was out of range.");
            }

            return _sceneManagerCache[index];
        }
        public SceneManager GetSceneManager(Type type)
        {
            if (type is null)
            {
                throw new Exception("type cannot be null.");
            }

            if (!type.IsAssignableFrom(typeof(SceneManager)))
            {
                throw new Exception("type must be equal to SceneManager or be assignable from SceneManager.");
            }

            foreach (SceneManager sceneManager in _sceneManagerCache)
            {
                if (sceneManager.GetType().IsAssignableFrom(type))
                {
                    return sceneManager;
                }
            }

            return null;
        }
        public T GetSceneManager<T>() where T : SceneManager
        {
            foreach (SceneManager sceneManager in _sceneManagerCache)
            {
                if (sceneManager.GetType().IsAssignableFrom(typeof(T)))
                {
                    return (T)sceneManager;
                }
            }

            return null;
        }
        public SceneManager[] GetSceneManagers()
        {
            return (SceneManager[])_sceneManagerCache.Clone();
        }
        public SceneManager[] GetSceneManagers(Type type)
        {
            if (type is null)
            {
                throw new Exception("type cannot be null.");
            }

            if (!type.IsAssignableFrom(typeof(SceneManager)))
            {
                throw new Exception("type must be equal to SceneManager or be assignable from SceneManager.");
            }

            List<SceneManager> output = new List<SceneManager>();

            foreach (SceneManager sceneManager in _sceneManagerCache)
            {
                if (sceneManager.GetType().IsAssignableFrom(type))
                {
                    output.Add(sceneManager);
                }
            }

            return output.ToArray();
        }
        public T[] GetSceneManagers<T>() where T : SceneManager
        {
            List<T> output = new List<T>();

            foreach (SceneManager sceneManager in _sceneManagerCache)
            {
                if (sceneManager.GetType().IsAssignableFrom(typeof(T)))
                {
                    output.Add((T)sceneManager);
                }
            }

            return output.ToArray();
        }
        public int GetSceneManagerCount()
        {
            return _sceneManagerCache.Length;
        }

        public GameObject GetGameObject(int index)
        {
            if (index < 0 || index >= _gameObjectCache.Length)
            {
                throw new Exception("index was out of range.");
            }

            return _gameObjectCache[index];
        }
        public GameObject GetGameObject(Type type)
        {
            if (type is null)
            {
                throw new Exception("type cannot be null.");
            }

            if (!type.IsAssignableFrom(typeof(GameObject)))
            {
                throw new Exception("type must be equal to GameObject or be assignable from GameObject.");
            }

            foreach (GameObject gameObject in _gameObjectCache)
            {
                if (gameObject.GetType().IsAssignableFrom(type))
                {
                    return gameObject;
                }
            }

            return null;
        }
        public T GetGameObject<T>() where T : GameObject
        {
            foreach (GameObject gameObject in _gameObjectCache)
            {
                if (gameObject.GetType().IsAssignableFrom(typeof(T)))
                {
                    return (T)gameObject;
                }
            }

            return null;
        }
        public GameObject[] GetGameObjects()
        {
            return (GameObject[])_gameObjectCache.Clone();
        }
        public GameObject[] GetGameObjects(Type type)
        {
            if (type is null)
            {
                throw new Exception("type cannot be null.");
            }

            if (!type.IsAssignableFrom(typeof(GameObject)))
            {
                throw new Exception("type must be equal to GameObject or be assignable from GameObject.");
            }

            List<GameObject> output = new List<GameObject>();

            foreach (GameObject gameObject in _gameObjectCache)
            {
                if (gameObject.GetType().IsAssignableFrom(type))
                {
                    output.Add(gameObject);
                }
            }

            return output.ToArray();
        }
        public T[] GetGameObjects<T>() where T : GameObject
        {
            List<T> output = new List<T>();

            foreach (GameObject gameObject in _gameObjectCache)
            {
                if (gameObject.GetType().IsAssignableFrom(typeof(T)))
                {
                    output.Add((T)gameObject);
                }
            }

            return output.ToArray();
        }
        public int GetGameObjectCount()
        {
            return _gameObjectCache.Length;
        }
        #endregion
        #region Internal Methods
        internal void DrawTextureWorldSpaceUnsafe(Microsoft.Xna.Framework.Graphics.Texture2D texture, int x, int y, byte r, byte g, byte b, byte a)
        {
            _XNAReusableDrawPosition.X = x - _cameraPositionX;
            _XNAReusableDrawPosition.Y = RenderHeight - y + _cameraPositionY - texture.Height;
            _XNAReusableDrawColor.R = r;
            _XNAReusableDrawColor.G = g;
            _XNAReusableDrawColor.B = b;
            _XNAReusableDrawColor.A = a;
            XNASpriteBatch.Draw(texture, _XNAReusableDrawPosition, _XNAReusableDrawColor);
        }
        internal void DrawTextureScreenSpaceUnsafe(Microsoft.Xna.Framework.Graphics.Texture2D texture, int x, int y, byte r, byte g, byte b, byte a)
        {
            _XNAReusableDrawPosition.X = x;
            _XNAReusableDrawPosition.Y = RenderHeight - y - texture.Height;
            _XNAReusableDrawColor.R = r;
            _XNAReusableDrawColor.G = g;
            _XNAReusableDrawColor.B = b;
            _XNAReusableDrawColor.A = a;
            XNASpriteBatch.Draw(texture, _XNAReusableDrawPosition, _XNAReusableDrawColor);
        }
        
        internal void RemoveSceneManager(SceneManager sceneManager)
        {
            _sceneManagers.Remove(sceneManager);

            if (!_sceneManagerValidateQued)
            {
                Game.SingleRunPump.RegisterPumpEventUnsafe(ValidateSceneManagerCache);
                _sceneManagerValidateQued = true;
            }
        }
        internal void AddSceneManager(SceneManager sceneManager)
        {
            _sceneManagers.Add(sceneManager);

            if (!_sceneManagerValidateQued)
            {
                Game.SingleRunPump.RegisterPumpEventUnsafe(ValidateSceneManagerCache);
                _sceneManagerValidateQued = true;
            }
        }

        internal void RemoveGameObject(GameObject gameObject)
        {
            _gameObjects.Remove(gameObject);

            if (!_gameObjectValidateQued)
            {
                Game.SingleRunPump.RegisterPumpEventUnsafe(ValidateGameObjectCache);
                _gameObjectValidateQued = true;
            }
        }
        internal void AddGameObject(GameObject gameObject)
        {
            _gameObjects.Add(gameObject);

            if (!_gameObjectValidateQued)
            {
                Game.SingleRunPump.RegisterPumpEventUnsafe(ValidateGameObjectCache);
                _gameObjectValidateQued = true;
            }
        }
        #endregion
        #region Private Methods
        private void Draw()
        {
            Game.DrawTextureUnsafe(XNARenderTarget);
        }
        private void RenderScene()
        {
            Rendering = true;

            Game.GameInterface.GraphicsDevice.SetRenderTarget(XNARenderTarget);

            Game.GameInterface.GraphicsDevice.Clear(_XNABackgroundColorCache);

            XNASpriteBatch.Begin(Microsoft.Xna.Framework.Graphics.SpriteSortMode.Deferred, Microsoft.Xna.Framework.Graphics.BlendState.NonPremultiplied, Microsoft.Xna.Framework.Graphics.SamplerState.PointClamp, null, null, null, null);

            RenderPump.Invoke();

            XNASpriteBatch.End();

            Game.GameInterface.GraphicsDevice.SetRenderTarget(null);

            Rendering = false;
        }
        private void ValidateSceneManagerCache()
        {
            _sceneManagerCache = _sceneManagers.ToArray();
            _sceneManagerValidateQued = false;
        }
        private void ValidateGameObjectCache()
        {
            _gameObjectCache = _gameObjects.ToArray();
            _gameObjectValidateQued = false;
        }
        #endregion
        #region Override Methods
        public override string ToString()
        {
            return $"EpsilonEngine.Scene()";
        }
        #endregion
        #region Overridable Methods
        protected virtual void Update()
        {

        }
        protected virtual void Render()
        {

        }
        #endregion
    }
}