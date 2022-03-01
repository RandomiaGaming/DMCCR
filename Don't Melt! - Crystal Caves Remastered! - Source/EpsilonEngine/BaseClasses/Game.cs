using System;
using System.Reflection;
using System.Collections.Generic;
namespace EpsilonEngine
{
    public class Game
    {
        #region Public Variables
        public bool Running { get; private set; } = false;
        public bool MarkedForDestruction { get; private set; } = false;
        public bool Destroyed { get; private set; } = false;

        public bool Drawing { get; private set; } = false;

        public bool OverridesUpdate { get; private set; } = false;
        public bool OverridesDraw { get; private set; } = false;

        public Color BackgroundColor
        {
            get
            {
                return _backgroundColor;
            }
            set
            {
                _backgroundColor._r = value._r;
                _backgroundColor._g = value._g;
                _backgroundColor._b = value._b;
                _backgroundColor._a = value._a;

                _XNABackgroundColorCache.R = value._r;
                _XNABackgroundColorCache.G = value._g;
                _XNABackgroundColorCache.B = value._b;
                _XNABackgroundColorCache.A = value._a;
            }
        }

        public ushort ViewportWidth { get; private set; } = 1920;
        public ushort ViewportHeight { get; private set; } = 1080;
        public Rectangle ViewportRect { get; private set; } = new Rectangle(0, 0, 0, 0);
        public float AspectRatio { get; private set; } = 16f / 9f;

        public float CurrentFPS { get; private set; } = 0f;
        public TimeSpan TimeSinceStart { get; private set; } = new TimeSpan(0);
        public TimeSpan DeltaTime { get; private set; } = new TimeSpan(0);

        public float TargetFPS
        {
            get
            {
                return _targetFPS;
            }
            set
            {
                if (value <= 0)
                {
                    throw new Exception("TargetFPS must be greater than 0.");
                }
                if (value == float.NaN)
                {
                    throw new Exception("TargetFPS must be a real number or infinity.");
                }
                if (value == float.PositiveInfinity)
                {
                    GameInterface.IsFixedTimeStep = false;
                    _targetFPS = value;
                    return;
                }
                if (value > 1000000.0f)
                {
                    throw new Exception("TargetFPS must less than 1000000 unless TargetFPS is infinity.");
                }
                GameInterface.IsFixedTimeStep = true;
                GameInterface.TargetElapsedTime = new TimeSpan((long)(10000000.0f / value));
                _targetFPS = value;
            }
        }

        public bool IsFullScreen
        {
            get
            {
                return _isFullScreen;
            }
            set
            {
                if(value != _isFullScreen)
                {
                    _isFullScreen = value;

                    GameInterface.XNAGraphicsDeviceManager.IsFullScreen = value;

                    GameInterface.XNAGraphicsDeviceManager.PreferredBackBufferWidth = GameInterface.XNAGraphicsDevice.Adapter.CurrentDisplayMode.Width;
                    GameInterface.XNAGraphicsDeviceManager.PreferredBackBufferHeight = GameInterface.XNAGraphicsDevice.Adapter.CurrentDisplayMode.Height;

                    GameInterface.XNAGraphicsDeviceManager.ApplyChanges();
                }
            }
        }
        #endregion
        #region Internal Variables
        internal GameInterface GameInterface = null;

        internal SingleRunPump CreationPump = new SingleRunPump();
        internal SingleRunPump InitializationPump = new SingleRunPump();

        internal OrderedPump PhysicsUpdatePump = new OrderedPump();
        internal OrderedPump UpdatePump = new OrderedPump();
        internal UnorderedPump RenderPump = new UnorderedPump();
        internal InverseOrderedPump DrawPump = new InverseOrderedPump();

        internal SingleRunPump OnDestroyPump = new SingleRunPump();
        internal SingleRunPump DestructionPump = new SingleRunPump();

        internal Microsoft.Xna.Framework.Graphics.SpriteBatch XNASpriteBatch = null;
        #endregion
        #region Private Variables
        private List<GameManager> _gameManagers = new List<GameManager>();

        private List<Canvas> _canvases = new List<Canvas>();

        private List<Scene> _scenes = new List<Scene>();

        private Microsoft.Xna.Framework.Color _XNABackgroundColorCache = new Microsoft.Xna.Framework.Color(byte.MinValue, byte.MinValue, byte.MinValue, byte.MaxValue);
        private Color _backgroundColor = new Color(byte.MinValue, byte.MinValue, byte.MinValue, byte.MinValue);

        private Microsoft.Xna.Framework.Rectangle _XNAViewportRectCache = new Microsoft.Xna.Framework.Rectangle(0, 0, 1, 1);

        private float _targetFPS = float.PositiveInfinity;

        private bool _isFullScreen = false;
        #endregion
        #region Constructors
        public Game()
        {
            GameInterface = new GameInterface(this);

            Type thisType = GetType();

            if (thisType.Assembly != typeof(Modloader).Assembly)
            {
                Modloader.Load(thisType.Assembly);
            }

            MethodInfo updateMethod = thisType.GetMethod("Update", BindingFlags.NonPublic | BindingFlags.Instance);
            if (updateMethod.DeclaringType != typeof(Game))
            {
                PumpPriorityAttribute pumpPriorityAttribute = updateMethod.GetCustomAttribute<PumpPriorityAttribute>();
                if (pumpPriorityAttribute is null)
                {
                    UpdatePump.RegisterPumpEventUnsafe(Update, 0);
                }
                else
                {
                    UpdatePump.RegisterPumpEventUnsafe(Update, pumpPriorityAttribute.Priority);
                }
                OverridesUpdate = true;
            }

            MethodInfo drawMethod = thisType.GetMethod("Draw", BindingFlags.NonPublic | BindingFlags.Instance);
            if (drawMethod.DeclaringType != typeof(Game))
            {
                PumpPriorityAttribute pumpPriorityAttribute = drawMethod.GetCustomAttribute<PumpPriorityAttribute>();
                if (pumpPriorityAttribute is null)
                {
                    DrawPump.RegisterPumpEventUnsafe(Draw, 0);
                }
                else
                {
                    DrawPump.RegisterPumpEventUnsafe(Draw, pumpPriorityAttribute.Priority);
                }
                OverridesDraw = true;
            }

            InitializationPump.RegisterPumpEventUnsafe(Initialize);

            XNASpriteBatch = new Microsoft.Xna.Framework.Graphics.SpriteBatch(GameInterface.XNAGraphicsDevice);

            ResizeCallback();
        }
        #endregion
        #region Public Methods
        public void Run()
        {
            if (Running)
            {
                throw new Exception("game is already running.");
            }

            Running = true;

            GameInterface.Run();
        }
        public void MarkForDestruction()
        {
            if (MarkedForDestruction)
            {
                throw new Exception("game has already been marked for destruction.");
            }

            if (Destroyed)
            {
                throw new Exception("game has already been destroyed.");
            }

            int gameManagerCount = _gameManagers.Count;
            for (int i = 0; i < gameManagerCount; i++)
            {
                _gameManagers[i].MarkForDestruction();
            }

            int canvasCount = _canvases.Count;
            for (int i = 0; i < canvasCount; i++)
            {
                //_canvases[i].MarkForDestruction();
            }

            int sceneCount = _scenes.Count;
            for (int i = 0; i < sceneCount; i++)
            {
                _scenes[i].MarkForDestruction();
            }

            OnDestroyPump.RegisterPumpEvent(OnDestroy);

            DestructionPump.RegisterPumpEvent(Destroy);

            MarkedForDestruction = true;
        }

        public GameManager GetGameManager(int index)
        {
            if (index < 0 || index >= _gameManagers.Count)
            {
                throw new Exception("index was out of range.");
            }

            return _gameManagers[index];
        }
        public GameManager GetGameManager(Type type)
        {
            if (type is null)
            {
                throw new Exception("type cannot be null.");
            }

            if (!type.IsAssignableFrom(typeof(GameManager)))
            {
                throw new Exception("type must be equal to GameManager or be assignable from GameManager.");
            }

            foreach (GameManager gameManager in _gameManagers)
            {
                if (gameManager.GetType().IsAssignableFrom(type))
                {
                    return gameManager;
                }
            }

            return null;
        }
        public T GetGameManager<T>() where T : GameManager
        {
            foreach (GameManager gameManager in _gameManagers)
            {
                if (gameManager.GetType().IsAssignableFrom(typeof(T)))
                {
                    return (T)gameManager;
                }
            }

            return null;
        }
        public List<GameManager> GetGameManagers()
        {
            return new List<GameManager>(_gameManagers);
        }
        public List<GameManager> GetGameManagers(Type type)
        {
            if (type is null)
            {
                throw new Exception("type cannot be null.");
            }

            if (!type.IsAssignableFrom(typeof(GameManager)))
            {
                throw new Exception("type must be equal to GameManager or be assignable from GameManager.");
            }

            List<GameManager> output = new List<GameManager>();

            foreach (GameManager gameManager in _gameManagers)
            {
                if (gameManager.GetType().IsAssignableFrom(type))
                {
                    output.Add(gameManager);
                }
            }

            return output;
        }
        public List<T> GetGameManagers<T>() where T : GameManager
        {
            List<T> output = new List<T>();

            foreach (GameManager gameManager in _gameManagers)
            {
                if (gameManager.GetType().IsAssignableFrom(typeof(T)))
                {
                    output.Add((T)gameManager);
                }
            }

            return output;
        }
        public int GetGameManagerCount()
        {
            return _gameManagers.Count;
        }

        public Canvas GetCanvas(int index)
        {
            if (index < 0 || index >= _canvases.Count)
            {
                throw new Exception("index was out of range.");
            }

            return _canvases[index];
        }
        public Canvas GetCanvas(Type type)
        {
            if (type is null)
            {
                throw new Exception("type cannot be null.");
            }

            if (!type.IsAssignableFrom(typeof(Canvas)))
            {
                throw new Exception("type must be equal to Canvas or be assignable from Canvas.");
            }

            foreach (Canvas canvas in _canvases)
            {
                if (canvas.GetType().IsAssignableFrom(type))
                {
                    return canvas;
                }
            }

            return null;
        }
        public T GetCanvas<T>() where T : Canvas
        {
            foreach (Canvas canvas in _canvases)
            {
                if (canvas.GetType().IsAssignableFrom(typeof(T)))
                {
                    return (T)canvas;
                }
            }

            return null;
        }
        public List<Canvas> GetCanvases()
        {
            return new List<Canvas>(_canvases);
        }
        public List<Canvas> GetCanvases(Type type)
        {
            if (type is null)
            {
                throw new Exception("type cannot be null.");
            }

            if (!type.IsAssignableFrom(typeof(Canvas)))
            {
                throw new Exception("type must be equal to Canvas or be assignable from Canvas.");
            }

            List<Canvas> output = new List<Canvas>();

            foreach (Canvas canvas in _canvases)
            {
                if (canvas.GetType().IsAssignableFrom(type))
                {
                    output.Add(canvas);
                }
            }

            return output;
        }
        public List<T> GetCanvases<T>() where T : Canvas
        {
            List<T> output = new List<T>();

            foreach (Canvas canvas in _canvases)
            {
                if (canvas.GetType().IsAssignableFrom(typeof(T)))
                {
                    output.Add((T)canvas);
                }
            }

            return output;
        }
        public int GetCanvasCount()
        {
            return _canvases.Count;
        }

        public Scene GetScene(int index)
        {
            if (index < 0 || index >= _scenes.Count)
            {
                throw new Exception("index was out of range.");
            }

            return _scenes[index];
        }
        public Scene GetScene(Type type)
        {
            if (type is null)
            {
                throw new Exception("type cannot be null.");
            }

            if (!type.IsAssignableFrom(typeof(Scene)))
            {
                throw new Exception("type must be equal to Scene or be assignable from Scene.");
            }

            foreach (Scene scene in _scenes)
            {
                if (scene.GetType().IsAssignableFrom(type))
                {
                    return scene;
                }
            }

            return null;
        }
        public T GetScene<T>() where T : Scene
        {
            foreach (Scene scene in _scenes)
            {
                if (scene.GetType().IsAssignableFrom(typeof(T)))
                {
                    return (T)scene;
                }
            }

            return null;
        }
        public List<Scene> GetScenes()
        {
            return new List<Scene>(_scenes);
        }
        public List<Scene> GetScenes(Type type)
        {
            if (type is null)
            {
                throw new Exception("type cannot be null.");
            }

            if (!type.IsAssignableFrom(typeof(Scene)))
            {
                throw new Exception("type must be equal to Scene or be assignable from Scene.");
            }

            List<Scene> output = new List<Scene>();

            foreach (Scene scene in _scenes)
            {
                if (scene.GetType().IsAssignableFrom(type))
                {
                    output.Add(scene);
                }
            }

            return output;
        }
        public List<T> GetScenes<T>() where T : Scene
        {
            List<T> output = new List<T>();

            foreach (Scene scene in _scenes)
            {
                if (scene.GetType().IsAssignableFrom(typeof(T)))
                {
                    output.Add((T)scene);
                }
            }

            return output;
        }
        public int GetSceneCount()
        {
            return _scenes.Count;
        }

        public void DrawTexture(Texture texture)
        {
            if (!Drawing)
            {
                throw new Exception("cannot draw texture because game is not drawing.");
            }

            if (texture is null)
            {
                throw new Exception("texture cannot be null.");
            }

            DrawTextureUnsafe(texture._XNABase);
        }
        #endregion
        #region Internal Methods
        internal void UpdateCallback()
        {
            if (Modloader.ProfilerEnabled)
            {
                Profiler.UpdateStart();
            }

            CreationPump.Invoke();

            InitializationPump.Invoke();

            PhysicsUpdatePump.Invoke();

            UpdatePump.Invoke();


            if (Modloader.ProfilerEnabled)
            {
                Profiler.UpdateEnd();

                Profiler.RenderStart();
            }

            RenderPump.Invoke();

            Drawing = true;

            GameInterface.XNAGraphicsDevice.Clear(_XNABackgroundColorCache);

            XNASpriteBatch.Begin(Microsoft.Xna.Framework.Graphics.SpriteSortMode.Deferred, Microsoft.Xna.Framework.Graphics.BlendState.NonPremultiplied, Microsoft.Xna.Framework.Graphics.SamplerState.PointClamp, null, null, null, null);

            DrawPump.Invoke();

            XNASpriteBatch.End();

            Drawing = false;

            OnDestroyPump.Invoke();

            DestructionPump.Invoke();

            if (Modloader.ProfilerEnabled)
            {
                Profiler.RenderEnd();

                Profiler.Print();
            }
        }
        internal void ResizeCallback()
        {
            if (_isFullScreen)
            {
                ViewportWidth = (ushort)GameInterface.XNAGraphicsDevice.Adapter.CurrentDisplayMode.Width;
                ViewportHeight = (ushort)GameInterface.XNAGraphicsDevice.Adapter.CurrentDisplayMode.Height;
            }
            else
            {
                ViewportWidth = (ushort)GameInterface.XNAGraphicsDevice.Viewport.Width;
                ViewportHeight = (ushort)GameInterface.XNAGraphicsDevice.Viewport.Height;
            }

            AspectRatio = ViewportWidth / (float)ViewportHeight;

            ViewportRect = new Rectangle(0, 0, ViewportWidth - 1, ViewportHeight - 1);

            _XNAViewportRectCache = new Microsoft.Xna.Framework.Rectangle(0, 0, ViewportWidth, ViewportHeight);

            foreach (Canvas canvas in _canvases)
            {
                canvas.OnScreenResize();
            }
        }

        internal void RemoveGameManager(GameManager gameManager)
        {
            _gameManagers.Remove(gameManager);
        }
        internal void AddGameManager(GameManager gameManager)
        {
            _gameManagers.Add(gameManager);
        }

        internal void RemoveCanvas(Canvas canvas)
        {
            _canvases.Remove(canvas);
        }
        internal void AddCanvas(Canvas canvas)
        {
            _canvases.Add(canvas);
        }

        internal void RemoveScene(Scene scene)
        {
            _scenes.Remove(scene);
        }
        internal void AddScene(Scene scene)
        {
            _scenes.Add(scene);
        }

        internal void DrawTextureUnsafe(Microsoft.Xna.Framework.Graphics.Texture2D texture)
        {
            XNASpriteBatch.Draw(texture, _XNAViewportRectCache, Microsoft.Xna.Framework.Color.White);
        }
        #endregion
        #region Private Methods
        private void Destroy()
        {
            GameInterface.Dispose();

            GameInterface = null;

            InitializationPump = null;
            UpdatePump = null;
            RenderPump = null;
            DrawPump = null;
            OnDestroyPump = null;
            DestructionPump = null;

            Running = false;

            Destroyed = true;
        }
        #endregion
        #region Override Methods
        public override string ToString()
        {
            return $"EpsilonEngine.Game()";
        }
        #endregion
        #region Overridable Methods
        protected virtual void Initialize()
        {

        }
        protected virtual void Update()
        {

        }
        protected virtual void Draw()
        {

        }
        protected virtual void OnDestroy()
        {

        }
        #endregion
    }
}