using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

// https://leetcode.com/problems/number-of-islands/

namespace LeetCodeSolutions.RandomTasks
{
	[TestClass]
	public class NumberOfIslands
	{
		[TestMethod]
		public void Solve()
		{
			char[][] input = new char[][]
			{
				new []{'1', '1', '1', '1', '0'},
				new []{'1', '1', '0', '1', '0'},
				new []{'1', '1', '0', '0', '0'},
				new []{'0', '0', '0', '0', '0'}
			};

			var result = NumIslands(input);

			result.ShouldBe(1);
		}

		private int _rows;
		private int _cols;

		public int NumIslands(char[][] grid)
		{
			var maxRows = grid.Length;
			var maxCols = grid[0].Length;

			_rows = maxRows;
			_cols = maxCols;

			int islandCount = 0;

			for (int row = 0; row < maxRows; row++)
			{
				for (int col = 0; col < maxCols; col++)
				{
					if (grid[row][col] == '1')
					{
						islandCount++;
						SearchIsland(grid, row, col);
					}
				}
			}

			return islandCount;
		}

		private void SearchIsland(char[][] grid, int row, int col)
		{
			if (row < 0
				|| row >= _rows)
			{
				return;
			}

			if (col < 0
				|| col >= _cols)
			{
				return;
			}

			if (grid[row][col] == 'X' || grid[row][col] == '0')
			{
				return;
			}

			grid[row][col] = 'X';

			SearchIsland(grid, row-1, col);
			SearchIsland(grid, row, col-1);
			SearchIsland(grid, row+1, col);
			SearchIsland(grid, row, col+1);
		}
	}
}
