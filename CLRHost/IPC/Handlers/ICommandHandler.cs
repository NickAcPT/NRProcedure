namespace CLRHost.IPC.Handlers
{
    public interface ICommandHandler
    {
        void HandleCommand(IpcService service, IpcCommand cmd);
    }
}