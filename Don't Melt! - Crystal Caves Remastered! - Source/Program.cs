using System;
namespace DMCCR
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            DMCCR game = new DMCCR();
            game.Run();
        }
    }
}