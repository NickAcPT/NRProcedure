package me.nickac.nrprocedure.clr.ipc;

import com.gilecode.yagson.com.google.gson.annotations.SerializedName;

public enum IpcCommandType {
    @SerializedName("Init")
    INIT,
    @SerializedName("Log")
    LOG,
    @SerializedName("Exit")
    EXIT
}
