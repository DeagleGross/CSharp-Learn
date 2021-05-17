using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace AwaitAnything.Default
{
    /*
     * Imagine we want to await integer like in `MainActivity()` method.
     * Super easy solution - implement `GetAwaiter()` extension method for `integer`!
     */

    public static class IntegerAwaiter
    {
        public static TaskAwaiter GetAwaiter(this int millisecondsDelay)
        {
            return Task.Delay(millisecondsDelay).GetAwaiter();
        }

        public static async Task MainActivity()
        {
            await 15000;
        }
    }
}
