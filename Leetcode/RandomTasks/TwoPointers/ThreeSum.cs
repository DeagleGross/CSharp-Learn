using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

// https://leetcode.com/problems/3sum/

namespace LeetCodeSolutions.RandomTasks
{
	[TestClass]
	public class ThreeSum1
	{
		[TestMethod]
		public void Solve()
		{
			int[] nums = new[] {-1, 0, 1, 2, -1, -4};

			var result = ThreeSum(nums);

			result.Count.ShouldBe(2);
		}

		[TestMethod]
		public void Solve2()
		{
			int[] nums = new[] {-1, 0, 1, 2, -1, -4};

			var result = ThreeSum(nums);

			result.Count.ShouldBe(2);
		}

		[TestMethod]
		public void Solve3()
		{
			int[] nums = new[] {-2, 0, 0, 2, 2};

			var result = ThreeSum(nums);

			result.Count.ShouldBe(1);
		}

		[TestMethod]
		public void Solve4()
		{
			int[] nums = new[] { -1, 0, 1, 2, -1, -4};

			var result = ThreeSum(nums);

			result.Count.ShouldBe(2);
		}

		public IList<IList<int>> ThreeSum(int[] nums)
		{
			if (nums.Length < 3)
			{
				return new List<IList<int>>();
			}

			if (nums.Length == 3)
			{
				if (nums.Sum() == 0)
				{
					return new List<IList<int>>()
					{
						nums
					};
				}

				return new List<IList<int>>();
			}

			var sorted = nums.OrderBy(x => x).ToArray();

			IList<IList<int>> ret = new List<IList<int>>();

			// since we sorted the input array - ramining values can't sum to zero
			// if pivot one is greater than zero
			for (int i = 0; i < sorted.Length && sorted[i] <= 0; i++)
			{
				var pivotElement = sorted[i];

				if (i != 0
					&& sorted[i - 1] == pivotElement)
				{
					// skip duplicate values
					continue;
				}

				var left = i + 1;
				var right = sorted.Length - 1;

				while (left < right)
				{
					var sum = sorted[i] + sorted[left] + sorted[right];

					if (sum == 0)
					{
						ret.Add(new List<int>() {sorted[i], sorted[left], sorted[right]});

						left++;
						right--;

						// skip duplicates
						while (left < right && sorted[left] == sorted[left - 1])
						{
							left++;
						}

						continue;
					}

					if (sum > 0)
					{
						right--;
						continue;
					}

					if (sum < 0)
					{
						left++;
						continue;
					}
				}
			}

			return ret;
		}

		#region Backtracking solution (suboptimal)

		[TestMethod]
		public void Solve1_Backtrack()
		{
			int[] nums = new[] {-1, 0, 1, 2, -1, -4};

			var result = ThreeSum_Backtrack(nums);

			result.Count.ShouldBe(2);
		}

		[TestMethod]
		public void Solve2_Backtrack()
		{
			int[] nums = new[] {0, 0, 0};

			var result = ThreeSum_Backtrack(nums);

			result.Count.ShouldBe(1);
		}

		[TestMethod]
		public void Solve3_Backtrack()
		{
			int[] nums = new[] {-1, 0, 1, 2, -1, -4};

			var result = ThreeSum_Backtrack(nums);

			result.Count.ShouldBe(2);
		}

		[TestMethod]
		public void Solve4_Backtrack()
		{
			int[] nums = new[]
			{
				-7, -10, -1, 3, 0, -7, -9, -1, 10, 8, -6, 4, 14, -8, 9, -15, 0, -4, -5, 9, 11, 3, -5, -8, 2, -6, -14, 7,
				-14, 10, 5, -6, 7, 11, 4, -7, 11, 11, 7, 7, -4, -14, -12, -13, -14, 4, -13, 1, -15, -2, -12, 11, -14,
				-2, 10, 3, -1, 11, -5, 1, -2, 7, 2, -10, -5, -8, -10, 14, 10, 13, -2, -9, 6, -7, -7, 7, 12, -5, -14, 4,
				0, -11, -8, 2, -6, -13, 12, 0, 5, -15, 8, -12, -1, -4, -15, 2, -5, -9, -7, 12, 11, 6, 10, -6, 14, -12,
				9, 3, -10, 10, -8, -2, 6, -9, 7, 7, -7, 4, -8, 5, -4, 8, 0, 3, 11, 0, -10, -9
			};

			var result = ThreeSum_Backtrack(nums);

			result.Count.ShouldBe(107);
		}

		private IList<IList<int>> _output = new List<IList<int>>();
		private HashSet<string> _seenTriplets = new();

		public IList<IList<int>> ThreeSum_Backtrack(int[] nums)
		{
			Backtrack(nums, 0, new List<int>(), new HashSet<int>());

			return _output;
		}

		public void Backtrack(int[] nums, int start, List<int> currentTriplet, HashSet<int> addedIndexes)
		{
			if (currentTriplet.Count == 3
				&& currentTriplet.Sum() == 0)
			{
				// add to output only if triplet was not already been seen
				if (_seenTriplets.Add(string.Join(",", currentTriplet.OrderBy(x => x))))
				{
					_output.Add(currentTriplet.ToList());
				}


				return;
			}

			if (currentTriplet.Count >= 3)
			{
				return;
			}

			for (int i = start; i < nums.Length; i++)
			{
				if (addedIndexes.Add(i))
				{
					// add integer to triplet
					currentTriplet.Add(nums[i]);
					//_seenNumbers.Add(nums[i]);

					// try to build further
					Backtrack(nums, start + 1, currentTriplet, addedIndexes);

					// remove this integer to try another one
					currentTriplet.RemoveAt(currentTriplet.Count - 1);

					//_seenNumbers.Remove(nums[i]);
					addedIndexes.Remove(i);
				}
			}
		}

		#endregion

	}
}
