using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace AwaitAnything.Default
{
    /*
     * Imagine we want to await IEnumerable<Task> like in `MainActivity()` method.
     * Super easy solution - implement `GetAwaiter()` extension method for `IEnumerable<Task>`!
     *
     * Also there is a `WithResult` methods duplicate for showing how to implement TaskAwaiter<TResult>
     */

    public static class EnumerableAwaiter
    {
        private static readonly List<string> Urls = new List<string>
        {
            "https://www.google.com/", "https://github.com/", "https://open.spotify.com/"
        };

        public static TaskAwaiter GetAwaiter(this IEnumerable<Task> tasks)
        {
            return Task.WhenAll(tasks).GetAwaiter();
        }

        public static TaskAwaiter<string[]> GetAwaiter(this IEnumerable<Task<string>> tasks)
        {
            return Task.WhenAll(tasks).GetAwaiter();
        }

        public static async Task MainActivity()
        {
            await Urls.Select(DownloadAsync);
        }

        public static async Task MainActivityWithResult()
        {
            var results = await Urls.Select(DownloadAsyncWithResult);

            foreach (var result in results)
            {
                Console.WriteLine(result);
            }
        }

        private static Task DownloadAsync(string url)
        {
            // some void actions occur here
            return Task.CompletedTask;
        }

        private static async Task<string> DownloadAsyncWithResult(string url)
        {
            var httpClient = HttpClientFactory.Create();
            var httpContent = await httpClient.GetAsync(url);

            return await httpContent.Content.ReadAsStringAsync();
        }
    }
}
