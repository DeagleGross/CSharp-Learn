using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

// https://leetcode.com/problems/two-sum/

namespace LeetCodeSolutions.RandomTasks
{
	[TestClass]
	public class TwoSum1
	{
		[TestMethod]
		public void Solve()
		{
			int[] nums = new int[] {2, 7, 11, 15};
			var target = 9;

			var result = TwoSum(nums, target);

			result.ShouldBe(new int[] {0, 1});
		}

		[TestMethod]
		public void Solve2()
		{
			int[] nums = new int[] { 3,2,4 };
			var target = 6;

			var result = TwoSum(nums, target);

			result.ShouldBe(new int[] { 1, 2 });
		}

		[TestMethod]
		public void Solve3()
		{
			int[] nums = new int[] { 3, 3 };
			var target = 6;

			var result = TwoSum(nums, target);

			result.ShouldBe(new int[] { 0, 1 });
		}


		// we can transform this into one-pass solution by checking whether a compliment exists in the first
		// loop where we are adding values to the dicitonary
		public int[] TwoSum(int[] nums, int target)
		{
			var numbers = new Dictionary<int, HashSet<int>>();
			for (int i = 0; i < nums.Length; i++)
			{
				if (!numbers.ContainsKey(nums[i]))
				{
					numbers[nums[i]] = new HashSet<int>();
				}

				numbers[nums[i]].Add(i);
			}

			for (int i = 0; i < nums.Length; i++)
			{
				if (numbers.ContainsKey(target - nums[i]))
				{
					var secondIndex = numbers[target - nums[i]].FirstOrDefault(e => e != i, Int32.MinValue);
					if (secondIndex == Int32.MinValue)
					{
						continue;
					}

					return new[] {i, secondIndex};
				}
			}

			return Array.Empty<int>();
		}

		public int[] TwoSum_BruteForce(int[] nums, int target)
		{
			for (int i = 0; i < nums.Length; i++)
			{
				for (int j = 1; j < nums.Length; j++)
				{
					if (i == j)
					{
						continue;
					}

					if (nums[i] + nums[j] == target)
					{
						return new int[] {i, j};
					}
				}
			}

			return Array.Empty<int>();
		}
	}
}
