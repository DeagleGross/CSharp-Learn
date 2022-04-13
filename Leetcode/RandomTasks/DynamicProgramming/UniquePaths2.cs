using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

// https://leetcode.com/problems/unique-paths-ii/

namespace LeetCodeSolutions.RandomTasks.DynamicProgramming
{
	[TestClass]
	public class UniquePaths2
	{
		[TestMethod]
		public void Solve()
		{
			int[][] input = new int[][]
			{
				new []{ 0, 0, 0 },
				new []{ 0, 1, 0 },
				new []{ 0, 0, 0 }
			};

			var result = UniquePathsWithObstacles(input);

			result.ShouldBe(2);
		}

		[TestMethod]
		public void Solve2()
		{
			int[][] input = new int[][]
			{
				new []{ 0, 1 },
				new []{ 0, 0 }
			};

			var result = UniquePathsWithObstacles(input);

			result.ShouldBe(1);
		}

		[TestMethod]
		public void Solve3()
		{
			int[][] input = new int[][]
			{
				new []{ 1, 0 }
			};

			var result = UniquePathsWithObstacles(input);

			result.ShouldBe(0);
		}

		[TestMethod]
		public void Solve4()
		{
			int[][] input = new int[][]
			{
				new []{ 0, 0 },
				new []{ 1, 1 },
				new []{ 0, 0 }
			};

			var result = UniquePathsWithObstacles(input);

			result.ShouldBe(0);
		}

		public int UniquePathsWithObstacles(int[][] obstacleGrid)
		{
			if (obstacleGrid[0][0] == 1)
			{
				// we are starting with an obstacle
				return 0;
			}

			var m = obstacleGrid.Length; // rows
			var n = obstacleGrid[0].Length; // cols

			// Number of ways of reaching the starting cell = 1.
			obstacleGrid[0][0] = 1;

			// Filling the values for the first column
			for (int row = 1; row < m; row++)
			{
				obstacleGrid[row][0] = (obstacleGrid[row][0] == 0 && obstacleGrid[row - 1][0] == 1)
					? 1
					: 0;
			}

			// Filling the values for the first row
			for (int col = 1; col < n; col++)
			{
				obstacleGrid[0][col] = (obstacleGrid[0][col] == 0 && obstacleGrid[0][col - 1] == 1)
					? 1
					: 0;
			}

			// Starting from cell(1,1) fill up the values
			// No. of ways of reaching cell[i][j] = cell[i - 1][j] + cell[i][j - 1]
			// i.e. From above and left.
			for (int i = 1; i < m; i++)
			{
				for (int j = 1; j < n; j++)
				{
					if (obstacleGrid[i][j] == 0)
					{
						obstacleGrid[i][j] = obstacleGrid[i - 1][j] + obstacleGrid[i][j - 1];
					}
					else
					{
						obstacleGrid[i][j] = 0;
					}
				}
			}

			// Return value stored in rightmost bottommost cell. That is the destination.
			return obstacleGrid[m - 1][n - 1];
		}
	}
}
