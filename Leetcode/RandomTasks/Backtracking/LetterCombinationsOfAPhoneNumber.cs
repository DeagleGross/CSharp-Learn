using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

// https://leetcode.com/problems/letter-combinations-of-a-phone-number/

namespace LeetCodeSolutions.RandomTasks.Backtracking
{
	[TestClass]
	public class LetterCombinationsOfAPhoneNumber
	{
		[TestMethod]
		public void Solve()
		{
			string digits = "23";

			var result = LetterCombinations(digits);

			result.Count.ShouldBe(9);
		}

		[TestMethod]
		public void Solve2()
		{
			string digits = "2";

			var result = LetterCombinations(digits);

			result.Count.ShouldBe(3);
		}

		[TestMethod]
		public void Solve3()
		{
			string digits = "27";

			var result = LetterCombinations(digits);

			result.Count.ShouldBe(12);
		}

		private readonly Dictionary<char, List<char>> _keyboard = new()
		{
			['2'] = new() {'a', 'b', 'c'},
			['3'] = new() {'d', 'e', 'f'},
			['4'] = new() {'g', 'h', 'i'},
			['5'] = new() {'j', 'k', 'l'},
			['6'] = new() {'m', 'n', 'o'},
			['7'] = new() {'p', 'q', 'r', 's'},
			['8'] = new() {'t', 'u', 'v'},
			['9'] = new() {'w', 'x', 'y', 'z'}
		};

		public IList<string> LetterCombinations(string digits)
		{
			List<string> current = new();

			foreach (var c in digits)
			{
				var charsToAdd = _keyboard[c];
				AddToCombination(charsToAdd, current);
			}

			return current;
		}

		private void AddToCombination(List<char> charsToAdd, List<string> currentCombination)
		{
			if (currentCombination.Count == 0)
			{
				currentCombination.AddRange(charsToAdd.Select(c=>c.ToString()));
				return;
			}

			List<string> newCombination = new();
			foreach (var c in charsToAdd)
			{
				foreach (var existingCombination in currentCombination)
				{
					newCombination.Add(existingCombination + c);
				}
			}
			currentCombination.Clear();
			currentCombination.AddRange(newCombination);
		}

		#region Backtracking solution (actually more time-consuming that simple iterative solution)

		private List<string> _combinations = new();
		private string _phoneDigits;

		public IList<string> LetterCombinations_Backtrack(string digits)
		{
			// If the input is empty, immediately return an empty answer array
			if (digits.Length == 0)
			{
				return _combinations;
			}

			// Initiate backtracking with an empty path and starting index of 0
			_phoneDigits = digits;

			Backtrack(0, new StringBuilder());

			return _combinations;
		}

		private void Backtrack(int index, StringBuilder path)
		{
			// If the path is the same length as digits, we have a complete combination
			if (path.Length == _phoneDigits.Length)
			{
				_combinations.Add(path.ToString());
				return; // Backtrack
			}

			// Get the letters that the current digit maps to, and loop through them
			List<char> possibleLetters = _keyboard[_phoneDigits[index]];
			foreach (char letter in possibleLetters)
			{
				// Add the letter to our current path
				path.Append(letter);

				// Move on to the next digit
				Backtrack(index + 1, path);

				// Backtrack by removing the letter before moving onto the next
				path.Remove(path.Length-1, 1);
			}
		}

		#endregion
	}
}
