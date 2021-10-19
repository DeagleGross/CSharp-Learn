# Compiling Source Code into Managed Modules

The **Common Language Runtime** (CLR) is a runtime that is usable by different and varied programming languages. The core features of the CLR are available to any programming languages that target it. I.e. all errors are reported as exceptions, so every language is using this method for reporting problems. The CLR has no idea which programming language is used by the developer.

The flow is the following:
**some programming language source code** -> **suitable compiler** -> **managed module (IL and metadata)**

A managed module is a standard 32-bit Microsoft Windows portable executable (PE32) file, or a standard 64-bit exe (PE32+) that requires the CLR to execute.

Parts of a managed Module
- **PE32 or PE32+ header**: standard windows PE file, indicates type of file (GUI, CUI or DLL) and timespan of creating (building). 
- **CRL header** contains version of CRL, some flags, `MethodDef` metadata token of the managed module's entry point method and location\size of module's metadata, resources, strong name, some flags and etc.
- **Metadata** is a set of tables (of 2 types): tables that describe types and members defined in the source code, and types and members that are referenced by source code.
- **IL Code** is just a code the compiler has produced. 