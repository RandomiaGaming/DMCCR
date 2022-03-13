using EpsilonEngine;
namespace DMCCR
{
    public sealed class Lava : PhysicsObject
    {
        public Lava(Stage stagePlayer, Rectangle colliderShape) : base(stagePlayer, true)
        {
            SetColliderShape(new Rectangle[1] { colliderShape });
        }
        public override string ToString()
        {
            return $"DMCCR.Lava()";
        }
    }
}