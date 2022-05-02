using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

// https://leetcode.com/problems/unique-binary-search-trees/

namespace LeetCodeSolutions.RandomTasks.DynamicProgramming
{
	[TestClass]
	public class UniqueBinarySearchTrees
	{
		[TestMethod]
		public void Solve()
		{
			var input = 3;

			var result = NumTrees(input);

			result.ShouldBe(5);
		}

		public int NumTrees(int n)
		{
			int[] numberOfBstsWithSpecificRoot = new int[n + 1];
			numberOfBstsWithSpecificRoot[0] = 1;
			numberOfBstsWithSpecificRoot[1] = 1;

			for (int i = 2; i <= n; ++i)
			{
				for (int j = 1; j <= i; ++j)
				{
					numberOfBstsWithSpecificRoot[i] +=
						numberOfBstsWithSpecificRoot[j - 1] * numberOfBstsWithSpecificRoot[i - j];
				}
			}
			return numberOfBstsWithSpecificRoot[n];
		}
	}
}
