using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// https://leetcode.com/problems/search-a-2d-matrix-ii/

namespace LeetCodeSolutions.RandomTasks
{
	[TestClass]
	public class SearchInA2DMatrix2
	{
		[TestMethod]
		public void Solve()
		{
			int[][] matrix = new int[][]
			{
				new int[] {1, 4, 7, 11, 15},
				new int[] {2, 5, 8, 12, 19},
				new int[] {3, 6, 9, 16, 22},
				new int[] {10, 13, 14, 17, 24},
				new int[] {18, 21, 23, 26, 30},
			};

			int target = 5;

			var result = SearchMatrix(matrix, target);

			result.Should().BeTrue();
		}

		public bool SearchMatrix(int[][] matrix, int target)
		{
			// find candidate rows
			// search each row using BSearch

			List<int[]> candidateRows = new();

			int i = 0;
			
			// we can further optimize and use binary search to search for the candidate rows in O(log n) time

			while (i < matrix.Length)
			{
				var row = matrix[i];
				if (target >= row[0])
				{
					candidateRows.Add(row);
					i++;
				}
				else
				{
					break;
				}
			}

			foreach (var candidateRow in candidateRows)
			{
				var position = Array.BinarySearch(candidateRow, target);
				if (position >= 0)
				{
					return true;
				}
			}

			return false;
		}
	}
}
