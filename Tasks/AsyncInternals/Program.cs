using System.Threading.Tasks;

namespace AsyncInternals
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await PooledValueTaskSource.Program.RunMain();
        }
    }
}
