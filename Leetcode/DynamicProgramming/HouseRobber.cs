using System;
using System.Collections.Generic;
using System.Data.Odbc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace LeetCodeSolutions.DynamicProgramming;

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