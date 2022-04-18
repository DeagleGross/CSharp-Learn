using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

// https://leetcode.com/problems/max-consecutive-ones-ii/

namespace LeetCodeSolutions.RandomTasks
{
	[TestClass]
	public class MaxConsecutiveOnes2
	{
		[TestMethod]
		public void Solve()
		{
			int[] nums = new[] {1, 0, 1, 1, 0};

			var result = FindMaxConsecutiveOnes(nums);

			result.ShouldBe(4);
		}

		[TestMethod]
		public void Solve2()
		{
			int[] nums = new[] { 1, 0, 1, 1, 0, 1 };

			var result = FindMaxConsecutiveOnes(nums);

			result.ShouldBe(4);
		}

		[TestMethod]
		public void Solve3()
		{
			int[] nums = new[] { 0 };

			var result = FindMaxConsecutiveOnes(nums);

			result.ShouldBe(1);
		}

		[TestMethod]
		public void Solve4()
		{
			int[] nums = new[] { 1,1 };

			var result = FindMaxConsecutiveOnes(nums);

			result.ShouldBe(2);
		}

		[TestMethod]
		public void Solve5()
		{
			int[] nums = new[] { 0, 0 };

			var result = FindMaxConsecutiveOnes(nums);

			result.ShouldBe(1);
		}

		public int FindMaxConsecutiveOnes(int[] nums)
		{
			if (nums.Length == 1)
			{
				return 1;
			}

			int flipsSoFar = 0;

			int left = 0;
			int right = 1;

			int flippedZeroPosition = -1;

			int max = 0;

			if (nums[0] == 0)
			{
				flipsSoFar = 1;
				flippedZeroPosition = 0;
				max = 1;
			}

			while (left < nums.Length - 1 && right < nums.Length)
			{
				if (nums[right] == 1)
				{
					max = Math.Max(right - left + 1, max);
					right++;
					continue;
				}

				if (nums[right] == 0)
				{
					if (flipsSoFar < 1)
					{
						flippedZeroPosition = right;

						max = Math.Max(right - left + 1, max);
						flipsSoFar++;
						right++;
					}
					else
					{
						left = flippedZeroPosition + 1;
						
						if (left > right)
						{
							break;
						}

						right--;
						flipsSoFar = 0;

						max = Math.Max(right - left + 1, max);

						right++;
					}
				}
			}

			return max;
		}
	}
}
