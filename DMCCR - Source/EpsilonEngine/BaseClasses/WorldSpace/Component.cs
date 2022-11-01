namespace EpsilonEngine
{
    public abstract class Component
    {
        #region Public Variables
        public bool Destroyed { get; private set; } = false;

        public Game Game { get; private set; } = null;
        public Scene Scene { get; private set; } = null;
        public GameObject GameObject { get; private set; } = null;

        public bool OverridesUpdate { get; private set; } = false;
        public int UpdatePriority { get; private set; } = 0;
        public bool OverridesRender { get; private set; } = false;
        public int RenderPriority { get; private set; } = 0;
        #endregion
        #region Constructors
        public Component(GameObject gameObject, int updatePriority, int renderPriority)
        {
            if (gameObject is null)
            {
                throw new System.Exception("gameObject cannot be null.");
            }

            GameObject = gameObject;
            Scene = GameObject.Scene;
            Game = Scene.Game;

            GameObject.AddComponent(this);

            Type thisType = GetType();

            UpdatePriority = updatePriority;

            MethodInfo updateMethod = thisType.GetMethod("Update", BindingFlags.NonPublic | BindingFlags.Instance);
            if (updateMethod.DeclaringType != typeof(Component))
            {
                Game.UpdatePump.RegisterPumpEventUnsafe(Update, UpdatePriority);
                OverridesUpdate = true;
            }

            RenderPriority = renderPriority;

            MethodInfo renderMethod = thisType.GetMethod("Render", BindingFlags.NonPublic | BindingFlags.Instance);
            if (renderMethod.DeclaringType != typeof(Component))
            {
                Scene.RenderPump.RegisterPumpEventUnsafe(Render, RenderPriority);
                OverridesRender = true;
            }
        }
        #endregion
        #region Public Methods
        public void Destroy()
        {
            GameObject.RemoveComponent(this);

            Game = null;
            Scene = null;
            GameObject = null;

            Destroyed = true;
        }
        #endregion
        #region Override Methods
        public override string ToString()
        {
            return $"EpsilonEngine.Component()";
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