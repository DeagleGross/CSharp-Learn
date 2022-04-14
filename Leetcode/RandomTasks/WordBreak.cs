using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

// https://leetcode.com/problems/word-break/

namespace LeetCodeSolutions.RandomTasks
{
	[TestClass]
	public class WordBreak1
	{
		[TestMethod]
		public void Solve()
		{
			string word = "leetcode";

			IList<string> dict = new List<string>()
			{
				"leet", "code"
			};

			var result = WordBreak(word, dict);

			result.ShouldBe(true);
		}

		[TestMethod]
		public void Solve2()
		{
			string word = "applepenapple";

			IList<string> dict = new List<string>()
			{
				"apple", "pen"
			};

			var result = WordBreak(word, dict);

			result.ShouldBe(true);
		}

		[TestMethod]
		public void Solve3()
		{
			string word = "bccdbacdbdacddabbaaaadababadad";

			IList<string> dict = new List<string>()
			{
				"cbc", "bcda", "adb", "ddca", "bad", "bbb", "dad", "dac", "ba", "aa", "bd", "abab", "bb", "dbda", "cb",
				"caccc", "d", "dd", "aadb", "cc", "b", "bcc", "bcd", "cd", "cbca", "bbd", "ddd", "dabb", "ab", "acd",
				"a", "bbcc", "cdcbd", "cada", "dbca", "ac", "abacd", "cba", "cdb", "dbac", "aada", "cdcda", "cdc",
				"dbc", "dbcb", "bdb", "ddbdd", "cadaa", "ddbc", "babb"
			};

			var result = WordBreak(word, dict);

			result.ShouldBe(true);
		}

		public bool WordBreak(string s, IList<string> wordDict)
		{
			var memo = new bool?[s.Length];

			return WordBreak_Memo(s, new HashSet<string>(wordDict), 0, memo);
		}

		private bool WordBreak_Memo(string s, HashSet<string> wordDict, int start, bool?[] memo)
		{
			if (start == s.Length)
			{
				return true;
			}

			if (memo[start].HasValue)
			{
				return memo[start].Value;
			}

			for (int end = start + 1; end <= s.Length; end++)
			{
				var prefix = s.Substring(start, end - start);

				// check if we have string prefix in dictionary and the rest of the sttring in dictionary using recursion
				if (wordDict.Contains(prefix)
					&& WordBreak_Recursive(s, wordDict, end))
				{
					memo[start] = true;
					return true;
				}
			}

			memo[start] = false;

			return false;
		}

		private bool WordBreak_Recursive(string s, HashSet<string> wordDict, int start)
		{
			if (start == s.Length)
			{
				return true;
			}

			for (int end = start + 1; end <= s.Length; end++)
			{
				var prefix = s.Substring(start, end-start);

				// check if we have string prefix in dictionary and the rest of the sttring in dictionary using recursion
				if (wordDict.Contains(prefix) 
					&& WordBreak_Recursive(s, wordDict, end))
				{
					return true;
				}
			}

			return false;
		}

		#region Backtrack solution (suboptimal)

		public bool _canBreak = false;

		public bool WordBreak_Backtrack(string s, IList<string> wordDict)
		{
			var combination = "";
			Backtrack(wordDict.OrderBy(s => s.Length).ToList(), 0, combination, s);

			return _canBreak;
		}

		public void Backtrack(IList<string> wordDict, int start, string combination, string target)
		{
			if (_canBreak
				|| combination.Length > target.Length
				|| (combination.Length == target.Length && !string.Equals(
					combination,
					target,
					StringComparison.InvariantCulture)))
			{
				return;
			}

			if (string.Equals(combination, target, StringComparison.InvariantCulture))
			{
				_canBreak = true;
			}

			for (int i = start; i < wordDict.Count; i++)
			{
				var initialLength = combination.Length;

				combination = combination + wordDict[i];

				Backtrack(wordDict, 0, combination, target);

				combination = combination.Remove(initialLength);
			}
		}

		#endregion
	}
}
