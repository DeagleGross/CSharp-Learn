using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// https://leetcode.com/problems/coin-change-2/

namespace LeetCodeSolutions.RandomTasks.DynamicProgramming
{
	[TestClass]
	public class CoinChange2
	{
		[TestMethod]
		public void Solve()
		{
			int[] coins = new[] { 1, 2, 5 };
			int amount = 5;

			var result = Change(amount, coins);

			result.Should().Be(4);
		}

		public int Change(int amount, int[] coins)
		{
			int[] dp = new int[amount + 1];

			// If the total amount of money is zero, there is only one combination: to take zero coins.
			// Don't know why - just remember!
			dp[0] = 1;

			// get coins one by one and try to combine them into all sums
			foreach (var coin in coins)
			{
				// all combinations with only one coin is ONE COMBINATION

				// trying to combine all sums that are larger that coin nominal
				for (int sum = coin; sum <= amount; sum++)
				{
					dp[sum] += dp[sum - coin];
				}

			}

			return dp[amount];
		}
	}
}
