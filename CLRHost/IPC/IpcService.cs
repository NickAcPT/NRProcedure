using System;
using System.Threading;
using System.Threading.Tasks;
using CLRHost.IPC.Readers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CLRHost.IPC
{
    public class IpcService
    {
        private long _requestCount;

        public IpcService()
        {
            JsonConvert.DefaultSettings = () =>
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                };
                settings.Converters.Add(new StringEnumConverter());
                return settings;
            };
        }

        public Task IpcReaderTask { get; set; }

        public IpcReader IpcReader { get; set; }

        public void PublishRaw(IpcCommand command)
        {
            command.RequestId = IncrementRequestCounter();
            Console.Out.WriteLine(JsonConvert.SerializeObject(command));
        }

        public long IncrementRequestCounter()
        {
            return Interlocked.Increment(ref _requestCount);
        }

        public void Start()
        {
            IpcReader = new IpcReader(this);
            IpcReaderTask = new Task(() => IpcReader.Run());

            IpcReaderTask.Start();

            Task.WhenAny(IpcReaderTask).Result.GetAwaiter().GetResult();
        }
    }
}