using System;
using System.Runtime.CompilerServices;

namespace AwaitingMethods
{
    public class LazyAwaiter<T> : INotifyCompletion
    {
        private readonly Lazy<T> _lazy;

        public LazyAwaiter(Lazy<T> lazy)
        {
            _lazy = lazy;
        }

        public bool IsCompleted => true;

        public T GetResult()
        {
            return _lazy.Value;
        }

        public void OnCompleted(Action continuation)
        {
            // do nothing
        }
    }

    public static class LazyAwaiterExtensions
    {
        public static LazyAwaiter<T> GetAwaiter<T>(this Lazy<T> lazy)
        {
            return new LazyAwaiter<T>(lazy);
        }
    }
}
