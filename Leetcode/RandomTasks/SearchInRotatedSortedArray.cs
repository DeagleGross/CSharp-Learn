using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

// https://leetcode.com/problems/search-in-rotated-sorted-array/

namespace LeetCodeSolutions.RandomTasks
{
	[TestClass]
	public class SearchInRotatedSortedArray
	{
		[TestMethod]
		public void Solve()
		{
			int[] nums = new[] {4, 5, 6, 7, 0, 1, 2};
			var traget = 0;

			var result = Search(nums, traget);

			result.ShouldBe(4);
		}

		[TestMethod]
		public void Solve2()
		{
			int[] nums = new[] { 4, 5, 6, 7, 0, 1, 2 };
			var traget = 1;

			var result = Search(nums, traget);

			result.ShouldBe(5);
		}

		[TestMethod]
		public void Solve3()
		{
			int[] nums = new[] { 1,3 };
			var traget = 0;

			var result = Search(nums, traget);

			result.ShouldBe(-1);
		}

		public int Search(int[] nums, int target)
		{
			if (nums.Length == 1)
			{
				return nums[0] == target
					? 0
					: -1;
			}

			var pivotPoint = -1;

			for (int i = 1; i < nums.Length; i++)
			{
				if (nums[i] < nums[i - 1])
				{
					pivotPoint = i;
					break;
				}
			}

			if (pivotPoint == -1)
			{
				// array was not pivoted

				var elementPosition = Array.BinarySearch(nums, 0, nums.Length, target);
				if (elementPosition < 0)
				{
					return -1;
				}

				return elementPosition;
			}

			if (nums[pivotPoint] == target)
			{
				return pivotPoint;
			}

			if (target >= nums[0] && target <= nums[pivotPoint-1])
			{
				// means we are in left part of an array (before pivot)
				var elementPosition = Array.BinarySearch(nums, 0, pivotPoint + 1, target);

				if (elementPosition < 0)
				{
					return -1;
				}

				return elementPosition;
			}
			else
			{
				// means we are in right part of an array (after pivot)
				var elementPosition = Array.BinarySearch(nums, pivotPoint, nums.Length - pivotPoint, target);
				
				if (elementPosition < 0)
				{
					return -1;
				}

				return elementPosition;
			}
		}
	}
}
