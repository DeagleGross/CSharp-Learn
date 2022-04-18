using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

// https://leetcode.com/problems/word-search/

namespace LeetCodeSolutions.RandomTasks
{
	[TestClass]
	public class WordSearch
	{
		[TestMethod]
		public void Solve()
		{
			char[][] board = new char[][]
			{
				new []{ 'A', 'B', 'C', 'E' },
				new []{ 'S', 'F', 'C', 'S' },
				new []{ 'A', 'D', 'E', 'E' }
			};

			string word = "ABCCED";

			var result = Exist(board, word);

			result.ShouldBe(true);
		}

		[TestMethod]
		public void Solve2()
		{
			char[][] board = new char[][]
			{
				new []{ 'A', 'B', 'C', 'E' },
				new []{ 'S', 'F', 'C', 'S' },
				new []{ 'A', 'D', 'E', 'E' }
			};

			string word = "ABCB";

			var result = Exist(board, word);

			result.ShouldBe(false);
		}

		[TestMethod]
		public void Solve3()
		{
			char[][] board = new char[][]
			{
				new []{ 'a'},
			};

			string word = "a";

			var result = Exist(board, word);

			result.ShouldBe(true);
		}

		[TestMethod]
		public void Solve4()
		{
			char[][] board = new char[][]
			{
				new []{ 'a', 'b'},
				new []{ 'c', 'd'},
			};

			string word = "acdb";

			var result = Exist(board, word);

			result.ShouldBe(true);
		}

		[TestMethod]
		public void Solve5()
		{
			char[][] board = new char[][]
			{
				new []{ 'A', 'B', 'C', 'E' },
				new []{ 'S', 'F', 'E', 'S' },
				new []{ 'A', 'D', 'E', 'E' }
			};

			string word = "ABCESEEEFS";

			var result = Exist(board, word);

			result.ShouldBe(true);
		}

		private int _rows;
		private int _cols;

		public bool Exist(char[][] board, string word)
		{
			_rows = board.Length;
			_cols = board[0].Length;

			bool[,] visited = new bool[_rows, _cols];
			
			for (int row = 0; row < _rows; row++)
			{
				for (var col = 0; col < _cols; col++)
				{
					if (board[row][col] == word[0])
					{
						if (word.Length == 1)
						{
							return true;
						}

						Array.Clear(visited);

						var wordFound = SearchChar(board, row, col, word, 0, visited);
						if (wordFound)
						{
							return true;
						}
					}
				}
			}

			return false;
		}

		public bool SearchChar(char[][] board, int row, int col, string word, int wordPosition, bool[,] visited)
		{
			if (row < 0
				|| row > _rows-1)
			{
				return false;
			}

			if (col < 0
				|| col > _cols - 1)
			{
				return false;
			}

			if (wordPosition > word.Length - 1)
			{
				return true; // maybe false
			}

			if (visited[row, col] == true)
			{
				return false;
			}

			if (board[row][col] != word[wordPosition])
			{
				return false;
			}


			visited[row, col] = true; // set only correct word letters visited

			var f1 = SearchChar(board, row, col + 1, word, wordPosition + 1, visited);
			var f2 = SearchChar(board, row, col - 1, word, wordPosition + 1, visited);
			var f3 = SearchChar(board, row + 1, col, word, wordPosition + 1, visited);
			var f4 = SearchChar(board, row - 1, col, word, wordPosition + 1, visited);

			visited[row, col] = false; // revert visited on this search path

			return f1 || f2|| f3 || f4;
		}
	}
}
