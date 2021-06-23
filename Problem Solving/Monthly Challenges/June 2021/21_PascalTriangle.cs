using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace ProblemSolving.Monthly_Challenges.June_2021
{
    [TestClass]
    public class PascalTriangle : IOlympiadTask
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

        [TestMethod]
        public void Solve()
        {
            var res_1 = Generate(5);
            res_1.Count.ShouldBe(5);

            res_1[0].ShouldBe(new []{ 1 });
            res_1[1].ShouldBe(new []{ 1, 1 });
            res_1[2].ShouldBe(new []{ 1, 2, 1 });
            res_1[3].ShouldBe(new []{ 1, 3, 3, 1 });
            res_1[4].ShouldBe(new []{ 1, 4, 6, 4, 1 });
        }
    }
}
