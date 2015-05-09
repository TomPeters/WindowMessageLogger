namespace WindowMessageLogger
{
    public interface INamedPipeClient
    {
        void ConnectToPipeServer(string pipeName, int timeout);
    }
}