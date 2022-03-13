//Approved 3/1/2022 With Note That Large Values Break NextFloat()
namespace EpsilonEngine
{
    public static class RandomnessHelper
    {
        #region Public Variables
        public static readonly System.Random RNG = new System.Random((int)System.DateTime.Now.Ticks);
        #endregion
        #region Public Methods
        public static byte[] NextBytes(int bufferSize)
        {
            if (bufferSize < 0)
            {
                throw new System.Exception("buffer size must be greater than or equal to 0.");
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
            if (min > max)
            {
                throw new System.Exception("max must be greater than min.");
            }
            if (min == max)
            {
                return min;
            }
            return ((float)RNG.NextDouble() * (max - min)) + min;
        }
        #endregion
    }
}