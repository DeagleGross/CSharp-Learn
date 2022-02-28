using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// https://leetcode.com/problems/rotate-image/
/*
The most elegant solution for rotating the matrix is to firstly reverse the matrix around the main diagonal, and then reverse it from left to right. 
These operations are called transpose and reflect in linear algebra.
*/

namespace LeetCodeSolutions.RandomTasks
{
	[TestClass]
	public class RotateImage
	{
		[TestMethod]
		public void Solve()
		{
			int[][] input =
			{
				new int[]{5, 1, 9, 11},
				new int[]{2, 4, 8, 10},
				new int[]{13, 3, 6, 7},
				new int[]{15, 14, 12, 16}
			};

			Rotate(input);
		}

		public void Rotate(int[][] matrix)
		{
			Transpose(matrix);
			Reflect(matrix);
		}

		private void Transpose(int[][] matrix)
		{
			int n = matrix.Length;
			for (int i = 0; i < n; i++)
			{
				for (int j = i + 1; j < n; j++)
				{
					int tmp = matrix[j][i];
					matrix[j][i] = matrix[i][j];
					matrix[i][j] = tmp;
				}
			}
		}

		private void Reflect(int[][] matrix)
		{
			int n = matrix.Length;
			for (int i = 0; i < n; i++)
			{
				for (int j = 0; j < n / 2; j++)
				{
					int tmp = matrix[i][j];
					matrix[i][j] = matrix[i][n - j - 1];
					matrix[i][n - j - 1] = tmp;
				}
			}
		}
	}
}
