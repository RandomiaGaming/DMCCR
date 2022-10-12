using EpsilonEngine;

namespace DMCCR
{
    public sealed class DMCCR : Game
    {
        public DMCCR() : base(0, 0)
        {
            BackgroundColor = new Color(255, 0, 0, 0);
            Stage stage = new Stage(this);
            TargetFPS = 60.0;
            //FPSCounter fPSCounter = new FPSCounter(stage);
            //Canvas canvas = new Canvas(this);
            //canvas.AddElement(new Element(canvas));
        }
        public override string ToString()
        {
            return $"DMCCR.DMCCR()";
        }
        public static string CreateFastRectArrayConstructor(Rect[] rectArray, string rectArrayName)
        {
            if (rectArray is null)
            {
                throw new System.Exception("rectArray cannot be null.");
            }
            if (rectArrayName is null)
            {
                throw new System.Exception("rectArrayName cannot be null.");
            }
            if (rectArrayName is "")
            {
                throw new System.Exception("rectArrayName cannot be empty.");
            }
            string output = $"#region {rectArrayName} - Fast Rect[] Constructor";
            output += $"\nRect[] {rectArrayName} = new Rect[{rectArray.Length}];";
            for (int i = 0; i < rectArray.Length; i++)
            {
                output += $"\n{rectArrayName}[{i}]._minX = {rectArray[i]._minX};";
                output += $"\n{rectArrayName}[{i}]._minY = {rectArray[i]._minY};";
                output += $"\n{rectArrayName}[{i}]._maxX = {rectArray[i]._maxX};";
                output += $"\n{rectArrayName}[{i}]._maxY = {rectArray[i]._maxY};";
            }
            output += $"\n#endregion";
            return output;
        }
    }
}
