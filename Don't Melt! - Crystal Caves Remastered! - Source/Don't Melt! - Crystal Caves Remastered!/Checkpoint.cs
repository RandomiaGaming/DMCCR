using EpsilonEngine;
namespace DMCCR
{
    public sealed class Checkpoint : PhysicsObject
    {
        public Checkpoint(StagePlayer stagePlayer, PhysicsLayer physicsLayer, Texture checkpointTexture) : base(stagePlayer, physicsLayer, true)
        {
            TextureRenderer textureRenderer = new TextureRenderer(this, 0);
            textureRenderer.Texture = checkpointTexture;

            SetColliderShape(new Rectangle[1] { new Rectangle(0, 0, 15, 31) } );

            CollisionPhysicsLayers = null;
        }
        public override string ToString()
        {
            return $"DMCCR.Checkpoint()";
        }
    }
}