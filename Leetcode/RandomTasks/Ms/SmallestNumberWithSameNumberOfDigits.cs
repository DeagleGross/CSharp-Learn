using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace LeetCodeSolutions.Ms
{
	[TestClass]
	public class SmallestNumberWithSameNumberOfDigits
	{
		[TestMethod]
		public void Solve()
		{
			int input = 125;
			var smallestNumber = SmallestNumber(input);
			smallestNumber.ShouldBe(100);
		}

		public int SmallestNumber(int input)
		{
			var numberOfDigits = input.ToString().Length;

			if (numberOfDigits == 1)
			{
				return 0;
			}

			var minimum = (int)Math.Pow(10, numberOfDigits-1);

			return minimum;
		}
	}
}
