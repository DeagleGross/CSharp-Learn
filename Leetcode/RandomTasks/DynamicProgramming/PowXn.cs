using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace LeetCodeSolutions.RandomTasks.DynamicProgramming
{
	[TestClass]
	public class PowXn
	{
		[TestMethod]
		public void Solve()
		{
			double x = 2.00000;
			int n = 10;

			var result = MyPow(x, n);

			result.ShouldBe(1024.00000);
		}

		[TestMethod]
		public void Solve2()
		{
			double x = 2.10000;
			int n = 3;

			var result = MyPow(x, n);

			result.ShouldBe(9.26100);
		}

		[TestMethod]
		public void Solve3()
		{
			double x = 2.00000;
			int n = -2;

			var result = MyPow(x, n);

			result.ShouldBe(0.25000);
		}

		[TestMethod]
		public void Solve4()
		{
			double x = 1.00000;
			int n = 2147483647;

			var result = MyPow(x, n);

			result.ShouldBe(1);
		}

		public double MyPow(double x, int n)
		{
			if (n == 0)
			{
				return 1;
			}

			if (x == 0)
			{
				return 0;
			}

			var num = x;
			var pow = n;

			if (n < 0)
			{
				num = 1 / num;
				pow = -pow;
			}

			var ret = FastPow(num, pow);

			return ret;
		}

		private double FastPow(double x, long n)
		{
			if (n == 0)
			{
				return 1.0;
			}

			var half = FastPow(x, n / 2);

			if (n % 2 == 0)
			{
				return half * half;
			}
			else
			{
				return half * half * x;
			}
		}

		public double MyPow_BruteForce(double x, int n)
		{
			if (n == 0)
			{
				return 1;
			}

			if (x == 0)
			{
				return 0;
			}

			var ret = x;
			var mult = x;

			if (n < 0)
			{
				ret = 1 / ret;
				mult = 1 / mult;
			}

			for (int i = 1; i < (int)Math.Abs(n); i++)
			{
				ret *= mult;
			}

			return ret;
		}
	}
}
