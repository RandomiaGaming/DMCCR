using EpsilonEngine;
namespace DMCCR
{
    public sealed class Lava : PhysicsObject
    {
        public Lava(StagePlayer stagePlayer, PhysicsLayer physicsLayer) : base(stagePlayer, physicsLayer, true)
        {
            SetColliderShape(new Rectangle[1] { new Rectangle(1, 1, 14, 14) });

            CollisionPhysicsLayers = null;
        }
        public override string ToString()
        {
            return $"DMCCR.Lava()";
        }
    }
}