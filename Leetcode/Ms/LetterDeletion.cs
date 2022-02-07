using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace LeetCodeSolutions.Ms
{
	[TestClass]
	public class LetterDeletion
	{
		[TestMethod]
		public void Solve()
		{
			var input = "example";
			var deletions = Solution(input);
			deletions.ShouldBe(4);
		}

		private int Solution(string input)
		{
			int deletions = 0;

			HashSet<int> seenOccurrences = new();

			var sortedString = input.OrderBy(c => c).ToArray();

			char currentLetter = sortedString[0];
			int currentLetterOccurrence = 0;

			foreach (var c in sortedString)
			{
				if (currentLetter == c)
				{
					currentLetterOccurrence++;
				}
				else
				{
					// seen different character
					if (seenOccurrences.Add(currentLetterOccurrence))
					{
						// unique
					}
					else
					{
						// non unique
						while (currentLetterOccurrence > 0 && !seenOccurrences.Add(currentLetterOccurrence))
						{
							currentLetterOccurrence--;
							deletions++;
						}
					}

					currentLetter = c;
					currentLetterOccurrence = 1;
				}
			}

			while (currentLetterOccurrence > 0 && !seenOccurrences.Add(currentLetterOccurrence))
			{
				currentLetterOccurrence--;
				deletions++;
			}

			return deletions;
		}
	}
}
