# NRProcedure
.Net Remote Procedure interaction with Java (w/ code generation)

## Goals of this project
This project has the goal to allow type-safe handling of Java code in a C# environment and vice versa.
This implies that code generation is used at some point.

## Implementation details
Right now, this project starts a CLR host written in CoreFX (read: .NET Core Runtime) and communicates with JSON-structured messages using the process input/output to communicate.
