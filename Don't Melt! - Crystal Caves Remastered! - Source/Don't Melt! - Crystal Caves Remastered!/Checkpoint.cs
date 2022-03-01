using EpsilonEngine;
using System.Reflection;
namespace DMCCR
{
    public sealed class Checkpoint : PhysicsObject
    {
        private StagePlayer _stagePlayer;

        private TextureRenderer _textureRenderer = null;

        private Texture _checkPointLockedTexture = null;
        private Texture _checkPointUnlockedTexture = null;
        public Checkpoint(StagePlayer stagePlayer, PhysicsLayer physicsLayer, PhysicsLayer[] collisionPhysicsLayers) : base(stagePlayer, physicsLayer, false)
        {
            _stagePlayer = stagePlayer;

            _checkPointLockedTexture = new Texture(Game, Assembly.GetExecutingAssembly().GetManifestResourceStream("DMCCR.Don_t_Melt____Crystal_Caves_Remastered_.Textures.Checkpoint Locked.png"));
            _checkPointUnlockedTexture = new Texture(Game, Assembly.GetExecutingAssembly().GetManifestResourceStream("DMCCR.Don_t_Melt____Crystal_Caves_Remastered_.Textures.Checkpoint Unlocked.png"));

            _textureRenderer = new TextureRenderer(this, -1);

            _textureRenderer.Texture = _checkPointLockedTexture;

            SetColliderShape(new Rectangle[1] { new Rectangle(0, 0, 15, 31) });

            CollisionPhysicsLayers = collisionPhysicsLayers;

            LogCollisionsUp = false;
            LogCollisionsDown = false;
            LogCollisionsLeft = false;
            LogCollisionsRight = false;
            LogOverlaps = true;
        }
        protected override void Update()
        {
            if(Position == _stagePlayer.CheckPointPos)
            {
                _textureRenderer.Texture = _checkPointUnlockedTexture;
            }
            else
            {
                _textureRenderer.Texture = _checkPointLockedTexture;
            }

            foreach (PhysicsObject physicsObject in _overlaps)
            {
                if (physicsObject.GetType() == typeof(Player))
                {
                    _stagePlayer.CheckPointPos = Position;
                }
            }
        }
        public override string ToString()
        {
            return $"DMCCR.Checkpoint()";
        }
    }
}