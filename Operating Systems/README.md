# Operating Systems

## Synchronization Basics
### Semaphore
Operations:
- increment
- decrement (if you are decrementing 0 value, semaphore is blocked)

Semaphore is released, when process increments semaphore, indicating work is finished.

### Mutex
A variable with 2 states - locked and free.

### Monitor
`lock(object)` in c# implemented using monitors.

---

## About Processes & Threads 
[Link](https://docs.microsoft.com/en-gb/windows/win32/procthread/about-processes-and-threads?redirectedfrom=MSDN)

`Process`  
Each process provides the resources needed to execute a program. A process has a virtual address space, executable code, open handles to system objects, a security context, a unique process identifier, environment variables, a priority class, minimum and maximum working set sizes, and at least one thread of execution. Each process is started with a single thread, often called the primary thread, but can create additional threads from any of its threads.

`Thread`  
A thread is an entity within a process that can be scheduled for execution. All threads of a process share its virtual address space and system resources. In addition, each thread maintains exception handlers, a scheduling priority, thread local storage, a unique thread identifier, and a set of structures the system will use to save the thread context until it is scheduled. The thread context includes the thread's set of machine registers, the kernel stack, a thread environment block, and a user stack in the address space of the thread's process. Threads can also have their own security context, which can be used for impersonating clients.

`Context switches`  
The scheduler maintains a queue of executable threads for each priority level. These are known as ready threads. When a processor becomes available, the system performs a context switch. The steps in a context switch are:
- Save the context of the thread that just finished executing.
- Place the thread that just finished executing at the end of the queue for its priority.
- Find the highest priority queue that contains ready threads.
- Remove the thread at the head of the queue, load its context, and execute it.

The following classes of threads are not ready threads.
- Threads created with the CREATE_SUSPENDED flag
- Threads halted during execution with the SuspendThread or SwitchToThread function
- Threads waiting for a synchronization object or input.

Until threads that are suspended or blocked become ready to run, the scheduler does not allocate any processor time to them, regardless of their priority.

The most common reasons for a context switch are:
- The time slice has elapsed.
- A thread with a higher priority has become ready to run.
- A running thread needs to wait.
- When a running thread needs to wait, it relinquishes the remainder of its time slice.

---
## Other Definitions

[In concurrent computing](https://stackoverflow.com/questions/6155951/whats-the-difference-between-deadlock-and-livelock), a `deadlock` is a state in which each member of a group of actions, is waiting for some other member to release a lock

A `livelock` is similar to a deadlock, except that the states of the processes involved in the livelock constantly change with regard to one another, none progressing. Livelock is a special case of resource starvation; the general definition only states that a specific process is not progressing.

A real-world example of livelock occurs when two people meet in a narrow corridor, and each tries to be polite by moving aside to let the other pass, but they end up swaying from side to side without making any progress because they both repeatedly move the same way at the same time.

Livelock is a risk with some algorithms that detect and recover from deadlock. If more than one process takes action, the deadlock detection algorithm can be repeatedly triggered. This can be avoided by ensuring that only one process (chosen randomly or by priority) takes action.