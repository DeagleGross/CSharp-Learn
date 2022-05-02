using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// https://leetcode.com/problems/string-compression/

namespace LeetCodeSolutions.RandomTasks.Strings
{
	[TestClass]
	public class StringCompression
	{
		[TestMethod]
		public void Solve()
		{
			char[] chars = new[] { 'a', 'a', 'b', 'b', 'c', 'c', 'c'};

			var result = Compress(chars);
			
			result.Should().Be(6);

			chars.Should().StartWith(new []{ 'a', '2', 'b', '2', 'c', '3' });
		}

		[TestMethod]
		public void Solve2()
		{
			char[] chars = new[] { 'a', 'a', 'b', 'b', 'c', 'c', 'c', 'd' };

			var result = Compress(chars);

			result.Should().Be(7);

			chars.Should().StartWith(new[] { 'a', '2', 'b', '2', 'c', '3', 'd' });
		}

		[TestMethod]
		public void Solve3()
		{
			char[] chars = new[] { 'a', 'b', 'c' };

			var result = Compress(chars);

			result.Should().Be(3);

			chars.Should().StartWith(new[] { 'a', 'b', 'c' });
		}

		public int Compress(char[] chars)
		{
			if (chars.Length == 1)
			{
				return 1;
			}

			int i = 1;

			int placeToMove = 0;

			int compressedLength = 0;

			while (i < chars.Length)
			{
				var thisChar = chars[i];
				var previousChar = chars[i - 1];

				if (thisChar == previousChar)
				{
					int repeatingCharsCount = 2;
					int j = i+1;
					
					while (j < chars.Length && chars[j] == previousChar)
					{
						repeatingCharsCount++;
						j++;
					}

					// here j is a new char

					// output previousChar + repeatingCharsCount

					chars[placeToMove] = previousChar;
					placeToMove++;
					compressedLength++;

					var repeatingCharsCountString = repeatingCharsCount.ToString();

					for (int k = 0; k < repeatingCharsCountString.Length; k++)
					{
						chars[placeToMove] = repeatingCharsCountString[k];
						compressedLength++;
						placeToMove++;
					}

					if (j >= chars.Length)
					{
						// means we are finished
						break;
					}

					if (j == chars.Length - 1)
					{
						// last cahracter left
						// add last character
						chars[placeToMove] = chars[j];
						compressedLength++;
						break;
					}

					i = j+1;

					continue;
				}

				if (thisChar != previousChar)
				{
					chars[placeToMove] = previousChar;
					placeToMove++;
					compressedLength++;

					if (i == chars.Length-1)
					{
						// last cahracter left
						// add last character
						chars[placeToMove] = chars[i];
						compressedLength++;
						break;
					}

					i++;
				}
			}

			return compressedLength;
		}
	}
}
