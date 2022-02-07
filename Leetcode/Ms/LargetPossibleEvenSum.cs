using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace LeetCodeSolutions.Ms
{
	[TestClass]
	public class LargetPossibleEvenSum
	{
		[DataTestMethod]
		[DataRow(new[]{4,9,8,2,6}, 3, 18)]
		[DataRow(new[]{5,6,3,4,2}, 5, 20)]
		[DataRow(new[]{7,7,7,7,7}, 1, -1)]
		[DataRow(new[]{10000}, 2, -1)]
		[DataRow(new[]{2,3,3,5,5}, 3, 12)]
		public void Solve(int[] input, int k, int result)
		{
			var sum = LargestPossibleSum(input, k);
			sum.ShouldBe(result);
		}

		public int LargestPossibleSum(int[] A, int K)
		{
			var sum = 0;

			if (K > A.Length)
			{
				return -1;
			}

			Stack<int> sortedEvens = new();
			Stack<int> sortedOdds = new();

			int elementsLeft = K;

			foreach (var i in A.OrderBy(a=>a))
			{
				if (i % 2 == 0)
				{
					sortedEvens.Push(i);
				}
				else
				{
					sortedOdds.Push(i);
				}
			}

			if (K == 1)
			{
				if (sortedEvens.Count == 0)
				{
					return -1;
				}
				
				return sortedEvens.Pop();
				
			}


			while (elementsLeft > 0)
			{
				var largestEven = sortedEvens.Count > 0 ? sortedEvens.Pop() : 0;
				var largestOdd = sortedOdds.Count > 0 ? sortedOdds.Pop() : 0;

				if (largestEven > largestOdd)
				{
					sum += largestEven;
					elementsLeft--;
					sortedOdds.Push(largestOdd);

					continue;
				}

				if (elementsLeft >= 2
					&& sortedOdds.Count >= 1)
				{
					sum += largestOdd + sortedOdds.Pop();
					elementsLeft -= 2;
					sortedEvens.Push(largestEven);
				}
				else
				{
					sum += largestEven;
					elementsLeft--;
					sortedOdds.Push(largestOdd);
				}
			}

			if (sum % 2 != 0)
			{
				return -1;
			}

			return sum;
		}
	}
}
