using EpsilonEngine;
namespace DMCCR
{
    public sealed class NoJump : PhysicsObject
    {
        public NoJump(Stage stagePlayer) : base(stagePlayer, true)
        {
            SetColliderShape(new Rectangle[1] { new Rectangle(0, 0, 15, 15) });
        }
        public override string ToString()
        {
            return $"DMCCR.NoJump()";
        }
    }
}