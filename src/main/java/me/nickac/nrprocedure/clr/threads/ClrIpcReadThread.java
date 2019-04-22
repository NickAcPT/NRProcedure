package me.nickac.nrprocedure.clr.threads;

import com.gilecode.yagson.YaGson;
import me.nickac.nrprocedure.clr.ClrHostConnection;
import me.nickac.nrprocedure.clr.ipc.IpcCommand;
import me.nickac.nrprocedure.clr.ipc.IpcCommandType;

import java.io.*;

public class ClrIpcReadThread extends Thread {

    private final BufferedReader reader;
    private Process hostProcess;
    private YaGson gson;
    private ClrHostConnection connection;

    public ClrIpcReadThread(ClrHostConnection connection) {
        setName("CLRHost: Read IPC");
        this.connection = connection;
        gson = connection.getGson();
        hostProcess = connection.getClrHostProcess();
        InputStream inputStream = hostProcess.getInputStream();
        reader = new BufferedReader(new InputStreamReader(inputStream));
        start();
    }

    @Override
    public void run() {

        try {
            String line;
            while ((line = reader.readLine()) != null) {
                IpcCommand ipcCommand = gson.fromJson(line, IpcCommand.class);

                if (ipcCommand != null) {
                    //TODO: Separate this into handlers
                    if (ipcCommand.getCommandType() == IpcCommandType.LOG) {
                        System.out.println(ipcCommand.getArguments()[0]);
                    }
                    connection.getRequestCount();
                }

            }

        } catch (IOException ioe) {
            System.out.println("Exception while reading input " + ioe);

        } finally {
            // close the streams using close method
            try {
                if (reader != null) {
                    reader.close();
                }
                hostProcess.destroy();
            } catch (IOException ioe) {
                System.out.println("Error while closing stream: " + ioe);
            }

        }
    }
}
