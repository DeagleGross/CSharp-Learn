using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

// https://leetcode.com/problems/reverse-integer/

namespace LeetCodeSolutions.RandomTasks
{
	[TestClass]
	public class ReverseInteger
	{
		[TestMethod]
		public void Solve()
		{
			int x = -123;
			int reversed = Reverse2(x);

			reversed.ShouldBe(-321);
		}

		public int Reverse(int x)
		{
			int multiplier = x < 0 ? -1 : 1;

			try
			{
				checked
				{
					int ret = 0;
					var str = x.ToString();

					var next10ThPower = 0;

					foreach (var c in str.StartsWith("-") ? str.Skip(1) : str)
					{
						var number = int.Parse(c.ToString());
						ret += (int)(number * Math.Pow(10, next10ThPower));

						next10ThPower++;
					}

					return ret * multiplier;
				}
			}
			catch (OverflowException)
			{
				return 0;
			}
		}

		public int Reverse2(int x)
		{
			// Leetcode actually deems this solution more time consuming than the String using one!
			int multiplier = x < 0 ? -1 : 1;

			try
			{
				checked
				{
					int ret = 0;

					var temp = Math.Abs(x);

					while (temp > 0)
					{
						var number = PopDigit(ref temp);

						ret += ret * 10 + number;
					}

					return ret * multiplier;
				}
			}
			catch (OverflowException)
			{
				return 0;
			}
		}

		/// <summary>
		/// Pops the last significant digit out of the specified number. Retruns a digit and places new number into the input parameter.
		/// </summary>
		/// <param name="number">Number to pop digit from.</param>
		/// <returns></returns>
		public int PopDigit(ref int number)
		{
			var ret = number % 10;
			number = number / 10;

			return ret;
		}
	}
}
