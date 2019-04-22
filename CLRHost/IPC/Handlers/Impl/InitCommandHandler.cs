using System;

namespace CLRHost.IPC.Handlers.Impl
{
    [CommandDefinition(IpcCommandType.Init)]
    public class InitCommandHandler : ICommandHandler
    {
        public void HandleCommand(IpcService service, IpcCommand cmd)
        {
            service.Log("######################");
            service.Log("#     NProcedure     #");
            service.Log("######################");
            service.Log(string.Empty);
        }
    }
}