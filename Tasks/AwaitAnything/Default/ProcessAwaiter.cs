using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace AwaitAnything.Default
{
    /*
     * Example with process awaiting (again look at `MainActivity()`)
     */

    public static class ProcessAwaiter
    {
        public static TaskAwaiter<int> GetAwaiter(this Process process)
        {
            var tcs = new TaskCompletionSource<int>();

            process.EnableRaisingEvents = true;
            process.Exited += (s, e) => tcs.TrySetResult(process.ExitCode);

            if (process.HasExited)
            {
                tcs.TrySetResult(process.ExitCode);
            }

            return tcs.Task.GetAwaiter();
        }

        public static async Task MainActivity()
        {
            // ReSharper disable once PossibleNullReferenceException
            await Process.Start("Foo.exe");
        }
    }
}
