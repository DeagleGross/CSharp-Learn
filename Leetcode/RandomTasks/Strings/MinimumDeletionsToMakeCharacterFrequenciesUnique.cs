using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

// https://leetcode.com/problems/minimum-deletions-to-make-character-frequencies-unique/

namespace LeetCodeSolutions.RandomTasks.Strings
{
	[TestClass]
	public class MinimumDeletionsToMakeCharacterFrequenciesUnique
	{
		[TestMethod]
		public void Solve()
		{
			string input = "aab";

			var result = MinDeletions(input);

			result.ShouldBe(0);
		}

		[TestMethod]
		public void Solve2()
		{
			string input = "aaabbbcc";

			var result = MinDeletions(input);

			result.ShouldBe(2);
		}

		[TestMethod]
		public void Solve3()
		{
			string input = "ceabaacb";

			var result = MinDeletions(input);

			result.ShouldBe(2);
		}
		
		[TestMethod]
		public void Solve4()
		{
			string input = "accdcdadddbaadbc";

			var result = MinDeletions(input);

			result.ShouldBe(1);
		}
		
		[TestMethod]
		public void Solve5()
		{
			string input = "abcabc";

			var result = MinDeletions(input);

			result.ShouldBe(3);
		}

		public int MinDeletions(string s)
		{
			var sorted = s.OrderByDescending(x => x).ToArray();

			int deletions = 0;

			char previousChar = sorted[0];

			int currentFrequency = 1;

			HashSet<int> encounteredFreqs = new HashSet<int>();

			for (int i = 1; i < sorted.Length; i++)
			{
				if (sorted[i] == previousChar)
				{
					currentFrequency++;
					continue;
				}
				else
				{
					// encountered new char

					if (encounteredFreqs.Add(currentFrequency))
					{
						// new frequency - all ok
						currentFrequency = 1;
						previousChar = sorted[i];
						continue;
					}
					else
					{
						while (currentFrequency > 0 && encounteredFreqs.Contains(currentFrequency))
						{
							currentFrequency--;
							deletions++;
						}

						if (currentFrequency > 0)
						{
							encounteredFreqs.Add(currentFrequency);
						}

						currentFrequency = 1;
						previousChar = sorted[i];
						continue;
					}
				}
			}

			// process last char
			while (currentFrequency > 0 && encounteredFreqs.Contains(currentFrequency))
			{
				currentFrequency--;
				deletions++;
			}

			return deletions;
		}
	}
}
