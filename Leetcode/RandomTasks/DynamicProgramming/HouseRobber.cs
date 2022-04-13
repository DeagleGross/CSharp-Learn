using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace LeetCodeSolutions.RandomTasks.DynamicProgramming;

/*

https://leetcode.com/explore/featured/card/dynamic-programming/631/strategy-for-solving-dp-problems/4148/
 
You are a professional robber planning to rob houses along a street.
Each house has a certain amount of money stashed, the only constraint stopping you from robbing each of them is 
that adjacent houses have security systems connected and it will automatically contact the police 
if two adjacent houses were broken into on the same night.

Given an integer array nums representing the amount of money of each house, 
return the maximum amount of money you can rob tonight without alerting the police.

Example 1:

Input: nums = [1,2,3,1]
Output: 4
Explanation: Rob house 1 (money = 1) and then rob house 3 (money = 3).
Total amount you can rob = 1 + 3 = 4.
Example 2:

Input: nums = [2,7,9,3,1]
Output: 12
Explanation: Rob house 1 (money = 2), rob house 3 (money = 9) and rob house 5 (money = 1).
Total amount you can rob = 2 + 9 + 1 = 12.

 */

[TestClass]
public class HouseRobber
{
    private int[] _nums;
    private IDictionary<int, int> _memoData = new Dictionary<int, int>();

    [TestMethod]
    public void Solve()
    {
        var result = Rob(new[] { 2, 7, 9, 3, 1 });
        result.ShouldBe(12);
    }

    public int Rob(int[] nums)
    {
        _nums = nums;
        return Dp(_nums.Length - 1);
    }

    private int Dp(int index)
    {
        if (index == 0) return _nums[0];
        if (index == 1) return (_nums[0] > _nums[1]) ? _nums[0] : _nums[1];

        if (!_memoData.ContainsKey(index))
        {
            var result = Math.Max(Dp(index - 1), Dp(index - 2) + _nums[index]);
            _memoData.Add(index, result);
        }

        return _memoData[index];
    }
}