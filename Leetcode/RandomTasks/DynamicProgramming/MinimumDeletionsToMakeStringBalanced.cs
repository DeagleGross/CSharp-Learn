using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// https://leetcode.com/problems/minimum-deletions-to-make-string-balanced/

namespace LeetCodeSolutions.RandomTasks.DynamicProgramming
{
	[TestClass]
	public class MinimumDeletionsToMakeStringBalanced
	{
		[TestMethod]
		public void Solve()
		{
			string s = "aababbab";

			var result = MinimumDeletions(s);

			result.Should().Be(2);
		}

		[TestMethod]
		public void Solve2()
		{
			string s = "bbaaaaabb";

			var result = MinimumDeletions(s);

			result.Should().Be(2);
		}

		[TestMethod]
		public void Solve3()
		{
			string s = "bb";

			var result = MinimumDeletions(s);

			result.Should().Be(0);
		}
		
		[TestMethod]
		public void Solve4()
		{
			string s = "baababbaabbaaabaabbabbbabaaaaaabaabababaaababbb";

			var result = MinimumDeletions(s);

			result.Should().Be(18);
		}

		public int MinimumDeletions(string s)
		{
			// here we store counts of b's before the given index
			// and count of a's after the given index
			int[] countOfBsBefore = new int[s.Length];
			int[] countOfAsAfter = new int[s.Length];


			// Preprocessing starts (calculate prefix sums for both a's and b's)

			// starting with 1 because at 0 there is nothing before
			for (int i = 1; i < s.Length; i++)
			{
				if (s[i - 1] == 'b')
				{
					countOfBsBefore[i] = countOfBsBefore[i - 1] + 1;
				}
				else
				{
					countOfBsBefore[i] = countOfBsBefore[i - 1];
				}
			}

			// starting with n - 2 because at n - 1  there is nothing after
			for (int i = s.Length - 2; i >= 0; i--)
			{
				if (s[i + 1] == 'a')
				{
					countOfAsAfter[i] = countOfAsAfter[i + 1] + 1;
				}
				else
				{
					countOfAsAfter[i] = countOfAsAfter[i + 1];
				}
			}

			// Preprocessing ends

			// for each index check how many no of b needs to be deleted
			// before i and how many no of a needs to be deleted after i
			int ans = int.MaxValue;
			for (int i = 0; i < s.Length; i++)
			{
				ans = Math.Min(ans, countOfBsBefore[i] + countOfAsAfter[i]);
			}
			return ans;
		}

		public int MinimumDeletions_Stack(string s)
		{
			Stack<char> st = new Stack<char>();

			int ans = 0; 
			int index = 0;

			while (index < s.Length)
			{
				if (st.Count != 0 && st.Peek() == 'b' && s[index] == 'a')
				{
					st.Pop();
					ans++;
				}
				else
				{
					st.Push(s[index]);
				}

				index++;
			}
			return ans;
		}
	}
}
