using System.Diagnostics;
using System.Threading.Tasks;
using CLRHost.IPC;

namespace CLRHost
{
    class Program
    {
        static void Main(string[] args)
        {

/*
            while (!Debugger.IsAttached)
            {
                Task.Delay(100);
            }
*/
            var service = new IpcService();
            service.Start();
        }
    }
}