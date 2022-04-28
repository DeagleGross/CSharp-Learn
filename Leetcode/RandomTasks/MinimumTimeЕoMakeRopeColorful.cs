using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

// https://leetcode.com/problems/minimum-time-to-make-rope-colorful/

namespace LeetCodeSolutions.RandomTasks
{
	[TestClass]
	public class MinimumTimeЕoMakeRopeColorful
	{
		[TestMethod]
		public void Solve()
		{
			string colors = "abaac";
			int[] neededTime = new[] {1, 2, 3, 4, 5};

			var result = MinCost(colors, neededTime);

			result.ShouldBe(3);
		}

		[TestMethod]
		public void Solve2()
		{
			string colors = "abc";
			int[] neededTime = new[] { 1, 2, 3 };

			var result = MinCost(colors, neededTime);

			result.ShouldBe(0);
		}

		[TestMethod]
		public void Solve3()
		{
			string colors = "aabaa";
			int[] neededTime = new[] { 1, 2, 3, 4, 1 };

			var result = MinCost(colors, neededTime);

			result.ShouldBe(2);
		}

		[TestMethod]
		public void Solve4()
		{
			string colors = "aaabbbabbbb";
			int[] neededTime = new[] { 3, 5, 10, 7, 5, 3, 5, 5, 4, 8, 1 };

			var result = MinCost(colors, neededTime);

			result.ShouldBe(26);
		}
		
		[TestMethod]
		public void Solve5()
		{
			string colors = "cddcdcae";
			int[] neededTime = new[] { 4, 8, 8, 4, 4, 5, 4, 2 };

			var result = MinCost(colors, neededTime);

			result.ShouldBe(8);
		}

		public int MinCost(string colors, int[] neededTime)
		{
			int minTime = 0;

			for (int i = 1; i < colors.Length; i++)
			{
				if (colors[i] == colors[i - 1])
				{
					var c = colors[i];
					var j = i+1;

					var set = new List<int>();
					set.Add(neededTime[i]);
					set.Add(neededTime[i-1]);

					while (j < colors.Length && colors[j] == c)
					{
						set.Add(neededTime[j]);
						j++;
					}

					set.Sort();

					var minTimes = set.Take(set.Count - 1);

					minTime += minTimes.Sum();

					if (set.Count > 2)
					{
						i = j;
					}
				}

			}

			return minTime;
		}
	}
}
