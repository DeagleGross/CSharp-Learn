using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace LeetCodeSolutions.RandomTasks.DynamicProgramming
{
	[TestClass]
	public class HouseRobber2
	{
		[TestMethod]
		public void Solve()
		{
			int[] nums = new[] { 2, 3, 2 };
			var result = Rob(nums);

			result.ShouldBe(3);
		}

		public int Rob(int[] nums)
		{
			if (nums.Length == 0)
			{
				return 0;
			}

			if (nums.Length == 1)
			{
				return nums[0];
			}

			// we either start from the first house and end with one before the end
			int max1 = RobSimple(nums, 0, nums.Length - 2);

			// or start from second house and end one the last one
			int max2 = RobSimple(nums, 1, nums.Length - 1);

			return Math.Max(max1, max2);
		}

		// That's another dynamic-less solution to the House Robber 1
		public int RobSimple(int[] nums, int start, int end)
		{
			int t1 = 0;
			int t2 = 0;

			for (int i = start; i <= end; i++)
			{
				int temp = t1; 
				int current = nums[i];
				t1 = Math.Max(current + t2, t1);
				t2 = temp; 
			}

			return t1;
		}
	}
}
