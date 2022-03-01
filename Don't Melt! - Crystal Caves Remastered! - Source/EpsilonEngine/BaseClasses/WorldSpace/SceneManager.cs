using System;
using System.Reflection;
namespace EpsilonEngine
{
    public abstract class SceneManager
    {
        #region Public Variables
        public bool Destroyed { get; private set; } = false;

        public Game Game { get; private set; } = null;
        public Scene Scene { get; private set; } = null;

        public bool OverridesUpdate { get; private set; } = false;
        public int UpdatePriority { get; private set; } = 0;
        public bool OverridesRender { get; private set; } = false;
        public int RenderPriority { get; private set; } = 0;
        #endregion
        #region Constructors
        public SceneManager(Scene scene)
        {
            if (scene is null)
            {
                throw new Exception("scene cannot be null.");
            }

            Scene = scene;
            Game = Scene.Game;

            Scene.AddSceneManager(this);

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
                Scene.RenderPump.RegisterPumpEventUnsafe(Render, RenderPriority);
                OverridesRender = true;
            }
        }
        #endregion
        #region Public Methods
        public void Destroy()
        {
            Scene.RemoveSceneManager(this);

            Game = null;
            Scene = null;

            Destroyed = true;
        }
        #endregion
        #region Override Methods
        public override string ToString()
        {
            return $"EpsilonEngine.SceneManager()";
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