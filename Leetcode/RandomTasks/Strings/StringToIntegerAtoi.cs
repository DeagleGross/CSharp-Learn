using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// https://leetcode.com/problems/string-to-integer-atoi/

namespace LeetCodeSolutions.RandomTasks.Strings
{
	[TestClass]
	public class StringToIntegerAtoi
	{
		[TestMethod]
		public void Solve()
		{
			string s = "42";
			var result = MyAtoi(s);

			result.Should().Be(42);
		}

		[TestMethod]
		public void Solve2()
		{
			string s = "     -42";
			var result = MyAtoi(s);

			result.Should().Be(-42);
		}

		[TestMethod]
		public void Solve3()
		{
			string s = "4193 with words";
			var result = MyAtoi(s);

			result.Should().Be(4193);
		}

		[TestMethod]
		public void Solve4()
		{
			string s = $"{int.MaxValue}"+"1";
			var result = MyAtoi(s);

			result.Should().Be(2147483647);
		}

		[TestMethod]
		public void Solve5()
		{
			string s = "-91283472332";
			var result = MyAtoi(s);

			result.Should().Be(-2147483648);
		}

		[TestMethod]
		public void Solve6()
		{
			string s = "+1";
			var result = MyAtoi(s);

			result.Should().Be(1);
		}

		[TestMethod]
		public void Solve7()
		{
			string s = "-6147483648";
			var result = MyAtoi(s);

			result.Should().Be(-2147483648);
		}

		public int MyAtoi(string s)
		{
			var str = s.Trim();

			int multiplier = 1;
			if (str.StartsWith("-"))
			{
				multiplier = -1;
				str = str[1..];
			}
			else if(str.StartsWith("+"))
			{
				str = str[1..];
			}

			int ret = 0;

			if (str.Length == 0)
			{
				return 0;
			}

			var position = 0;

			while (ReadNextHeadDigit(str, ref position, out int digit))
			{
				// Either check overbflow ising checked context or use a manual check

				//checked
				//{
				//	try
				//	{
						// Check overflow and underflow conditions. Manually instead of using checked context
						// Here we are accounting for *10 multiplication of the ret before digit appendage
						// If ret==int.MaxValue / 10  then we can append only digits 0-7 (7 is the int.MaxValue % 10)

						if ((ret > int.MaxValue / 10) ||
							(ret == int.MaxValue / 10 && digit > int.MaxValue % 10))
						{
							// If integer overflowed return 2^31-1, otherwise if underflowed return -2^31.    
							return multiplier == 1 ? int.MaxValue : int.MinValue;
						}

						ret = ret == 0
							? digit
							: ret * 10 + digit;
					//}
					//catch (OverflowException)
					//{
					//	return multiplier == 1 ? int.MaxValue : int.MinValue;
					//}
				//}
			}

			return ret * multiplier;
		}

		public bool ReadNextHeadDigit(string s, ref int position, out int digit)
		{
			digit = int.MinValue;

			if (position >= s.Length)
			{
				return false;
			}

			var c = s[position];

			if (char.IsDigit(c))
			{
				digit = c - '0';
				position++;
				return true;
			}

			return false;
		}
	}
}
