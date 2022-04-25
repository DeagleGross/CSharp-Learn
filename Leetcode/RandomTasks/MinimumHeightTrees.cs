using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

// https://leetcode.com/problems/minimum-height-trees/
// this solution operates on graph centroids
// see solution https://leetcode.com/problems/minimum-height-trees/solution/

namespace LeetCodeSolutions.RandomTasks
{
	[TestClass]
	public class MinimumHeightTrees
	{
		[TestMethod]
		public void Solve()
		{
			int[][] edges = new[]
			{
				new[]{1,0},
				new[]{1,2},
				new[]{1,3},
			};

			var n = 4;

			var result = FindMinHeightTrees(n, edges);

			result.Count.ShouldBe(1);
		}

		[TestMethod]
		public void Solve2()
		{
			int[][] edges = new[]
			{
				new[]{3,0},
				new[]{3,1},
				new[]{3,2},
				new[]{3,4},
				new[]{5,4},
			};

			var n = 6;

			var result = FindMinHeightTrees(n, edges);

			result.Count.ShouldBe(2);
		}

		[TestMethod]
		public void Solve3()
		{
			int[][] edges = Array.Empty<int[]>();
			var n = 1;

			var result = FindMinHeightTrees(n, edges);

			result.Count.ShouldBe(1);
		}

		[TestMethod]
		public void Solve4()
		{
			int[][] edges = new[]
			{
				new[]{0,1},
			};
			var n = 2;

			var result = FindMinHeightTrees(n, edges);

			result.Count.ShouldBe(2);
		}

		[TestMethod]
		public void Solve5()
		{
			int[][] edges = new[]
			{
				new[]{0,1},
				new[]{0,2},
				new[]{0,3},
				new[]{3,4},
				new[]{4,5},
			};
			var n = 6;

			var result = FindMinHeightTrees(n, edges);

			result.Count.ShouldBe(1);
		}

		public IList<int> FindMinHeightTrees(int n, int[][] edges)
		{
			// edge cases
			if (n < 2)
			{
				List<int> centroids = new List<int>();
				for (int i = 0; i < n; i++)
				{
					centroids.Add(i);
				}
				return centroids;
			}

			Dictionary<int, List<int>> adjacencyList = new Dictionary<int, List<int>>();

			for (int i = 0; i < edges.Length; i++)
			{
				var source = edges[i][0];
				var target = edges[i][1];

				if (!adjacencyList.ContainsKey(source))
				{
					adjacencyList[source] = new();
				}

				if (!adjacencyList.ContainsKey(target))
				{
					adjacencyList[target] = new ();
				}

				adjacencyList[source].Add(target);
				adjacencyList[target].Add(source);
			}

			// Initialize the first layer of leaves
			List<int> leaves = new();
			for (int i = 0; i < n; i++)
			{
				if (adjacencyList[i].Count == 1)
				{
					leaves.Add(i);
				}
			}

			// Trim the leaves until reaching the centroids
			int remainingNodes = n;

			while (remainingNodes > 2)
			{
				remainingNodes -= leaves.Count;
				List<int> newLeaves = new();

				// remove the current leaves along with the edges
				foreach(var leaf in leaves)
				{
					// the only neighbor left for the leaf node
					int neighbor = adjacencyList[leaf][0];

					// remove the edge along with the leaf node
					adjacencyList[neighbor].Remove(leaf);
					if (adjacencyList[neighbor].Count == 1)
					{
						newLeaves.Add(neighbor);
					}
				}

				// prepare for the next round
				leaves = newLeaves;
			}

			// The remaining nodes are the centroids of the graph
			return leaves;
		}

		#region Incorrect approach

		public IList<int> FindMinHeightTrees_IncorrectApproach(int n, int[][] edges)
		{
			if (edges.Length == 0)
			{
				return new List<int>() { 0 };
			}

			Dictionary<int, List<int>> adjacencyList = new Dictionary<int, List<int>>();

			Dictionary<int, int> nodesCount = new Dictionary<int, int>();

			for (int i = 0; i < edges.Length; i++)
			{
				var source = edges[i][0];
				var target = edges[i][1];

				if (!adjacencyList.ContainsKey(source))
				{
					adjacencyList[source] = new List<int>();
				}

				if (!adjacencyList.ContainsKey(target))
				{
					adjacencyList[target] = new List<int>();
				}

				adjacencyList[source].Add(target);
				adjacencyList[target].Add(source);

				if (!nodesCount.ContainsKey(source))
				{
					nodesCount[source] = 0;
				}

				if (!nodesCount.ContainsKey(target))
				{
					nodesCount[target] = 0;
				}

				nodesCount[source] += 1;
				nodesCount[target] += 1;
			}


			var nodeWithMaxCount = nodesCount.OrderByDescending(kv => kv.Value).ToList();

			var ret = new List<int>();
			ret.Add(nodeWithMaxCount[0].Key);

			var minHeight = BuildTree(nodeWithMaxCount[0].Key, adjacencyList, nodesCount, -1);

			int j = 1;

			while (j < n)
			{
				var newHeight = BuildTree(nodeWithMaxCount[j].Key, adjacencyList, nodesCount, -1);

				if (newHeight < minHeight)
				{
					minHeight = newHeight;
					ret.Clear();
					ret.Add(nodeWithMaxCount[j].Key);
				}
				else if (newHeight == minHeight)
				{
					ret.Add(nodeWithMaxCount[j].Key);
				}
				else
				{
					break;
				}

				j++;
			}


			return ret;
		}

		public int BuildTree(
			int rootNode,
			Dictionary<int, List<int>> adjacencyList,
			Dictionary<int, int> nodesCount,
			int previousNode)
		{
			var adjacent = adjacencyList.GetValueOrDefault(rootNode);
			if (adjacent is null
				|| adjacent.Count == 0)
			{
				return 0;
			}

			int nodeWithMaxCount = -1;
			int maxCount = -1;

			foreach (var node in adjacent)
			{
				if (node == previousNode)
				{
					continue;
				}

				if (nodesCount[node] > maxCount)
				{
					maxCount = nodesCount[node];
					nodeWithMaxCount = node;
				}
			}

			return BuildTree(nodeWithMaxCount, adjacencyList, nodesCount, rootNode) + 1;
		}

		#endregion
	}
}
