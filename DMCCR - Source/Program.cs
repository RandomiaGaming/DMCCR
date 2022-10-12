using EpsilonEngine;
namespace DMCCR
{
    public static class Program
    {
        [System.STAThread]
        public static void Main()
        {
            DMCCR dmccr = new DMCCR();
            //RunSpeedTest(dmccr);
            dmccr.Run();
        }
        public static void RunSpeedTest(Game game)
        {
            System.Diagnostics.Stopwatch speedTestStopwatch = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 0; i < 1000; i++)
            {
                Texture a = new Texture(game, 16, 16, Color.Red);
            }
            speedTestStopwatch.Stop();
            System.Console.WriteLine(speedTestStopwatch.ElapsedTicks);
            System.Console.ReadLine();
        }
    }
}