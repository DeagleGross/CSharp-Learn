using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

// https://leetcode.com/problems/course-schedule/

namespace LeetCodeSolutions.RandomTasks.GraphTheory
{
	[TestClass]
	public class CourseSchedule
	{
		[TestMethod]
		public void Solve()
		{
			var numCourses = 2;

			int[][] prerequisites = new int[][]
			{
				new int[]{1,0}
			};

			var output = CanFinish(numCourses, prerequisites);

			output.ShouldBe(true);
		}

		[TestMethod]
		public void Solve2()
		{
			var numCourses = 2;

			int[][] prerequisites = new int[][]
			{
				new int[]{1,0},
				new int[]{0,1},
			};

			var output = CanFinish(numCourses, prerequisites);

			output.ShouldBe(false);
		}

		[TestMethod]
		public void Solve3()
		{
			var numCourses = 2;

			int[][] prerequisites = new int[][]
			{
				new int[]{0,1}
			};

			var output = CanFinish(numCourses, prerequisites);

			output.ShouldBe(true);
		}

		public bool CanFinish(int numCourses, int[][] prerequisites)
		{
			Dictionary<int, List<int>> adjacencyList = new Dictionary<int, List<int>>();
			int[] indegree = new int[numCourses];

			for (int i = 0; i < prerequisites.Length; i++)
			{
				var source = prerequisites[i][1];
				var target = prerequisites[i][0];

				if (!adjacencyList.ContainsKey(source))
				{
					adjacencyList[source] = new List<int>();
				}

				adjacencyList[source].Add(target);

				indegree[target] += 1;
			}

			Queue<int> nodesWithIndegree0 = new Queue<int>();
			for (int i = 0; i < indegree.Length; i++)
			{
				if (indegree[i] == 0)
				{
					nodesWithIndegree0.Enqueue(i);
				}
			}

			int canFinishCount = 0;

			while (nodesWithIndegree0.Count > 0)
			{
				var node = nodesWithIndegree0.Dequeue();
				canFinishCount++;

				var adjacentNodes = adjacencyList.ContainsKey(node) ? adjacencyList[node] : Enumerable.Empty<int>();

				foreach (var adjNode in adjacentNodes)
				{
					indegree[adjNode] -= 1;
					if (indegree[adjNode] == 0)
					{
						nodesWithIndegree0.Enqueue(adjNode);
					}
				}
			}

			return canFinishCount == numCourses;
		}
	}
}
