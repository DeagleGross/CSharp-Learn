using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// https://leetcode.com/problems/pascals-triangle/

var triangle = Generate(5);
foreach (var row in triangle)
{
    foreach (var symbol in row)
    {
        Console.Write($"{symbol} ");
    }

    Console.WriteLine();
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