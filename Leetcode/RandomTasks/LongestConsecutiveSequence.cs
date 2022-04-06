using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

//https://leetcode.com/problems/longest-consecutive-sequence/

namespace LeetCodeSolutions.RandomTasks
{
	[TestClass]
	public class LongestConsecutiveSequence
	{
		// You must write an algorithm that runs in O(n) time.

		[TestMethod]
		public void Solve()
		{
			int[] input = new[] { 100, 4, 200, 1, 3, 2 };

			var result = LongestConsecutive(input);

			result.ShouldBe(4);
		}

		[TestMethod]
		public void Solve2()
		{
			int[] input = { 0, 3, 7, 2, 5, 8, 4, 6, 0, 1 };

			var result = LongestConsecutive(input);

			result.ShouldBe(9);
		}

		[TestMethod]
		public void Solve3()
		{
			int[] input = { 1,2,0,1 };

			var result = LongestConsecutive(input);

			result.ShouldBe(3);
		}

		private Dictionary<int, int> _seqs = new(); // sequence ends with their lengths
		private HashSet<int> _seenNumbers = new(); // for duplicates elimination

		// Looks like this might be not O(n) solution
		public int LongestConsecutive(int[] nums)
		{
			Build(nums);

			int maxLength = 0;

			// merge

			bool wasMerge = true;

			while (wasMerge)
			{
				wasMerge = false;

				foreach (var seqEnd in _seqs.Keys)
				{
					var currentSequenceLength = _seqs[seqEnd];

					maxLength = Math.Max(maxLength, currentSequenceLength);

					var otherSequenceStart = seqEnd - currentSequenceLength;

					if (_seqs.ContainsKey(otherSequenceStart))
					{
						var newLength = _seqs[otherSequenceStart] + currentSequenceLength;

						_seqs.Remove(otherSequenceStart);
						_seqs[seqEnd] = newLength;

						maxLength = Math.Max(maxLength, newLength);

						wasMerge = true;
					}
				}
			}

			return maxLength;
		}

		private void Build(int[] nums)
		{
			foreach (var n in nums)
			{
				if (_seenNumbers.Add(n) == false)
				{
					// skip duplicates
					continue;
				}

				if (_seqs.ContainsKey(n - 1))
				{
					// previous number already encountered
					_seqs.Remove(n - 1, out int seqLength);
					_seqs[n] = seqLength + 1;
				}
				else if (_seqs.ContainsKey(n + 1))
				{
					_seqs[n + 1] += 1;
				}
				else
				{
					_seqs[n] = 1;
				}
			}
		}

		public int LongestConsecutive_Optimized_FromSolution(int[] nums)
		{
			HashSet<int> numSet = new();

			foreach (int num in nums)
			{
				numSet.Add(num);
			}

			int longestStreak = 0;

			foreach (int num in numSet)
			{
				// we only attempt to build sequences from numbers that are not already part of a longer sequence.
				// This is accomplished by first ensuring that the number that would immediately precede
				// the current number in a sequence is not present, as that number would necessarily
				// be part of a longer sequence.
				if (!numSet.Contains(num - 1))
				{
					int currentNum = num;
					int currentStreak = 1;

					// despite looking like O(n⋅n) complexity, the nested loops actually run in O(n + n) time.
					// All other computations occur in constant time, so the overall runtime is linear.
					while (numSet.Contains(currentNum + 1))
					{
						currentNum += 1;
						currentStreak += 1;
					}

					longestStreak = Math.Max(longestStreak, currentStreak);
				}
			}

			return longestStreak;
		}
	}
}
