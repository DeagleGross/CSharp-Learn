using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Graphs
{
    public class BreadthFirstSearch
    {
        private GraphRepresentation Graph { get; set; } = null!;

        private class GraphRepresentation
        {
            private List<int>[] Adj { get; }

            public GraphRepresentation() : this(10)
            {
            }

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
                DfsInternal(startingPoint);
            }

            private void DfsInternal(int v)
            {
                var visited = new bool[Adj.Length];

                var queue = new Queue<int>();
                visited[v] = true;
                queue.Enqueue(v);

                while (queue.Any())
                {
                    var current = queue.Dequeue();
                    Console.WriteLine($"Visited V = {current}");

                    foreach (var neighbour in Adj[current])
                    {
                        if (!visited[neighbour])
                        {
                            visited[neighbour] = true;
                            queue.Enqueue(neighbour);
                        }
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

        public void RunDfs()
        {
            Graph.Dfs(2);
        }
    }
}
