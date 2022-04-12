using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

// https://leetcode.com/problems/minimum-path-sum/

namespace LeetCodeSolutions.DynamicProgramming
{
	[TestClass]
	public class MinimumPathSum
	{
		[TestMethod]
		public void Solve()
		{
			int[][] input = new int[][]
			{
				new []{ 1, 3, 1 },
				new []{ 1, 5, 1 },
				new []{ 4, 2, 1 }
			};

			var output = MinPathSum(input);

			output.ShouldBe(7);
		}

		[TestMethod]
		public void Solve2()
		{
			int[][] input = new int[][]
			{
				new []{ 1, 2, 3 },
				new []{ 4, 5, 6 }
			};

			var output = MinPathSum(input);

			output.ShouldBe(12);
		}

		private int Sum_BruteForce(int[][] grid, int i, int j)
		{
			// check out of bounds
			if (i >= _rows
				|| j >= _cols)
			{
				// returning max value for Math.Min to not return sum for this path
				return int.MaxValue;
			}

			// check border cell
			if (i == _rows - 1
				&& j == _cols - 1)
			{
				return grid[i][j];
			}

			return grid[i][j] + Math.Min(Sum_BruteForce(grid, i + 1, j), Sum_BruteForce(grid, i, j + 1));
		}

		private int _rows;
		private int _cols;

		public int MinPathSum(int[][] grid)
		{
			_rows = grid.Length;
			_cols = grid[0].Length;
			
			// trick is to go from the bottom right of the matrix to the top left
			for (int row = _rows - 1; row >= 0; row--)
			{
				for (int col = _cols - 1; col >= 0; col--)
				{
					if (row == _rows - 1
						&& col == _cols - 1)
					{
						// we don't need to process case when we are exactly in the bottom left cell
						// since it's path sum is already calculated - it equals the csll content

						continue;
					}
					else if (row == _rows - 1 
							&& col != _cols - 1)
					{
						// when we are on the bottom boundary of the matrix

						grid[row][col] = grid[row][col] + grid[row][col + 1];
					}
					else if (row != _rows - 1 
							&& col == _cols - 1)
					{
						// when we are on the right boundary of the matrix

						grid[row][col] = grid[row][col] + grid[row+1][col];
					}
					else if (row != _rows - 1 
							&& col != _cols - 1)
					{
						// case when we are not on the boundaries of a matrix

						grid[row][col] = grid[row][col] + Math.Min(grid[row + 1][col], grid[row][col + 1]);
					}
				}
			}

			return grid[0][0];
		}
	}
}
