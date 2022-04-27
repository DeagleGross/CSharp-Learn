using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

// https://leetcode.com/problems/two-sum-ii-input-array-is-sorted/

namespace LeetCodeSolutions.RandomTasks
{
	[TestClass]
	public class TwoSum2InputArrayIsSorted
	{
		[TestMethod]
		public void Solve()
		{
			int[] nums = new int[] { 2, 7, 11, 15 };
			var target = 9;

			var result = TwoSum(nums, target);

			result.ShouldBe(new int[] { 1, 2 }); // 1 based array
		}

		// This solution uses the fact that the array is sorted but it is slower than hashset-based one
		public int[] TwoSum(int[] numbers, int target)
		{
			var left = 0;
			var right = numbers.Length - 1;

			while (left < right)
			{
				var sum = numbers[left] + numbers[right];

				if (sum == target)
				{
					return new[] {left + 1, right + 1};
				}

				if (sum > target)
				{
					right--;
					continue;
				}

				if (sum < target)
				{
					left++;
					continue;
				}
			}

			return Array.Empty<int>();
		}

		public int[] TwoSum_DefaultSolution(int[] numbers, int target)
		{
			Dictionary<int, List<int>> candidates = new();
			for (int i = 0; i < numbers.Length; i++)
			{
				if (!candidates.ContainsKey(numbers[i]))
				{
					candidates.Add(numbers[i], new List<int>());
				}

				candidates[numbers[i]].Add(i);
			}

			for (int i = 0; i < numbers.Length; i++)
			{
				if (candidates.ContainsKey(target - numbers[i]))
				{
					var secondIndex = candidates[target - numbers[i]].FirstOrDefault(e => e != i, Int32.MinValue);
					if (secondIndex == Int32.MinValue)
					{
						continue;
					}

					return new[] { i+1, secondIndex+1 };
				}
			}

			return Array.Empty<int>();
		}
	}
}
