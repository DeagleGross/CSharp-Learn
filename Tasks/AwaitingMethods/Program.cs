using System;
using System.Threading.Tasks;

namespace AwaitingMethods
{
    class Program
    {
        static void Main(string[] args)
        {
            // Test custom AsyncVoidMethodBuilder
            RunAsyncVoid();

            // Test custom awaiter class
            LazyTest().RunSynchronously();
        }

        static void RunAsyncVoid()
        {
            Console.WriteLine("Before VoidAsync");
            VoidAsync();
            Console.WriteLine("After VoidAsync");

            Task VoidAsync() { return Task.CompletedTask; }
        }

        static async Task LazyTest()
        {
            var lazy = new Lazy<int>(() => 42);
            var res = await lazy;
            Console.WriteLine(res);
        }
    }
}
