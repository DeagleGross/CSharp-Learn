using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace ProblemSolving
{
    /*

    Task:
    -----
    Write a function, that receives an integer and returns true, if number is even
    
    Solution Description:
    -----
    You can use `%` operator for modulo-division. 
    `X % 2 == 0` stands for `is number X even?`

     */

    [TestClass]
    public class ExampleProblem : IOlympiadTask
    {
        private bool IsEven(int num)
        {
            return num % 2 == 0;
        }

        [TestMethod]
        public void Solve()
        {
            IsEven(2).ShouldBeTrue();
            IsEven(16).ShouldBeTrue();
            IsEven(0).ShouldBeTrue();

            IsEven(3).ShouldBeFalse();
        }
    }
}
