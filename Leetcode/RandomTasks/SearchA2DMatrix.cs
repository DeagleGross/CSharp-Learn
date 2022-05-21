using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// https://leetcode.com/problems/search-a-2d-matrix/

namespace LeetCodeSolutions.RandomTasks
{
	[TestClass]
	public class SearchA2DMatrix
	{
		[TestMethod]
		public void Solve()
		{
			int[][] matrix = new int[][]
			{
				new int[] {1, 3, 5, 7},
				new int[] {10, 11, 16, 20},
				new int[] {23, 30, 34, 60}
			};

			int target = 3;

			var result = SearchMatrix(matrix, target);

			result.Should().BeTrue();
		}

		[TestMethod]
		public void Solve2()
		{
			int[][] matrix = new int[][]
			{
				new int[] {1},
			};

			int target = 0;

			var result = SearchMatrix(matrix, target);

			result.Should().BeFalse();
		}

		public bool SearchMatrix(int[][] matrix, int target)
		{
			int r = 0;
			int[] candidateRow = null;
			while (r < matrix.Length)
			{
				var row = matrix[r];

				if (target >= row[0])
				{
					candidateRow = row;
				}
				else
				{
					break;
				}

				r++;
			}

			return candidateRow is not null && Array.BinarySearch(candidateRow, target) >= 0;
		}
	}
}
