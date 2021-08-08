using System.Collections.Generic;
using System.Threading.Tasks;

namespace DissectingAsyncMethods
{
    public class StockPrices
    {
        // it has to be private in production code, but for other classes to use these methods I've defined them as internal
        internal Dictionary<string, decimal> _stockPrices;

        public async Task<decimal> GetStockPriceForAsync(string companyId)
        {
            await InitializeMapIfNeededAsync();
            _stockPrices.TryGetValue(companyId, out var result);
            return result;
        }

        // it has to be private in production code, but for other classes to use these methods I've defined them as internal
        internal async Task InitializeMapIfNeededAsync()
        {
            if (_stockPrices != null)
                return;

            await Task.Delay(42);
            // Getting the stock prices from the external source and cache in memory.
            _stockPrices = new Dictionary<string, decimal> { { "MSFT", 42 } };
        }
    }
}
