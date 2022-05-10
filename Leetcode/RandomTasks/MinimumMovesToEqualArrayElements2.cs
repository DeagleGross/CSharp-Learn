using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// https://leetcode.com/problems/minimum-moves-to-equal-array-elements-ii/

namespace LeetCodeSolutions.RandomTasks
{
	[TestClass]
	public class MinimumMovesToEqualArrayElements2
	{
		[TestMethod]
		public void Solve()
		{
			int[] nums = new[] {1, 10, 2 ,9};

			var moves = MinMoves2(nums);

			moves.Should().Be(16);
		}

		[TestMethod]
		public void Solve2()
		{
			int[] nums = new[] { 1, 2, 3 };

			var moves = MinMoves2(nums);

			moves.Should().Be(2);
		}

		public int MinMoves2(int[] nums)
		{
			Array.Sort(nums);
			int sum = 0;

			// we are going to eqalize elements towards median
			var median = nums[nums.Length / 2];

			foreach (int num in nums)
			{
				sum += Math.Abs(median - num);
			}
			return sum;
		}
		
		public int MinMoves2_SecondSolution(int[] nums)
		{
			int l = 0;
			int r = nums.Length - 1; 
			int sum = 0;

			/*
			 Let's look at the maximum(max) and the minimum numbers(min) in the array, 
			which currently lie at its extreme positions. We know, at the end, both these numbers 
			should be equalized to k. For the number max, the number of moves required to do this is given by 
				max−k. 
			Similarly, for the number minminmin, the number of moves is given by 
				k−min
			Thus, the total number of moves for both max and min is given by 
				max−k+(k−min)=max−min      ,which is independent of the number k. 
			Thus, we can continue now, with the next maximum and the next minimum number in the array, 
			until the complete array is exhausted.
			 */

			Array.Sort(nums);
			while (l < r)
			{
				sum += nums[r] - nums[l];
				l++;
				r--;
			}
			return sum;
		}
	}
}
