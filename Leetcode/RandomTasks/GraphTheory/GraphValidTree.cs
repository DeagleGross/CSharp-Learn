using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

// https://leetcode.com/problems/graph-valid-tree/

namespace LeetCodeSolutions.RandomTasks.GraphTheory
{
	[TestClass]
	public class GraphValidTree
	{
		[TestMethod]
		public void Solve()
		{
			int[][] edges = new[]
			{
				new[]{0,1},
				new[]{0,2},
				new[]{0,3},
				new[]{1,4},
			};

			var n = 5;

			var result = ValidTree(n, edges);

			result.ShouldBe(true);
		}

		[TestMethod]
		public void Solve2()
		{
			int[][] edges = new[]
			{
				new[]{0,1},
				new[]{1,2},
				new[]{2,3},
				new[]{1,3},
				new[]{1,4},
			};

			var n = 5;

			var result = ValidTree(n, edges);

			result.ShouldBe(false);
		}

		[TestMethod]
		public void Solve3()
		{
			int[][] edges = new[]
			{
				new[]{1,0},
				new[]{2,0},
			};

			var n = 3;

			var result = ValidTree(n, edges);

			result.ShouldBe(true);
		}

		[TestMethod]
		public void Solve4()
		{
			int[][] edges = new[]
			{
				new[]{0,1},
				new[]{2,3},
			};

			var n = 4;

			var result = ValidTree(n, edges);

			result.ShouldBe(false);
		}

		public bool ValidTree(int n, int[][] edges)
		{
			#region Possible optimization - advanced graph theory

			// For the graph to be a valid tree, it must have exactly n - 1 edges.
			// Any less, and it can't possibly be fully connected.
			// Any more, and it has to contain cycles.
			// Additionally, if the graph is fully connected and contains exactly n - 1 edges,
			// it can't possibly contain a cycle, and therefore must be a tree!
			if (edges.Length != n - 1)
			{
				return false;
			}

			// Possible optimization :

			// after this check we simply can do a DFS without a seen array and simply add seen nodes in the list 
			// if the node exists in seen list - return; if it is not in the list - add it there
			// return true if seen.Length == n

			#endregion

			Dictionary<int, List<int>> adjacencyList = new Dictionary<int, List<int>>();

			for (int i = 0; i < edges.Length; i++)
			{
				var source = edges[i][0];
				var target = edges[i][1];

				if (!adjacencyList.ContainsKey(source))
				{
					adjacencyList[source]= new List<int>();
				}

				if (!adjacencyList.ContainsKey(target))
				{
					adjacencyList[target] = new List<int>();
				}

				adjacencyList[source].Add(target);
				adjacencyList[target].Add(source);
			}

			// use BFS or DFS to walk tree nodes

			// we use previous node to eliminate trivial cycles since out adjacency list contains two edges
			// between two adjacent nodes A, B : A->B, B->A. To not go backwards we keep track of the node we came from

			bool[] visited = new bool[n];
			Dfs(adjacencyList, 0, visited, -1);

			if (_hasCycle)
			{
				return false;
			}

			if (visited.Any(v => v == false))
			{
				return false;
			}

			return true;
		}

		bool _hasCycle = false;

		private void Dfs(Dictionary<int, List<int>> adjacencyList, int node, bool[] visited, int previousNode)
		{
			if (_hasCycle)
			{
				// no need to recurse further - cyclic graph is not a tree
				return;
			}

			if (visited[node] == true)
			{
				_hasCycle = true;
				return;
			}

			visited[node] = true;

			if (!adjacencyList.ContainsKey(node))
			{
				return;
			}

			foreach (var adjacentNode in adjacencyList[node])
			{
				if (adjacentNode == previousNode)
				{
					continue;
				}

				Dfs(adjacencyList, adjacentNode, visited, node);
			}
		}
	}
}
