using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

// https://leetcode.com/problems/unique-paths/

namespace LeetCodeSolutions.DynamicProgramming
{
	[TestClass]
	public class UniquePaths
	{
		[TestMethod]
		public void Solve()
		{
			var m = 3; // rows
			var n = 7; // cols
			var uniquePaths = FindMaxUniquePaths_Dynamic(m, n);

			uniquePaths.ShouldBe(28);
		}

		public int FindMaxUniquePaths_Recursive(int m, int n)
		{
			if (m == 1 || n == 1)
			{
				return 1;
			}

			return FindMaxUniquePaths_Recursive(m - 1, n) + FindMaxUniquePaths_Recursive(m, n - 1);
		}

		public int FindMaxUniquePaths_Dynamic(int m, int n)
		{
			int[,] map = new int[m,n]; // m rows, n cols

			for (int row = 0; row < m; row++)
			{
				for (int col = 0; col < n; col++)
				{
					map[row, col] = 1;
				}
			}

			for (int row = 1; row < m; row++)
			{
				for (int col = 1; col < n; col++)
				{
					map[row, col] = map[row-1, col] + map[row, col-1];
				}
			}

			return map[m - 1, n - 1];
		}
	}
}
