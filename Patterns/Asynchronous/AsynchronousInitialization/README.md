# Asynchronous Initialization
There are some cases you need to run class initialization logic from a constructor. 
And it is not a synchronous! 

But it is a bad idea to just run method in constructor.
And you don't really know has it already completed or not...

So you can use an asynchronous initialization pattern following these steps:

1. Run a separate task with your `Initialize` logic
```c#
public Constructor()
{
    Task.Run(() => Initialize());
}
```

2. 
```c#
public async Task Initialize()
{
    // your initialization logic
    await Task.Delay(10000);

    // private readonly field of `TaskCompletionSource` type is set to finished
    _taskCompletionSource.SetResult();
}
```

3. Make a task property, that is pointing to the Task of `_taskCompletionSource` that is set in `Initialize` method
```c#
private readonly TaskCompletionSource _taskCompletionSource = new();
private Task IsInitialized => _taskCompletionSource.Task;
```

4. In any of your methods just await `IsInitialized` to make sure your data has really loaded properly and to the end
```c#
public async Task GetSomething()
{
    await IsInitialized;

    // some data returned
    return _items.First();
}
```

**View the example in a project** for testing this approach.