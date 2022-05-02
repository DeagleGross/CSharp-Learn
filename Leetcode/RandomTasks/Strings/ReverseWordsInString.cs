using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

// https://leetcode.com/problems/reverse-words-in-a-string/

namespace LeetCodeSolutions.RandomTasks.Strings
{
	[TestClass]
	public class ReverseWordsInString
	{
		[TestMethod]
		public void Solve()
		{
			string input = "the sky is blue";

			var result = ReverseWords(input);

			result.ShouldBe("blue is sky the");
		}

		private List<string> _words = new();

		public string ReverseWords(string s)
		{
			var cleanString = s.Trim();

			while (cleanString.Contains("  "))
			{
				cleanString = cleanString.Replace("  ", " ");
			}

			var parts = cleanString.Split(" ").Reverse();
			var newString = string.Join(" ", parts);

			return newString;
		}
	}
}
