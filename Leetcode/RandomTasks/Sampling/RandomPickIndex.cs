using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

// https://leetcode.com/problems/random-pick-index/

namespace LeetCodeSolutions.RandomTasks.Sampling
{
	[TestClass]
	public class RandomPickIndex
	{
		[TestMethod]
		public void Solve()
		{
			Solution solution = new Solution(new int[]{1, 2, 3, 3, 3});
			var t1 = solution.Pick(3); // It should return either index 2, 3, or 4 randomly. Each index should have equal probability of returning.
			(t1 == 2 || t1 == 3 || t1 == 4).ShouldBe(true);

			var t2 = solution.Pick(1); // It should return 0. Since in the array only nums[0] is equal to 1.
			t2.ShouldBe(0);

			var t3 = solution.Pick(3); // It should return either index 2, 3, or 4 randomly. Each index should have equal probability of returning.
			(t3 == 2 || t3 == 3 || t3 == 4).ShouldBe(true);
		}

		public class Solution
		{
			private readonly Dictionary<int, List<int>> _indexesByNumbers = new Dictionary<int, List<int>>();
			private readonly Random _rnd = new Random(DateTime.Now.Millisecond);
			private int[] _nums;

			public Solution(int[] nums)
			{
				_nums = nums;

				for (int i = 0; i < nums.Length; i++)
				{
					if (!_indexesByNumbers.ContainsKey(nums[i]))
					{
						_indexesByNumbers[nums[i]] = new();
					}

					_indexesByNumbers[nums[i]].Add(i);
				}
			}

			public int Pick(int target)
			{
				var indexes = _indexesByNumbers[target];

				if (indexes.Count == 1)
				{
					return indexes[0];
				}

				var randomIndexIndex = _rnd.Next(0, indexes.Count);

				return indexes[randomIndexIndex];
			}

			// This one is from the solution. It optimizes the total space requirement ot the solution
			// But, oddly enough, this one does not get past Time Limit ))
			public int Pick_ReservoirSampling(int target)
			{
				int count = 0;
				int idx = 0;
				for (int i = 0; i < _nums.Length; i++)
				{
					// if nums[i] is equal to target, i is a potential candidate
					// which needs to be chosen uniformly at random
					if (_nums[i] == target)
					{
						// increment the count of total candidates
						// available to be chosen uniformly at random
						count++;

						// we pick the current number with probability 1 / count (reservoir sampling)
						if (_rnd.Next(count) == 0)
						{
							idx = i;
						}
					}
				}
				return idx;
			}
		}
	}
	
}
