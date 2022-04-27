using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

// https://leetcode.com/problems/permutations/

namespace LeetCodeSolutions.RandomTasks
{
	[TestClass]
	public class Permutations
	{
		[TestMethod]
		public void Solve()
		{
			int[] nums = new int[] {1, 2, 3};
			var result = Permute(nums);

			// since the order is random the test method for this would involve parsing of the resulting expected result
			// so we skip it
			//Print(result).ShouldBe("[[1,2,3],[1,3,2],[2,1,3],[2,3,1],[3,1,2],[3,2,1]]");
		}

		#region Backtracking solution (from problem solution)

		public IList<IList<int>> Permute_Backtrack(int[] nums)
		{
			// init output list
			IList<IList<int>> output = new List<IList<int>>();

			Backtrack(nums, output, 0);

			return output;
		}

		// The great explanation of this technique is given in https://leetcode.com/problems/permutations-ii/solution/
		private void Backtrack(IList<int> nums, IList<IList<int>> output, int firstPositionToTry)
		{
			// if all integers are used up - we've built a permutation
			if (firstPositionToTry == nums.Count)
			{
				output.Add(new List<int>(nums));
			}

			for (int i = firstPositionToTry; i < nums.Count; i++)
			{
				// place i-th integer first 
				// in the current permutation

				Swap(nums, firstPositionToTry, i);

				// use next integers to complete the permutations
				Backtrack(nums, output, firstPositionToTry + 1);

				// backtrack (swap elements back)
				Swap(nums, firstPositionToTry, i);
			}
		}

		private IList<T> Swap<T>(IList<T> list, int indexA, int indexB)
		{
			(list[indexA], list[indexB]) = (list[indexB], list[indexA]);
			return list;
		}

		#endregion

		public IList<IList<int>> Permute(int[] nums)
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
				previousPermutations.Add(new List<int>(){num});
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
				previousPermutations.Add(p);
			}
		}

		public string Print(IList<IList<int>> target)
		{
			List<string> parts = new(target.Count);

			foreach (var p in target)
			{
				parts.Add("["+string.Join(",", p)+"]");
			}

			return "[" + string.Join(",", parts) + "]";
		}
	}
}
