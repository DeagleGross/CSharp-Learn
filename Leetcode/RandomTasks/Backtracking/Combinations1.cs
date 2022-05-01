using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// https://leetcode.com/problems/combinations/

namespace LeetCodeSolutions.RandomTasks.Backtracking
{
	[TestClass]
	public class Combinations1
	{
		[TestMethod]
		public void Solve()
		{

			var result = Combine(4, 2);
		}

		private int _n;
		private int _k;
		private IList<IList<int>> _output = new List<IList<int>>();

		public IList<IList<int>> Combine(int n, int k)
		{
			this._n = n;
			this._k = k;

			Backtrack(1, new List<int>());

			return _output;
		}

		public void Backtrack(int first, List<int> currentCombination)
		{
			// if the combination is done
			if (currentCombination.Count == _k)
			{
				// here we add the copy of the combination since it is changed by backtracking
				_output.Add(new List<int>(currentCombination)); 
			}

			for (int i = first; i < _n + 1; ++i)
			{
				// add i into the current combination
				currentCombination.Add(i);

				// use next integers to complete the combination
				Backtrack(i + 1, currentCombination);

				// backtrack - remove last integer
				currentCombination.RemoveAt(currentCombination.Count - 1);
			}
		}
	}
}
