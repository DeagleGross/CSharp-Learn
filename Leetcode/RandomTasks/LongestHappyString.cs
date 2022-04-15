using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

// https://leetcode.com/problems/longest-happy-string/

namespace LeetCodeSolutions.RandomTasks
{
	[TestClass]
	public class LongestHappyString
	{
		[TestMethod]
		public void Solve()
		{
			int a = 1;
			int b = 1; 
			int c = 7;

			var result = LongestDiverseString(a, b, c);

			(result == "ccaccbcc" || result == "ccbccacc").ShouldBe(true);
		}

		[TestMethod]
		public void Solve2()
		{
			int a = 0;
			int b = 2;
			int c = 3;

			var result = LongestDiverseString(a, b, c);

			result.ShouldBe("ccbbc");
		}

		[TestMethod]
		public void Solve3()
		{
			int a = 0;
			int b = 8;
			int c = 11;

			var result = LongestDiverseString(a, b, c);

			result.ShouldBe("ccbccbccbbccbbccbbc");
		}

		[TestMethod]
		public void Solve4()
		{
			int a = 4;
			int b = 42;
			int c = 7;

			var result = LongestDiverseString(a, b, c);

			result.ShouldBe("bbcbbcbbcbbcbbabbabbcbbcbbabbabbcbb");
		}

		public string LongestDiverseString(int a, int b, int c)
		{
			List<(char c, int count)> letters = new();
			
			letters.Add(('a', a));
			letters.Add(('b', b));
			letters.Add(('c', c));

			string ret = "";

			do
			{
				letters = letters.OrderByDescending(l => l.count).ToList();
			}
			while (
				AddChar(ref ret, letters, 0) 
				|| AddChar(ref ret, letters, 1) 
				|| AddChar(ref ret, letters, 2));

			return ret;
		}

		private bool AddChar(ref string s, List<(char c, int count)> letters, int i)
		{
			if (letters[i].count == 0)
			{
				return false;
			}

			bool canAdd = s.Length < 2 
				|| !(s[^1] == letters[i].c && s[^2] == letters[i].c);

			if (!canAdd)
			{
				return false;
			}

			s += letters[i].c;

			letters[i] = (letters[i].c, letters[i].count - 1);

			return true;
		}


		#region Inefficient backtracking solution

		private int _maxSameLetters = 2;
		public string _output = "";

		void AppendLetter(Dictionary<char, int> letters)
		{

			var previousChar = _output.Length == 0
				? 'x'
				: _output[^1];

			var maxLeft = letters
				.Where(kv => kv.Key != previousChar)
				.MaxBy(kv => kv.Value);

			if (maxLeft.Value == 0)
			{
				return;
			}

			var charToAppend = maxLeft.Key;
			var timesToAppend = maxLeft.Value >= _maxSameLetters
				? _maxSameLetters
				: maxLeft.Value;

			var stringToAppend = new string(charToAppend, timesToAppend);

			letters[charToAppend] -= timesToAppend;

			_output = _output + stringToAppend;

			AppendLetter(letters);
		}

		void Backtrack(Dictionary<char, int> letters, ref string stringSoFar)
		{
			if (stringSoFar.Length > _output.Length)
			{
				_output = stringSoFar;
			}

			if (letters.All(kv => kv.Value == 0))
			{
				return;
			}

			var previousChar = stringSoFar.Length == 0
				? 'x'
				: stringSoFar[^1];

			foreach (var letter in letters.Keys.Where(kv => kv != previousChar))
			{
				if (letters[letter] == 0)
				{
					continue;
				}

				if (letters[letter] >= 2)
				{
					stringSoFar += new string(letter, 2);
					letters[letter] -= 2;

					Backtrack(letters, ref stringSoFar);

					stringSoFar = stringSoFar.Remove(stringSoFar.Length - 2);
					letters[letter] += 2;
				}

				stringSoFar += letter.ToString();
				letters[letter] -= 1;

				Backtrack(letters, ref stringSoFar);

				stringSoFar = stringSoFar.Remove(stringSoFar.Length - 1);
				letters[letter] += 1;
			}
		}

		#endregion
	}
}
