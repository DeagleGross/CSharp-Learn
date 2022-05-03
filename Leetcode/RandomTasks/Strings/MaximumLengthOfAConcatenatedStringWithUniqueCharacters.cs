using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// https://leetcode.com/problems/maximum-length-of-a-concatenated-string-with-unique-characters/

namespace LeetCodeSolutions.RandomTasks.Strings
{
	[TestClass]
	public class MaximumLengthOfAConcatenatedStringWithUniqueCharacters
	{
		[TestMethod]
		public void Solve()
		{
			string[] arr = new[] { "un", "iq", "ue" };

			var result = MaxLength(arr);

			result.Should().Be(4);
		}

		[TestMethod]
		public void Solve2()
		{
			string[] arr = new[] { "cha", "r", "act", "ers" };

			var result = MaxLength(arr);

			result.Should().Be(6);
		}

		private int _maxLength = 0;
		HashSet<char> _uniqueChars = new(26);

		public int MaxLength(IList<string> arr)
		{
			HashSet<string> combination = new();

			Backtrack(arr, 0, combination, 0);

			return _maxLength;
		}

		// we can optimize this solution by storing only character counts in a Dictionary<char, int>
		// instead of the HashSet<string>
		private void Backtrack(IList<string> parts, int start, HashSet<string> combination, int combinationLength)
		{
			if (combinationLength > _maxLength
				&& IsUnique(combination))
			{
				_maxLength = combinationLength;
			}

			if (start > parts.Count - 1
				|| combination.Count == parts.Count)
			{
				// means all parts are used or nex part is beyond parts
				return;
			}

			for (int i = start; i < parts.Count; i++)
			{
				var partAdded = combination.Add(parts[i]);

				if (!partAdded)
				{
					// skip duplicates
					continue;
				}

				var newCombinationLength = combinationLength + parts[i].Length;

				Backtrack(parts, i + 1, combination, newCombinationLength);

				combination.Remove(parts[i]);
			}
		}

		private bool IsUnique(HashSet<string> combination)
		{
			_uniqueChars.Clear();

			foreach (var part in combination)
			{
				foreach (var c in part)
				{
					if (!_uniqueChars.Add(c))
					{
						return false;
					}
				}
			}

			return true;
		}
	}
}
