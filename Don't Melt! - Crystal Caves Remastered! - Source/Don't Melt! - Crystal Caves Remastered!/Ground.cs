using EpsilonEngine;
namespace DMCCR
{
    public sealed class Ground : PhysicsObject
    {
        public Ground(StagePlayer stagePlayer, PhysicsLayer physicsLayer) : base(stagePlayer, physicsLayer, true)
        {
            SetColliderShape(new Rectangle[1] { new Rectangle(0, 0, 15, 15) });

            CollisionPhysicsLayers = null;
        }
        public override string ToString()
        {
            return $"DMCCR.Ground()";
        }
    }
}