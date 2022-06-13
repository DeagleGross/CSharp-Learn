using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

// https://leetcode.com/problems/longest-palindromic-subsequence/

namespace LeetCodeSolutions.RandomTasks.DynamicProgramming
{
	[TestClass]
	public class LongestPalindromicSubsequence
	{
		[TestMethod]
		public void Solve()
		{
			var s = "bbbab";
			var result = LongestPalindromeSubseq(s);

			result.ShouldBe(4);
		}

		[TestMethod]
		public void Solve2()
		{
			var s = "cbbd";
			var result = LongestPalindromeSubseq(s);

			result.ShouldBe(2);
		}

		public int LongestPalindromeSubseq(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return 0;
			}

			int n = s.Length;

			int[,] memo = new int[n,n];

			return LongestPalindromeSubseqImpl(s, 0, n - 1, memo);
		}

		private int LongestPalindromeSubseqImpl(string s, int left, int right, int[,] maxSubseqLengths)
		{
			if (left > right)
			{
				return 0;
			}

			if (left == right)
			{
				return 1;
			}

			if (maxSubseqLengths[left,right] > 0)
			{
				// means we have already calculated longest subseq
				return maxSubseqLengths[left,right];
			}

			if (s[left] == s[right])
			{
				maxSubseqLengths[left, right] = LongestPalindromeSubseqImpl(s, left + 1, right - 1, maxSubseqLengths) + 2;
				return maxSubseqLengths[left, right];
			}

			maxSubseqLengths[left,right] = Math.Max(
				LongestPalindromeSubseqImpl(s, left + 1, right, maxSubseqLengths),
				LongestPalindromeSubseqImpl(s, left, right - 1, maxSubseqLengths)
			);

			return maxSubseqLengths[left, right];
		}
	}
}
