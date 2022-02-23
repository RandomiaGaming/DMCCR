using System;
using System.Reflection;
using System.Collections.Generic;
namespace EpsilonEngine
{
    public class Game
    {
        #region Public Variables
        public bool Running { get; private set; } = false;
        public bool Exited { get; private set; } = false;

        public bool Updating { get; private set; } = false;
        public bool Rendering { get; private set; } = false;
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
                _backgroundColor = value;
                _XNABackgroundColorCache = _backgroundColor.ToXNA();
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
        #endregion
        #region Internal Variables
        internal GameInterface GameInterface = null;

        internal OrderedPump UpdatePump = new OrderedPump();

        internal UnorderedPump RenderPump = new UnorderedPump();

        internal OrderedPump DrawPump = new OrderedPump();

        internal SingleRunPump SingleRunPump = new SingleRunPump();

        internal Microsoft.Xna.Framework.Graphics.SpriteBatch XNASpriteBatch = null;
        #endregion
        #region Private Variables
        private List<GameManager> _gameManagers = new List<GameManager>();
        private GameManager[] _gameManagerCache = new GameManager[0];
        private bool _gameManagerValidateQued = false;

        private List<Canvas> _canvases = new List<Canvas>();
        private Canvas[] _canvasCache = new Canvas[0];
        private bool _canvasValidateQued = false;

        private List<Scene> _scenes = new List<Scene>();
        private Scene[] _sceneCache = new Scene[0];
        private bool _sceneValidateQued = false;

        private Microsoft.Xna.Framework.Color _XNABackgroundColorCache = new Microsoft.Xna.Framework.Color(byte.MinValue, byte.MinValue, byte.MinValue, byte.MaxValue);
        private Color _backgroundColor = new Color(byte.MinValue, byte.MinValue, byte.MinValue, byte.MinValue);

        private Microsoft.Xna.Framework.Rectangle _XNAViewportRectCache = new Microsoft.Xna.Framework.Rectangle(0, 0, 1, 1);

        private float _targetFPS = float.PositiveInfinity;
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
        public void Exit()
        {
            if (Exited)
            {
                throw new Exception("game has already exited.");
            }
            if (!Running)
            {
                throw new Exception("cannot exit game before it has started running.");
            }

            foreach (Canvas canvas in _canvasCache)
            {
                canvas.Destroy();
            }
            foreach (Scene scene in _sceneCache)
            {
                scene.Destroy();
            }

            GameInterface.Dispose();

            Running = false;
            Exited = true;
        }

        public GameManager GetGameManager(int index)
        {
            if (index < 0 || index >= _gameManagerCache.Length)
            {
                throw new Exception("index was out of range.");
            }

            return _gameManagerCache[index];
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

            foreach (GameManager gameManager in _gameManagerCache)
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
            foreach (GameManager gameManager in _gameManagerCache)
            {
                if (gameManager.GetType().IsAssignableFrom(typeof(T)))
                {
                    return (T)gameManager;
                }
            }

            return null;
        }
        public GameManager[] GetGameManagers()
        {
            return (GameManager[])_gameManagerCache.Clone();
        }
        public GameManager[] GetGameManagers(Type type)
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

            foreach (GameManager gameManager in _gameManagerCache)
            {
                if (gameManager.GetType().IsAssignableFrom(type))
                {
                    output.Add(gameManager);
                }
            }

            return output.ToArray();
        }
        public T[] GetGameManagers<T>() where T : GameManager
        {
            List<T> output = new List<T>();

            foreach (GameManager gameManager in _gameManagerCache)
            {
                if (gameManager.GetType().IsAssignableFrom(typeof(T)))
                {
                    output.Add((T)gameManager);
                }
            }

            return output.ToArray();
        }
        public int GetGameManagerCount()
        {
            return _gameManagerCache.Length;
        }

        public Canvas GetCanvas(int index)
        {
            if (index < 0 || index >= _canvasCache.Length)
            {
                throw new Exception("index was out of range.");
            }

            return _canvasCache[index];
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

            foreach (Canvas canvas in _canvasCache)
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
            foreach (Canvas canvas in _canvasCache)
            {
                if (canvas.GetType().IsAssignableFrom(typeof(T)))
                {
                    return (T)canvas;
                }
            }

            return null;
        }
        public Canvas[] GetCanvases()
        {
            return (Canvas[])_canvasCache.Clone();
        }
        public Canvas[] GetCanvases(Type type)
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

            foreach (Canvas canvas in _canvasCache)
            {
                if (canvas.GetType().IsAssignableFrom(type))
                {
                    output.Add(canvas);
                }
            }

            return output.ToArray();
        }
        public T[] GetCanvases<T>() where T : Canvas
        {
            List<T> output = new List<T>();

            foreach (Canvas canvas in _canvasCache)
            {
                if (canvas.GetType().IsAssignableFrom(typeof(T)))
                {
                    output.Add((T)canvas);
                }
            }

            return output.ToArray();
        }
        public int GetCanvasCount()
        {
            return _canvasCache.Length;
        }

        public Scene GetScene(int index)
        {
            if (index < 0 || index >= _sceneCache.Length)
            {
                throw new Exception("index was out of range.");
            }

            return _sceneCache[index];
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

            foreach (Scene scene in _sceneCache)
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
            foreach (Scene scene in _sceneCache)
            {
                if (scene.GetType().IsAssignableFrom(typeof(T)))
                {
                    return (T)scene;
                }
            }

            return null;
        }
        public Scene[] GetScenes()
        {
            return (Scene[])_sceneCache.Clone();
        }
        public Scene[] GetScenes(Type type)
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

            foreach (Scene scene in _sceneCache)
            {
                if (scene.GetType().IsAssignableFrom(type))
                {
                    output.Add(scene);
                }
            }

            return output.ToArray();
        }
        public T[] GetScenes<T>() where T : Scene
        {
            List<T> output = new List<T>();

            foreach (Scene scene in _sceneCache)
            {
                if (scene.GetType().IsAssignableFrom(typeof(T)))
                {
                    output.Add((T)scene);
                }
            }

            return output.ToArray();
        }
        public int GetSceneCount()
        {
            return _sceneCache.Length;
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

            DrawTextureUnsafe(texture.ToXNA());
        }
        #endregion
        #region Internal Methods
        internal void UpdateCallback()
        {
            if (Modloader.ProfilerEnabled)
            {
                Profiler.UpdateStart();
            }

            Updating = true;

            SingleRunPump.Invoke();

            UpdatePump.Invoke();

            Updating = false;

            if (Modloader.ProfilerEnabled)
            {
                Profiler.UpdateEnd();

                Profiler.RenderStart();
            }

            Rendering = true;

            RenderPump.Invoke();

            Rendering = false;

            if (Modloader.ProfilerEnabled)
            {
                Profiler.RenderEnd();

                Profiler.DrawStart();
            }

            Drawing = true;

            GameInterface.XNAGraphicsDevice.Clear(_XNABackgroundColorCache);

            XNASpriteBatch.Begin(Microsoft.Xna.Framework.Graphics.SpriteSortMode.Deferred, Microsoft.Xna.Framework.Graphics.BlendState.NonPremultiplied, Microsoft.Xna.Framework.Graphics.SamplerState.PointClamp, null, null, null, null);

            DrawPump.Invoke();

            XNASpriteBatch.End();

            Drawing = false;

            if (Modloader.ProfilerEnabled)
            {
                Profiler.DrawEnd();

                Profiler.Print();
            }
        }
        internal void ResizeCallback()
        {
            if (GameInterface.XNAGraphicsDeviceManager.IsFullScreen)
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

            foreach (Canvas canvas in _canvasCache)
            {
                canvas.OnScreenResize();
            }
        }

        internal void RemoveGameManager(GameManager gameManager)
        {
            _gameManagers.Remove(gameManager);

            if (!_gameManagerValidateQued)
            {
                SingleRunPump.RegisterPumpEventUnsafe(ValidateGameManagerCache);
                _gameManagerValidateQued = true;
            }
        }
        internal void AddGameManager(GameManager gameManager)
        {
            _gameManagers.Add(gameManager);

            if (!_gameManagerValidateQued)
            {
                SingleRunPump.RegisterPumpEventUnsafe(ValidateGameManagerCache);
                _gameManagerValidateQued = true;
            }
        }

        internal void RemoveCanvas(Canvas canvas)
        {
            _canvases.Remove(canvas);

            if (!_canvasValidateQued)
            {
                SingleRunPump.RegisterPumpEventUnsafe(ValidateCanvasCache);
                _canvasValidateQued = true;
            }
        }
        internal void AddCanvas(Canvas canvas)
        {
            _canvases.Add(canvas);

            if (!_canvasValidateQued)
            {
                SingleRunPump.RegisterPumpEventUnsafe(ValidateCanvasCache);
                _canvasValidateQued = true;
            }
        }

        internal void RemoveScene(Scene scene)
        {
            _scenes.Remove(scene);

            if (!_sceneValidateQued)
            {
                SingleRunPump.RegisterPumpEventUnsafe(ValidateSceneCache);
                _sceneValidateQued = true;
            }
        }
        internal void AddScene(Scene scene)
        {
            _scenes.Add(scene);

            if (!_sceneValidateQued)
            {
                SingleRunPump.RegisterPumpEventUnsafe(ValidateSceneCache);
                _sceneValidateQued = true;
            }
        }

        internal void DrawTextureUnsafe(Microsoft.Xna.Framework.Graphics.Texture2D texture)
        {
            XNASpriteBatch.Draw(texture, _XNAViewportRectCache, Microsoft.Xna.Framework.Color.White);
        }
        #endregion
        #region Private Methods
        private void ValidateGameManagerCache()
        {
            _gameManagerCache = _gameManagers.ToArray();
            _gameManagerValidateQued = false;
        }
        private void ValidateCanvasCache()
        {
            _canvasCache = _canvases.ToArray();
            _canvasValidateQued = false;
        }
        private void ValidateSceneCache()
        {
            _sceneCache = _scenes.ToArray();
            _sceneValidateQued = false;
        }
        #endregion
        #region Overridable Methods
        protected virtual void Update()
        {

        }
        protected virtual void Draw()
        {

        }
        #endregion
        #region Override Methods
        public override string ToString()
        {
            return $"EpsilonEngine.Game()";
        }
        #endregion
    }
}