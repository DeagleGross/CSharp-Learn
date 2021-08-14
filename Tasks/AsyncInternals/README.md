# Async Internals in .NET
Author - **Adam Furmanek**  
Conference - **Update Conference Prague**  
Link - https://youtu.be/FjIIbG9abM8  

---
## async and await and chaining
- await can be executed on anything awaitable - not necessarily a `Task`
- awaitable type must return a `GetAwaiter()` with the following:
    - implements `INotifyCompletion` interface
    - contains `bool IsCompleted { get; }`
    - contains `void OnCompleted(Action continuation)`
    - contains `TResult GetResult()` or `void GetResult()`
- async means nothing -> it only instructs the compiler to create a state machine

When `Task` is [scheduled and started](https://source.dot.net/System.Private.CoreLib/R/107ac97251bea153.html) there is a call to task scheduler [m_taskScheduler.InternalQueueTask(this);](https://source.dot.net/#System.Private.CoreLib/Task.cs,1681).

It is a special type that schedules tasks on the threads and makes sure that task is eventually executed. `TPL` and `PLINQ` is based on the thread pool.

We can easily implement our own [TaskScheduler](./MyTaskScheduler.cs)

`Task.ContinueWith` is just creating a continuation that executes asynchronously when the target task complets. Main logic in located at [void ContinueWithCore](https://source.dot.net/System.Private.CoreLib/R/0a28c847d772a11b.html), where [TaskContinuation](https://source.dot.net/#System.Private.CoreLib/Task.cs,4364) instance is created with type `ContinueWithTaskContinuation`. And then an [AddTaskContinuation](https://source.dot.net/#System.Private.CoreLib/Task.cs,4422) is called.

`Complete()` tries to [TrySetResult](https://source.dot.net/#System.Private.CoreLib/Task.cs,3225) for finishing. And then tries to call a continuation. But the continuation is scheduling and starting the continuation task. That`s all about a loop for continuationing tasks.

---
## Disposing a Task
A rule of thumb is `do not dispose a task`.  

Task **may** allocated `WaitHandle` which allocated `IDisposable`.
Disposing a `Task` in .NET 4 was making it unusable - we couldn't even schedule continuation.
In .NET 4.5 this was changed, `Task` is still usable, but `WaitHandle` is not.
Starting in .NET 4.5 `WaitHandle` is allocated only when it is explicitly accessed.

Summary:
- .NET 4 - **don't dispose** unless you have to. But you can not use `Task` instance anymore.
- .NET 4.5 - it should not make any difference, so **don't bother**.

---
## ValueTask
`Task` is a class, so it is allocated on the heap and needs to be collected by GC.
To avoid explicit allocation we can you `ValueTask`, which is a struct and is allocated on the stack.
