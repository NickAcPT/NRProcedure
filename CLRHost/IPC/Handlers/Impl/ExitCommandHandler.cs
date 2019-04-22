using System;

namespace CLRHost.IPC.Handlers.Impl
{
    [CommandDefinition(IpcCommandType.Exit)]
    public class ExitCommandHandler : ICommandHandler
    {
        public void HandleCommand(IpcService service, IpcCommand cmd)
        {
            Environment.Exit(0);
        }
    }
}