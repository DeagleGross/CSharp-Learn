using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

// https://leetcode.com/problems/max-consecutive-ones-iii/
//TODO: return to this one since looks like the solution is incorrect

namespace LeetCodeSolutions.RandomTasks;

[TestClass]
public class MaxConsecutiveOnes3
{
	[TestMethod]
	public void Solve()
	{
		int flipCount = 0;
		var arr = new []{0, 0, 1, 1, 1, 0, 0};
		
		var longestOnesResult = LongestOnes(arr, flipCount);
		longestOnesResult.ShouldBe(3);
	}

	[TestMethod]
	public void Solve2()
	{
		int flipCount = 2;
		var arr = new []{ 1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 0 };
		
		var longestOnesResult = LongestOnes(arr, flipCount);
		longestOnesResult.ShouldBe(6);
	}

	[TestMethod]
	public void Solve3()
	{
		int flipCount = 3;
		var arr = new []{ 0, 0, 1, 1, 0, 0, 1, 1, 1, 0, 1, 1, 0, 0, 0, 1, 1, 1, 1};
		
		var longestOnesResult = LongestOnes(arr, flipCount);
		longestOnesResult.ShouldBe(10);
	}
	
	[TestMethod]
	public void Solve4()
	{
		int flipCount = 4;
		var arr = new []{0, 0, 0, 1};
		
		var longestOnesResult = LongestOnes(arr, flipCount);
		longestOnesResult.ShouldBe(4);
	}
	
	[TestMethod]
	public void Solve5()
	{
		int flipCount = 0;
		var arr = new []{1, 1, 1, 0, 0, 0, 1, 1, 1, 1};
		
		var longestOnesResult = LongestOnes(arr, flipCount);
		longestOnesResult.ShouldBe(4);
	}

	public int LongestOnes(int[] nums, int k)
	{
		if (nums.Length == 1)
		{
			if(k > 0)
			{
				return 1;
			}
			else
			{
				return 0;
			}
		}

		if (nums.Length == k)
		{
			return nums.Length;
		}

		if (k == 0)
		{
			var p = 0;

			int mx = 0;
			int l = 0;
			while (p < nums.Length)
			{
				if (nums[p] == 1)
				{
					l++;
					p++;
					mx = Math.Max(l, mx);
				}
				else
				{
					// 0
					mx = Math.Max(l, mx);
					l = 0;
					p++;
				}
			}

			return mx;
		}

		int flipsSoFar = 0;

		int left = 0;
		int right = 1;

		int max = 0;

		Queue<int> flippedZeroPosition = new Queue<int>();

		if (nums[0] == 0 && k > 0)
		{
			flipsSoFar++;
			flippedZeroPosition.Enqueue(0);
			max = 1;
		}

		while (left < nums.Length - 1 && right < nums.Length)
		{
			if (nums[right] == 1)
			{
				max = Math.Max(right - left + 1, max);
				right++;
				continue;
			}

			if (nums[right] == 0)
			{
				if (flipsSoFar < k)
				{
					flippedZeroPosition.Enqueue(right);

					max = Math.Max(right - left + 1, max);
					flipsSoFar++;
					right++;
				}
				else
				{
					left = flippedZeroPosition.Dequeue() + 1;

					if (left > right)
					{
						break;
					}

					right--;
					flipsSoFar--;

					max = Math.Max(right - left + 1, max);

					right++;
				}
			}
		}

		return max;
	}

	private int[] GenerateArray(int length)
	{
		Random rnd = new(1567);
		return Enumerable.Range(0, length).Select(_ => rnd.Next(0, 2)).ToArray();
	}

	private void PrintArray(int[] target)
	{
		var ret = target.Aggregate(
			new StringBuilder(),
			(acc, value) =>
			{
				acc.Append(value);
				return acc;
			});

		Console.WriteLine(ret);
	}
}