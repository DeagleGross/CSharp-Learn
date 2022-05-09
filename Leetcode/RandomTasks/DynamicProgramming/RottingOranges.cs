using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// https://leetcode.com/problems/rotting-oranges/

namespace LeetCodeSolutions.RandomTasks.DynamicProgramming
{
	[TestClass]
	public class RottingOranges
	{
		[TestMethod]
		public void Solve()
		{
			int[][] grid = new int[][]
			{
				new []{2,1,1},
				new []{1,1,0},
				new []{0,1,1},
			};

			var result = OrangesRotting(grid);

			result.Should().Be(4);
		}

		[TestMethod]
		public void Solve2()
		{
			int[][] grid = new int[][]
			{
				new []{2,1,1},
				new []{0,1,1},
				new []{1,0,1},
			};

			var result = OrangesRotting(grid);

			result.Should().Be(-1);
		}

		[TestMethod]
		public void Solve3()
		{
			int[][] grid = new int[][]
			{
				new []{0,2}
			};

			var result = OrangesRotting(grid);

			result.Should().Be(0);
		}
		
		[TestMethod]
		public void Solve4()
		{
			int[][] grid = new int[][]
			{
				new []{2,1,1},
				new []{1,1,1},
				new []{0,1,2},
			};

			var result = OrangesRotting(grid);

			result.Should().Be(2);
		}

		private int[][] _rottenGrid;
		private bool[][] _visitedGrid;

		public int OrangesRotting(int[][] grid)
		{
			_rottenGrid = new int[grid.Length][];
			for (int i = 0; i < grid.Length; i++)
			{
				_rottenGrid[i] = new int[grid[0].Length];
			}
			
			_visitedGrid = new bool[grid.Length][];
			for (int i = 0; i < grid.Length; i++)
			{
				_visitedGrid[i] = new bool[grid[0].Length];
			}

			Queue<(int row, int col)> rotten = new();

			int freshCount = 0;

			for (int row = 0; row < grid.Length; row++)
			{
				for (int col = 0; col < grid[0].Length; col++)
				{
					if (grid[row][col] == 2)
					{
						// found rotten orange - enqueue it

						_visitedGrid[row][col] = true;
						
						rotten.Enqueue((row, col));
					}

					if (grid[row][col] == 1)
					{
						freshCount++;
					}
				}
			}

			if (freshCount == 0)
			{
				return 0;
			}

			int minute = 0;

			while (rotten.Count > 0)
			{
				Queue<(int row, int col)> newRotten = new();

				while (rotten.Count > 0)
				{
					var r = rotten.Dequeue();

					Rot(grid, r.row + 1, r.col, newRotten);
					Rot(grid, r.row - 1, r.col, newRotten);
					Rot(grid, r.row, r.col + 1, newRotten);
					Rot(grid, r.row, r.col - 1, newRotten);
				}

				if (newRotten.Count > 0)
				{
					freshCount -= newRotten.Count;
					minute++;
				}

				rotten = newRotten;
			}

			
			return freshCount == 0 ? minute : -1;
		}

		public bool Rot(
			int[][] grid,
			int row,
			int col,
			Queue<(int row, int col)> newRotten
			)
		{
			if (row >= grid.Length
				|| row < 0)
			{
				return false;
			}

			if (col >= grid[0].Length
				|| col < 0)
			{
				return false;
			}

			if (_visitedGrid[row][col] == true)
			{
				// already visited
				return false;
			}

			_visitedGrid[row][col] = true;

			if (grid[row][col] == 1)
			{
				newRotten.Enqueue((row, col));
				return true;
			}

			return false;
		}
	}
}
