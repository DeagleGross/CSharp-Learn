using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

// https://leetcode.com/problems/maximal-network-rank/

namespace LeetCodeSolutions.RandomTasks.GraphTheory
{
	[TestClass]
	public class MaximalNetworkRank1
	{
		[TestMethod]
		public void Solve()
		{
			int[][] roads = new int[][]
			{
				new int[]{0,1},
				new int[]{0,3},
				new int[]{1,2},
				new int[]{1,3},
			};

			int n = 4;

			var result = MaximalNetworkRank(n, roads);

			result.ShouldBe(4);
		}

		[TestMethod]
		public void Solve2()
		{
			int[][] roads = new int[][]
			{
				new int[]{0,1},
				new int[]{1,2},
				new int[]{2,3},
				new int[]{2,4},
				new int[]{5,6},
				new int[]{5,7},
			};

			int n = 8;

			var result = MaximalNetworkRank(n, roads);

			result.ShouldBe(5);
		}

		[TestMethod]
		public void Solve3()
		{
			// two distince cities
			int[][] roads = new int[][] { };

			int n = 2;

			var result = MaximalNetworkRank(n, roads);

			result.ShouldBe(0);
		}

		[TestMethod]
		public void Solve4()
		{
			// two distince cities
			int[][] roads = new int[][] { new []{1,0}};

			int n = 2;

			var result = MaximalNetworkRank(n, roads);

			result.ShouldBe(1);
		}

		[TestMethod]
		public void Solve5()
		{
			// inly two cities are connected, others are separate
			int[][] roads = new int[][] { new[] { 2, 4 } };

			int n = 6;

			var result = MaximalNetworkRank(n, roads);

			result.ShouldBe(1);
		}

		[TestMethod]
		public void Solve6()
		{
			int[][] roads = new int[][]
			{
				new int[]{8,12},
				new int[]{5,11},
				new int[]{5,12},
				new int[]{9,4},
				new int[]{0,9},
				new int[]{1,8},
				new int[]{10,2},
				new int[]{13,14},
				new int[]{3,4},
				new int[]{11,3},
				new int[]{11,8},
				new int[]{5,10},
			};

			int n = 15;

			var result = MaximalNetworkRank(n, roads);

			result.ShouldBe(6);
		}

		public int MaximalNetworkRank(int n, int[][] roads)
		{
			if (roads.Length == 0)
			{
				return 0;
			}

			Dictionary<int, HashSet<int>> adjacencyList = new ();
			Dictionary<int, int> indegree = new();

			for (int i = 0; i < n; i++)
			{
				indegree[i] = 0;
			}

			var maxIndegree = -1;

			HashSet<int> nodesWithMaxIndegrees = new HashSet<int>();

			for (int i = 0; i < roads.Length; i++)
			{
				var source = roads[i][1];
				var target = roads[i][0];

				if (!adjacencyList.ContainsKey(source))
				{
					adjacencyList[source] = new();
				}

				if (!adjacencyList.ContainsKey(source))
				{
					adjacencyList[source] = new();
				}

				if (!adjacencyList.ContainsKey(target))
				{
					adjacencyList[target] = new();
				}

				adjacencyList[source].Add(target);
				adjacencyList[target].Add(source);

				indegree[target] += 1;
				indegree[source] += 1;

				if (indegree[target] > maxIndegree)
				{
					maxIndegree = indegree[target];
					nodesWithMaxIndegrees.Clear();
					nodesWithMaxIndegrees.Add(target);
				}

				if (indegree[target] == maxIndegree)
				{
					nodesWithMaxIndegrees.Add(target);
				}

				if (indegree[source] > maxIndegree)
				{
					maxIndegree = indegree[source];
					nodesWithMaxIndegrees.Clear();
					nodesWithMaxIndegrees.Add(source);
				}

				if (indegree[source] == maxIndegree)
				{
					nodesWithMaxIndegrees.Add(source);
				}
			}

			
			int maxRank = 0;

			foreach (var nodeWithMaxIndegree in nodesWithMaxIndegrees)
			{
				var initialRank = adjacencyList[nodeWithMaxIndegree].Count;

				foreach (var kv in indegree.OrderByDescending(kv => kv.Value))
				{
					var node = kv.Key;
					if (node == nodeWithMaxIndegree)
					{
						continue;
					}

					var currentPairRank = initialRank + kv.Value;

					if (adjacencyList[nodeWithMaxIndegree].Contains(node)
						|| (adjacencyList.ContainsKey(node) && adjacencyList[node].Contains(nodeWithMaxIndegree)))
					{
						currentPairRank--;
					}

					maxRank = Math.Max(maxRank, currentPairRank);
				}
			}

			return maxRank;
		}
	}
}
