# Interoperability with Unmanaged Code
CLR offers mechanisms that allow an application to consist of both managed and unmanaged parts:
- *Managed code can call an unmanaged function in a DLL* using `P/Invoke`. I.e. c# app can call `CreateSemaphore` from `Kernel32.dll`
- *Managed code can use an existing COM component (server)*. 
- *Unmanaged code can use a managed type (server)*. 