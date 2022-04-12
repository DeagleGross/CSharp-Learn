using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

// https://leetcode.com/problems/jump-game/

namespace LeetCodeSolutions.DynamicProgramming
{
	[TestClass]
	public class JumpGame
	{
		[TestMethod]
		public void Solve()
		{
			int[] nums = new[] {2, 3, 1, 1, 4};

			var result = CanJump(nums);

			result.ShouldBe(true);
		}

		[TestMethod]
		public void Solve2()
		{
			int[] nums = new[] { 2, 0 };

			var result = CanJump(nums);

			result.ShouldBe(true);
		}

		[TestMethod]
		public void Solve3()
		{
			int[] nums = new[] { 2, 5, 0, 0 };

			var result = CanJump(nums);

			result.ShouldBe(true);
		}

		public bool CanJump(int[] nums)
		{
			bool[] canReachEnd = new bool[nums.Length];

			canReachEnd[nums.Length - 1] = true; // add last element

			for (int i = nums.Length - 2; i >= 0; i--) // start from one element before the end and check whether we can reach end
			{
				var targetCellIndex = i + nums[i];
				
				if (targetCellIndex > nums.Length - 1) // we are past the end of an array
				{
					canReachEnd[i] = true;
					continue;
				}

				while (targetCellIndex > i)
				{
					if (canReachEnd[targetCellIndex] == true)
					{
						canReachEnd[i] = true;
					}

					targetCellIndex--;
				}
			}

			return canReachEnd[0];
		}
	}
}
