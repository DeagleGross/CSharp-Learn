using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

//https://leetcode.com/problems/surrounded-regions/

namespace LeetCodeSolutions.RandomTasks
{
	[TestClass]
	public class SurroundedRegions
	{
		[TestMethod]
		public void SolveTest()
		{
			char[][] input = new char[][]
			{
				new []{'X', 'X', 'X', 'X'}, 
				new []{'X', 'O', 'O', 'X'}, 
				new []{'X', 'X', 'O', 'X'}, 
				new []{'X', 'O', 'X', 'X'}
			};

			Solve(input);
		}

		private int _rows;
		private int _cols;

		public void Solve(char[][] board)
		{
			if (board == null || board.Length == 0)
			{
				return;
			}

			_rows = board.Length;
			_cols = board[0].Length;

			List<(int row,int col)> borders = new();

			// Step 1). construct the list of border cells

			for (int r = 0; r < _rows; ++r)
			{
				// top border
				borders.Add((r, 0));

				// bottom border
				borders.Add((r, _cols - 1));
			}

			for (int c = 0; c < _cols; ++c)
			{
				// left border
				borders.Add((0, c));

				// right border
				borders.Add((_rows - 1, c));
			}

			// Step 2). mark the escaped cells

			foreach (var borderCell in borders)
			{
				Dfs(board, borderCell.row, borderCell.col);

				//NOTE: alternative solution - use BFS
				//Bfs(board, pair.row, pair.col);
			}

			// Step 3). flip the cells to their correct final states
			for (int r = 0; r < _rows; ++r)
			{
				for (int c = 0; c < _cols; ++c)
				{
					if (board[r][c] == 'O')
					{
						board[r][c] = 'X';
					}

					if (board[r][c] == 'E')
					{
						board[r][c] = 'O';
					}
				}
			}
		}

		private void Dfs(char[][] board, int row, int col)
		{
			if (board[row][col] != 'O')
			{
				return;
			}

			board[row][col] = 'E';
			if (col < _cols - 1)
			{
				Dfs(board, row, col + 1);
			}

			if (row < _rows - 1)
			{
				Dfs(board, row + 1, col);
			}

			if (col > 0)
			{
				Dfs(board, row, col - 1);
			}

			if (row > 0)
			{
				Dfs(board, row - 1, col);
			}
		}

		private void Bfs(char[][] board, int r, int c)
		{
			Queue<(int row, int col)> queue = new();

			queue.Enqueue((r, c));

			while (queue.Count > 0)
			{
				var pair = queue.Dequeue();

				if (board[pair.row][pair.col] != 'O')
				{
					continue;
				}

				board[pair.row][pair.col] = 'E';

				if (pair.col < _cols - 1)
				{
					queue.Enqueue((pair.row, pair.col + 1));
				}

				if (pair.row < _rows - 1)
				{
					queue.Enqueue((pair.row + 1, pair.col));
				}

				if (pair.col > 0)
				{
					queue.Enqueue((pair.row, pair.col - 1));
				}

				if (pair.row > 0)
				{
					queue.Enqueue((pair.row - 1, pair.col));
				}
			}
		}
	}
}
