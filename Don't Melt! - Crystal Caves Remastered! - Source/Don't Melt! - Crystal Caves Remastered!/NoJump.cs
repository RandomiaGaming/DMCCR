using EpsilonEngine;
namespace DMCCR
{
    public sealed class NoJump : PhysicsObject
    {
        public NoJump(StagePlayer stagePlayer, PhysicsLayer physicsLayer) : base(stagePlayer, physicsLayer, true)
        {
            SetColliderShape(new Rectangle[1] { new Rectangle(0, 0, 15, 15) });

            CollisionPhysicsLayers = null;
        }
        public override string ToString()
        {
            return $"DMCCR.NoJump()";
        }
    }
}