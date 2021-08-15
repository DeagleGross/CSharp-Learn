# ExecutionContext vs SynchronizationContext
Author - Stephen Toub  
link - https://devblogs.microsoft.com/pfxteam/executioncontext-vs-synchronizationcontext/?WT.mc_id=DT-MVP-5003978

---
## ExecutionContext

ExecutionContext is a container for other contexts.  Execution context stores *ambient* information - all about the current environment or *context*. In many systems such ambient information is maintained in thread-local storage (TLS), such as in a `ThreadStatic` field or in a `ThreadLocal<T>`. 

In synchronous world such a *thread-local* information is sufficient: all happening on that one thread. All the code running on that thread can see and be influenced by data specific to that thread. I.e. one of the contexts contained by `ExecutionContext` is a `SecurityContext`. It maintains information like the current *principal* and information about code access security denies and permits. CLR is checing the current thread's denials to see if the operation is allowed and it'll find the data put there by the caller.

Things get more complicated in asynchronous world - TLS becomes largely irrelevant. Operation *A* might start on one thread, complete on another. This means thread can not hold all of the ambient information operation needs.  

ExecutionContext is a state bag that can be used to capture all of this state from one thread and then restore it onto another thread while the logical flow continues.

One can capture ExecutionContext:
```
var ec = ExecutionContext.Capture();
```

and it can be restored during the invocation of a delegate via a static run method:
```
ExecutionContext.Run(ec, delegate {
    ... // code here will see ec's state as ambient
}, null);
```

When the delegate provided to Task.Run is later invoked as part of that Task’s execution, it’s done so via ExecutionContext.Run using the stored context.  This is true for Task.Run, for ThreadPool.QueueUserWorkItem, for Delegate.BeginInvoke, for Stream.BeginRead, for DispatcherSynchronizationContext.Post, and for any other async API you can think of.  All of them capture the ExecutionContext, store it, and then use the stored context later on during the invocation of some code.

---
## SynchronizationContext
It is an abstraction, one that represents a particular environmnet you want to do some work in. 

We now have two different APIs for achieving the same basic operation, so how do I write my component to be agnostic of the UI framework?  By using SynchronizationContext.  SynchronizationContext provides a virtual Post method; this method simply takes a delegate and runs it wherever, whenever, and however the SynchronizationContext implementation deems fit.  Windows Forms provides the WindowsFormSynchronizationContext type which overrides Post to call Control.BeginInvoke.  WPF provides the DispatcherSynchronizationContext type which overrides Post to call Dispatcher.BeginInvoke.  And so on.  As such, I can now code my component to use SynchronizationContext instead of tying it to a specific framework.

Windows Forms style UI and bakcground logic
```
public static void DoWork(Control c)
{
    ThreadPool.QueueUserWorkItem(delegate
    {
        … // do work on ThreadPool
        c.BeginInvoke(delegate
        {
            … // do work on UI
        });
    });
}
```

Windows Forms style UI and bakcground logic **using SynchronizationContext**
```
public static void DoWork(SynchronizationContext sc)
{
    ThreadPool.QueueUserWorkItem(delegate
    {
        … // do work on ThreadPool
        sc.Post(delegate
        {
            … // do work on UI
        }, null);
    });
}
```

One can retrieve synchro-context in such a way:
```
var sc = SynchronizationContext.Current;
```

---
## Flowing ExecutionContext vs using SynchronizationContext
When you flow ExecutionContext, you’re capturing the state from one thread and then restoring that state such that it’s ambient during the supplied delegate’s execution.  That’s not what happens when you capture and use a SynchronizationContext.  The capturing part is the same, in that you’re grabbing data from the current thread, but you then use that state differently.  Rather than making that state current during the invocation of the delegate, with SynchronizationContext.Post you’re simply using that captured state to invoke the delegate.  Where and when and how that delegate runs is completely up to the implementation of the Post method.

---
## How does it apply to `async\await`
The framework support behind the async and await keywords automatically interacts with both ExecutionContext and SynchronizationContext.

Whenever code awaits an awaitable whose awaiter says it’s not yet complete (i.e. the awaiter’s IsCompleted returns false), the method needs to suspend, and it’ll resume via a continuation off of the awaiter. When the async method is about to suspend, the infrastructure captures an ExecutionContext. The delegate that gets passed to the awaiter has a reference to this ExecutionContext instance and will use it when resuming the method.  This is what enables the important “ambient” information represented by ExecutionContext to flow across awaits.

The Framework also has support for SynchronizationContext.  The aforementioned support for ExecutionContext is built into the “builders” that represent async methods (e.g. `System.Runtime.CompilerServices.AsyncTaskMethodBuilder`), and these builders ensure that ExecutionContext is flowed across await points regardless of what kind of awaitable is being used.  In contrast, support for SynchronizationContext is built into the support for awaiting `Task` and `Task<TResult>`, specifically.  Custom awaiters could add similar logic themselves, but they don’t get it automatically; that’s by design, as being able to customize when and how the continuation gets invoked is part of why custom awaiters are useful.

**Important**
When you await a task, by default the awaiter will capture the current SynchronizationContext, and if there was one, when the task completes it’ll Post the supplied continuation delegate back to that context, rather than running the delegate on whatever thread the task completed or rather than scheduling it to run on the ThreadPool. `ConfigureAwait()` is really suppressing or enabling this marshaling behaviour.

There is no a analog of `ConfigureAwait()` for ExecutionContext.

---
## Is SynchroContext part of ExecutionContext ?
Yes - it is, like others (`SecurityContext`, `HostExecutionContext`, `CallContext` and etc).

When you call the public ExecutionContext.Capture() method, that checks for a current SynchronizationContext, and if there is one, it stores that into the returned ExecutionContext instance. Then, when the public ExecutionContext.Run method is used, that captured SynchronizationContext is restored as Current during the execution of the supplied delegate.

It is a little bit confusing:
- `SynchronizationContext.Current` is supposed to be something you can access to get back to environment that you're currently in at the time you access Current.
- But what if `SynchronizationContext` is on another thread ? You can not trust what it really means.

Here is a problematic code:
```
private void button1_Click(object sender, EventArgs e)
{
    button1.Text = await Task.Run(async delegate
    {
        string data = await DownloadAsync();
        return Compute(data);
    });
}
```

`Task.Run` captures `ExecutionContext` when invoked, and uses it to run the delegate passed to it.
That means that the **UI SynchronizationContext** which was current when `Task.Run` was invoked would flow into the `Task` and would be `Current` while invoking `DownloadAsync` and awaiting the resulting task.

That then means that the await will see the Current SynchronizationContext and Post the remainder of asynchronous method as a continuation to run back on the UI thread.  And that means my `Compute` method will very likely be running on the UI thread, not on the ThreadPool, causing responsiveness problems for my app.

The story now gets a bit messier: ExecutionContext actually has two Capture methods, but only one of them is public.  The internal one (internal to mscorlib) is the one used by most asynchronous functionality exposed from mscorlib, and it optionally allows the caller to suppress the capturing of SynchronizationContext as part of ExecutionContext; corresponding to that, there’s also an internal overload of the Run method that supports ignoring a SynchronizationContext that’s stored in the ExecutionContext, in effect pretending one wasn’t captured (this is, again, the overload used by most functionality in mscorlib).  What this means is that pretty much any asynchronous operation whose core implementation resides in mscorlib won’t flow SynchronizationContext as part of ExecutionContext, but any asynchronous operation whose core implementation resides anywhere else will flow SynchronizationContext as part of ExecutionContext.  I previously mentioned that the “builders” for async methods were the types responsible for flowing ExecutionContext in async methods, and these builders do live in mscorlib, and they do use the internal overloads… as such, SynchronizationContext is not flowed as part of ExecutionContext across awaits (this, again, is separate from how task awaiters support capturing the SynchronizationContext and Post’ing back to it).   To help deal with the cases where ExecutionContext does flow SynchronizationContext, the async method infrastructure tries to ignore SynchronizationContexts set as Current due to being flowed.

In short, SynchronizationContext.Current does not “flow” across await points.