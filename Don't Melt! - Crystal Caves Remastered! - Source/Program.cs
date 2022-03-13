namespace DMCCR
{
    public static class Program
    {
        [System.STAThread]
        public static void Main()
        {
            DMCCR game = new DMCCR();
            game.Run();
        }
    }
}