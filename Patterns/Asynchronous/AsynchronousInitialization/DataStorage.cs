using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsynchronousInitialization
{
    internal class DataStorage
    {
        private readonly IList<int> _items = new List<int>();

        private readonly TaskCompletionSource _initializationCompletionSource = new();

        private Task IsInitialized => _initializationCompletionSource.Task;

        public DataStorage()
        {
            // starting a task from a constructor, not blocking any thread
            Task.Run(() => Initialize());
        }

        private async Task Initialize()
        {
            // initialization logic
            for (var i = 0; i < 10; i++)
            {
                await Task.Delay(100);
                _items.Add(i);
            }

            // setting task_completion_source as finished
            // from `Running` to `RanToCompletion` status.
            // 
            // only after all of initialization logic has completed, it is marked with such a status
            _initializationCompletionSource.SetResult();

            // you can .SetException() if initialization has failed also
            // _initializationCompletionSource.SetException();
        }

        public async Task<int> GetData(int index)
        {
            // making sure it is really initialized -> it will only after TaskCompletionSource sets Results
            await IsInitialized;

            if (index < 0 || index > _items.Count) throw new ArgumentOutOfRangeException(nameof(index));
            return _items[index];
        }
    }
}
