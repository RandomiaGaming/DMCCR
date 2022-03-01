using System;
namespace EpsilonEngine
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public sealed class PumpPriorityAttribute : Attribute
    {
        #region Public Variables
        public int Priority { get; private set; } = 0;
        #endregion
        #region Constructors
        public PumpPriorityAttribute(int priority)
        {
            Priority = priority;
        }
        #endregion
        #region Override Methods
        public override string ToString()
        {
            return $"EpsilonEngine.PumpPriorityAttribute()";
        }
        #endregion
    }
}