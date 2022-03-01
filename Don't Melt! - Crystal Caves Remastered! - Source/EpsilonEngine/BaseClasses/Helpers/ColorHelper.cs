namespace EpsilonEngine
{ 
    public static class ColorHelper
    {
        public static Color FlattenMix(Color a, Color b)
        {
            return new Color((byte)MathHelper.Lerp(b.A / 255.0, a.R, b.R), (byte)MathHelper.Lerp(b.A / 255.0, a.G, b.G), (byte)MathHelper.Lerp(b.A / 255.0, a.B, b.B), (byte)(a.A + b.A));
        }
        public static Color EvenMix(Color a, Color b)
        {
            return new Color((byte)((a.R + b.R) / 2), (byte)((a.G + b.G) / 2), (byte)((a.B + b.B) / 2), (byte)(a.A + b.A));
        }
        public static Color Mix(Color a, Color b)
        {
            return new Color((byte)MathHelper.Lerp(b.A / 255.0, a.R, b.R), (byte)MathHelper.Lerp(b.A / 255.0, a.G, b.G), (byte)MathHelper.Lerp(b.A / 255.0, a.B, b.B), (byte)((1 - ((255.0 - b.A) * (255.0 - a.A))) * 255));
        }
        public static Color SampleHueGradient(double t, byte brightness)
        {
            t = t * 6.0;
            t = MathHelper.LoopClamp(t, 0.0, 6.0);
            if (t < 1)
            {
                return SampleGradient(t, new Color(255, brightness, brightness), new Color(255, 255, brightness));
            }
            else if (t < 2)
            {
                t = t - 1.0;
                return SampleGradient(t, new Color(255, 255, brightness), new Color(brightness, 255, brightness));
            }
            else if (t < 3)
            {
                t = t - 2.0;
                return SampleGradient(t, new Color(brightness, 255, brightness), new Color(brightness, 255, 255));
            }
            else if (t < 4)
            {
                t = t - 3.0;
                return SampleGradient(t, new Color(brightness, 255, 255), new Color(brightness, brightness, 255));
            }
            else if (t < 5)
            {
                t = t - 4.0;
                return SampleGradient(t, new Color(brightness, brightness, 255), new Color(255, brightness, 255));
            }
            else
            {
                t = t - 5.0;
                return SampleGradient(t, new Color(255, brightness, 255), new Color(255, brightness, brightness));
            }
        }
        public static Color SampleHueGradient(double t)
        {
            t = t * 6.0;
            t = MathHelper.LoopClamp(t, 0.0, 6.0);
            if (t < 1)
            {
                return SampleGradient(t, new Color(255, byte.MinValue, byte.MinValue), new Color(255, 255, byte.MinValue));
            }
            else if (t < 2)
            {
                t = t - 1.0;
                return SampleGradient(t, new Color(255, 255, byte.MinValue), new Color(byte.MinValue, 255, byte.MinValue));
            }
            else if (t < 3)
            {
                t = t - 2.0;
                return SampleGradient(t, new Color(byte.MinValue, 255, byte.MinValue), new Color(byte.MinValue, 255, 255));
            }
            else if (t < 4)
            {
                t = t - 3.0;
                return SampleGradient(t, new Color(byte.MinValue, 255, 255), new Color(byte.MinValue, byte.MinValue, 255));
            }
            else if (t < 5)
            {
                t = t - 4.0;
                return SampleGradient(t, new Color(byte.MinValue, byte.MinValue, 255), new Color(255, byte.MinValue, 255));
            }
            else
            {
                t = t - 5.0;
                return SampleGradient(t, new Color(255, byte.MinValue, 255), new Color(255, byte.MinValue, byte.MinValue));
            }
        }
        public static Color SampleGradient(double t, Color a, Color b)
        {
            t = MathHelper.LoopClamp(t, 0, 1);
            double _r = MathHelper.Lerp(t, a.R, b.R);
            double _g = MathHelper.Lerp(t, a.G, b.G);
            double _b = MathHelper.Lerp(t, a.B, b.B);
            double _a = MathHelper.Lerp(t, a.A, b.A);
            return new Color((byte)_r, (byte)_g, (byte)_b, (byte)_a);
        }
    }
}
