using EpsilonEngine;
namespace DMCCR
{
    public static class Program
    {
        [System.STAThread]
        public static void Main()
        {
            //RunSpeedTest();
            DMCCR dmccr = new DMCCR();
            dmccr.Run();
        }
        public static void RunSpeedTest()
        {
            System.Diagnostics.Stopwatch speedTestStopwatch = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 0; i < 100000000; i++)
            {

            }
            speedTestStopwatch.Stop();
            System.Console.WriteLine(speedTestStopwatch.ElapsedTicks);
            System.Console.ReadLine();
        }
    }
}