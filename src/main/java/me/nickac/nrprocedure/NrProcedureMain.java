package me.nickac.nrprocedure;

import me.nickac.nrprocedure.clr.ClrHostConnection;

import java.io.IOException;

public class NrProcedureMain {

    private static ClrHostConnection clrHostConnection;

    public static ClrHostConnection getClrHostConnection() {
        return clrHostConnection;
    }

    public static void main(String[] args) {
        clrHostConnection = new ClrHostConnection();
        clrHostConnection.start();

        try {
            System.in.read();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

}
