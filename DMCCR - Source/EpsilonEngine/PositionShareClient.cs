using System;
using System.Threading;
using System.IO;
using System.IO.Pipes;

namespace EpsilonEngine
{
    public sealed class PositionShareClient
    {
        public int PositionX { get; private set; }
        public int PositionY { get; private set; }

        private NamedPipeClientStream _pipeClient;
        public PositionShareClient(string pipeName)
        {
            if (pipeName is null)
            {
                throw new Exception("pipeName cannot be null.");
            }
            if (pipeName == string.Empty)
            {
                throw new Exception("pipeName cannot be empty.");
            }

            _pipeClient = new NamedPipeClientStream(".", pipeName, PipeDirection.In);

            Thread clientThead = new Thread(() =>
            {
                RunClientLoop();
            });
            clientThead.Start();
        }
        private void RunClientLoop()
        {
            _pipeClient.Connect();

            StreamReader sr = new StreamReader(_pipeClient);
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
