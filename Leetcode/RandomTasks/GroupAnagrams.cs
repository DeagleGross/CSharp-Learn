using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

// https://leetcode.com/problems/group-anagrams/

namespace LeetCodeSolutions.RandomTasks
{
	[TestClass]
	public class GroupAnagrams1
	{
		[TestMethod]
		public void Solve()
		{
			var strs = new[] {"eat", "tea", "tan", "ate", "nat", "bat"};

			var result = GroupAnagrams(strs);

			result.Count.ShouldBe(3);
		}

		[TestMethod]
		public void Solve2()
		{
			var strs = new[] { "", "" };

			var result = GroupAnagrams(strs);

			result.Count.ShouldBe(1);

			result[0].Count.ShouldBe(2);
		}


		[TestMethod]
		public void Solve3()
		{
			var strs = new[] { "", "", "" };

			var result = GroupAnagrams(strs);

			result.Count.ShouldBe(1);

			result[0].Count.ShouldBe(3);
		}

		[TestMethod]
		public void Solve4()
		{
			var strs = new[] { "dis", "sid", "sid" };

			var result = GroupAnagrams(strs);

			result.Count.ShouldBe(1);

			result[0].Count.ShouldBe(3);
		}

		public IList<IList<string>> GroupAnagrams(string[] strs)
		{
			Dictionary<string, List<string>> anagramms = new Dictionary<string, List<string>>();

			for (int i = 0; i < strs.Length; i++)
			{
				var sorted = new string(strs[i].OrderBy(c => c).ToArray());
				if (!anagramms.ContainsKey(sorted))
				{
					anagramms.Add(sorted, new());
				}

				anagramms[sorted].Add(strs[i]);
			}

			IList<IList<string>> ret = new List<IList<string>>();

			foreach (var kv in anagramms)
			{
				ret.Add(kv.Value);
			}

			return ret;
		}

		#region IsAnagram - based solution (time limit exceeded)

		public IList<IList<string>> GroupAnagrams_Suboptimal2(string[] strs)
		{
			HashSet<string> seenWords = new HashSet<string>();

			IList<IList<string>> ret = new List<IList<string>>();

			for (int i = 0; i < strs.Length; i++)
			{
				var word = strs[i];

				if (seenWords.Contains(word))
				{
					continue;
				}

				List<string> currentAnagramms = new List<string>()
				{
					word
				};

				HashSet<string> seenAnagramms = new HashSet<string>()
				{
					word
				};

				for (int j = i + 1; j < strs.Length; j++)
				{
					if (strs[j] == word || seenAnagramms.Contains(strs[j]))
					{
						currentAnagramms.Add(strs[j]);
						continue;
					}

					if (seenWords.Contains(strs[j]))
					{
						continue;
					}

					if (IsAnagram(word, strs[j]))
					{
						currentAnagramms.Add(strs[j]);
						seenWords.Add(strs[j]);
						seenAnagramms.Add(strs[j]);
					}
				}

				ret.Add(currentAnagramms);

				seenWords.Add(word);
			}

			return ret;
		}

		public bool IsAnagram(string word, string anagram)
		{
			if (word.Length != anagram.Length)
			{
				return false;
			}

			if (word.Length == 1
				&& anagram.Length == 1)
			{
				return word[0] == anagram[0];
			}

			var s1 = word.OrderBy(c => c).ToArray();
			var s2 = anagram.OrderBy(c => c).ToArray();

			for (int i = 0; i < word.Length; i++)
			{
				if (s1[i] != s2[i])
				{
					return false;
				}
			}

			return true;
		}

		#endregion

		#region Anagramm generating solution (Time limit exceeded)

		public IList<IList<string>> GroupAnagrams_Suboptimal(string[] strs)
		{
			if (strs.Length == 1)
			{
				return new List<IList<string>>()
				{
					new List<string>() {strs[0]}
				};
			}

			Dictionary<string, int> words = new Dictionary<string, int>();

			foreach (var str in strs)
			{
				if (!words.ContainsKey(str))
				{
					words.Add(str, 0);
				}

				words[str] += 1;
			}

			IList<IList<string>> ret = new List<IList<string>>();

			for (int i = 0; i < strs.Length; i++)
			{
				var word = strs[i];

				if (words[word] == 0)
				{
					continue;
				}

				List<string> currentAnagrams = new List<string>();

				while (words[word] > 0)
				{
					currentAnagrams.Add(word);
					words[word] -= 1;
				}

				var wordAnagrams = GetAnagramms(word);
				wordAnagrams.Remove(word);

				foreach (var anagram in wordAnagrams)
				{
					if (words.ContainsKey(anagram))
					{
						currentAnagrams.Add(anagram);
						words[anagram] -= 1;
					}
				}

				ret.Add(currentAnagrams);
			}

			return ret;
		}

		private HashSet<string> GetAnagramms(string str)
		{
			HashSet<string> anagrams = new();

			GetAnagramm(str.ToArray(), 0, anagrams);

			return anagrams;
		}

		private void GetAnagramm(char[] s, int firstPositionToTry, HashSet<string> output)
		{
			if (firstPositionToTry == s.Length)
			{
				output.Add(new string(s));
				return;
			}

			for (int i = firstPositionToTry; i < s.Length; i++)
			{
				Swap(s, firstPositionToTry, i);

				GetAnagramm(s, firstPositionToTry + 1, output);

				Swap(s, firstPositionToTry, i);
			}
		}

		private IList<T> Swap<T>(IList<T> list, int indexA, int indexB)
		{
			(list[indexA], list[indexB]) = (list[indexB], list[indexA]);
			return list;
		}

		#endregion
	}
}
