using System;
using System.Collections.Generic;

/// <summary>
/// https://leetcode.com/problems/pascals-triangle/
/// </summary>

namespace PascalTriangle
{
    class Program
    {
        public IList<IList<int>> Generate(int numRows)
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

        static void Main(string[] args)
        {
            var program = new Program();

            var triangle = program.Generate(5);

            foreach (var row in triangle)
            {
                foreach (var symbol in row)
                {
                    Console.Write($"{symbol} ");
                }

                Console.WriteLine();
            }
        }
    }
}
