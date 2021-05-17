using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace AwaitAnything.Default
{
    /*
     * Imagine we want to await TimeSpan like in `MainActivity()` method.
     * Super easy solution - implement `GetAwaiter()` extension method for `TimeSpan`!
     */

    public static class TimeSpanAwaiter
    {
        public static TaskAwaiter GetAwaiter(this TimeSpan timeSpan)
        {
            return Task.Delay(timeSpan).GetAwaiter();
        }

        public static async Task MainActivity()
        {
            await TimeSpan.FromMinutes(15);
        }
    }
}
