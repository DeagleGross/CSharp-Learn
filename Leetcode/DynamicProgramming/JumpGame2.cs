using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;

namespace LeetCodeSolutions.DynamicProgramming;

/*

https://leetcode.com/problems/jump-game-ii/ 

Given an array of non-negative integers nums, you are initially positioned at the first index of the array.
Each element in the array represents your maximum jump length at that position.

Your goal is to reach the last index in the minimum number of jumps.
You can assume that you can always reach the last index.

Example 1:

Input: nums = [2,3,1,1,4]
Output: 2
Explanation: The minimum number of jumps to reach the last index is 2. Jump 1 step from index 0 to 1, then 3 steps to the last index.
Example 2:

Input: nums = [2,3,0,1,4]
Output: 2

*/

[TestClass]
public class JumpGame2
{
    [TestMethod]
    public void Solve()
    {
        var nums = new int[] { 2, 3, 1, 1, 4 };
        var result = Jump(nums);
        result.ShouldBe(2);
    }

    private int[] _nums;
    private int[] _jumpResults;

    public int Jump(int[] nums)
    {
        _nums = nums;
        _jumpResults = new int[_nums.Length];

        for (int i = _nums.Length - 1; i >= 0; --i)
        {
            CountJumpResultForPosition(i);
        }

        return _jumpResults[0];
    }

    public void CountJumpResultForPosition(int index)
    {
        // no jumps from last position to last position required
        if (index == _nums.Length - 1)
        {
            _jumpResults[index] = 0;
            return;
        }

        var jumpsCapacity = _nums[index];
        var minJumpsRequired = int.MaxValue - 1; // in case nums[index] = 0, then we will get int.Max + 1 = int.Min...

        for (int i = 1; i <= jumpsCapacity; i++)
        {
            // reached more than finish
            if (index + i > _nums.Length - 1) break;

            if (_jumpResults[index + i] < minJumpsRequired)
            {
                minJumpsRequired = _jumpResults[index + i];
            }
        }

        _jumpResults[index] = minJumpsRequired + 1;
    }
}