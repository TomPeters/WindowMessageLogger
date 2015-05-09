using System;
using System.IO.Pipes;

namespace WindowMessageLogger
{
    public class NamedPipeClient : INamedPipeClient
    {
        private const string ServerName = ".";

        public void ConnectToPipeServer(string pipeName, int timeout)
        {
            using (NamedPipeClientStream pipeClient = new NamedPipeClientStream(ServerName, pipeName, PipeDirection.Out))
            {
                try
                {
                    pipeClient.Connect(timeout);
                }
                catch (TimeoutException) // No servers exist - do nothing
                {
                }
            }
        }
    }
}
