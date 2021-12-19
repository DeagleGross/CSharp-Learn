# Building Types into a Module

Basic *hello world* application like this
```
public sealed class Program 
{
    public static void Main() 
    {
        System.Console.WriteLine("Hello world!");
    }
}
```
can be built into an .exe file with this command
```
csc.exe /out:Program.exe /t:exe /r:MSCorLib.dll Program.cs
```
This command line tells the C# compiler to emit an executable file called `Program.exe` *(/out:Program.exe)*.

When C# compiler processes the source file, it sees the reference to `System.Console` type's `WriteLine` method.
Compiler wants to ensure that this type exists somewhere, so that we are giving compiler a set of assemblies that it can use to resolve references to external types.
In the command line about `/r[eference]:MSCorLib.dll` switch stands for this. `MSCorLib.dll` is a special *.dll*-file that contains all core types: `Byte`, `Char`, `String`, `Int32` and etc.

But we could just call
```
csc.exe Program.cs
```
because all other switches are the defaults picked by the compiler. 

If we don't want to reference `MSCorLib.dll`, we can use `/nostdlib`. Microsoft uses it when building `MSCorLib.dll` itself.

## Response Files
**Response file** - a file that contains a set of compiler command-line switches.