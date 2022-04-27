using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace LeetCodeSolutions.RandomTasks
{
	[TestClass]
	public class CourseSchedule2
	{
		[TestMethod]
		public void Solve()
		{
			var numCourses = 2;
			int[][] prerequisites = new int[][]
			{
				new int[]{1,0}
			};

			var result = FindOrder(numCourses, prerequisites);

			result.ShouldBe(new []{0,1});
		}

		[TestMethod]
		public void Solve2()
		{
			var numCourses = 4;
			int[][] prerequisites = new int[][]
			{
				new int[]{1,0},
				new int[]{2,0},
				new int[]{3,1},
				new int[]{3,2},
			};

			var result = FindOrder(numCourses, prerequisites);

			result.ShouldBe(new[] { 0, 1, 2, 3 });
		}

		[TestMethod]
		public void Solve3()
		{
			var numCourses = 2;
			int[][] prerequisites = new int[][]
			{
				new int[]{0,1},
				new int[]{1,0},
			};

			var result = FindOrder(numCourses, prerequisites);

			result.Length.ShouldBe(0);
		}

		[TestMethod]
		public void Solve4()
		{
			var numCourses = 3;
			int[][] prerequisites = new int[][]
			{
				new int[]{1,0},
				new int[]{0,2},
				new int[]{2,1},
			};

			var result = FindOrder(numCourses, prerequisites);

			result.Length.ShouldBe(0);
		}
		
		[TestMethod]
		public void Solve5()
		{
			var numCourses = 3;
			int[][] prerequisites = new int[][]
			{
				new int[]{0,1},
				new int[]{0,2},
				new int[]{1,0},
			};

			var result = FindOrder(numCourses, prerequisites);

			result.Length.ShouldBe(0);
		}
		
		[TestMethod]
		public void Solve6()
		{
			var numCourses = 4;
			int[][] prerequisites = new int[][]
			{
				new int[]{3,0},
				new int[]{0,1},
			};

			var result = FindOrder(numCourses, prerequisites);

			result.ShouldBe(new[] {1, 2, 0, 3});
		}

		public int[] FindOrder(int numCourses, int[][] prerequisites)
		{
			// this is actually an adjacency list
			Dictionary<int, List<int>> coursesByPrereqs = new Dictionary<int, List<int>>();

			int[] indegree = new int[numCourses];

			for (int i = 0; i < prerequisites.Length; i++)
			{
				var course = prerequisites[i][0];
				var prereq = prerequisites[i][1];

				if (!coursesByPrereqs.ContainsKey(prereq))
				{
					coursesByPrereqs[prereq] = new List<int>();
				}

				coursesByPrereqs[prereq].Add(course);

				indegree[course] += 1;
			}

			Queue<int> nodesWithIndegree0 = new();

			for (int i = 0; i < indegree.Length; i++)
			{
				if (indegree[i] == 0)
				{
					nodesWithIndegree0.Enqueue(i);
				}
			}

			List<int> ret = new(); // topological sort results

			while (nodesWithIndegree0.Count > 0)
			{
				var currentNode = nodesWithIndegree0.Dequeue();

				ret.Add(currentNode);

				var adjacentNodes = coursesByPrereqs.ContainsKey(currentNode)
					? coursesByPrereqs[currentNode]
					: Enumerable.Empty<int>();

				foreach (var node in adjacentNodes)
				{
					indegree[node] -= 1;
					if (indegree[node] == 0)
					{
						nodesWithIndegree0.Enqueue(node);
					}
				}
			}

			if (ret.Count < numCourses)
			{
				return Array.Empty<int>();
			}

			return ret.Take(numCourses).ToArray();
		}

		#region Time limit exceeded

		public int[] FindOrder_Suboptimal1(int numCourses, int[][] prerequisites)
		{
			Dictionary<int, List<int>> prereqsByCourses = new Dictionary<int, List<int>>();

			for (int i = 0; i < prerequisites.Length; i++)
			{
				var prereq = prerequisites[i][1];
				var course = prerequisites[i][0];

				if (!prereqsByCourses.ContainsKey(course))
				{
					prereqsByCourses[course] = new List<int>();
				}

				prereqsByCourses[course].Add(prereq);
			}

			var ret = new HashSet<int>();
			var prev = new HashSet<int>();

			TakeCourse(0, prereqsByCourses, ret, numCourses, prev);

			if (ret.Count != numCourses)
			{
				return Array.Empty<int>();
			}

			return ret.ToArray();
		}

		private bool TakeCourse(int course, Dictionary<int, List<int>> prereqsByCourses, HashSet<int> output, int numCourses, HashSet<int> previousCourses)
		{
			if (course >= numCourses)
			{
				return false;
			}

			if (!prereqsByCourses.ContainsKey(course))
			{
				// means no prereqs

				output.Add(course);
				if (previousCourses.Count == 0)
				{
					TakeCourse(course + 1, prereqsByCourses, output, numCourses, previousCourses);
				}

				return true;
			}
			else
			{
				bool canTakeCourse = true;
				foreach (var prereqCourse in prereqsByCourses[course])
				{
					if (previousCourses.Contains(prereqCourse))
					{
						return false;
					}

					if (!output.Contains(prereqCourse))
					{
						previousCourses.Add(course);

						bool courseTaken = TakeCourse(prereqCourse, prereqsByCourses, output, numCourses, previousCourses);

						previousCourses.Remove(course);

						canTakeCourse = canTakeCourse && courseTaken;
					}
				}

				if (canTakeCourse)
				{
					output.Add(course);
					TakeCourse(course + 1, prereqsByCourses, output, numCourses, previousCourses);
				}
				else
				{
					return false;
				}
			}

			return true;
		}

		#endregion
	}
}
