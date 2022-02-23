using EpsilonEngine;
namespace DMCCR
{
    public sealed class DMCCR : Game
    {
        public DMCCR()
        {
            BackgroundColor = new Color(194, 194, 194, 0);
            new StagePlayer(this);
        }
        public override string ToString()
        {
            return $"DMCCR.DMCCR()";
        }
    }
}
