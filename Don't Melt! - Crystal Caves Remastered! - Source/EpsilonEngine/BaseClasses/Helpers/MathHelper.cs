
namespace EpsilonEngine
{
    public static class MathHelper
    {
        #region Public Constants
        public const double TauDouble = 6.2831853071795862;
        public const float TauFloat = 6.28318548f;

        public const double PIDouble = 3.1415926535897931;
        public const float PIFloat = 3.14159274f;

        public const double EDouble = 2.7182818284590451;
        public const float EFloat = 2.71828175f;

        public const double SQRT2Double = 1.4142135623730952;
        public const double SQRT2Float = 1.41421354f;

        public const double SQRT3Double = 1.7320508075688772;
        public const float SQRT3Float = 1.73205078f;

        public const double RadToDegDouble = 57.295779513082323;
        public const float RadToDegFloat = 57.2957764f;

        public const double DegToRadDouble = 0.017453292519943295;
        public const float DegToRadFloat = 0.0174532924f;

        public const double SinOf45Double = 0.70710678118654746;
        public const float SinOf45Float = 0.707106769f;

        public const double SinOf60Double = 0.8660254037844386;
        public const float SinOf60Float = 0.8660254f;
        #endregion
        #region Public Static Methods
        //System.Math
        #region Abs
        public static int Abs(int value)
        {
            if (value < 0)
            {
                return -value;
            }
            return value;
        }
        public static float Abs(float value)
        {
            if (value == float.NaN || value == float.PositiveInfinity || value == float.NegativeInfinity)
            {
                throw new System.Exception("value must be a real number.");
            }
            if (value < 0)
            {
                return -value;
            }
            return value;
        }
        public static double Abs(double value)
        {
            if (value == double.NaN || value == double.PositiveInfinity || value == double.NegativeInfinity)
            {
                throw new System.Exception("value must be a real number.");
            }
            if (value < 0)
            {
                return -value;
            }
            return value;
        }
        #endregion
        #region ACos
        public static double ACos(double value)
        {
            if (value == double.NaN || value == double.PositiveInfinity || value == double.NegativeInfinity)
            {
                throw new System.Exception("value must be a real number.");
            }
            if (value > 1)
            {
                throw new System.Exception("value must be less than or equal to 1.");
            }
            if (value < -1)
            {
                throw new System.Exception("value must be greater than or equal to -1.");
            }
            double output = (System.Math.Acos(value) * 180) / PIDouble;
            if (output < 0)
            {
                return 360 + output;
            }
            return output;
        }
        #endregion
        #region ASin
        public static double ASin(double value)
        {
            if (value == double.NaN || value == double.PositiveInfinity || value == double.NegativeInfinity)
            {
                throw new System.Exception("value must be a real number.");
            }
            if (value > 1)
            {
                throw new System.Exception("value must be less than or equal to 1.");
            }
            if (value < -1)
            {
                throw new System.Exception("value must be greater than or equal to -1.");
            }
            double output = (System.Math.Asin(value) * 180) / PIDouble;
            if (output < 0)
            {
                return 360 + output;
            }
            return output;
        }
        #endregion
        #region ATan
        public static double ATan(double value)
        {
            if (value == double.NaN || value == double.PositiveInfinity || value == double.NegativeInfinity)
            {
                throw new System.Exception("value must be a real number.");
            }
            double output = (System.Math.Atan(value) * 180) / PIDouble;
            if (output < 0)
            {
                return 360 + output;
            }
            return output;
        }
        #endregion
        #region ATan2
        public static double ATan2(double x, double y)
        {
            if (x == double.NaN || x == double.PositiveInfinity || x == double.NegativeInfinity)
            {
                throw new System.Exception("x must be a real number.");
            }
            if (y == double.NaN || y == double.PositiveInfinity || y == double.NegativeInfinity)
            {
                throw new System.Exception("y must be a real number.");
            }
            double output = (System.Math.Atan2(x, y) * 180) / PIDouble;
            if (output < 0)
            {
                return 360 + output;
            }
            return output;
        }
        #endregion
        #region Cbrt
        public static double Cbrt(double value)
        {
            if (value == double.NaN || value == double.PositiveInfinity || value == double.NegativeInfinity)
            {
                throw new System.Exception("value must be a real number.");
            }
            return System.Math.Pow(value, 0.33333333333333331);
        }
        #endregion
        #region Ceil
        public static double Ceil(double value)
        {
            if (value == double.NaN || value == double.PositiveInfinity || value == double.NegativeInfinity)
            {
                throw new System.Exception("value must be a real number.");
            }
            return System.Math.Ceiling(value);
        }
        #endregion
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
            return value;
        }
        public static float Clamp(float value, float min, float max)
        {
            if (value == float.NaN || value == float.PositiveInfinity || value == float.NegativeInfinity)
            {
                throw new System.Exception("value must be a real number.");
            }
            if (min == float.NaN || min == float.PositiveInfinity || min == float.NegativeInfinity)
            {
                throw new System.Exception("min must be a real number.");
            }
            if (max == float.NaN || max == float.PositiveInfinity || max == float.NegativeInfinity)
            {
                throw new System.Exception("max must be a real number.");
            }
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
            return value;
        }
        public static double Clamp(double value, double min, double max)
        {
            if (value == double.NaN || value == double.PositiveInfinity || value == double.NegativeInfinity)
            {
                throw new System.Exception("value must be a real number.");
            }
            if (min == double.NaN || min == double.PositiveInfinity || min == double.NegativeInfinity)
            {
                throw new System.Exception("min must be a real number.");
            }
            if (max == double.NaN || max == double.PositiveInfinity || max == double.NegativeInfinity)
            {
                throw new System.Exception("max must be a real number.");
            }
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
            return value;
        }
        #endregion
        #region Cos
        public static double Cos(double value)
        {
            if (value == double.NaN || value == double.PositiveInfinity || value == double.NegativeInfinity)
            {
                throw new System.Exception("value must be a real number.");
            }
            return System.Math.Cos((value * PIDouble) / 180);
        }
        #endregion
        #region Rem
        public static double Rem(double dividend, double divisor)
        {
            if (dividend == float.NaN || dividend == float.PositiveInfinity || dividend == float.NegativeInfinity)
            {
                throw new System.Exception("dividend must be a real number.");
            }
            if (divisor == float.NaN || divisor == float.PositiveInfinity || divisor == float.NegativeInfinity)
            {
                throw new System.Exception("divisor must be a real number.");
            }
            if (divisor == 0)
            {
                throw new System.Exception("divisor cannot be 0.");
            }
            double quotient = dividend / divisor;
            if (quotient < 0)
            {
                quotient = -System.Math.Floor(-quotient);
            }
            else
            {
                quotient = System.Math.Floor(quotient);
            }
            return dividend - (quotient * divisor);
        }
        #endregion
        #region Exp
        public static double Exp(double value)
        {
            if (value == double.NaN || value == double.PositiveInfinity || value == double.NegativeInfinity)
            {
                throw new System.Exception("value must be a real number.");
            }
            return System.Math.Exp(value);
        }
        #endregion
        #region Floor
        public static double Floor(double value)
        {
            if (value == double.NaN || value == double.PositiveInfinity || value == double.NegativeInfinity)
            {
                throw new System.Exception("value must be a real number.");
            }
            return System.Math.Floor(value);
        }
        #endregion
        #region Ln
        public static double Ln(double value)
        {
            if (value == double.NaN || value == double.PositiveInfinity || value == double.NegativeInfinity)
            {
                throw new System.Exception("value must be a real number.");
            }
            if (value < 0)
            {
                throw new System.Exception("value must be greater than or equal to 0.");
            }
            return System.Math.Log(value);
        }
        #endregion
        #region Log
        public static double Log(double value, double logBase)
        {
            if (value == double.NaN || value == double.PositiveInfinity || value == double.NegativeInfinity)
            {
                throw new System.Exception("value must be a real number.");
            }
            if (logBase == double.NaN || logBase == double.PositiveInfinity || logBase == double.NegativeInfinity)
            {
                throw new System.Exception("logBase must be a real number.");
            }
            if (value < 0)
            {
                throw new System.Exception("value must be greater than or equal to 0.");
            }
            if (logBase <= 0)
            {
                throw new System.Exception("logBase must be greater than 0.");
            }
            return System.Math.Log(value, logBase);
        }
        #endregion
        #region Log2
        public static double Log2(double value)
        {
            if (value == double.NaN || value == double.PositiveInfinity || value == double.NegativeInfinity)
            {
                throw new System.Exception("value must be a real number.");
            }
            if (value < 0)
            {
                throw new System.Exception("value must be greater than or equal to 0.");
            }
            return System.Math.Log(value, 2);
        }
        #endregion
        #region Log10
        public static double Log10(double value)
        {
            if (value == double.NaN || value == double.PositiveInfinity || value == double.NegativeInfinity)
            {
                throw new System.Exception("value must be a real number.");
            }
            if (value < 0)
            {
                throw new System.Exception("value must be greater than or equal to 0.");
            }
            return System.Math.Log(value, 10);
        }
        #endregion
        #region Max
        public static int Max(int a, int b)
        {
            if (a > b)
            {
                return a;
            }
            return b;
        }
        public static float Max(float a, float b)
        {
            if (a == float.NaN || a == float.PositiveInfinity || a == float.NegativeInfinity)
            {
                throw new System.Exception("a must be a real number.");
            }
            if (b == float.NaN || b == float.PositiveInfinity || b == float.NegativeInfinity)
            {
                throw new System.Exception("b must be a real number.");
            }
            if (a < b)
            {
                return b;
            }
            return a;
        }
        public static double Max(double a, double b)
        {
            if (a == double.NaN || a == double.PositiveInfinity || a == double.NegativeInfinity)
            {
                throw new System.Exception("a must be a real number.");
            }
            if (b == double.NaN || b == double.PositiveInfinity || b == double.NegativeInfinity)
            {
                throw new System.Exception("b must be a real number.");
            }
            if (a < b)
            {
                return b;
            }
            return a;
        }
        #endregion
        #region MaxMag
        public static int MaxMag(int a, int b)
        {
            if (a >= 0 && b >= 0)
            {
                if (a < b)
                {
                    return b;
                }
                return a;
            }
            else if (a < 0 && b < 0)
            {
                if (a > b)
                {
                    return b;
                }
                return a;
            }
            else if (a >= 0 && b < 0)
            {
                if (a < -b)
                {
                    return b;
                }
                return a;
            }
            else if (a > -b)
            {
                return b;
            }
            return a;
        }
        public static float MaxMag(float a, float b)
        {
            if (a == float.NaN || a == float.PositiveInfinity || a == float.NegativeInfinity)
            {
                throw new System.Exception("a must be a real number.");
            }
            if (b == float.NaN || b == float.PositiveInfinity || b == float.NegativeInfinity)
            {
                throw new System.Exception("b must be a real number.");
            }
            if (a >= 0 && b >= 0)
            {
                if (a < b)
                {
                    return b;
                }
                return a;
            }
            else if (a < 0 && b < 0)
            {
                if (a > b)
                {
                    return b;
                }
                return a;
            }
            else if (a >= 0 && b < 0)
            {
                if (a < -b)
                {
                    return b;
                }
                return a;
            }
            else if (a > -b)
            {
                return b;
            }
            return a;
        }
        public static double MaxMag(double a, double b)
        {
            if (a == double.NaN || a == double.PositiveInfinity || a == double.NegativeInfinity)
            {
                throw new System.Exception("a must be a real number.");
            }
            if (b == double.NaN || b == double.PositiveInfinity || b == double.NegativeInfinity)
            {
                throw new System.Exception("b must be a real number.");
            }
            if (a >= 0 && b >= 0)
            {
                if (a < b)
                {
                    return b;
                }
                return a;
            }
            else if (a < 0 && b < 0)
            {
                if (a > b)
                {
                    return b;
                }
                return a;
            }
            else if (a >= 0 && b < 0)
            {
                if (a < -b)
                {
                    return b;
                }
                return a;
            }
            else if (a > -b)
            {
                return b;
            }
            return a;
        }
        #endregion
        #region Min
        public static int Min(int a, int b)
        {
            if (a < b)
            {
                return a;
            }
            return b;
        }
        public static float Min(float a, float b)
        {
            if (a == float.NaN || a == float.PositiveInfinity || a == float.NegativeInfinity)
            {
                throw new System.Exception("a must be a real number.");
            }
            if (b == float.NaN || b == float.PositiveInfinity || b == float.NegativeInfinity)
            {
                throw new System.Exception("b must be a real number.");
            }
            if (a > b)
            {
                return b;
            }
            return a;
        }
        public static double Min(double a, double b)
        {
            if (a == double.NaN || a == double.PositiveInfinity || a == double.NegativeInfinity)
            {
                throw new System.Exception("a must be a real number.");
            }
            if (b == double.NaN || b == double.PositiveInfinity || b == double.NegativeInfinity)
            {
                throw new System.Exception("b must be a real number.");
            }
            if (a > b)
            {
                return b;
            }
            return a;
        }
        #endregion
        #region MinMag
        public static int MinMag(int a, int b)
        {
            if (a >= 0 && b >= 0)
            {
                if (a > b)
                {
                    return b;
                }
                return a;
            }
            else if (a < 0 && b < 0)
            {
                if (a < b)
                {
                    return b;
                }
                return a;
            }
            else if (a >= 0 && b < 0)
            {
                if (a > -b)
                {
                    return b;
                }
                return a;
            }
            else if (a < -b)
            {
                return b;
            }
            return a;
        }
        public static float MinMag(float a, float b)
        {
            if (a == float.NaN || a == float.PositiveInfinity || a == float.NegativeInfinity)
            {
                throw new System.Exception("a must be a real number.");
            }
            if (b == float.NaN || b == float.PositiveInfinity || b == float.NegativeInfinity)
            {
                throw new System.Exception("b must be a real number.");
            }
            if (a >= 0 && b >= 0)
            {
                if (a > b)
                {
                    return b;
                }
                return a;
            }
            else if (a < 0 && b < 0)
            {
                if (a < b)
                {
                    return b;
                }
                return a;
            }
            else if (a >= 0 && b < 0)
            {
                if (a > -b)
                {
                    return b;
                }
                return a;
            }
            else if (a < -b)
            {
                return b;
            }
            return a;
        }
        public static double MinMag(double a, double b)
        {
            if (a == double.NaN || a == double.PositiveInfinity || a == double.NegativeInfinity)
            {
                throw new System.Exception("a must be a real number.");
            }
            if (b == double.NaN || b == double.PositiveInfinity || b == double.NegativeInfinity)
            {
                throw new System.Exception("b must be a real number.");
            }
            if (a >= 0 && b >= 0)
            {
                if (a > b)
                {
                    return b;
                }
                return a;
            }
            else if (a < 0 && b < 0)
            {
                if (a < b)
                {
                    return b;
                }
                return a;
            }
            else if (a >= 0 && b < 0)
            {
                if (a > -b)
                {
                    return b;
                }
                return a;
            }
            else if (a < -b)
            {
                return b;
            }
            return a;
        }
        #endregion
        #region Pow
        public static double Pow(double value, double power)
        {
            if (value == double.NaN || value == double.PositiveInfinity || value == double.NegativeInfinity)
            {
                throw new System.Exception("value must be a real number.");
            }
            if (power == double.NaN || power == double.PositiveInfinity || power == double.NegativeInfinity)
            {
                throw new System.Exception("power must be a real number.");
            }
            return System.Math.Pow(value, power);
        }
        #endregion
        #region Recip
        public static double Recip(double value)
        {
            if (value == double.NaN || value == double.PositiveInfinity || value == double.NegativeInfinity)
            {
                throw new System.Exception("value must be a real number.");
            }
            if (value == 0)
            {
                throw new System.Exception("value cannot be 0.");
            }
            return 1 / value;
        }
        #endregion
        #region Round
        public static double Round(double value)
        {
            if (value == double.NaN || value == double.PositiveInfinity || value == double.NegativeInfinity)
            {
                throw new System.Exception("value must be a real number.");
            }
            return System.Math.Round(value);
        }
        #endregion
        #region Sign
        public static int Sign(int value)
        {
            if (value == 0)
            {
                return 0;
            }
            else if (value < 0)
            {
                return -1;
            }
            return 0;
        }
        public static float Sign(float value)
        {
            if (value == float.NaN || value == float.PositiveInfinity || value == float.NegativeInfinity)
            {
                throw new System.Exception("value must be a real number.");
            }
            if (value == 0)
            {
                return 0;
            }
            else if (value < 0)
            {
                return -1;
            }
            return 0;
        }
        public static double Sign(double value)
        {
            if (value == double.NaN || value == double.PositiveInfinity || value == double.NegativeInfinity)
            {
                throw new System.Exception("value must be a real number.");
            }
            if (value == 0)
            {
                return 0;
            }
            else if (value < 0)
            {
                return -1;
            }
            return 0;
        }
        #endregion
        #region Sin
        public static double Sin(double value)
        {
            if (value == double.NaN || value == double.PositiveInfinity || value == double.NegativeInfinity)
            {
                throw new System.Exception("value must be a real number.");
            }
            return System.Math.Sin((value * PIDouble) / 180);
        }
        #endregion
        #region Sqrt
        public static double Sqrt(double value)
        {
            if (value == double.NaN || value == double.PositiveInfinity || value == double.NegativeInfinity)
            {
                throw new System.Exception("value must be a real number.");
            }
            return System.Math.Sqrt(value);
        }
        #endregion
        #region Tan
        public static double Tan(double value)
        {
            if (value == double.NaN || value == double.PositiveInfinity || value == double.NegativeInfinity)
            {
                throw new System.Exception("value must be a real number.");
            }
            return System.Math.Tan((value * PIDouble) / 180);
        }
        #endregion
        //UnityEngine.Mathf


        //EpsilonEngine.MathHelper
        #region BiDirCeil
        public static float BiDirCeil(float value)
        {
            if (value == float.NaN || value == float.PositiveInfinity || value == float.NegativeInfinity)
            {
                throw new System.Exception("value must be a real number.");
            }
            if (value < 0)
            {
                return (float)-System.Math.Ceiling(-value);
            }
            return (float)System.Math.Ceiling(value);
        }
        public static double BiDirCeil(double value)
        {
            if (value == double.NaN || value == double.PositiveInfinity || value == double.NegativeInfinity)
            {
                throw new System.Exception("value must be a real number.");
            }
            if (value < 0)
            {
                return -System.Math.Ceiling(-value);
            }
            return System.Math.Ceiling(value);
        }
        #endregion
        #region BiDirFloor
        public static float BiDirFloor(float value)
        {
            if (value == float.NaN || value == float.PositiveInfinity || value == float.NegativeInfinity)
            {
                throw new System.Exception("value must be a real number.");
            }
            if (value < 0)
            {
                return (float)-System.Math.Floor(-value);
            }
            return (float)System.Math.Floor(value);
        }
        public static double BiDirFloor(double value)
        {
            if (value == double.NaN || value == double.PositiveInfinity || value == double.NegativeInfinity)
            {
                throw new System.Exception("value must be a real number.");
            }
            if (value < 0)
            {
                return -System.Math.Floor(-value);
            }
            return System.Math.Floor(value);
        }
        #endregion
        #region IsRealNumber
        public static bool IsRealNumber(float input)
        {
            if (input == float.NaN || input == float.PositiveInfinity || input == float.NegativeInfinity)
            {
                return false;
            }
            return true;
        }
        public static bool IsRealNumber(double input)
        {
            if (input == double.NaN || input == double.PositiveInfinity || input == double.NegativeInfinity)
            {
                return false;
            }
            return true;
        }
        #endregion
        #region RadToDeg
        public static double RadToDeg(double input)
        {
            if (!IsRealNumber(input))
            {
                throw new System.Exception("input must be a real number.");
            }
            return (input * 180) / PIDouble;
        }
        public static float RadToDeg(float input)
        {
            if (!IsRealNumber(input))
            {
                throw new System.Exception("input must be a real number.");
            }
            return (input * 180) / PIFloat;
        }
        #endregion
        #region DegToRad
        public static double DegToRad(double input)
        {
            if (!IsRealNumber(input))
            {
                throw new System.Exception("input must be a real number.");
            }
            return (input * PIDouble) / 180.0;
        }
        public static float DegToRad(float input)
        {
            if (!IsRealNumber(input))
            {
                throw new System.Exception("input must be a real number.");
            }
            return (input * PIFloat) / 180.0f;
        }
        #endregion
        #region LoopClamp
        public static int LoopClamp(int value, int min, int max)
        {
            if (min > max)
            {
                throw new System.Exception("min cannot be greater than max.");
            }
            if (min == max)
            {
                return min;
            }
            int loopCount = (value - min) / (max - min);
            return value - (loopCount * (max - min));
        }
        public static float LoopClamp(float value, float min, float max)
        {
            if (value == float.NaN || value == float.PositiveInfinity || value == float.NegativeInfinity)
            {
                throw new System.Exception("value must be a real number.");
            }
            if (min == float.NaN || min == float.PositiveInfinity || min == float.NegativeInfinity)
            {
                throw new System.Exception("min must be a real number.");
            }
            if (max == float.NaN || max == float.PositiveInfinity || max == float.NegativeInfinity)
            {
                throw new System.Exception("max must be a real number.");
            }
            if (min > max)
            {
                throw new System.Exception("min cannot be greater than max.");
            }
            if (min == max)
            {
                return min;
            }
            int loopCount = (int)((value - min) / (max - min));
            return value - (loopCount * (max - min));
        }
        public static double LoopClamp(double value, double min, double max)
        {
            if (value == double.NaN || value == double.PositiveInfinity || value == double.NegativeInfinity)
            {
                throw new System.Exception("value must be a real number.");
            }
            if (min == double.NaN || min == double.PositiveInfinity || min == double.NegativeInfinity)
            {
                throw new System.Exception("min must be a real number.");
            }
            if (max == double.NaN || max == double.PositiveInfinity || max == double.NegativeInfinity)
            {
                throw new System.Exception("max must be a real number.");
            }
            if (min > max)
            {
                throw new System.Exception("min cannot be greater than max.");
            }
            if (min == max)
            {
                return min;
            }
            int loopCount = (int)((value - min) / (max - min));
            return value - (loopCount * (max - min));
        }
        #endregion
        #region Sqrt
        public static float Sqrt(float value)
        {
            if (value == float.NaN || value == float.PositiveInfinity || value == float.NegativeInfinity)
            {
                throw new System.Exception("value must be a real number.");
            }
            return (float)System.Math.Sqrt(value);
        }
        public static double Sqrt(double value)
        {
            if (value == double.NaN || value == double.PositiveInfinity || value == double.NegativeInfinity)
            {
                throw new System.Exception("value must be a real number.");
            }
            return System.Math.Sqrt(value);
        }
        #endregion
        #region Lerp
        public static int Lerp(float sample, int a, int b)
        {
            if (sample == float.NaN || sample == float.PositiveInfinity || sample == float.NegativeInfinity)
            {
                throw new System.Exception("sample must be a real number.");
            }
            return a + (int)((b - a) * sample);
        }
        public static float Lerp(float sample, float a, float b)
        {
            if (sample == float.NaN || sample == float.PositiveInfinity || sample == float.NegativeInfinity)
            {
                throw new System.Exception("sample must be a real number.");
            }
            if (a == float.NaN || a == float.PositiveInfinity || a == float.NegativeInfinity)
            {
                throw new System.Exception("a must be a real number.");
            }
            if (b == float.NaN || b == float.PositiveInfinity || b == float.NegativeInfinity)
            {
                throw new System.Exception("b must be a real number.");
            }
            return a + ((b - a) * sample);
        }
        public static double Lerp(float sample, double a, double b)
        {
            if (sample == float.NaN || sample == float.PositiveInfinity || sample == float.NegativeInfinity)
            {
                throw new System.Exception("sample must be a real number.");
            }
            if (a == double.NaN || a == double.PositiveInfinity || a == double.NegativeInfinity)
            {
                throw new System.Exception("a must be a real number.");
            }
            if (b == double.NaN || b == double.PositiveInfinity || b == double.NegativeInfinity)
            {
                throw new System.Exception("b must be a real number.");
            }
            return a + ((b - a) * sample);
        }
        public static int Lerp(double sample, int a, int b)
        {
            if (sample == double.NaN || sample == double.PositiveInfinity || sample == double.NegativeInfinity)
            {
                throw new System.Exception("sample must be a real number.");
            }
            return a + (int)((b - a) * sample);
        }
        public static float Lerp(double sample, float a, float b)
        {
            if (sample == double.NaN || sample == double.PositiveInfinity || sample == double.NegativeInfinity)
            {
                throw new System.Exception("sample must be a real number.");
            }
            if (a == float.NaN || a == float.PositiveInfinity || a == float.NegativeInfinity)
            {
                throw new System.Exception("a must be a real number.");
            }
            if (b == float.NaN || b == float.PositiveInfinity || b == float.NegativeInfinity)
            {
                throw new System.Exception("b must be a real number.");
            }
            return a + (float)((b - a) * sample);
        }
        public static double Lerp(double sample, double a, double b)
        {
            if (sample == double.NaN || sample == double.PositiveInfinity || sample == double.NegativeInfinity)
            {
                throw new System.Exception("sample must be a real number.");
            }
            if (a == double.NaN || a == double.PositiveInfinity || a == double.NegativeInfinity)
            {
                throw new System.Exception("a must be a real number.");
            }
            if (b == double.NaN || b == double.PositiveInfinity || b == double.NegativeInfinity)
            {
                throw new System.Exception("b must be a real number.");
            }
            return a + ((b - a) * sample);
        }
        #endregion
        #region InverseLerp
        public static int InverseLerp(float sample, int a, int b)
        {
            if (sample == float.NaN || sample == float.PositiveInfinity || sample == float.NegativeInfinity)
            {
                throw new System.Exception("sample must be a real number.");
            }
            return Lerp(sample * -1 + 1, a, b);
        }
        public static float InverseLerp(float sample, float a, float b)
        {
            if (sample == float.NaN || sample == float.PositiveInfinity || sample == float.NegativeInfinity)
            {
                throw new System.Exception("sample must be a real number.");
            }
            if (a == float.NaN || a == float.PositiveInfinity || a == float.NegativeInfinity)
            {
                throw new System.Exception("a must be a real number.");
            }
            if (b == float.NaN || b == float.PositiveInfinity || b == float.NegativeInfinity)
            {
                throw new System.Exception("b must be a real number.");
            }
            return Lerp(sample * -1 + 1, a, b);
        }
        public static double InverseLerp(float sample, double a, double b)
        {
            if (sample == float.NaN || sample == float.PositiveInfinity || sample == float.NegativeInfinity)
            {
                throw new System.Exception("sample must be a real number.");
            }
            if (a == double.NaN || a == double.PositiveInfinity || a == double.NegativeInfinity)
            {
                throw new System.Exception("a must be a real number.");
            }
            if (b == double.NaN || b == double.PositiveInfinity || b == double.NegativeInfinity)
            {
                throw new System.Exception("b must be a real number.");
            }
            return Lerp(sample * -1 + 1, a, b);
        }
        public static int InverseLerp(double sample, int a, int b)
        {
            if (sample == double.NaN || sample == double.PositiveInfinity || sample == double.NegativeInfinity)
            {
                throw new System.Exception("sample must be a real number.");
            }
            return Lerp(sample * -1 + 1, a, b);
        }
        public static float InverseLerp(double sample, float a, float b)
        {
            if (sample == double.NaN || sample == double.PositiveInfinity || sample == double.NegativeInfinity)
            {
                throw new System.Exception("sample must be a real number.");
            }
            if (a == float.NaN || a == float.PositiveInfinity || a == float.NegativeInfinity)
            {
                throw new System.Exception("a must be a real number.");
            }
            if (b == float.NaN || b == float.PositiveInfinity || b == float.NegativeInfinity)
            {
                throw new System.Exception("b must be a real number.");
            }
            return Lerp(sample * -1 + 1, a, b);
        }
        public static double InverseLerp(double sample, double a, double b)
        {
            if (sample == double.NaN || sample == double.PositiveInfinity || sample == double.NegativeInfinity)
            {
                throw new System.Exception("sample must be a real number.");
            }
            if (a == double.NaN || a == double.PositiveInfinity || a == double.NegativeInfinity)
            {
                throw new System.Exception("a must be a real number.");
            }
            if (b == double.NaN || b == double.PositiveInfinity || b == double.NegativeInfinity)
            {
                throw new System.Exception("b must be a real number.");
            }
            return Lerp(sample * -1 + 1, a, b);
        }
        #endregion

        //PingPong Lerp
        //Sinusoidial Lerp
        //Complex Lerp
        //Complex Sinusoidial Lerp

        /*
         *     public static float DegreeClamp(float inputDegree)
    {
        while (inputDegree > 360)
        {
            inputDegree -= 360;
        }
        while (inputDegree < 0)
        {
            inputDegree += 360;
        }
        return inputDegree;
    }
         public static double DegreeDifference(double degreeA, double degreeB)
        {
            degreeA = DegreeClamp(degreeA);
            degreeB = DegreeClamp(degreeB);
            double Output = Math.Abs(degreeA - degreeB);
            if (Output > 180)
            {
                Output = 360 - Output;
            }
            return Math.Abs(Output);
        }
        public static Vector VectorFromDirection(double direction, double magnitude)
        {
            return new Vector(Math.Cos(direction * DegToRad), Math.Sin(direction * DegToRad)) * magnitude;
        }
        public static Vector VectorFromDirection(double direction)
        {
            return VectorFromDirection(direction, 1);
        }
        public static double Distance(Vector vectorA, Vector vectorB)
        {
            return Math.Sqrt(((vectorA.x - vectorB.x) * (vectorA.x - vectorB.x)) + ((vectorA.y - vectorB.y) * (vectorA.y - vectorB.y)));
        }
        public static Vector ClampUnitCircle(Vector inputVector)
        {
            double Distance = VectorMagnitude(inputVector);
            return new Vector(inputVector.x / Distance, inputVector.y / Distance);
        }
        public static Vector RotateVector(Vector input, double rotation)
        {
            double ca = Math.Cos(rotation * DegToRad);
            double sa = Math.Sin(rotation * DegToRad);
            return new Vector(ca * input.x - sa * input.y, sa * input.x + ca * input.y);
        }
        public static double VectorDirection(Vector inputVector)
        {
            inputVector = ClampUnitCircle(inputVector);
            double Output = RadiansToDegrees(Math.Atan(Math.Abs(inputVector.x) / Math.Abs(inputVector.y)));
            if (inputVector.x >= 0 && inputVector.y >= 0)
            {
                return 90 - Output;
            }
            else if (inputVector.x < 0 && inputVector.y >= 0)
            {
                return 90 + Output;
            }
            else if (inputVector.x < 0 && inputVector.y < 0)
            {
                return 180 + (90 - Output);
            }
            else if (inputVector.x >= 0 && inputVector.y < 0)
            {
                return 270 + Output;
            }
            else
            {
                return Output;
            }
        }
        public static double RadiansToDegrees(double value)
        {
            return 360 / Math.PI * 2 * value;
        }
        public static double VectorMagnitude(Vector inputVector)
        {
            return Math.Sqrt((inputVector.x * inputVector.x) + (inputVector.y * inputVector.y));
        }
        //Tringulation and Polygons
        public static List<List<Vector>> Triangulate(List<Vector> polygonPoints)
        {
            List<List<Vector>> Output = new List<List<Vector>>();
            while (polygonPoints.Count > 3)
            {
                int First_Convex_Vertice = -1;
                for (int i = 0; i < polygonPoints.Count; i++)
                {
                    if (!VerticeConcave(polygonPoints, i))
                    {
                        First_Convex_Vertice = i;
                        break;
                    }
                }
                if (First_Convex_Vertice >= 0 && First_Convex_Vertice < polygonPoints.Count)
                {
                    Vector Point_A;
                    Vector Point_B = polygonPoints[First_Convex_Vertice];
                    Vector Point_C;
                    if (First_Convex_Vertice - 1 < 0)
                    {
                        Point_A = polygonPoints[polygonPoints.Count - 1];
                    }
                    else
                    {
                        Point_A = polygonPoints[First_Convex_Vertice - 1];
                    }
                    if (First_Convex_Vertice + 1 >= polygonPoints.Count)
                    {
                        Point_C = polygonPoints[0];
                    }
                    else
                    {
                        Point_C = polygonPoints[First_Convex_Vertice + 1];
                    }
                    Output.Add(new List<Vector>() { Point_A, Point_B, Point_C });
                    polygonPoints.RemoveAt(First_Convex_Vertice);
                }
                else
                {
                    Output.Add(polygonPoints);
                    return Output;
                }
            }
            Output.Add(polygonPoints);
            return Output;
        }
        public static bool PolygonConcave(List<Vector> polygonPoints)
        {
            for (int i = 0; i < polygonPoints.Count; i++)
            {
                if (VerticeConcave(polygonPoints, i))
                {
                    return true;
                }
            }
            return false;
        }
        public static bool VerticeConcave(List<Vector> polygonPoints, int pointIndex)
        {
            Vector Point_A;
            Vector Point_B = polygonPoints[pointIndex];
            Vector Point_C;
            if (pointIndex - 1 < 0)
            {
                Point_A = polygonPoints[polygonPoints.Count - 1];
            }
            else
            {
                Point_A = polygonPoints[pointIndex - 1];
            }
            if (pointIndex + 1 >= polygonPoints.Count)
            {
                Point_C = polygonPoints[0];
            }
            else
            {
                Point_C = polygonPoints[pointIndex + 1];
            }
            double Degree_AB = VectorDirection(new Vector(Point_A.x - Point_B.x, Point_A.y - Point_B.y));
            double Degree_BC = VectorDirection(new Vector(Point_C.x - Point_B.x, Point_C.y - Point_B.y));
            if (Degree_BC > Degree_AB && Degree_BC - Degree_AB > 180)
            {
                return true;
            }
            else if (Degree_BC < Degree_AB && (360 - Degree_AB) + Degree_BC > 180)
            {
                return true;
            }
            return false;
        }
        //Lines and Stuff
        public static double DistanceToLine(Vector a, Vector b, Vector p)
        {
            return Distance(ClosestPointOnLine(a, b, p), p);
        }
        public static Vector ClosestPointOnLine(Vector lineStart, Vector lineEnd, Vector targetPoint)
        {
            Vector line = (lineEnd - lineStart);
            double len = VectorMagnitude(line);
            line = ClampUnitCircle(line);

            Vector v = targetPoint - lineStart;
            double d = v.x * line.x + v.y * line.y;
            d = Clamp(d, 0f, len);
            return lineStart + line * d;
        }
         */
        #endregion
    }
}