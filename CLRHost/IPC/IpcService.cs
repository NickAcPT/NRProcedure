using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using CLRHost.IPC.Handlers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CLRHost.IPC
{
    public class IpcService
    {
        private long _requestCount;

        public Dictionary<IpcCommandType, (ICommandHandler, Delegate)> Handlers { get; set; }

        public IpcService()
        {
            LoadHandlers();
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

        private void LoadHandlers()
        {
            var handlerMethod = typeof(ICommandHandler).GetMethod(nameof(ICommandHandler.HandleCommand));

            Handlers = AppDomain.CurrentDomain.GetAssemblies().SelectMany(c => c.GetTypes())
                .Where(t => t.IsClass && typeof(ICommandHandler).IsAssignableFrom(t))
                .Select(t => (t, t.GetCustomAttribute<CommandDefinitionAttribute>()))
                .Where(c => c.Item2 != null)
                .Select(c => (c.Item2.Type,
                    (Activator.CreateInstance(c.Item1) as ICommandHandler,
                        Delegate.CreateDelegate(typeof(Action<ICommandHandler, IpcService, IpcCommand>),
                            handlerMethod))))
                .ToDictionary(c => c.Item1, c => c.Item2);
        }

        public void InvokeHandler(IpcCommandType type, IpcCommand cmd)
        {
            Handlers.TryGetValue(type, out var del);
            del.Item2?.DynamicInvoke(del.Item1, this, cmd);
        }

        public Task IpcReaderTask { get; set; }

        public IpcReader IpcReader { get; set; }

        public void PublishRaw(IpcCommand command)
        {
            command.RequestId = IncrementRequestCounter();
            Console.Out.WriteLine(JsonConvert.SerializeObject(command));
        }

        public void Log(string msg)
        {
            PublishRaw(new IpcCommand
            {
                CommandType = IpcCommandType.Log,
                Arguments = new object[]{msg}
            });
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