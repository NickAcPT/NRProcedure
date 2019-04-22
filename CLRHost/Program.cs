using CLRHost.IPC;

namespace CLRHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new IpcService();
            service.Start();
        }
    }
}