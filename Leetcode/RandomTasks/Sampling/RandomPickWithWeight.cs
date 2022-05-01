using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

// https://leetcode.com/problems/random-pick-with-weight/

namespace LeetCodeSolutions.RandomTasks.Sampling
{
	[TestClass]
	public class RandomPickWithWeight
	{
		[TestMethod]
		public void Solve()
		{
			Solution solution = new Solution(new[]{1});
			var index = solution.PickIndex();

			index.ShouldBe(0);
		}

		[TestMethod]
		public void Solve2()
		{
			Solution solution = new Solution(new[] { 1, 3 });
			var index = solution.PickIndex();

			index.ShouldBe(1);
		}

		[TestMethod]
		public void Solve3()
		{
			Solution solution = new Solution(new[] { 3, 14, 1, 7 });
			var index = solution.PickIndex();

			index.ShouldBe(14);
		}

		public class Solution
		{
			private readonly int[] _prefixSums;
			private readonly int _maxSum;
			private readonly int _minSum;
			private readonly Random _rnd = new Random(DateTime.UtcNow.Millisecond);

			public Solution(int[] w)
			{
				_prefixSums = new int[w.Length];

				_minSum = w[0];

				var prefixSum = 0;
				for (int i = 0; i < w.Length; i++)
				{
					prefixSum += w[i];
					_prefixSums[i] = prefixSum;
				}

				_maxSum = prefixSum;
			}

			public int PickIndex()
			{
				// get random double and scale it up to the desired value
				var randomNumber = _rnd.NextDouble() * _maxSum;

				// somehow this does not get get accepted
				//var randomNumber = _rnd.Next(_minSum, _maxSum+1);

				// binary search but by hand
				
				var high = _prefixSums.Length-1; 
				var low = 0;

				while (low < high)
				{
					var mid = low + (high - low) / 2;

					if (randomNumber > _prefixSums[mid])
					{
						low = mid + 1;
					}
					else
					{
						high = mid;
					}
				}

				return low;
			}
		}
	}
}
