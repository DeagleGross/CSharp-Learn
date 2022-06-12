using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

// https://leetcode.com/problems/palindrome-partitioning/

namespace LeetCodeSolutions.RandomTasks.Backtracking
{
	[TestClass]
	public class PalindromePartitioning
	{
		[TestMethod]
		public void Solve()
		{
			var s = "aab";

			var result = Partition(s);

			result.Count.Should().Be(2);
		}

		public IList<IList<string>> Partition(string s)
		{
			List<IList<string>> result = new();

			var temp = new List<string>();

			Backtrack(0, result, temp, s);

			return result;
		}

		private void Backtrack(int start, List<IList<string>> result, List<string> currentPalindromeList, string s)
		{
			if (start >= s.Length)
			{
				// here we are certain that currentPalindromeList contains only palindromes
				result.Add(new List<string>(currentPalindromeList));
			}

			for (int potentialPalindromeEnd = start; potentialPalindromeEnd < s.Length; potentialPalindromeEnd++)
			{
				if (IsPalindrome(s, start, potentialPalindromeEnd))
				{
					// add current substring in the currentList
					currentPalindromeList.Add(s[start..(potentialPalindromeEnd + 1)]);

					Backtrack(potentialPalindromeEnd + 1, result, currentPalindromeList, s);

					// backtrack and remove the current substring from currentList

					currentPalindromeList.RemoveAt(currentPalindromeList.Count - 1);
				}
			}
		}

		private bool IsPalindrome(string s, int low, int high)
		{
			while (low < high)
			{
				if (s[low] != s[high])
				{
					return false;
				}

				low++;
				high--;
			}
			return true;
		}
	}
}
