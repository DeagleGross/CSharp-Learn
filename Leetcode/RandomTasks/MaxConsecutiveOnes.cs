﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

// https://leetcode.com/explore/challenge/card/june-leetcoding-challenge-2021/607/week-5-june-29th-june-30th/3796/

namespace LeetCodeSolutions.RandomTasks;

[TestClass]
public class MaxConsecutiveOnes
{
    [TestMethod]
    public void Solve()
    {
        int flipCount = 2;
        var arr = GenerateArray(10);
        PrintArray(arr);
        var longestOnesResult = LongestOnes(arr, flipCount);
        longestOnesResult.ShouldBe(6);
    }

    static int LongestOnes(int[] nums, int k)
    {
        int windowStart = 0;
        var windowEnd = 0;
        var maxWindowLength = 0;

        Queue<int> zeroIndexes = new();

        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] == 0)
            {
                if (zeroIndexes.Count < k)
                {
                    // we still have flips left
                    zeroIndexes.Enqueue(i);
                }
                else
                {
                    // we don't have any flips left
                    var currentWindowLength = windowEnd - windowStart + 1;
                    if (currentWindowLength > maxWindowLength)
                    {
                        maxWindowLength = currentWindowLength;
                    }

                    windowStart = zeroIndexes.Dequeue() + 1;
                    zeroIndexes.Enqueue(i);
                }
            }

            windowEnd = i;
        }

        var lastWindowLength = windowEnd - windowStart + 1;
        if (lastWindowLength > maxWindowLength)
        {
            return lastWindowLength;
        }

        return maxWindowLength;
    }

    static int[] GenerateArray(int length)
    {
        Random rnd = new(1567);
        return Enumerable.Range(0, length).Select(_ => rnd.Next(0, 2)).ToArray();
    }

    static void PrintArray(int[] target)
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