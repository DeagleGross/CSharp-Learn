using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// https://leetcode.com/problems/generate-parentheses/

namespace LeetCodeSolutions.RandomTasks.Backtracking
{
	[TestClass]
	public class GenerateParentheses
	{
		[TestMethod]
		public void Solve()
		{
			int n = 3;

			var result = GenerateParenthesis(n);

			result.Count.Should().Be(5);
		}

		public IList<string> GenerateParenthesis(int n)
		{
			List<string> ret = new();
			StringBuilder current = new();

			Backtrack(ret, current, 0, 0, n);
			
			return ret;
		}

		public void Backtrack(List<string> ret, StringBuilder currentCombination, int open, int close, int max)
		{
			if (currentCombination.Length == max * 2)
			{
				ret.Add(currentCombination.ToString());
				return;
			}

			if (open < max)
			{
				currentCombination.Append("(");
				Backtrack(ret, currentCombination, open + 1, close, max);
				currentCombination.Remove(currentCombination.Length-1, 1);
			}

			if (close < open)
			{
				currentCombination.Append(")");
				Backtrack(ret, currentCombination, open, close + 1, max);
				currentCombination.Remove(currentCombination.Length - 1, 1);
			}
		}
	}
}
