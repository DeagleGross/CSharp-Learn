using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

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

    static string LongestPalindrome(string s)
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