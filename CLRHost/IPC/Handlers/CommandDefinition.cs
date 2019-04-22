using System;
using System.Data;

namespace CLRHost.IPC.Handlers
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    sealed class CommandDefinitionAttribute : Attribute
    {
        public IpcCommandType Type { get; }

        public CommandDefinitionAttribute(IpcCommandType type)
        {
            Type = type;
        }
    }
}