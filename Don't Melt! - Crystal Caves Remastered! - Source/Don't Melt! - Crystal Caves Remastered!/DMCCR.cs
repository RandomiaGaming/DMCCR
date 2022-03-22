using EpsilonEngine;

namespace DMCCR
{
    public sealed class DMCCR : Game
    {
        public DMCCR()
        {
            BackgroundColor = new Color(255, 0, 0, 0);
            Stage stage = new Stage(this);
            //Scene scene = new Scene(this, 256, 144, 0);
            FPSCounter fPSCounter = new FPSCounter(stage);
        }
        public override string ToString()
        {
            return $"DMCCR.DMCCR()";
        }
    }
}
