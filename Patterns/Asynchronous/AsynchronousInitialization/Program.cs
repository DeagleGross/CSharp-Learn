using System.Threading.Tasks;

namespace AsynchronousInitialization
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            var dataStorage = new DataStorage();
            var data = await dataStorage.GetData(0);
        }
    }
}
