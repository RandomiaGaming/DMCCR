//Approved 3/1/2022
namespace EpsilonEngine
{
    public static class MathHelper
    {
        #region Public Constants
        public const double PIDouble = 3.1415926535897931;
        public const float PIFloat = 3.14159274f;

        public const double EDouble = 2.7182818284590451;
        public const float EFloat = 2.71828175f;

        public const double SQRT2Double = 1.4142135623730952;
        public const double SQRT2Float = 1.41421354f;

        public const double SQRT3Double = 1.7320508075688772;
        public const double SQRT3Float = 1.73205078;
        #endregion
        #region Public Methods
        #region Clamp
        public static int Clamp(int value, int min, int max)
        {
            if (min > max)
            {
                throw new System.Exception("min cannot be greater than max.");
            }
            if (value > max)
            {
                return max;
            }
            else if (value < min)
            {
                return min;
            }
            else
            {
                return value;
            }
        }
        public static float Clamp(float value, float min, float max)
        {
            if (min > max)
            {
                throw new System.Exception("min cannot be greater than max.");
            }
            if (value > max)
            {
                return max;
            }
            else if (value < min)
            {
                return min;
            }
            else
            {
                return value;
            }
        }
        public static double Clamp(double value, double min, double max)
        {
            if (min > max)
            {
                throw new System.Exception("min cannot be greater than max.");
            }
            if (value > max)
            {
                return max;
            }
            else if (value < min)
            {
                return min;
            }
            else
            {
                return value;
            }
        }
        #endregion
        #region LoopClamp
        public static int LoopClamp(int value, int min, int max)
        {
            if (min > max)
            {
                throw new System.Exception("min cannot be greater than max.");
            }
            int loopCount = (value - min) / (max - min);
            return value - (loopCount * (max - min));
        }
        public static float LoopClamp(float value, float min, float max)
        {
            if (min > max)
            {
                throw new System.Exception("min cannot be greater than max.");
            }
            int loopCount = (int)((value - min) / (max - min));
            return value - (loopCount * (max - min));
        }
        public static double LoopClamp(double value, double min, double max)
        {
            if (min > max)
            {
                throw new System.Exception("min cannot be greater than max.");
            }
            int loopCount = (int)((value - min) / (max - min));
            return value - (loopCount * (max - min));
        }
        #endregion
        #region Abs
        public static int Abs(int value)
        {
            if (value < 0)
            {
                return -value;
            }
            else
            {
                return value;
            }
        }
        public static float Abs(float value)
        {
            if (value < 0)
            {
                return -value;
            }
            else
            {
                return value;
            }
        }
        public static double Abs(double value)
        {
            if (value < 0)
            {
                return -value;
            }
            else
            {
                return value;
            }
        }
        #endregion
        #region Min
        public static int Min(int a, int b)
        {
            if (a < b)
            {
                return a;
            }
            else
            {
                return b;
            }
        }
        public static float Min(float a, float b)
        {
            if (a < b)
            {
                return a;
            }
            else
            {
                return b;
            }
        }
        public static double Min(double a, double b)
        {
            if (a < b)
            {
                return a;
            }
            else
            {
                return b;
            }
        }
        #endregion
        #region Max
        public static int Max(int a, int b)
        {
            if (a > b)
            {
                return a;
            }
            else
            {
                return b;
            }
        }
        public static float Max(float a, float b)
        {
            if (a > b)
            {
                return a;
            }
            else
            {
                return b;
            }
        }
        public static double Max(double a, double b)
        {
            if (a > b)
            {
                return a;
            }
            else
            {
                return b;
            }
        }
        #endregion
        #region Sqrt
        public static int Sqrt(int value)
        {
            return (int)System.Math.Sqrt(value);
        }
        public static float Sqrt(float value)
        {
            return (float)System.Math.Sqrt(value);
        }
        public static double Sqrt(double value)
        {
            return System.Math.Sqrt(value);
        }
        #endregion
        #region Lerp
        public static int Lerp(float sample, int a, int b)
        {
            return a + (int)((b - a) * sample);
        }
        public static float Lerp(float sample, float a, float b)
        {
            return a + ((b - a) * sample);
        }
        public static double Lerp(float sample, double a, double b)
        {
            return a + ((b - a) * sample);
        }
        public static int Lerp(double sample, int a, int b)
        {
            return a + (int)((b - a) * sample);
        }
        public static float Lerp(double sample, float a, float b)
        {
            return a + (float)((b - a) * sample);
        }
        public static double Lerp(double sample, double a, double b)
        {
            return a + ((b - a) * sample);
        }
        #endregion
        #region InverseLerp
        public static int InverseLerp(float sample, int a, int b)
        {
            return Lerp(sample * -1 + 1, a, b);
        }
        public static float InverseLerp(float sample, float a, float b)
        {
            return Lerp(sample * -1 + 1, a, b);
        }
        public static double InverseLerp(float sample, double a, double b)
        {
            return Lerp(sample * -1 + 1, a, b);
        }
        public static int InverseLerp(double sample, int a, int b)
        {
            return Lerp(sample * -1 + 1, a, b);
        }
        public static float InverseLerp(double sample, float a, float b)
        {
            return Lerp(sample * -1 + 1, a, b);
        }
        public static double InverseLerp(double sample, double a, double b)
        {
            return Lerp(sample * -1 + 1, a, b);
        }
        #endregion
        #endregion
    }
}