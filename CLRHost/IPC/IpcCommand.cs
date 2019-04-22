namespace CLRHost.IPC
{
    public class IpcCommand
    {
        public long RequestId { get; set; }
        
        public IpcCommandType CommandType { get; set; }

        public object[] Arguments { get; set; }
    }
}