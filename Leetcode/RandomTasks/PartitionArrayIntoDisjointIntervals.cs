using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// https://leetcode.com/problems/partition-array-into-disjoint-intervals/

namespace LeetCodeSolutions.RandomTasks
{
	[TestClass]
	public class PartitionArrayIntoDisjointIntervals
	{
		[TestMethod]
		public void Solve()
		{
			int[] nums = new[] {5, 0, 3, 8, 6};

			var result = PartitionDisjoint(nums);

			result.Should().Be(3);
		}

		public int PartitionDisjoint_TwoArrays(int[] nums)
		{
			// we need to check whether the max element in the left part of an array
			// is smaller than the minimum element in the right part

			// stores max element to the left of i (including i itself)
			int[] maxElementToTheLeft = new int[nums.Length];

			// stores min element to the right of i (including i itself)
			int[] minElementToTheRight = new int[nums.Length];

			maxElementToTheLeft[0] = nums[0];
			minElementToTheRight[^1] = nums[^1];

			for (int i = 1; i < nums.Length; i++)
			{
				// current maximum in Max(previous maximum, nums[i])
				maxElementToTheLeft[i] = Math.Max(maxElementToTheLeft[i - 1], nums[i]);
			}

			for (int i = nums.Length - 2; i >= 0; i--)
			{
				// current minimum in Max(previous maximum, nums[i])
				minElementToTheRight[i] = Math.Min(minElementToTheRight[i + 1], nums[i]);
			}

			for (int i = 1; i < nums.Length; i++)
			{
				// here we find element all 
				// left array index is 1 element behind right element index
				if (maxElementToTheLeft[i - 1] <= minElementToTheRight[i])
				{
					return i;
				}
			}

			// In case there is no solution, we'll return -1
			return -1;
		}

		public int PartitionDisjoint(int[] nums)
		{
			// we need to check whether the max element in the left part of an array
			// is smaller than the minimum element in the right part


			// stores min element to the right of i (including i itself)
			int[] minElementToTheRight = new int[nums.Length];
			minElementToTheRight[^1] = nums[^1];
			
			for (int i = nums.Length - 2; i >= 0; i--)
			{
				// current minimum in Max(previous maximum, nums[i])
				minElementToTheRight[i] = Math.Min(minElementToTheRight[i + 1], nums[i]);
			}

			var currentMax = nums[0];
			for (int i = 1; i < nums.Length; i++)
			{
				currentMax = Math.Max(currentMax, nums[i-1]);

				// here we find element all 
				if (currentMax <= minElementToTheRight[i])
				{
					return i;
				}
			}

			// In case there is no solution, we'll return -1
			return -1;
		}

		public int PartitionDisjoint_Fastest(int[] nums)
		{
			int currMax = nums[0];
			int possibleMax = nums[0];
			int length = 1;

			for (int i = 1; i < nums.Length; ++i)
			{
				if (nums[i] < currMax)
				{
					/*
					If nums[i] is less than curr_max, it means that, currently, not every element in left is 
					less than or equal to every element in right, so our condition is violated. 
					Therefore, we need to extend our left array, so that it includes all the values 
					up to nums[i] (inclusive). We update length accordingly and set curr_max equal to 
					possible_max because now we know possible_max must be part of the left subarray. 
					We must now compare every subsequent element starting from nums[i + 1] with the 
					maximum value seen so far.*/

					length = i + 1;
					currMax = possibleMax;
				}
				else
				{
					possibleMax = Math.Max(possibleMax, nums[i]);
				}
			}

			return length;
		}
	}
}
