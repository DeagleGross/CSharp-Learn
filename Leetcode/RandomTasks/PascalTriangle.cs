using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

// https://leetcode.com/problems/pascals-triangle/

namespace LeetCodeSolutions.RandomTasks;

[TestClass]
public class PascalTriangle
{
    [TestMethod]
    public void Solve()
    {
        var triangle = Generate(3);
        
        triangle[0].ShouldBe(new [] { 1 });
        triangle[1].ShouldBe(new[] { 1, 1 });
        triangle[2].ShouldBe(new[] { 1, 2, 1 });
    }

    static IList<IList<int>> Generate(int numRows)
    {
        var result = new List<IList<int>>(numRows);

        for (var row = 0; row < numRows; row++)
        {
            var currentValues = new int[row + 1];

            // first and last are `1`
            currentValues[0] = 1;
            currentValues[^1] = 1;

            for (var i = 1; i < currentValues.Length - 1; i++)
            {
                currentValues[i] = result[row - 1][i - 1] + result[row - 1][i];
            }

            result.Add(currentValues);
        }

        return result;
    }
}