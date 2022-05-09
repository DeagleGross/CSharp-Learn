using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// https://leetcode.com/problems/coin-change/

namespace LeetCodeSolutions.RandomTasks.DynamicProgramming
{
	[TestClass]
	public class CoinChange1
	{
		[TestMethod]
		public void Solve()
		{
			int[] coins = new[] {1, 2, 5};
			int amount = 11;

			var result = CoinChange(coins, amount);

			result.Should().Be(3);
		}

		[TestMethod]
		public void Solve2()
		{
			int[] coins = new[] { 2 };
			int amount = 3;

			var result = CoinChange(coins, amount);

			result.Should().Be(-1);
		}

		[TestMethod]
		public void Solve3()
		{
			int[] coins = new[] { 2 };
			int amount = 0;

			var result = CoinChange(coins, amount);

			result.Should().Be(0);
		}

		[TestMethod]
		public void Solve4()
		{
			int[] coins = new[] { 1,2,3,4,5 };
			int amount = 100;

			var result = CoinChange(coins, amount);

			result.Should().Be(20);
		}

		[TestMethod]
		public void Solve5()
		{
			int[] coins = new[] { 1, 2, 3, 4, 5 };
			int amount = 101;

			var result = CoinChange(coins, amount);

			result.Should().Be(21);
		}

		[TestMethod]
		public void Solve6()
		{
			int[] couins = new[] { 186, 419, 83, 408 };
			int amount = 6249;

			var result = CoinChange(couins, amount);

			result.Should().Be(20);
		}

		private int[] _memo;

		public int CoinChange(int[] coins, int amount)
		{
			_memo = new int[amount + 1]; // +1 to acount for amount 0
			Array.Fill(_memo, Int32.MaxValue);

			var ret = GetCoinChange(coins, amount);

			return ret;
		}

		private int GetCoinChange(int[] coins, int amount)
		{
			if (amount < 0)
			{
				return -1;
			}

			if (amount == 0)
			{
				return 0;
			}

			if (_memo[amount] != Int32.MaxValue)
			{
				return _memo[amount];
			}

			int minCount = Int32.MaxValue;

			for (int i = 0; i < coins.Length; i++)
			{
				// choose one of the coins

				var nextCoinAmount = GetCoinChange(coins, amount - coins[i]);
				if (nextCoinAmount == -1)
				{
					// means that chosen path can't combine into the given amount
					continue;
				}

				// + 1 beacuse we have already chosen a coin couns[i]
				minCount = Math.Min(minCount, nextCoinAmount + 1); 
			}

			_memo[amount] = minCount == Int32.MaxValue
				? -1
				: minCount;

			return _memo[amount];
		}

		public int CoinChange_Dp(int[] coins, int amount)
		{
			int[] dp = new int[amount + 1];

			// amount+1 is the sentinel value
			// we don't choose Int.MaxValue to not acuse integer overflow on dp[i - coins[j]] + 1 operation
			Array.Fill(dp, amount + 1); 

			dp[0] = 0;

			for (int i = 1; i <= amount; i++)
			{
				for (int j = 0; j < coins.Length; j++)
				{
					// choose coin
					if (coins[j] > i)
					{
						// if the cheosen coin nominal is larger than the remaining amount - don't choose it
						continue;
					}

					// minimum between current sentinel value and previous value (current - value of the chosen coin) + 1 
					// +1 beacuse we account for a chosen coin
					var min = Math.Min(dp[i], dp[i - coins[j]] + 1); 

					dp[i] = min;
				}
			}

			return dp[amount] > amount // check sentinel value
				? -1
				: dp[amount];
		}
	}
}
