using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// https://leetcode.com/problems/longest-mountain-in-array/

namespace LeetCodeSolutions.RandomTasks.TwoPointers
{
	[TestClass]
	public class LongestMountainInArray
	{
		[TestMethod]
		public void Solve()
		{
			int[] arr = new[] { 2, 1, 4, 7, 3, 2, 5 };
			var result = LongestMountain(arr);

			result.Should().Be(5);
		}

		[TestMethod]
		public void Solve2()
		{
			int[] arr = new[] { 2,2,2};
			var result = LongestMountain(arr);

			result.Should().Be(0);
		}
		
		[TestMethod]
		public void Solve3()
		{
			int[] arr = new[] { 0, 1, 2, 3, 4, 5, 4, 3, 2, 1, 0};
			var result = LongestMountain(arr);

			result.Should().Be(11);
		}

		[TestMethod]
		public void Solve4()
		{
			int[] arr = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
			var result = LongestMountain(arr);

			result.Should().Be(0);
		}

		[TestMethod]
		public void Solve5()
		{
			int[] arr = new[] { 0, 2,2 };
			var result = LongestMountain(arr);

			result.Should().Be(0);
		}

		public int LongestMountain(int[] arr)
		{
			if (arr.Length < 3)
			{
				return 0;
			}

			var start = 1;

			int length = 0;

			while (start < arr.Length)
			{
				if (arr[start - 1] >= arr[start])
				{
					start++;
				}
				else
				{
					var mountainPeak = FindLeftSlope(arr, start);
					var rightSlopeEnd = FindRightSlope(arr, mountainPeak);

					if (rightSlopeEnd == -1)
					{
						// means no right slope
						return length;
					}

					if (rightSlopeEnd == mountainPeak)
					{
						// emans no left slope
						start = rightSlopeEnd + 1;
						continue;
					}

					// +1 for slope start index adjustment since it's (realSlopeStart + 1)
					// +1 for the last element in length
					length = Math.Max(length, (rightSlopeEnd - start + 1 + 1));
					start = rightSlopeEnd + 1;
				}
			}

			return length;
		}

		public int FindLeftSlope(int[] arr, int start)
		{
			int right = start+1;
			int peak = start;
			while (right < arr.Length)
			{
				if (arr[right - 1] < arr[right])
				{
					peak = right;
				}
				else
				{
					break;
				}

				right++;
			}

			// return left slope end
			return peak;
		}

		public int FindRightSlope(int[] arr, int start)
		{
			int right = start + 1;

			if (right >= arr.Length)
			{
				return -1;
			}

			int leftSlopeEnd = start;
			while (right < arr.Length)
			{
				if (arr[right - 1] > arr[right])
				{
					leftSlopeEnd = right;
				}
				else
				{
					break;
				}

				right++;
			}

			return leftSlopeEnd;
		}
	}
}
