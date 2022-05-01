using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// https://leetcode.com/problems/permutations-ii/

namespace LeetCodeSolutions.RandomTasks.Permutations
{
	[TestClass]
	public class Permutations2
	{
		[TestMethod]
		public void Solve()
		{
			int[] nums = new int[] { 1, 1, 2 };
			var result = PermuteUnique(nums);
		}

		private readonly HashSet<string> _seenPermutations = new();

		public IList<IList<int>> PermuteUnique(int[] nums)
		{
			IList<IList<int>> ret = new List<IList<int>>(); // length - factorial of nums.Length

			foreach (var num in nums)
			{
				GetPermutations(num, ret);
			}

			return ret;
		}

		public void GetPermutations(int num, IList<IList<int>> previousPermutations)
		{
			if (previousPermutations.Count == 0)
			{
				previousPermutations.Add(new List<int>() { num });
				return;
			}

			List<List<int>> newPermutations = new();

			foreach (var previous in previousPermutations)
			{
				for (int position = 0; position <= previous.Count; position++)
				{
					var perm = new List<int>(previous);

					perm.Insert(position, num);

					newPermutations.Add(perm);
				}
			}

			previousPermutations.Clear();

			foreach (var p in newPermutations)
			{
				if (_seenPermutations.Add(string.Join("", p)))
				{
					previousPermutations.Add(p);
				}
			}
		}
	}
}
