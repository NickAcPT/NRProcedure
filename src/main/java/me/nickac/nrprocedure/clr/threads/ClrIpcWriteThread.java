package me.nickac.nrprocedure.clr.threads;

import com.gilecode.yagson.YaGson;
import com.gilecode.yagson.YaGsonBuilder;
import com.gilecode.yagson.types.TypeInfoPolicy;
import me.nickac.nrprocedure.clr.ClrHostConnection;
import me.nickac.nrprocedure.clr.ipc.IpcCommand;

import java.io.BufferedReader;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.PrintWriter;

public class ClrIpcWriteThread extends Thread {

    private final Process hostProcess;
    private PrintWriter printWriter;
    private YaGson gson;

    public ClrIpcWriteThread(ClrHostConnection connection) {
        setName("CLRHost: Write IPC");
        gson = connection.getGson();
        hostProcess = connection.getClrHostProcess();
        printWriter = new PrintWriter(hostProcess.getOutputStream());
        start();
    }

    public void writeRaw(IpcCommand command) {
        String x = gson.toJson(command);
        printWriter.println(x);
        printWriter.flush();
        System.out.println("Line written : " + x);
    }

    @Override
    public void run() {
        try {
            hostProcess.waitFor();
        } catch (InterruptedException ignored) {

        }
    }
}
