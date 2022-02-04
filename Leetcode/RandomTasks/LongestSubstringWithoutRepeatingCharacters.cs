using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

// https://leetcode.com/problems/longest-substring-without-repeating-characters/
/*
Given a string s, find the length of the longest substring without repeating characters.

Input: s = "abcabcbb"
Output: 3
Explanation: The answer is "abc", with the length of 3.

Input: s = "bbbbb"
Output: 1
Explanation: The answer is "b", with the length of 1.

Input: s = "pwwkew"
Output: 3
Explanation: The answer is "wke", with the length of 3.
Notice that the answer must be a substring, "pwke" is a subsequence and not a substring.
*/

namespace LeetCodeSolutions.RandomTasks
{
	[TestClass]
	public class LongestSubstringWithoutRepeatingCharacters
	{
		[TestMethod]
		public void Solve()
		{
			var s = "dvdf";
			int length = LengthOfLongestSubstring(s);

			length.ShouldBe(3);
		}

		public int LengthOfLongestSubstring(string s)
		{
			var maxLength = 0;

			Queue<char> chars = new();
			HashSet<char> encounteredChars = new();

			for (int i = 0; i < s.Length; i++)
			{
				var c = s[i];
				if (encounteredChars.Add(c))
				{
					chars.Enqueue(c);
					maxLength = Math.Max(maxLength, chars.Count);
				}
				else
				{
					char dequeuedChar;
					do
					{
						dequeuedChar = chars.Dequeue();
						encounteredChars.Remove(dequeuedChar);
					}
					while (dequeuedChar != c);

					encounteredChars.Add(c);
					chars.Enqueue(c);
				}
			}

			return maxLength;
		}
	}
}
