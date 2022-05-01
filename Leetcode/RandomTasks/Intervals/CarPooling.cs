using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

// https://leetcode.com/problems/car-pooling/

namespace LeetCodeSolutions.RandomTasks.Intervals
{
	[TestClass]
	public class CarPooling1
	{
		[TestMethod]
		public void Solve()
		{
			int[][] input = new int[][]
			{
				new []{ 2,1,5 },
				new []{ 3,3,7 }
			};

			int capacity = 4;

			var result = CarPooling(input, capacity);

			result.ShouldBe(false);
		}

		[TestMethod]
		public void Solve2()
		{
			int[][] input = new int[][]
			{
				new []{ 2,1,5 },
				new []{ 3,3,7 }
			};

			int capacity = 5;

			var result = CarPooling(input, capacity);

			result.ShouldBe(true);
		}

		class TripPoint
		{
			public int KmPoint;
			public int NumPassengers;
		}

		public bool CarPooling(int[][] trips, int capacity)
		{
			List<TripPoint> starts = new();
			List<TripPoint> ends = new();

			for (int trip = 0; trip < trips.Length; trip++)
			{
				starts.Add(new TripPoint()
				{
					KmPoint = trips[trip][1],
					NumPassengers = trips[trip][0]
				});

				ends.Add(new TripPoint() {
					KmPoint = trips[trip][2],
					NumPassengers = trips[trip][0]
				});
			}

			starts = starts.OrderBy(v => v.KmPoint).ToList();
			ends = ends.OrderBy(v => v.KmPoint).ToList();

			var start = 0;
			var end = 0;

			var currentCapacity = 0;

			while (start < trips.Length)
			{
				var currentStart = starts[start];
				var currentEnd = ends[end];

				if (currentStart.KmPoint < currentEnd.KmPoint)
				{
					// ride started
					currentCapacity += currentStart.NumPassengers;
					if (currentCapacity > capacity)
					{
						return false;
					}

					start++;
				}
				else
				{
					// some ride has ended
					currentCapacity -= currentEnd.NumPassengers;

					end++;
				}
			}

			return true;
		}
	}
}
