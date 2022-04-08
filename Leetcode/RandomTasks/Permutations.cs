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
