using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TaskContext
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
