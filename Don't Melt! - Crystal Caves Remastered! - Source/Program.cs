namespace DMCCR
{
    public static class Program
    {
        [System.STAThread]
        public static void Main()
        {
            DMCCR dmccr = new DMCCR();
            dmccr.Run();
        }
    }
}