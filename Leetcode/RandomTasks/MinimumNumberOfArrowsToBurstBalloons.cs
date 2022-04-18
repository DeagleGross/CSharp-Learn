using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
			List<Point> baloons = new List<Point>();

			for (int i = 0; i < points.Length; i++)
			{
				baloons.Add(new Point(points[i][0], points[i][1]));
			}

			baloons = baloons.OrderBy(p => p.Y).ToList();

			int arrows = 1;

			var lastBaloonEnd = baloons[0].Y;

			int baloon = 1;

			while (baloon < baloons.Count)
			{
				var currentBaloon = baloons[baloon];
				if (currentBaloon.X > lastBaloonEnd)
				{
					arrows++;
					lastBaloonEnd = currentBaloon.Y;
				}
				else
				{
					baloon++;
				}
			}

			return arrows;
		}
	}
}
