using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace LeetCodeSolutions.RandomTasks
{
	[TestClass]
	public class CombinationSum1
	{
		[TestMethod]
		public void Solve()
		{
			var candidates = new[] {2, 3, 6, 7};
			var target = 7;
			var result = CombinationSum(candidates, target);

			result.Count.ShouldBe(2);
		}

		private IList<IList<int>> _output = new List<IList<int>>();

		public IList<IList<int>> CombinationSum(int[] candidates, int target)
		{
			Backtrack(candidates, 0, target, new List<int>());

			return _output;
		}

		public void Backtrack(int[] candidates, int first, int target, List<int> currentCombination)
		{
			if (currentCombination.Sum() > target)
			{
				return;
			}

			if(currentCombination.Sum() == target)
			{
				_output.Add(currentCombination.ToList());
				return;
			}
			
			for (int i = first; i < candidates.Length; i++)
			{
				currentCombination.Add(candidates[i]);

				// use same integres to complete combination
				Backtrack(candidates, i, target, currentCombination);

				currentCombination.RemoveAt(currentCombination.Count - 1);
			}
		}
	}
}
