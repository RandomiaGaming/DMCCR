namespace EpsilonEngine
{
    public class Game
    {
        #region Public Variables
        public bool Running { get; private set; } = false;
        public bool MarkedForExit { get; private set; } = false;
        public bool Exited { get; private set; } = false;

        public bool Rendering { get; private set; } = false;

        public bool KillProcessOnExit = true;
        public bool DestroyChildrenOnExit = false;

        public readonly bool OverridesUpdate = false;
        public readonly bool OverridesDraw = false;

        public Color BackgroundColor
        {
            get
            {
                return _backgroundColor;
            }
            set
            {
                _backgroundColor = value;

                _XNABackgroundColorCache = new Microsoft.Xna.Framework.Color(value.R, value.G, value.B, value.A);
            }
        }

        public int ViewportWidth { get; private set; } = 1920;
        public int ViewportHeight { get; private set; } = 1080;
        public float AspectRatio { get; private set; } = 1.66666663f;

        public float CurrentFPS { get; private set; }
        public System.TimeSpan TimeSinceStart { get; private set; }
        public System.TimeSpan DeltaTime { get; private set; }

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
                    throw new System.Exception("TargetFPS must be greater than 0.");
                }
                if (value == float.NaN)
                {
                    throw new System.Exception("TargetFPS must be a real number or infinity.");
                }
                if (value == float.PositiveInfinity)
                {
                    GameInterface.IsFixedTimeStep = false;
                    _targetFPS = value;
                    return;
                }
                if (value > 1000000.0f)
                {
                    throw new System.Exception("TargetFPS must less than 1000000 unless TargetFPS is infinity.");
                }
                GameInterface.IsFixedTimeStep = true;
                GameInterface.TargetElapsedTime = new System.TimeSpan((long)(10000000.0f / value));
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
                if (value == _isFullScreen)
                {
                    return;
                }
                _isFullScreen = value;
                GameInterface.XNAGraphicsDeviceManager.IsFullScreen = value;


                GameInterface.XNAGraphicsDeviceManager.PreferredBackBufferWidth = GameInterface.XNAGraphicsDevice.Adapter.CurrentDisplayMode.Width;
                GameInterface.XNAGraphicsDeviceManager.PreferredBackBufferHeight = GameInterface.XNAGraphicsDevice.Adapter.CurrentDisplayMode.Height;

                GameInterface.XNAGraphicsDeviceManager.ApplyChanges();
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
        private System.Collections.Generic.List<GameManager> _gameManagers = new System.Collections.Generic.List<GameManager>();

        private System.Collections.Generic.List<Canvas> _canvases = new System.Collections.Generic.List<Canvas>();

        private System.Collections.Generic.List<Scene> _scenes = new System.Collections.Generic.List<Scene>();

        private Microsoft.Xna.Framework.Color _XNABackgroundColorCache;
        private Color _backgroundColor;

        private Microsoft.Xna.Framework.Rectangle _XNAViewportRect;

        private float _targetFPS = float.PositiveInfinity;

        private bool _isFullScreen = false;

        private bool _gameCreatedAlready = false;
        #endregion
        #region Constructors
        public Game()
        {
            if (_gameCreatedAlready)
            {
                throw new System.Exception("cannot create more than one game on a single process.");
            }
            _gameCreatedAlready = true;

            Profiler.InitializeStart();

            GameInterface = new GameInterface(this);

            System.Type thisType = GetType();

            if (thisType.Assembly != typeof(Modloader).Assembly)
            {
                Modloader.Load(thisType.Assembly);
            }

            System.Reflection.MethodInfo updateMethod = thisType.GetMethod("Update", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (updateMethod.DeclaringType != typeof(Game))
            {
                PumpPriorityAttribute pumpPriorityAttribute = System.Reflection.CustomAttributeExtensions.GetCustomAttribute<PumpPriorityAttribute>(updateMethod);
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

            System.Reflection.MethodInfo drawMethod = thisType.GetMethod("Draw", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (drawMethod.DeclaringType != typeof(Game))
            {
                PumpPriorityAttribute pumpPriorityAttribute = System.Reflection.CustomAttributeExtensions.GetCustomAttribute<PumpPriorityAttribute>(drawMethod);
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
                throw new System.Exception("game is already running.");
            }

            Running = true;

            GameInterface.Run();
        }
        public void MarkForExit()
        {
            if (MarkedForExit)
            {
                throw new System.Exception("game has already been marked for destruction.");
            }

            if (Exited)
            {
                throw new System.Exception("game has already been destroyed.");
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

            MarkedForExit = true;
        }

        public GameManager GetGameManager(int index)
        {
            if (index < 0 || index >= _gameManagers.Count)
            {
                throw new System.Exception("index was out of range.");
            }

            return _gameManagers[index];
        }
        public GameManager GetGameManager(System.Type type)
        {
            if (type is null)
            {
                throw new System.Exception("type cannot be null.");
            }

            if (!type.IsAssignableFrom(typeof(GameManager)))
            {
                throw new System.Exception("type must be equal to GameManager or be assignable from GameManager.");
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
        public System.Collections.Generic.List<GameManager> GetGameManagers()
        {
            return new System.Collections.Generic.List<GameManager>(_gameManagers);
        }
        public System.Collections.Generic.List<GameManager> GetGameManagers(System.Type type)
        {
            if (type is null)
            {
                throw new System.Exception("type cannot be null.");
            }

            if (!type.IsAssignableFrom(typeof(GameManager)))
            {
                throw new System.Exception("type must be equal to GameManager or be assignable from GameManager.");
            }

            System.Collections.Generic.List<GameManager> output = new System.Collections.Generic.List<GameManager>();

            foreach (GameManager gameManager in _gameManagers)
            {
                if (gameManager.GetType().IsAssignableFrom(type))
                {
                    output.Add(gameManager);
                }
            }

            return output;
        }
        public System.Collections.Generic.List<T> GetGameManagers<T>() where T : GameManager
        {
            System.Collections.Generic.List<T> output = new System.Collections.Generic.List<T>();

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
                throw new System.Exception("index was out of range.");
            }

            return _canvases[index];
        }
        public Canvas GetCanvas(System.Type type)
        {
            if (type is null)
            {
                throw new System.Exception("type cannot be null.");
            }

            if (!type.IsAssignableFrom(typeof(Canvas)))
            {
                throw new System.Exception("type must be equal to Canvas or be assignable from Canvas.");
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
        public System.Collections.Generic.List<Canvas> GetCanvases()
        {
            return new System.Collections.Generic.List<Canvas>(_canvases);
        }
        public System.Collections.Generic.List<Canvas> GetCanvases(System.Type type)
        {
            if (type is null)
            {
                throw new System.Exception("type cannot be null.");
            }

            if (!type.IsAssignableFrom(typeof(Canvas)))
            {
                throw new System.Exception("type must be equal to Canvas or be assignable from Canvas.");
            }

            System.Collections.Generic.List<Canvas> output = new System.Collections.Generic.List<Canvas>();

            foreach (Canvas canvas in _canvases)
            {
                if (canvas.GetType().IsAssignableFrom(type))
                {
                    output.Add(canvas);
                }
            }

            return output;
        }
        public System.Collections.Generic.List<T> GetCanvases<T>() where T : Canvas
        {
            System.Collections.Generic.List<T> output = new System.Collections.Generic.List<T>();

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
                throw new System.Exception("index was out of range.");
            }

            return _scenes[index];
        }
        public Scene GetScene(System.Type type)
        {
            if (type is null)
            {
                throw new System.Exception("type cannot be null.");
            }

            if (!type.IsAssignableFrom(typeof(Scene)))
            {
                throw new System.Exception("type must be equal to Scene or be assignable from Scene.");
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
        public System.Collections.Generic.List<Scene> GetScenes()
        {
            return new System.Collections.Generic.List<Scene>(_scenes);
        }
        public System.Collections.Generic.List<Scene> GetScenes(System.Type type)
        {
            if (type is null)
            {
                throw new System.Exception("type cannot be null.");
            }

            if (!type.IsAssignableFrom(typeof(Scene)))
            {
                throw new System.Exception("type must be equal to Scene or be assignable from Scene.");
            }

            System.Collections.Generic.List<Scene> output = new System.Collections.Generic.List<Scene>();

            foreach (Scene scene in _scenes)
            {
                if (scene.GetType().IsAssignableFrom(type))
                {
                    output.Add(scene);
                }
            }

            return output;
        }
        public System.Collections.Generic.List<T> GetScenes<T>() where T : Scene
        {
            System.Collections.Generic.List<T> output = new System.Collections.Generic.List<T>();

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
            if (!Rendering)
            {
                throw new System.Exception("cannot draw texture because game is not drawing.");
            }

            if (texture is null)
            {
                throw new System.Exception("texture cannot be null.");
            }

            DrawTextureUnsafe(texture._XNABase);
        }
        #endregion
        #region Internal Methods
        private bool _profilerInitialized = false;
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

            Rendering = true;

            GameInterface.XNAGraphicsDevice.Clear(_XNABackgroundColorCache);

            XNASpriteBatch.Begin(Microsoft.Xna.Framework.Graphics.SpriteSortMode.Deferred, Microsoft.Xna.Framework.Graphics.BlendState.NonPremultiplied, Microsoft.Xna.Framework.Graphics.SamplerState.PointClamp, null, null, null, null);

            DrawPump.Invoke();

            XNASpriteBatch.End();

            Rendering = false;

            OnDestroyPump.Invoke();

            DestructionPump.Invoke();

            if (Modloader.ProfilerEnabled)
            {
                Profiler.RenderEnd();

                Profiler.Print();
            }

            if (!_profilerInitialized)
            {
                Profiler.InitializeEnd();
                _profilerInitialized = true;
            }
        }
        internal void ResizeCallback()
        {
            if (_isFullScreen)
            {
                ViewportWidth = GameInterface.XNAGraphicsDevice.Adapter.CurrentDisplayMode.Width;
                ViewportHeight = GameInterface.XNAGraphicsDevice.Adapter.CurrentDisplayMode.Height;
            }
            else
            {
                ViewportWidth = GameInterface.XNAGraphicsDevice.Viewport.Width;
                ViewportHeight = GameInterface.XNAGraphicsDevice.Viewport.Height;
            }

            AspectRatio = ViewportWidth / (float)ViewportHeight;

            _XNAViewportRect = new Microsoft.Xna.Framework.Rectangle(0, 0, ViewportWidth, ViewportHeight);

            foreach (Canvas canvas in _canvases)
            {
                canvas.OnScreenResize();
            }
        }
        internal void ExitCallback()
        {
            if (KillProcessOnExit)
            {
                System.Diagnostics.Process.GetCurrentProcess().Kill();
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
            XNASpriteBatch.Draw(texture, _XNAViewportRect, Microsoft.Xna.Framework.Color.White);
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

            Exited = true;
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