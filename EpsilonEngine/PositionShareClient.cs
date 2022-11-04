namespace EpsilonEngine
{
    public sealed class PositionShareClient
    {
        public int PositionX { get; private set; }
        public int PositionY { get; private set; }
        private System.IO.Pipes.NamedPipeClientStream _pipeClient;
        public PositionShareClient(string pipeName)
        {
            if (pipeName is null)
            {
                throw new System.Exception("pipeName cannot be null.");
            }
            if (pipeName == string.Empty)
            {
                throw new System.Exception("pipeName cannot be empty.");
            }
            _pipeClient = new System.IO.Pipes.NamedPipeClientStream(".", pipeName, System.IO.Pipes.PipeDirection.In);
            System.Threading.Thread clientThead = new System.Threading.Thread(() => { RunClientLoop(); });
            clientThead.Start();
        }
        private void RunClientLoop()
        {
            _pipeClient.Connect();
            System.IO.StreamReader sr = new System.IO.StreamReader(_pipeClient);
            while (true)
            {
                string packet = sr.ReadLine();
                string[] splitPacket = packet.Split(',');
                PositionX = int.Parse(splitPacket[0]);
                PositionY = int.Parse(splitPacket[1]);
            }
        }
    }
}