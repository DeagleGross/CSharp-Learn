using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// https://leetcode.com/problems/multiply-strings/

namespace LeetCodeSolutions.RandomTasks.Strings
{
	[TestClass]
	public class MultiplyStrings
	{
		[TestMethod]
		public void Solve()
		{
			string num1 = "2";
			string num2 = "3";

			string result = Multiply(num1, num2);

			result.Should().Be("6");
		}

		[TestMethod]
		public void Solve2()
		{
			string num1 = "123";
			string num2 = "456";

			string result = Multiply(num1, num2);

			result.Should().Be("56088");
		}

		public string Multiply(string num1, string num2)
		{
			if (num1 == "0"
				|| num2 == "0")
			{
				return "0";
			}

			Stack<string> parts = new();

			string min;
			string max;

			if (num1.Length < num2.Length)
			{
				min = num1;
				max = num2;
			}
			else
			{
				max = num1;
				min = num2;
			}

			for (int i = min.Length - 1; i >= 0; i--)
			{
				var digit = min[i];
				string part = MultiplyOne(max, digit, min.Length - (i + 1));

				parts.Push(part);
			}

			while (parts.Count > 1)
			{
				var str1 = parts.Pop();
				var str2 = parts.Pop();

				var sum = SumStr(str1, str2);

				parts.Push(sum);
			}
			
			return parts.Pop();
		}

		private string MultiplyOne(string str, int str2, int pow)
		{
			int multiplyBy = str2 - '0';

			int carry = 0;

			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < pow; i++)
			{
				sb.Append("0");
			}

			for (int i = str.Length - 1; i >= 0; i--)
			{
				var c = str[i];
				var integer = c - '0';
				var mult = integer * multiplyBy + carry;

				carry = mult / 10;

				sb.Insert(0, mult % 10);
			}

			if (carry > 0)
			{
				sb.Insert(0, carry);
			}

			return sb.ToString();
		}

		private string SumStr(string str1, string str2)
		{
			string min;
			string max;

			if (str1.Length < str2.Length)
			{
				min = str1;
				max = str2;
			}
			else
			{
				max = str1;
				min = str2;
			}

			StringBuilder ret = new();

			int carry = 0;

			int i = max.Length - 1;
			int j = min.Length - 1;

			while (i >=0 || j >=0)
			{
				var i1 = max[i] - '0';
				var i2 = j >= 0
					? min[j] - '0'
					: 0;

				var sum = i1 + i2 + carry;

				carry = sum / 10;

				ret.Insert(0, sum % 10);

				i--;
				j--;
			}

			if (carry > 0)
			{
				ret.Insert(0, carry);
			}

			return ret.ToString();
		}
	}
}
