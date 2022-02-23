using System;
using System.Reflection;
namespace EpsilonEngine
{
    public abstract class GameManager
    {
        #region Public Variables
        public Game Game { get; private set; } = null;

        public bool IsDestroyed { get; private set; } = false;
        public bool OverridesUpdate { get; private set; } = false;
        public bool OverridesRender { get; private set; } = false;
        #endregion
        #region Constructors
        public GameManager(Game game, int updatePriority, int drawPriority)
        {
            if (game is null)
            {
                throw new Exception("game cannot be null.");
            }

            Game = game;

            Game.AddGameManager(this);

            Type thisType = GetType();

            MethodInfo updateMethod = thisType.GetMethod("Update", BindingFlags.NonPublic | BindingFlags.Instance);
            if (updateMethod.DeclaringType != typeof(GameManager))
            {
                OverridesUpdate = true;
                Game.UpdatePump.RegisterPumpEventUnsafe(Update, updatePriority);
            }

            MethodInfo drawMethod = thisType.GetMethod("Draw", BindingFlags.NonPublic | BindingFlags.Instance);
            if (drawMethod.DeclaringType != typeof(GameManager))
            {
                OverridesRender = true;
                Game.DrawPump.RegisterPumpEventUnsafe(Draw, drawPriority);
            }
        }
        #endregion
        #region Public Methods
        public void Destroy()
        {
            Game.RemoveGameManager(this);

            if (OverridesUpdate)
            {
                Game.UpdatePump.UnregisterPumpEventUnsafe(Update);
            }

            if (OverridesRender)
            {
                Game.DrawPump.UnregisterPumpEventUnsafe(Draw);
            }

            Game = null;

            IsDestroyed = true;
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
            return $"EpsilonEngine.GameManager()";
        }
        #endregion
    }
}