using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

// https://leetcode.com/problems/jump-game-ii/

namespace LeetCodeSolutions.RandomTasks.DynamicProgramming
{
	[TestClass]
	public class JumpGameTwo
	{
		[TestMethod]
		public void Solve()
		{
			int[] nums = new[] { 2, 3, 1, 1, 4 };

			var result = Jump(nums);

			result.ShouldBe(2);
		}

		[TestMethod]
		public void Solve2()
		{
			int[] nums = new[] { 2, 3, 0, 1, 4 };

			var result = Jump(nums);

			result.ShouldBe(2);
		}

		[TestMethod]
		public void Solve3()
		{
			int[] nums = new[] { 5, 9, 3, 2, 1, 0, 2, 3, 3, 1, 0, 0 };

			var result = Jump(nums);

			result.ShouldBe(3);
		}

		public int Jump(int[] nums)
		{
			int[] stepsToReachEnd = new int[nums.Length];
			stepsToReachEnd[nums.Length - 1] = 0; // add last element

			for (int i = nums.Length - 2; i >= 0; i--) // start from one element before the end and check whether we can reach end
			{
				if (nums[i] == 0)
				{
					// we use int.MaxValue to prevent from integer overflow
					// on +1 operation resulting in int.MinValue which will break algorithm
					stepsToReachEnd[i] = int.MaxValue; 
					continue;
				}

				var targetCellIndex = i + nums[i];

				if (targetCellIndex > nums.Length - 1) // we are past the end of an array
				{
					stepsToReachEnd[i] = 1;
					continue;
				}

				while (targetCellIndex > i)
				{
					var nextStepsToReachEnd = stepsToReachEnd[targetCellIndex];

					if (stepsToReachEnd[i] == 0)
					{
						stepsToReachEnd[i] = nextStepsToReachEnd == int.MaxValue
							? int.MaxValue
							: nextStepsToReachEnd + 1;
					}
					else
					{
						stepsToReachEnd[i] = Math.Min(
							stepsToReachEnd[i],
							nextStepsToReachEnd == int.MaxValue
								? int.MaxValue
								: nextStepsToReachEnd + 1);
					}

					targetCellIndex--;
				}
			}

			return stepsToReachEnd[0];
		}
	}
}
