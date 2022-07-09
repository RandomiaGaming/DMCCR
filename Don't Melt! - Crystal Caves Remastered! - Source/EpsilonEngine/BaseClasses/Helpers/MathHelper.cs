//Approved 07/09/2022
namespace EpsilonEngine
{
    public static class MathHelper
    {
        #region Public Constants
        public const double Tau = 6.2831853071795862;
        public const double PI = 3.1415926535897931;
        public const double E = 2.7182818284590451;
        public const double SQRT2 = 1.4142135623730952;
        public const double SQRT3 = 1.7320508075688772;
        public const double RadToDeg = 57.295779513082323;
        public const double DegToRad = 0.017453292519943295;
        public const double SinOf45 = 0.70710678118654746;
        public const double SinOf60 = 0.8660254037844386;
        #endregion
        #region Public Static Methods
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
            double output = System.Math.Acos(value) * RadToDeg;
            if (output < 0)
            {
                return 360 + output;
            }
            return output;
        }
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
            double output = System.Math.Asin(value) * RadToDeg;
            if (output < 0)
            {
                return 360 + output;
            }
            return output;
        }
        public static double ATan(double value)
        {
            if (value == double.NaN || value == double.PositiveInfinity || value == double.NegativeInfinity)
            {
                throw new System.Exception("value must be a real number.");
            }
            double output = System.Math.Atan(value) * RadToDeg;
            if (output < 0)
            {
                return 360 + output;
            }
            return output;
        }
        public static double Cbrt(double value)
        {
            if (value == double.NaN || value == double.PositiveInfinity || value == double.NegativeInfinity)
            {
                throw new System.Exception("value must be a real number.");
            }
            return System.Math.Pow(value, 0.33333333333333331);
        }
        public static double Ceil(double value)
        {
            if (value == double.NaN || value == double.PositiveInfinity || value == double.NegativeInfinity)
            {
                throw new System.Exception("value must be a real number.");
            }
            return System.Math.Ceiling(value);
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
        public static double Clamp01(double value)
        {
            if (value == double.NaN || value == double.PositiveInfinity || value == double.NegativeInfinity)
            {
                throw new System.Exception("value must be a real number.");
            }
            if (value > 1)
            {
                return 1;
            }
            else if (value < 0)
            {
                return 0;
            }
            return value;
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
            double loopCount = (value - min) / (max - min);
            if (loopCount > 0)
            {
                loopCount = System.Math.Floor(loopCount);
            }
            else
            {
                loopCount = -System.Math.Floor(-loopCount);
            }
            return value - (loopCount * (max - min));
        }
        public static double LoopClamp01(double value)
        {
            if (value == double.NaN || value == double.PositiveInfinity || value == double.NegativeInfinity)
            {
                throw new System.Exception("value must be a real number.");
            }
            double loopCount = value;
            if (loopCount > 0)
            {
                loopCount = System.Math.Floor(loopCount);
            }
            else
            {
                loopCount = -System.Math.Floor(-loopCount);
            }
            return value - loopCount;
        }
        public static double Cos(double value)
        {
            if (value == double.NaN || value == double.PositiveInfinity || value == double.NegativeInfinity)
            {
                throw new System.Exception("value must be a real number.");
            }
            return System.Math.Cos(value * DegToRad);
        }
        public static double Rem(double dividend, double divisor)
        {
            if (dividend == double.NaN || dividend == double.PositiveInfinity || dividend == double.NegativeInfinity)
            {
                throw new System.Exception("dividend must be a real number.");
            }
            if (divisor == double.NaN || divisor == double.PositiveInfinity || divisor == double.NegativeInfinity)
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
        public static double Exp(double value)
        {
            if (value == double.NaN || value == double.PositiveInfinity || value == double.NegativeInfinity)
            {
                throw new System.Exception("value must be a real number.");
            }
            return System.Math.Exp(value);
        }
        public static double Floor(double value)
        {
            if (value == double.NaN || value == double.PositiveInfinity || value == double.NegativeInfinity)
            {
                throw new System.Exception("value must be a real number.");
            }
            return System.Math.Floor(value);
        }
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
            if(logBase == 1)
            {
                throw new System.Exception("logBase must not be 1.");
            }
            if (logBase <= 0)
            {
                throw new System.Exception("logBase must be greater than 0.");
            }
            return System.Math.Log(value, logBase);
        }
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
            if(value < 0 && power != System.Math.Floor(power))
            {
                throw new System.Exception("When value is less than 0 power must be an integer.");
            }
            return System.Math.Pow(value, power);
        }
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
        public static double Round(double value)
        {
            if (value == double.NaN || value == double.PositiveInfinity || value == double.NegativeInfinity)
            {
                throw new System.Exception("value must be a real number.");
            }
            return System.Math.Round(value);
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
        public static double Sin(double value)
        {
            if (value == double.NaN || value == double.PositiveInfinity || value == double.NegativeInfinity)
            {
                throw new System.Exception("value must be a real number.");
            }
            return System.Math.Sin(value * DegToRad);
        }
        public static bool IsInt(double value)
        {
            if (value == double.NaN || value == double.PositiveInfinity || value == double.NegativeInfinity)
            {
                throw new System.Exception("value must be a real number.");
            }
            return value == System.Math.Floor(value);
        }
        public static double Sqrt(double value)
        {
            if (value == double.NaN || value == double.PositiveInfinity || value == double.NegativeInfinity)
            {
                throw new System.Exception("value must be a real number.");
            }
            return System.Math.Sqrt(value);
        }
        public static double Tan(double value)
        {
            if (value == double.NaN || value == double.PositiveInfinity || value == double.NegativeInfinity)
            {
                throw new System.Exception("value must be a real number.");
            }
            return System.Math.Tan(value * DegToRad);
        }
        public static bool Approx(double a, double b)
        {
            if (a == double.NaN || a == double.PositiveInfinity || a == double.NegativeInfinity)
            {
                throw new System.Exception("a must be a real number.");
            }
            if (b == double.NaN || b == double.PositiveInfinity || b == double.NegativeInfinity)
            {
                throw new System.Exception("b must be a real number.");
            }
            double difference = a - b;
            if (difference <= double.Epsilon && difference >= -double.Epsilon)
            {
                return true;
            }
            return false;
        }
        public static bool Within(double value, double tollerance)
        {
            if (value == double.NaN || value == double.PositiveInfinity || value == double.NegativeInfinity)
            {
                throw new System.Exception("value must be a real number.");
            }
            if (tollerance == double.NaN || tollerance == double.PositiveInfinity || tollerance == double.NegativeInfinity)
            {
                throw new System.Exception("tollerance must be a real number.");
            }
            if (tollerance < 0)
            {
                throw new System.Exception("tollerance must be greater than or equal to 0.");
            }
            if (value <= tollerance && value >= -tollerance)
            {
                return true;
            }
            return false;
        }
        public static bool DifWithin(double a, double b, double tollerance)
        {
            if (a == double.NaN || a == double.PositiveInfinity || a == double.NegativeInfinity)
            {
                throw new System.Exception("a must be a real number.");
            }
            if (b == double.NaN || b == double.PositiveInfinity || b == double.NegativeInfinity)
            {
                throw new System.Exception("b must be a real number.");
            }
            if (tollerance == double.NaN || tollerance == double.PositiveInfinity || tollerance == double.NegativeInfinity)
            {
                throw new System.Exception("tollerance must be a real number.");
            }
            if (tollerance < 0)
            {
                throw new System.Exception("tollerance must be greater than or equal to 0.");
            }
            double difference = a - b;
            if (difference <= tollerance && difference >= -tollerance)
            {
                return true;
            }
            return false;
        }
        public static double LinInterp(double t, double a, double b)
        {
            if (t == double.NaN || t == double.PositiveInfinity || t == double.NegativeInfinity)
            {
                throw new System.Exception("t must be a real number.");
            }
            if (a == double.NaN || a == double.PositiveInfinity || a == double.NegativeInfinity)
            {
                throw new System.Exception("a must be a real number.");
            }
            if (b == double.NaN || b == double.PositiveInfinity || b == double.NegativeInfinity)
            {
                throw new System.Exception("b must be a real number.");
            }
            return a + ((b - a) * t);
        }
        public static double InverseLinInterp(double s, double a, double b)
        {
            if (s == double.NaN || s == double.PositiveInfinity || s == double.NegativeInfinity)
            {
                throw new System.Exception("s must be a real number.");
            }
            if (a == double.NaN || a == double.PositiveInfinity || a == double.NegativeInfinity)
            {
                throw new System.Exception("a must be a real number.");
            }
            if (b == double.NaN || b == double.PositiveInfinity || b == double.NegativeInfinity)
            {
                throw new System.Exception("b must be a real number.");
            }
            return (s - a) / (b - a);
        }
        public static double Csc(double value)
        {
            if (value == double.NaN || value == double.PositiveInfinity || value == double.NegativeInfinity)
            {
                throw new System.Exception("value must be a real number.");
            }
            return 1 / System.Math.Sin(value * DegToRad);
        }
        public static double Sec(double value)
        {
            if (value == double.NaN || value == double.PositiveInfinity || value == double.NegativeInfinity)
            {
                throw new System.Exception("value must be a real number.");
            }
            return 1 / System.Math.Cos(value * DegToRad);
        }
        public static double Cot(double value)
        {
            if (value == double.NaN || value == double.PositiveInfinity || value == double.NegativeInfinity)
            {
                throw new System.Exception("value must be a real number.");
            }
            return 1 / System.Math.Tan(value * DegToRad);
        }
        public static double ACsc(double value)
        {
            if (value == double.NaN || value == double.PositiveInfinity || value == double.NegativeInfinity)
            {
                throw new System.Exception("value must be a real number.");
            }
            if(value < -1)
            {
                throw new System.Exception("value must be greater than or equal to -1.");
            }
            if (value > 1)
            {
                throw new System.Exception("value must be less than or equal to 1.");
            }
            double output = System.Math.Asin(1 / value) * RadToDeg;
            if (output < 0)
            {
                return 360 + output;
            }
            return output;
        }
        public static double ASec(double value)
        {
            if (value == double.NaN || value == double.PositiveInfinity || value == double.NegativeInfinity)
            {
                throw new System.Exception("value must be a real number.");
            }
            if (value < -1)
            {
                throw new System.Exception("value must be greater than or equal to -1.");
            }
            if (value > 1)
            {
                throw new System.Exception("value must be less than or equal to 1.");
            }
            double output = System.Math.Acos(1 / value) * RadToDeg;
            if (output < 0)
            {
                return 360 + output;
            }
            return output;
        }
        public static double ACot(double value)
        {
            if (value == double.NaN || value == double.PositiveInfinity || value == double.NegativeInfinity)
            {
                throw new System.Exception("value must be a real number.");
            }
            return 1 / (System.Math.Tan(value * PI) * RadToDeg);
        }
        public static double Fact(double value)
        {
            if (value == double.NaN || value == double.PositiveInfinity || value == double.NegativeInfinity)
            {
                throw new System.Exception("value must be a real number.");
            }
            if (value < 0)
            {
                throw new System.Exception("value must be greater than or equal to 0.");
            }
            if (value != System.Math.Floor(value))
            {
                throw new System.Exception("value must be an integer.");
            }
            double output = 1;
            while (value > 0)
            {
                output *= value;
                value--;
            }
            return output;
        }
        public static double Sqr(double value)
        {
            if (value == double.NaN || value == double.PositiveInfinity || value == double.NegativeInfinity)
            {
                throw new System.Exception("value must be a real number.");
            }
            return value * value;
        }
        public static double Cube(double value)
        {
            if (value == double.NaN || value == double.PositiveInfinity || value == double.NegativeInfinity)
            {
                throw new System.Exception("value must be a real number.");
            }
            return value * value * value;
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
        public static double CeilMag(double value)
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
        public static double FloorMag(double value)
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
        public static bool IsRealNumber(double input)
        {
            if (input == double.NaN || input == double.PositiveInfinity || input == double.NegativeInfinity)
            {
                return false;
            }
            return true;
        }
        public static double RadToDegAcc(double input)
        {
            if (!IsRealNumber(input))
            {
                throw new System.Exception("input must be a real number.");
            }
            return (input * 180) / PI;
        }
        public static double DegToRadAcc(double input)
        {
            if (!IsRealNumber(input))
            {
                throw new System.Exception("input must be a real number.");
            }
            return (input * PI) / 180.0;
        }
        public static double VectorAngle(Vector vector)
        {
            double output = System.Math.Atan2(vector.X, vector.Y) * RadToDeg;
            if (output < 0)
            {
                return 360 + output;
            }
            return output;
        }
        public static double AngleClamp(double value)
        {
            if (value == double.NaN || value == double.PositiveInfinity || value == double.NegativeInfinity)
            {
                throw new System.Exception("value must be a real number.");
            }
            double loopCount = value / 360;
            if (loopCount > 0)
            {
                loopCount = System.Math.Floor(loopCount);
            }
            else
            {
                loopCount = -System.Math.Floor(-loopCount);
            }
            return value - (loopCount * 360);
        }
        public static double VectorMag(Vector input)
        {
            return System.Math.Sqrt((input.X * input.X) + (input.Y * input.Y));
        }
        public static Vector ClampUnitCircle(Vector input)
        {
            double distance = System.Math.Sqrt((input.X * input.X) + (input.Y * input.Y));
            return new Vector(input.X / distance, input.Y / distance);
        }
        //SinInterp
        //ConstInterp
        //PingPong
        //AngleDif
        public static double AngleDif(double a, double b)
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
        //CartisianToPolar
        //PolarToCartisian
        //Rotate Vector
        public static Vector RotateVector(Vector input, double rotation)
        {
            double ca = System.Math.Cos((rotation * PI) / 180);
            double sa = System.Math.Sin(rotation * DegToRad);
            return new Vector(ca * input.X - sa * input.Y, sa * input.X + ca * input.Y);
        }
        //DisToLine
        //ClosestPointOnLine
        //PointInTri
        //ClosestPointInTri
        //DistanceToTri
        //ClosestPointInCircle
        //DistanceToCircle
        #endregion
        /*

public static Vector VectorFromDirection(double direction, double magnitude)
{
    return new Vector(Math.Cos(direction * DegToRad), Math.Sin(direction * DegToRad)) * magnitude;
}
public static Vector VectorFromDirection(double direction)
{
    return VectorFromDirection(direction, 1);
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
    }
}