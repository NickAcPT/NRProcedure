using System;
using Newtonsoft.Json;

namespace CLRHost.IPC
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
                    _service.InvokeHandler(command.CommandType, command);
                }
            }
        }
    }
}