using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// https://leetcode.com/problems/flip-string-to-monotone-increasing/

namespace LeetCodeSolutions.RandomTasks.DynamicProgramming
{
	[TestClass]
	public class FlipStringToMonotoneIncreasing
	{
		[TestMethod]
		public void Solve()
		{
			var s = "00110";

			var result = MinFlipsMonoIncr(s);

			result.Should().Be(1);
		}

		public int MinFlipsMonoIncr(string s)
		{
			// here we store counts of b's before the given index
			// and count of a's after the given index
			int[] countOfOnesBefore = new int[s.Length];
			int[] countOfZerosAfter = new int[s.Length];


			// Preprocessing starts

			// starting with 1 because at 0 there is nothing before
			for (int i = 1; i < s.Length; i++)
			{
				if (s[i - 1] == '1')
				{
					countOfOnesBefore[i] = countOfOnesBefore[i - 1] + 1;
				}
				else
				{
					countOfOnesBefore[i] = countOfOnesBefore[i - 1];
				}
			}

			// starting with n - 2 because at n - 1  there is nothing after
			for (int i = s.Length - 2; i >= 0; i--)
			{
				if (s[i + 1] == '0')
				{
					countOfZerosAfter[i] = countOfZerosAfter[i + 1] + 1;
				}
				else
				{
					countOfZerosAfter[i] = countOfZerosAfter[i + 1];
				}
			}

			// Preprocessing ends

			// for each index check how many no of b needs to be deleted
			// before i and how many no of a needs to be deleted after i
			int ans = int.MaxValue;
			for (int i = 0; i < s.Length; i++)
			{
				ans = Math.Min(ans, countOfOnesBefore[i] + countOfZerosAfter[i]);
			}
			return ans;
		}

		public int MinFlipsMonoIncr_MoreOptimal(string s)
		{
			int inputLength = s.Length;
			
			int[] prefixSumOfOnesBefore = new int[inputLength + 1];

			for (int i = 0; i < inputLength; ++i)
			{
				prefixSumOfOnesBefore[i + 1] = prefixSumOfOnesBefore[i] + (s[i] == '1' ? 1 : 0);
			}

			int ans = int.MaxValue;

			for (int j = 0; j <= inputLength; ++j)
			{
				ans = Math.Min(ans, prefixSumOfOnesBefore[j] + inputLength - j - (prefixSumOfOnesBefore[inputLength] - prefixSumOfOnesBefore[j]));
			}

			return ans;
		}
	}
}
