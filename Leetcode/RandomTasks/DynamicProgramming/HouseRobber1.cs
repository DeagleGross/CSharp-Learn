using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

// https://leetcode.com/problems/house-robber/

namespace LeetCodeSolutions.RandomTasks.DynamicProgramming
{
	[TestClass]
	public class HouseRobber1
	{
		[TestMethod]
		public void Solve()
		{
			int[] nums = new[] {1, 2, 3, 1};
			var result = Rob(nums);

			result.ShouldBe(4);
		}

		[TestMethod]
		public void Solve2()
		{
			int[] nums = new[] { 2, 7, 9, 3, 1 };
			var result = Rob(nums);

			result.ShouldBe(12);
		}

		[TestMethod]
		public void Solve3()
		{
			int[] nums = new[] { 2, 1, 1, 2 };
			var result = Rob(nums);

			result.ShouldBe(4);
		}

		private Dictionary<int, int> _robbedFromMemo = new();

		public int Rob(int[] nums)
		{
			var result = RobFrom(nums, 0);
			return result;
		}

		public int RobFrom(int[] nums, int position)
		{
			if (position >= nums.Length)
			{
				return 0;
			}

			// if we don't rob this house then we simply rob the next one
			var robFromNextHouse = _robbedFromMemo.ContainsKey(position + 1)
				? _robbedFromMemo[position + 1]
				: RobFrom(nums, position + 1);

			// this is always filled in so we don't bother with sentinel values
			_robbedFromMemo[position + 1] = robFromNextHouse;


			// if we rob this house then we can only rob the one after the next one thus +2
			var robFromHouseNextToNext = _robbedFromMemo.ContainsKey(position + 2)
				? _robbedFromMemo[position + 2]
				: RobFrom(nums, position + 2);
			_robbedFromMemo[position + 2] = robFromNextHouse;

			var robFromThisHouse = nums[position] + robFromHouseNextToNext;

			return Math.Max(robFromThisHouse, robFromNextHouse);
		}
	}
}
