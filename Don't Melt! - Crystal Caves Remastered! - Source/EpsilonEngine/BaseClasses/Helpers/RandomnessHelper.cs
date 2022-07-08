//Approved 07/05/2022
namespace EpsilonEngine
{
    public static class RandomnessHelper
    {
        #region Public Static Variables
        public static readonly System.Random RNG = new System.Random((int)System.DateTime.Now.Ticks);
        #endregion
        #region Public Static Methods
        public static byte[] NextBytes(int bufferSize)
        {
            if (bufferSize < 0)
            {
                throw new System.Exception("bufferSize must be greater than or equal to 0.");
            }
            if (bufferSize == 0)
            {
                return new byte[0];
            }
            byte[] buffer = new byte[bufferSize];
            RNG.NextBytes(buffer);
            return buffer;
        }
        public static int NextInt(int min, int max)
        {
            if (min > max)
            {
                throw new System.Exception("max must be greater or equal to min.");
            }
            if (max == int.MaxValue)
            {
                throw new System.Exception("max must be less than int.MaxValue.");
            }
            if (min == max)
            {
                return min;
            }
            return RNG.Next(min, max + 1);
        }
        public static float NextFloat(float min, float max)
        {
            if (min == float.NaN || min == float.PositiveInfinity || min == float.NegativeInfinity)
            {
                throw new System.Exception("min must be a real number.");
            }
            if (max == float.NaN || max == float.PositiveInfinity || max == float.NegativeInfinity)
            {
                throw new System.Exception("max must be a real number.");
            }
            if (min >= max)
            {
                throw new System.Exception("max must be greater than or equal to min.");
            }
            if (min == max)
            {
                return min;
            }
            return ((float)RNG.NextDouble() * (max - min)) + min;
        }
        public static double NextDouble(double min, double max)
        {
            if (min == double.NaN || min == double.PositiveInfinity || min == double.NegativeInfinity)
            {
                throw new System.Exception("min must be a real number.");
            }
            if (max == double.NaN || max == double.PositiveInfinity || max == double.NegativeInfinity)
            {
                throw new System.Exception("max must be a real number.");
            }
            if (min >= max)
            {
                throw new System.Exception("max must be greater than or equal to min.");
            }
            if (min == max)
            {
                return min;
            }
            return (RNG.NextDouble() * (max - min)) + min;
        }
        #endregion
    }
}