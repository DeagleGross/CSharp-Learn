using System;
using System.Collections.Generic;

namespace Algorithms.Graphs
{
    public class DepthFirstSearch
    {
        private GraphRepresentation Graph { get; set; } = null!;

        private class GraphRepresentation
        {
            private List<int>[] Adj { get; }

            public GraphRepresentation(int number)
            {
                Adj = new List<int>[number];
                for (int i = 0; i < number; i++)
                {
                    Adj[i] = new List<int>();
                }
            }

            public void AddEdge(int v, int w)
            {
                Adj[v].Add(w);
            }

            public void Dfs(int startingPoint)
            {
                var visited = new bool[Adj.Length];
                DfsInternal(startingPoint, visited);
            }

            private void DfsInternal(int v, bool[] visited)
            {
                visited[v] = true;
                Console.WriteLine($"Visited V = {v}");

                foreach (var node in Adj[v])
                {
                    if (!visited[node])
                    {
                        DfsInternal(node, visited);
                    }
                }
            }
        }

        public void Initialize()
        {
            Graph = new GraphRepresentation(4);

            Graph.AddEdge(0, 1);
            Graph.AddEdge(0, 2);
            Graph.AddEdge(1, 2);
            Graph.AddEdge(2, 0);
            Graph.AddEdge(2, 3);
            Graph.AddEdge(3, 3);
        }

        public void RunBfs()
        {
            Graph.Dfs(2);
        }
    }
}
