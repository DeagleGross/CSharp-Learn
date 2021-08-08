using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DissectingAsyncMethods
{
    class Program
    {
        public static void Main()
        {
            var stockPrices = new StockPrices();

            Task.Run(async () =>
            {
                var result = await stockPrices.GetStockPriceForAsync("companyIdentifier_abc");
            });
        }
    }
}
