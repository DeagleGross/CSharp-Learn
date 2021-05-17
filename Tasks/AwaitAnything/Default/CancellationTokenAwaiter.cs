using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace AwaitAnything.Default
{
    /*
     * Another example with cancellation token awaiting (look at `MainActivity()`)
     */

    public static class CancellationTokenAwaiter
    {
        public static TaskAwaiter GetAwaiter(this CancellationToken cancellationToken)
        {
            var tcs = new TaskCompletionSource<bool>();
            Task t = tcs.Task;
            if (cancellationToken.IsCancellationRequested) tcs.SetResult(true);
            else cancellationToken.Register(s => ((TaskCompletionSource<bool>)s).SetResult(true), tcs);
            return t.GetAwaiter();
        }

        public static async Task MainActivity(CancellationToken cancellationToken)
        {
            await cancellationToken;
        }
    }
}
