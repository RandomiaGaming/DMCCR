using EpsilonEngine;

namespace DMCCR
{
    public sealed class DMCCR : Game
    {
        public DMCCR()
        {
            BackgroundColor = new Color(255, 0, 0, 0);
           Stage stage = new Stage(this);
            TargetFPS = 60.0f;
        }
        public override string ToString()
        {
            return $"DMCCR.DMCCR()";
        }
    }
}
