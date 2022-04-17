using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

// https://leetcode.com/problems/minimum-number-of-arrows-to-burst-balloons/

namespace LeetCodeSolutions.RandomTasks
{
	[TestClass]
	public class MinimumNumberOfArrowsToBurstBalloons
	{
		[TestMethod]
		public void Solve()
		{
			int[][] input = new int[][]
			{
				new []{ 10, 16 },
				new []{ 2, 8 },
				new []{ 1, 6 },
				new []{ 7, 12 }
			};

			var result = FindMinArrowShots(input);

			result.ShouldBe(2);
		}

		[TestMethod]
		public void Solve2()
		{
			int[][] input = new int[][]
			{
				new []{ 1, 2 },
				new []{ 3, 4 },
				new []{ 5, 6 },
				new []{ 7, 8 }
			};

			var result = FindMinArrowShots(input);

			result.ShouldBe(4);
		}

		[TestMethod]
		public void Solve3()
		{
			int[][] input = new int[][]
			{
				new[]{ 0, 9 },
				new []{ 1, 8 },
				new []{ 7, 8 },
				new []{ 1, 6 },
				new []{ 9, 16 },
				new []{ 7, 13 },
				new []{ 7, 10 },
				new []{ 6, 11 },
				new []{ 6, 9 },
				new []{ 9, 13 }
			};

			var result = FindMinArrowShots(input);

			result.ShouldBe(3);
		}
		
		[TestMethod]
		public void Solve4()
		{
			int[][] input = new int[][]
			{
				new[]{ 1, 2 },
				new []{ 2, 3 },
				new []{ 3, 4 },
				new []{ 4, 5 }
			};

			var result = FindMinArrowShots(input);

			result.ShouldBe(2);
		}

		[TestMethod]
		public void Solve5()
		{

			int[][] input = new int[][]
			{
				new[]{ 4, 12 },
				new []{ 7, 8 },
				new []{ 7, 9 },
				new []{ 7, 9 },
				new []{ 2, 8 },
				new []{ 6, 7 },
				new []{ 5, 14 },
				new []{ 4, 13 }
			};

			var result = FindMinArrowShots(input);

			result.ShouldBe(1);
		}

		public int FindMinArrowShots(int[][] points)
		{
			List<int> starts = new();
			List<int> ends = new();

			for (int i = 0; i < points.Length; i++)
			{
				starts.Add(points[i][0]);

				ends.Add(points[i][1]);
			}

			starts.Sort();
			ends.Sort();

			var start = 0;
			var end = 0;

			int arrows = 0;

			var overlappedBaloons = 0;

			while (start < starts.Count)
			{
				var currentStart = starts[start];
				var currentEnd = ends[end];

				if (currentStart < currentEnd)
				{
					overlappedBaloons++;
					start++;
				}
				else if (currentStart == currentEnd)
				{
					arrows++;

					start++;
					end += overlappedBaloons+1; // we don't need to process destroyed baloons ends
					overlappedBaloons = 0;
				}
				else
				{
					
					arrows++;
					end += overlappedBaloons; // we don't need to process destroyed baloons ends
					overlappedBaloons = 0;
					
				}
			}

			if (overlappedBaloons > 0)
			{
				arrows++;
			}

			return arrows;
		}
	}
}
