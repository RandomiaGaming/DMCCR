using EpsilonEngine;
namespace DMCCR
{
    public sealed class Ground : PhysicsObject
    {
        public Ground(Stage stagePlayer, Rectangle colliderShape) : base(stagePlayer, true)
        {
           SetColliderShape(new Rectangle[1] { colliderShape });
        }
        public override string ToString()
        {
            return $"DMCCR.Ground()";
        }
    }
}