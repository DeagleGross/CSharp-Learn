using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

// https://leetcode.com/problems/longest-palindromic-substring/

namespace LeetCodeSolutions.RandomTasks.Strings;

[TestClass]
public class LongestPalindromicString
{
	[TestMethod]
	public void Solve()
	{
		string input = "qweabaabaqwe";
		var res = LongestPalindrome(input);
		res.ShouldBe("abaaba");
	}

	public string LongestPalindrome(string s)
	{
		if (string.IsNullOrEmpty(s))
		{
			return "";
		}

		int start = 0; 
		int end = 0;

		for (int i = 0; i < s.Length; i++)
		{
			// case with center on letter
			int len1 = ExpandAroundCenter(s, i, i);

			// case with center between letters
			int len2 = ExpandAroundCenter(s, i, i + 1);

			int len = Math.Max(len1, len2);

			if (len > end - start)
			{
				start = i - (len - 1) / 2;
				end = i + len / 2;
			}
		}
		return s.Substring(start, end-start+1);
	}

	private int ExpandAroundCenter(string s, int left, int right)
	{
		int L = left;
		int	R = right;
		while (L >= 0 && R < s.Length && s[L] == s[R])
		{
			L--;
			R++;
		}

		return R - L - 1;
	}

	static string LongestPalindrome_Old(string s)
	{
		if (s.Length == 1)
		{
			return s;
		}

		string result = s[0].ToString();

		int start = 0;

		Dictionary<char, List<int>> letterPositions = new();


		void RemeberLetterPosition(char letter, int position)
		{
			if (!letterPositions.ContainsKey(letter))
			{
				letterPositions[letter] = new();
			}

			letterPositions[letter].Add(position);
		}

		for (int i = s.Length - 1; i >= 0; i--)
		{
			RemeberLetterPosition(s[i], i);
		}

		while (start < s.Length - 1)
		{
			if (letterPositions[s[start]].Count <= 1)
			{
				start++;
				continue;
			}

			int sameLetterIndex = 0;

			while (sameLetterIndex < letterPositions[s[start]].Count)
			{
				var sameLetterPosition = letterPositions[s[start]][sameLetterIndex];

				if (sameLetterPosition < start)
				{
					break;
				}

				var left = start;
				var right = sameLetterPosition;
				var found = true;

				while (right - left > 1)
				{
					left++;
					right--;

					if (s[left] != s[right])
					{
						found = false;
						break;
					}
				}

				if (found)
				{
					var palindrome = s.Substring(start, sameLetterPosition - start + 1);
					if (palindrome.Length > result.Length)
					{
						result = palindrome;
					}

					break;
				}

				sameLetterIndex++;
			}

			start++;

			if (result.Length >= s.Length - start)
			{
				break;
			}
		}

		return result;
	}
}