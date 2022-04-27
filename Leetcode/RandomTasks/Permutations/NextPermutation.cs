using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

// https://leetcode.com/problems/next-permutation/

namespace LeetCodeSolutions.RandomTasks
{
	[TestClass]
	public class NextPermutationTask
	{
		[TestMethod]
		public void Solve()
		{
			int[] nums = new int[] { 1, 2, 3 };

			NextPermutation(nums);

			var result = "[" + string.Join(",", nums) + "]";

			result.ShouldBe("[1,3,2]");
		}
		
		public void NextPermutation(int[] nums)
		{
			int i = nums.Length - 2; // start from second element from the end for the following to work

			while (i >= 0 && nums[i + 1] <= nums[i])
			{
				i--;
			}

			if (i >= 0)
			{
				int j = nums.Length - 1; // first element from the end

				while (nums[j] <= nums[i])
				{
					j--;
				}

				Swap(nums, i, j);
			}

			//Reverse(nums, i + 1); - or just use the inbuilt Array.Reverse

			Array.Reverse(nums, i+1, nums.Length - (i+1));
		}

		private void Reverse(int[] nums, int start)
		{
			int i = start;
			int j = nums.Length - 1;

			while (i < j)
			{
				Swap(nums, i, j);
				i++;
				j--;
			}
		}

		private IList<T> Swap<T>(IList<T> list, int indexA, int indexB)
		{
			(list[indexA], list[indexB]) = (list[indexB], list[indexA]);
			return list;
		}
	}
}
