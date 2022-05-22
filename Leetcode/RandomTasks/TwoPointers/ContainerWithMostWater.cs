using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

// https://leetcode.com/problems/container-with-most-water/

namespace LeetCodeSolutions.RandomTasks.TwoPointers;

[TestClass]
public class ContainerWithMostWater
{
    [TestMethod]
    public void Solve()
    {
        var input = new[] { 1, 8, 6, 2, 5, 4, 8, 3, 7 };
        var area = MaxArea(input);
        area.ShouldBe(49);
    }

    public int MaxArea(int[] height)
    {
        int s = 0;

        int left = 0;
        int right = height.Length - 1;

        while (right > left)
        {
            var leftHeight = height[left];
            var rightHeight = height[right];

            var newS = Math.Min(leftHeight, rightHeight) * (right - left);

            if (newS > s)
            {
                s = newS;
            }

            if (leftHeight > rightHeight)
            {
                right--;
            }
            else
            {
                left++;
            }
        }

        return s;
    }
}