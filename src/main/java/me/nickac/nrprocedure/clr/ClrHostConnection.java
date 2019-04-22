package me.nickac.nrprocedure.clr;

import com.gilecode.yagson.YaGson;
import com.gilecode.yagson.YaGsonBuilder;
import com.gilecode.yagson.com.google.gson.FieldNamingPolicy;
import com.gilecode.yagson.types.TypeInfoPolicy;
import me.nickac.nrprocedure.clr.ipc.IpcCommand;
import me.nickac.nrprocedure.clr.ipc.IpcCommandType;
import me.nickac.nrprocedure.clr.threads.ClrIpcReadThread;
import me.nickac.nrprocedure.clr.threads.ClrIpcWriteThread;

import java.io.IOException;
import java.util.concurrent.atomic.AtomicLong;

public class ClrHostConnection {

    private final YaGson gson;
    private Process clrHostProcess;
    private ClrIpcReadThread ipcReadThread;
    private ClrIpcWriteThread ipcWriteThread;
    private AtomicLong requestCounter = new AtomicLong();

    public ClrHostConnection() {

        gson = new YaGsonBuilder()
                .disableHtmlEscaping()
                .setTypeInfoPolicy(TypeInfoPolicy.DISABLED)
                .setFieldNamingPolicy(FieldNamingPolicy.UPPER_CAMEL_CASE)
                .create();
    }

    public long getRequestCount() {
        return requestCounter.incrementAndGet();
    }

    public YaGson getGson() {
        return gson;
    }

    public Process getClrHostProcess() {
        return clrHostProcess;
    }

    public ClrIpcReadThread getIpcReadThread() {
        return ipcReadThread;
    }

    public void publishRaw(IpcCommand command) {
        command.setRequestId(getRequestCount());
        ipcWriteThread.writeRaw(command);
    }

    public void start() {
        try {
            clrHostProcess = new ProcessBuilder("dotnet", "CLRHost/bin/Debug/netcoreapp2.1/NrProcedure.CLRHost.dll")
                    .start();

            ipcReadThread = new ClrIpcReadThread(this);
            ipcWriteThread = new ClrIpcWriteThread(this);

            publishRaw(new IpcCommand(IpcCommandType.INIT));
        } catch (IOException e) {
            e.printStackTrace();
            System.exit(-1);
        }
    }

}
