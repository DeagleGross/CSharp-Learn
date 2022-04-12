using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client.Payloads;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

// https://leetcode.com/problems/spiral-matrix/

namespace LeetCodeSolutions.RandomTasks
{
	[TestClass]
	public class SpiralMatrix
	{
		[TestMethod]
		public void Solve()
		{
			int[][] matrix = new int[][]
			{
				new[] {1, 2, 3},
				new[] {4, 5, 6},
				new[] {7, 8, 9},
			};

			var result = SpiralOrder(matrix);
			result.ShouldBe(new []{ 1, 2, 3, 6, 9, 8, 7, 4, 5 });
		}

		public IList<int> SpiralOrder(int[][] matrix)
		{
			var m = matrix.Length; // Y
			var n = matrix[0].Length; // X

			List<int> ret = new List<int>();
			int totalNumbers = m * n;

			int startX = 0;
			int startY = 0;
			int endX = n-1;
			int endY = m-1;

			while (ret.Count < totalNumbers)
			{
				ret.AddRange(GetFirstRow(matrix, startX, startY, endX, endY));
				startY++;

				ret.AddRange(GetLastCol(matrix, startX, startY, endX, endY));
				endX--;

				ret.AddRange(GetLastRowReversed(matrix, startX, startY, endX, endY));
				endY--;

				ret.AddRange(GetFirstColReversed(matrix, startX, startY, endX, endY));
				startX++;
			}

			return ret;
		}

		public IList<int> GetFirstRow(int[][] matrix, int startX, int startY, int endX, int endY)
		{
			List<int> ret = new();
			for (int col = startX; col <= endX; col++)
			{
				ret.Add(matrix[startY][col]);
			}

			return ret;
		}

		public IList<int> GetLastCol(int[][] matrix, int startX, int startY, int endX, int endY)
		{
			List<int> ret = new();
			for (int row = startY; row <= endY; row++)
			{
				ret.Add(matrix[row][endX]);
			}

			return ret;
		}

		public IList<int> GetLastRowReversed(int[][] matrix, int startX, int startY, int endX, int endY)
		{
			List<int> ret = new();
			for (int col = endX; col >= startX; col--)
			{
				ret.Add(matrix[endY][col]);
			}

			return ret;
		}

		public IList<int> GetFirstColReversed(int[][] matrix, int startX, int startY, int endX, int endY)
		{
			List<int> ret = new();
			for (int row = endY; row >= startY; row--)
			{
				ret.Add(matrix[row][startX]);
			}

			return ret;
		}
	}
}
