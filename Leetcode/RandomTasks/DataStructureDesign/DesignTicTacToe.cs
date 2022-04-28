using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace LeetCodeSolutions.RandomTasks.DataStructureDesign
{
	[TestClass]
	public class DesignTicTacToe
	{

		[TestMethod]
		public void Solve()
		{
			TicTacToe ticTacToe = new TicTacToe(3);

			var t1= ticTacToe.Move(0, 0, 1); // return 0 (no one wins)

			t1.ShouldBe(0);
			
			var t2 = ticTacToe.Move(0, 2, 2); // return 0 (no one wins)

			t2.ShouldBe(0);

			var t3 = ticTacToe.Move(2, 2, 1); // return 0 (no one wins)

			t3.ShouldBe(0);

			var t4 = ticTacToe.Move(1, 1, 2); // return 0 (no one wins)

			t4.ShouldBe(0);

			var t5 = ticTacToe.Move(2, 0, 1); // return 0 (no one wins)

			t5.ShouldBe(0);

			var t6 = ticTacToe.Move(1, 0, 2); // return 0 (no one wins)

			t5.ShouldBe(0);

			var t7 = ticTacToe.Move(2, 1, 1); // return 1 (player 1 wins)

			t7.ShouldBe(1);
		}

		public class TicTacToe
		{
			private readonly List<int> _rows;
			private readonly List<int> _cols;
			// 0 - main diagonal, 1 - secondary diagonal
			private readonly List<int> _diagonals;

			private readonly int _gridSize;
			private int? _winningPlayer = null;

			public TicTacToe(int n)
			{
				_gridSize = n;
				_rows = new List<int>(n);
				_cols = new List<int>(n);
				_diagonals = new List<int>(2){0,0};

				Fill(_rows);
				Fill(_cols);

				void Fill(List<int> collection)
				{
					for (int i = 0; i < n; i++)
					{
						collection.Add(0);
					}
				}
			}

			public int Move(int row, int col, int player)
			{
				if (_winningPlayer is not null)
				{
					return _winningPlayer.Value;
				}

				var playerValue = player == 1
					? 1
					: -1;

				_rows[row] += playerValue;
				if (Math.Abs(_rows[row]) == _gridSize)
				{
					_winningPlayer = player;
					return player;
				}

				_cols[col] += playerValue;
				if (Math.Abs(_cols[col]) == _gridSize)
				{
					_winningPlayer = player;
					return player;
				}

				var isMainDiagonal = row == col;
				if (isMainDiagonal)
				{
					_diagonals[0] += playerValue;
				}

				if (Math.Abs(_diagonals[0]) == _gridSize)
				{
					_winningPlayer = player;
					return player;
				}

				var isSecondaryDiagonal = row + col == _gridSize - 1;
				if (isSecondaryDiagonal)
				{
					_diagonals[1] += playerValue;
				}

				if (Math.Abs(_diagonals[1]) == _gridSize)
				{
					_winningPlayer = player;
					return player;
				}

				return 0;
			}
		}
	}
}
