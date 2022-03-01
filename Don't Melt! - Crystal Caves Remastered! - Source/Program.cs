using System;
namespace DMCCR
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            //DMCCR game = new DMCCR();
            //game.Run();
            Test();
        }
        public static void Test()
        {
            EpsilonEngine.Rectangle rectangle = new EpsilonEngine.Rectangle();

            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();
            
            for (int i = 0; i < 10000000; i++)
            {
                rectangle.MinX = 0;
            }

            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedTicks);
            while (true)
            {
                System.Threading.Thread.Sleep(int.MaxValue);
            }
        }
    }
}