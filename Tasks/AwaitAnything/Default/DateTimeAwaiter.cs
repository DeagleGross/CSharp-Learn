using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AwaitAnything.Default
{
    /*
     * Imagine we want to await DateTimeOffset like in `MainActivity()` method.
     * Super easy solution - implement `GetAwaiter()` extension method for `DateTimeOffset`!
     */

    public static class DateTimeAwaiter
    {
        public static TaskAwaiter GetAwaiter(this DateTimeOffset dateTimeOffset)
        {
            return (dateTimeOffset - DateTimeOffset.UtcNow).GetAwaiter();
        }

        public static async Task MainActivity()
        {
            await DateTimeOffset.UtcNow.AddMilliseconds(10000);
        }
    }
}
