using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// https://leetcode.com/problems/palindromic-substrings/

namespace LeetCodeSolutions.RandomTasks.DynamicProgramming
{
	[TestClass]
	public class PalindromicSubstrings
	{
		[TestMethod]
		public void Solve()
		{
			var s = "abc";
			var result = CountSubstrings(s);

			result.Should().Be(3);
		}

		[TestMethod]
		public void Solve2()
		{
			var s = "aaa";
			var result = CountSubstrings(s);

			result.Should().Be(6);
		}

		public int CountSubstrings(string s)
		{
			int n = s.Length;
			int ans = 0;

			if (n <= 0)
			{
				return 0;
			}

			bool[,] dp = new bool[n, n];

			// Base case: single letter substrings
			for (int i = 0; i < n; i++, ans++)
			{
				dp[i, i] = true;
			}

			// Base case: double letter substrings
			for (int i = 0; i < n - 1; i++)
			{
				dp[i,i + 1] = (s[i] == s[i + 1]);
				ans += (dp[i,i + 1] ? 1 : 0);
			}

			// All other cases: substrings of length 3 to n
			for (int len = 3; len <= n; len++)
			{
				for (int substrStart = 0, substrEnd = substrStart + len - 1; 
					substrEnd < n; 
					substrStart++, substrEnd++)
				{
					dp[substrStart, substrEnd] = dp[substrStart + 1, substrEnd - 1] && (s[substrStart] == s[substrEnd]);
					ans += (dp[substrStart, substrEnd]
						? 1
						: 0);
				}
			}

			return ans;
		}
	}
}
