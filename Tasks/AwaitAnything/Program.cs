using System.Threading.Tasks;
using AwaitAnything.Default;

namespace AwaitAnything
{
    public static class Program
    {
        public static async Task Main()
        {
            await EnumerableAwaiter.MainActivityWithResult();
        }
    }
}
