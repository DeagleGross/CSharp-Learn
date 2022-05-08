using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// https://leetcode.com/problems/decode-string/

namespace LeetCodeSolutions.RandomTasks.Strings
{
	[TestClass]
	public class DecodeString1
	{
		[TestMethod]
		public void Solve()
		{
			var s = "3[a]2[bc]";

			string result = DecodeString(s);

			result.Should().Be("aaabcbc");
		}

		[TestMethod]
		public void Solve2()
		{
			var s = "3[a2[c]]";

			string result = DecodeString(s);

			result.Should().Be("accaccacc");
		}

		[TestMethod]
		public void Solve3()
		{
			var s = "2[abc]3[cd]ef";

			string result = DecodeString(s);

			result.Should().Be("abcabccdcdcdef");
		}

		public string DecodeString(string s)
		{
			if (s.Length == 1)
			{
				return s;
			}

			if (s.IndexOf("[", StringComparison.InvariantCultureIgnoreCase) == -1)
			{
				// means there is no folding - just return input string
				return s;
			}

			StringBuilder ret = new();
			int i = 0;
			while (i < s.Length)
			{
				if (!char.IsDigit(s[i]))
				{
					ret.Append(s[i]);
					i++;
					continue;
				}

				if (char.IsDigit(s[i]))
				{
					int times = 0;

					while (char.IsDigit(s[i]))
					{
						times = times * 10 + (s[i] - '0');
						i++;
					}
					
					// i now points to the first open bracket

					i++; // first inside sequence

					int openBrackets = 1;

					int subseqStart = i;
					
					while (openBrackets > 0)
					{
						if (s[i] == '[')
						{
							openBrackets++;
							i++;
							continue;
						}

						if (s[i] == ']')
						{
							openBrackets--;
							i++;
							continue;
						}

						i++;
					}

					var subseqEnd = i -1; // cause i is on the next cahr after the last brack

					string substr = s[subseqStart..subseqEnd];

					var decoded = DecodeString(substr);

					for (int j = 0; j < times; j++)
					{
						ret.Append(decoded);
					}
				}
			}

			var r = ret.ToString();

			return r;
		}
	}
}
