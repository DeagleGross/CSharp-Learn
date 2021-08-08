# Extending the async methods
Source: [Extending the async methods in C#](https://devblogs.microsoft.com/premier-developer/extending-the-async-methods-in-c/)  
Author: **Sergey Tepliakov**

## Ways to control the async method's machinery:
1. Provide your own async method builder in the `System.Runtime.CompilerServices` namespace
2. Use custom task awaiters
3. Define your own task-like types

## Custom types from `System.Runtime.CompilerServices` namespace
You can provide your own implementation of [AsyncVoidMethodBuilder](https://referencesource.microsoft.com/#mscorlib/system/runtime/compilerservices/AsyncMethodBuilder.cs,b07562c618ee846c) in your project and the C# compiler will “bind” async machinery to your custom type.

Here is the example - [my custom AsyncVoidMethodBuilder](./AsyncVoidMethodBuilder.cs). Please, note that I've changed `namespace` to `System.Runtime.CompilerServices` and implemented all of the included in class methods. Otherwise, where was an error like this: 
```
Error	CS0656	Missing compiler required member 'System.Runtime.CompilerServices.AsyncVoidMethodBuilder.SetStateMachine'
```

After that run method [RunAsyncVoid in Program.cs](./Program.cs) - here is the output:
```
Before VoidAsync
.ctor // written from AsyncVoidMethodBuilder
Start // written from AsyncVoidMethodBuilder
SetResult // written from AsyncVoidMethodBuilder
After VoidAsync
```

To change the behavior for `async Task` and `async Task<T>` methods you should provide your own version of `AsyncTaskMethodBuilder` and `AsyncTaskMethodBuilder<T>`. Let me try to reimplement different async-builders like Sergey Teplyakov. I've named the project the same way John Skeet has done it to Linq - [EduAsync](./EduAsync).

## Custom awaiters
This is an approach as described in [Await Anything](./../AwaitAnything).

- Compiler should be able to find an instance or an extension method called `GetAwaiter`. The return type of this method should follow certain requirements:
- The type should implement INotifyCompletion interface.
- The type should have bool IsCompleted {get;} property and T GetResult() method.

Example - [LazyAwaiter<T>](./LazyAwaiter.cs)