# Await Anything
Source: [Microsoft DevBlog, Stephen Toub - await anything](https://devblogs.microsoft.com/pfxteam/await-anything/)  
Author: **Stephen Toub**

## What you need
`await` keyword and everything asynchronous happening to it in C# is pattern based. That's awesome, because one can even `await` type that is not `Task` or `Task<TResult>`!

Imagine you want to create a type `MyClass` that could be awaited. This is what a type has to expose:
- `public MyAwaiter GetAwaiter()`
- `MyAwaiter` in turn has to expose these members:
    - `bool IsCompleted { get; }`
    - `void OnCompleted(Action continuation);`  `OnCompleted()` method registers the `Action` as continuation onto the Task (you use it with `.ContinueWith()`)
    - `TResult GetResult();`, where `TResult` could be void

If you want to explore .NET Core sources, here is `Task`'s [TaskAwaiter GetAwaiter](https://source.dot.net/#System.Private.CoreLib/Task.cs,2426) and this is how it is declared:  
[public readonly struct TaskAwaiter : ICriticalNotifyCompletion, ITaskAwaiter](https://source.dot.net/#System.Private.CoreLib/TaskAwaiter.cs,56)

There are two different approaches to making something awaitable:  
1 - [develop new awaiter type that exposes above described pattern](Custom)  
2 - [use `Task` or `Task<T>` types to use their awaiters](Default)