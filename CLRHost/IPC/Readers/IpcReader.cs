using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CLRHost.IPC.Readers
{
    public class IpcReader
    {
        private readonly IpcService _service;

        public IpcReader(IpcService service)
        {
            _service = service;
        }

        public void Run()
        {
            string line;

            while ((line = Console.In.ReadLine()) != null)
            {
                var command = JsonConvert.DeserializeObject<IpcCommand>(line);

                if (command != null)
                {
                    _service.IncrementRequestCounter();
                    //TODO: Command handling
                    
                    _service.PublishRaw(command);
                }

            }
        }
    }
}