using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// https://leetcode.com/problems/cinema-seat-allocation/

namespace LeetCodeSolutions.RandomTasks
{
	[TestClass]
	public class CinemaSeatAllocation
	{
		[TestMethod]
		public void Solve()
		{
			int n = 3;
			int[][] reservedSeats = new[]
			{
				new[] { 1,2},
				new[] { 1,3},
				new[] { 1,8},
				new[] { 2,6},
				new[] { 3,1},
				new[] { 3,10},
			};

			var result = MaxNumberOfFamilies(n, reservedSeats);

			result.Should().Be(4);
		}

		[TestMethod]
		public void Solve2()
		{
			int n = 4;
			int[][] reservedSeats = new[]
			{
				new[] { 4,3},
				new[] { 1,4},
				new[] { 4,6},
				new[] { 1,7},
			};

			var result = MaxNumberOfFamilies(n, reservedSeats);

			result.Should().Be(4);
		}

		[TestMethod]
		public void Solve3()
		{
			int n = 5;
			int[][] reservedSeats = new[]
			{
				new[] { 4,7},
				new[] { 4,1},
				new[] { 3,1},
				new[] { 5,9},
				new[] { 4,4},
				new[] { 3,7},
				new[] { 1,3},
				new[] { 5,5},
				new[] { 1,6},
				new[] { 1,8},
				new[] { 3,9},
				new[] { 2,9},
				new[] { 1,4},
				new[] { 1,9},
				new[] { 1,10},
			};

			var result = MaxNumberOfFamilies(n, reservedSeats);

			result.Should().Be(2);
		}

		public int MaxNumberOfFamilies(int n, int[][] reservedSeats)
		{
			Dictionary<int, HashSet<int>> reserved = new();

			for (int i = 0; i < reservedSeats.Length; i++)
			{
				var row = reservedSeats[i][0];
				var seat = reservedSeats[i][1];

				reserved.TryAdd(row, new HashSet<int>());

				reserved[row].Add(seat);
			}

			var ret = 2 * (n - reserved.Count); // all rows that are not in reserved are considered having 2 groups of 4

			foreach (var kv in reserved)
			{
				var reservedSeatsOnRow = kv.Value;

				bool hasSplitGroup = false;

				// check left

				if (!reservedSeatsOnRow.Contains(2)
					&& !reservedSeatsOnRow.Contains(3)
					&& !reservedSeatsOnRow.Contains(4)
					&& !reservedSeatsOnRow.Contains(5))
				{
					ret++;
					hasSplitGroup = true;
				}

				// check right

				if (!reservedSeatsOnRow.Contains(6)
					&& !reservedSeatsOnRow.Contains(7)
					&& !reservedSeatsOnRow.Contains(8)
					&& !reservedSeatsOnRow.Contains(9))
				{
					ret++;
					hasSplitGroup = true;
				}

				if (hasSplitGroup == false
					&& !reservedSeatsOnRow.Contains(4)
					&& !reservedSeatsOnRow.Contains(5)
					&& !reservedSeatsOnRow.Contains(6)
					&& !reservedSeatsOnRow.Contains(7))
				{
					ret++;
				}
			}

			return ret;
		}

		public int MaxNumberOfFamilies_Suboptimal(int n, int[][] reservedSeats)
		{
			Dictionary<int, HashSet<int>> reserved = new();

			for (int i = 0; i < reservedSeats.Length; i++)
			{
				var row = reservedSeats[i][0]; 
				var seat = reservedSeats[i][1];

				reserved.TryAdd(row, new HashSet<int>());

				reserved[row].Add(seat);
			}

			// ROW : xxx xxxxx xxx

			int ret = 0;

			for (int row = 1; row <= n; row++)
			{
				// for each row
				if (!reserved.ContainsKey(row))
				{
					// row is free
					ret += 2;
					continue;
				}

				var reservedSeatsOnRow = reserved[row];

				// left part

				reservedSeatsOnRow.Add(1);

				if (reservedSeatsOnRow.Contains(3))
				{
					reservedSeatsOnRow.Add(2);
				}

				if (reservedSeatsOnRow.Contains(2))
				{
					reservedSeatsOnRow.Add(3);
				}

				// right part

				reservedSeatsOnRow.Add(10);

				if (reservedSeatsOnRow.Contains(8))
				{
					reservedSeatsOnRow.Add(9);
				}

				if (reservedSeatsOnRow.Contains(9))
				{
					reservedSeatsOnRow.Add(8);
				}

				// center

				if (reservedSeatsOnRow.Contains(5))
				{
					reservedSeatsOnRow.Add(4);
				}

				if (reservedSeatsOnRow.Contains(4))
				{
					reservedSeatsOnRow.Add(5);
					reservedSeatsOnRow.Add(2);
					reservedSeatsOnRow.Add(3);
				}
				
				if (reservedSeatsOnRow.Contains(6))
				{
					reservedSeatsOnRow.Add(7);
				}

				if (reservedSeatsOnRow.Contains(7))
				{
					reservedSeatsOnRow.Add(6);
					reservedSeatsOnRow.Add(8);
					reservedSeatsOnRow.Add(9);
				}

				var groups = (10 - reservedSeatsOnRow.Count) / 4;

				ret += groups;
			}

			return ret;
		}
	}
}
