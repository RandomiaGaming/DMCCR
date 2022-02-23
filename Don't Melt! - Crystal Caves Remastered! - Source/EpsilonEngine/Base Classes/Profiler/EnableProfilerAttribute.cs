using System;
namespace EpsilonEngine
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
    public sealed class EnableProfilerAttribute : Attribute
    {
        #region Override Methods
        public override string ToString()
        {
            return $"EpsilonEngine.EnableProfilerAttribute()";
        }
        #endregion
    }
}