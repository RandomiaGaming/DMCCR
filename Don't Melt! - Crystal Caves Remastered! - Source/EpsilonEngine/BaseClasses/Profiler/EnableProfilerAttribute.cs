//Approved 3/1/2022
namespace EpsilonEngine
{
    [System.AttributeUsage(System.AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
    public sealed class EnableProfilerAttribute : System.Attribute
    {
        #region Public Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.EnableProfilerAttribute()";
        }
        #endregion
    }
}