using EpsilonEngine;

namespace DMCCR
{
    public sealed class DMCCR : Game
    {
        public DMCCR() : base(0, 0)
        {
            BackgroundColor = new Color(255, 0, 0, 0);
            Stage stage = new Stage(this);
            TargetFPS = 60.0f;
            FPSCounter fPSCounter = new FPSCounter(stage);
        }
        public override string ToString()
        {
            return $"DMCCR.DMCCR()";
        }
    }
}
