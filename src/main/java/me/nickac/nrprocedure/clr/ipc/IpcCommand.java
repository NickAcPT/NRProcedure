package me.nickac.nrprocedure.clr.ipc;

public class IpcCommand {
    private long requestId;
    private long requestReplyId = -1;
    private IpcCommandType commandType;
    private Object[] arguments;

    public IpcCommand(IpcCommandType commandType) {
        this.commandType = commandType;
    }

    public IpcCommand(IpcCommandType commandType, Object[] arguments) {
        this.commandType = commandType;
        this.arguments = arguments;
    }

    public long getRequestReplyId() {
        return requestReplyId;
    }

    public void setRequestReplyId(long requestReplyId) {
        this.requestReplyId = requestReplyId;
    }

    public long getRequestId() {
        return requestId;
    }

    public void setRequestId(long requestId) {
        this.requestId = requestId;
    }

    public IpcCommandType getCommandType() {
        return commandType;
    }

    public void setCommandType(IpcCommandType commandType) {
        this.commandType = commandType;
    }

    public Object[] getArguments() {
        return arguments;
    }

    public void setArguments(Object[] arguments) {
        this.arguments = arguments;
    }
}
