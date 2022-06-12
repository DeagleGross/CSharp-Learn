using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// https://leetcode.com/problems/interleaving-string/

namespace LeetCodeSolutions.RandomTasks.DynamicProgramming
{
	[TestClass]
	public class InterleavingString
	{
		[TestMethod]
		public void Solve()
		{
			var s1 = "aabcc";
			var s2 = "dbbca";
			var s3 = "aadbbcbcac";

			var result = IsInterleave(s1, s2, s3);

			result.Should().BeTrue();
		}

		[TestMethod]
		public void Solve2()
		{
			var s1 = "aabcc";
			var s2 = "dbbca";
			var s3 = "aadbbbaccc";

			var result = IsInterleave(s1, s2, s3);

			result.Should().BeFalse();
		}

		public bool IsInterleave(string s1, string s2, string s3)
		{
			if (s3.Length > s1.Length + s2.Length)
			{
				return false;
			}

			// + 1 since dp[i,j] represents chars of s3[i-1 \ j-1]
			bool[,] dp = new bool[s1.Length + 1, s2.Length+1];
			
			for (int i = 0; i <= s1.Length; i++)
			{
				for (int j = 0; j <= s2.Length; j++)
				{
					if (i == 0 && j == 0)
					{
						// add first value ??
						dp[i,j] = true;
					}
					else if (i == 0)
					{
						dp[i,j] = dp[i,j - 1] 
							&& s2[j - 1] == s3[i + j - 1];
					}
					else if (j == 0)
					{
						dp[i,j] = dp[i - 1,j]
							&& s1[i - 1] == s3[i + j - 1];
					}
					else
					{
						dp[i, j] = (
								dp[i - 1, j]
								&& s1[i - 1] == s3[i + j - 1]
							)
							||
							(
								dp[i, j - 1]
								&& s2[j - 1] == s3[i + j - 1]
							);
					}
				}
			}
			return dp[s1.Length,s2.Length];
		}

		public bool IsInterleave2(String s1, String s2, String s3)
		{
			if (s3.Length > s1.Length + s2.Length)
			{
				return false;
			}

			bool[] dp = new bool[s2.Length + 1];

			for (int i = 0; i <= s1.Length; i++)
			{
				for (int j = 0; j <= s2.Length; j++)
				{
					if (i == 0 && j == 0)
					{
						dp[j] = true;
					}
					else if (i == 0)
					{
						dp[j] = dp[j - 1] && s2[j - 1] == s3[i + j - 1];
					}
					else if (j == 0)
					{
						dp[j] = dp[j] && s1[i - 1] == s3[i + j - 1];
					}
					else
					{
						dp[j] = (
								dp[j]
								&& s1[i - 1] == s3[i + j - 1]
							)
							|| (
								dp[j - 1]
								&& s2[j - 1] == s3[i + j - 1]
							);
					}
				}
			}

			return dp[s2.Length];
		}
	}
}
