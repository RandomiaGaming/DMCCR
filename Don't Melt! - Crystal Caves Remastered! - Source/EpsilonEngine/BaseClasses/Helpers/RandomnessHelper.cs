using System;
namespace EpsilonEngine
{
    public static class RandomnessHelper
    {
        #region Public Variables
        public static Random RNG { get; private set; } = new Random((int)DateTime.Now.Ticks);
        #endregion
        #region Public Methods
        public static int NextInt()
        {
            return BitConverter.ToInt32(NextBytes(4), 0);
        }
        public static int NextInt(int min, int max)
        {
            if (min > max)
            {
                throw new Exception("max must be greater than min.");
            }
            if (min == max)
            {
                return min;
            }
            if (max == int.MaxValue)
            {
                return RNG.Next(min, max);
            }
            return RNG.Next(min, max + 1);
        }
        public static byte[] NextBytes(int bufferSize)
        {
            if (bufferSize < 0)
            {
                throw new Exception("buffer size must be greater than or equal to 0.");
            }
            if (bufferSize == 0)
            {
                return new byte[0];
            }
            byte[] buffer = new byte[bufferSize];
            RNG.NextBytes(buffer);
            return buffer;
        }
        public static double NextDouble(double min, double max)
        {
            if (min > max)
            {
                throw new Exception("max must be greater than min.");
            }
            if (min == max)
            {
                return min;
            }
            return min + (RNG.NextDouble() * (max - min));
        }
        #endregion
    }
}