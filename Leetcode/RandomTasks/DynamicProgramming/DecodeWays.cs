using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

//https://leetcode.com/problems/decode-ways/

namespace LeetCodeSolutions.RandomTasks.DynamicProgramming
{
	[TestClass]
	public class DecodeWays
	{
		[TestMethod]
		public void Solve()
		{
			string s = "12";
			var result = NumDecodings(s);

			result.Should().Be(2);
		}

		[TestMethod]
		public void Solve2()
		{
			string s = "112342";
			var result = NumDecodings(s);

			result.Should().Be(5);
		}

		[TestMethod]
		public void Solve3()
		{
			string s = "226";
			var result = NumDecodings(s);

			result.Should().Be(3);
		}
		
		[TestMethod]
		public void Solve4()
		{
			string s = "11106";
			var result = NumDecodings(s);

			result.Should().Be(2);
		}

		[TestMethod]
		public void Solve5()
		{
			string s = "06";
			var result = NumDecodings(s);

			result.Should().Be(0);
		}

		[TestMethod]
		public void Solve6()
		{
			string s = "10";
			var result = NumDecodings(s);

			result.Should().Be(1);
		}
		
		[TestMethod]
		public void Solve7()
		{
			string s = "2101";
			var result = NumDecodings(s);

			result.Should().Be(1);
		}

		[TestMethod]
		public void Solve8()
		{
			string s = "1123";
			var result = NumDecodings(s);

			result.Should().Be(5);
		}

		public int NumDecodings(string s)
		{
			// DP array to store the subproblem results
			// The index i of dp is character at index i-1 of s.
			int[] dp = new int[s.Length + 1];
			dp[0] = 1;

			// Ways to decode a string of size 1 is 1. Unless the string is '0'.
			// '0' doesn't have a single digit decode.
			dp[1] = s[0] == '0' ? 0 : 1;

			for (int i = 2; i < dp.Length; i++)
			{
				// Check if successful single digit decode is possible.
				if (s[i-1] != '0')
				{
					dp[i] = dp[i - 1];
				}

				// Check if successful two digit decode is possible.
				int twoDigit = int.Parse(s.Substring(i-2, 2));
				if (twoDigit >= 10 && twoDigit <= 26)
				{
					dp[i] += dp[i - 2];
				}
			}

			return dp[s.Length];
		}
	}
}
