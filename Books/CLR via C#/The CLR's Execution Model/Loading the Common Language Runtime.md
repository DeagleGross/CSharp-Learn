# Loading the Common Language Runtime

There are 2 types of one could build: an executable application or a DLL containing a set of types for use by an exe app. `.NET` has to be installed on the host machine so that CLR could manage the execution of these assemblies.

One can see if a `.NET` is installed by looking for `MSCorEE.dll` at `%SystemRoot%\System32` directory. `.NET Framework SDK` includes a cmd utility called `CLRVer.exe` showing all of the CLRs installed on the machine.

using `platform` cmd-switch CLR allows to specify whether the resulting assembly could run on
- x86 machine running 32-bit Windows
- x64 machine running 64-bit Windows
- ARM machine running 32-bit Windows RT

Also if no specification is provided, the defaul `anycpu` is passed, so any OS could be used.

When running an exe file, Windows examines the EXE file's header to determing which version of Windows is required.

Windows examines the EXE file's header, creates a suitable process (32 or 64 bit) and loads the version of MSCorEE.dll into the process's address space.Then the process's primary thread calls a method defined inside MSCorEE.dll. This method initialized the CLR, loads the EXE assembly and calls its entry poing (Main).