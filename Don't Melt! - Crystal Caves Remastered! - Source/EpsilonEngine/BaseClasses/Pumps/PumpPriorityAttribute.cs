//Approved 3/1/2022
namespace EpsilonEngine
{
    [System.AttributeUsage(System.AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public sealed class PumpPriorityAttribute : System.Attribute
    {
        #region Public Variables
        public readonly int Priority = 0;
        #endregion
        #region Public Constructors
        public PumpPriorityAttribute(int priority)
        {
            Priority = priority;
        }
        #endregion
        #region Public Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.PumpPriorityAttribute({Priority})";
        }
        #endregion
    }
}