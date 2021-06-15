using System;
using Serilog;
using Trees.Algorithms;

namespace Trees
{
    public static class Program
    {
        public static void Main()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .MinimumLevel.Debug()
                .CreateLogger();

            Log.Information("DFS check");
            var dfs = new DepthFirstSearch();
            dfs.Initialize();
            dfs.RunBfs();
            Log.Information("---------");
            Log.Information("");

            Log.Information("BFS check");
            var bfs = new DepthFirstSearch();
            bfs.Initialize();
            bfs.RunBfs();
            Log.Information("---------");
            Log.Information("");

            Log.Information("Dijkstra check");
            var dijkstra = new Dijkstra();
            dijkstra.Run();
            Log.Information("---------");
            Log.Information("");
        }
    }
}
