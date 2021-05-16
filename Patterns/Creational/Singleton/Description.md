# Singleton
Source: [C# in Depth, Jon Skeet - Implementing the Singleton Pattern in C#](https://csharpindepth.com/articles/singleton)

## Description
Singleton is a class which only allows a single instance of itself to be created. Commonly, singletons don't require any parameters to be instantiated.

Every implementation is placed in a separate file and shares four common characteristics:
- a single `private and parameterless` constructor to prevent external classes to instantiate it.
- class is sealed for JIT optimizations
- there is a `static field` in a class that holds a reference to the single created instance
- access to singleton is provided using a `public static property` - it is created, if it was not before.

Versions:  
1.  **bad** [Not thread safe](.\Singleton_v1.cs)
2.  **bad** [Simple thread-safety](.\Singleton_v2.cs)
3.  **bad** [Not correct thread-safe with use of double-check locking](.\Singleton_v3.cs)
4.  **ok** [No locks thread-safe without using locks](.\Singleton_v4.cs)
5.  **ok** [Fully lazy instatiation](.\Singleton_v5.cs)
6.  **ok** [Using .NET Lazy\<T\> type](.\Singleton_v6.cs)

## Conclusion:

Jon prefers solution 4. However I really like the last one due to my experience with later versions of .NET. Last 3 solutions work pretty well and it's up to you to choose the one you really need.