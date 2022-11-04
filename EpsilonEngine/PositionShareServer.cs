namespace EpsilonEngine
{
    public sealed class PositionShareServer
    {
        public int PositionX;
        public int PositionY;
        private System.IO.Pipes.NamedPipeServerStream _pipeServer;
        public PositionShareServer(string pipeName)
        {
            if (pipeName is null)
            {
                throw new System.Exception("pipeName cannot be null.");
            }
            if (pipeName == string.Empty)
            {
                throw new System.Exception("pipeName cannot be empty.");
            }
            _pipeServer = new System.IO.Pipes.NamedPipeServerStream(pipeName, System.IO.Pipes.PipeDirection.Out);
            System.Threading.Thread serverThead = new System.Threading.Thread(() => { RunServerLoop(); });
            serverThead.Start();
        }
        private void RunServerLoop()
        {
            _pipeServer.WaitForConnection();
            System.IO.StreamWriter sw = new System.IO.StreamWriter(_pipeServer);
            sw.AutoFlush = true;
            int lastSentPositionX = 0;
            int lastSentPositionY = 0;
            while (true)
            {
                sw.WriteLine($"{PositionX},{PositionY}");
                lastSentPositionX = PositionX;
                lastSentPositionY = PositionY;
            }
        }
    }
}