using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace LeetCodeSolutions.RandomTasks
{
	[TestClass]
	public class MergeIntervals
	{
		[TestMethod]
		public void Solve()
		{
			int[][] input = new int[][]
			{
				new []{ 1, 3 },
				new []{ 2, 6 },
				new []{ 8, 10 },
				new []{ 15, 18 }
			};

			var result = Merge(input);

			result.Length.ShouldBe(3);
		}

		public int[][] Merge(int[][] intervals)
		{
			List<int> starts = new();
			List<int> ends = new();

			for (int i = 0; i < intervals.Length; i++)
			{
				starts.Add(intervals[i][0]);
				ends.Add(intervals[i][1]);
			}

			starts.Sort();
			ends.Sort();

			int start = 0;
			int end = 0;

			int ovelappingIntervals = 0;

			List<int[]> ret = new List<int[]>();

			int mergedIntervalStart = starts[0];

			while (start < starts.Count)
			{
				var currentIntervalStart = starts[start];
				var currentIntervalEnd = ends[end];

				if (currentIntervalStart <= currentIntervalEnd)
				{
					ovelappingIntervals++;
					start++;
				}
				else
				{
					ovelappingIntervals--;
					end++;

					if(ovelappingIntervals == 0)
					{
						ret.Add(new[] {mergedIntervalStart, currentIntervalEnd});
						mergedIntervalStart = currentIntervalStart;
					}
				}
			}

			if (ovelappingIntervals > 0)
			{
				ret.Add(new []{mergedIntervalStart, ends[^1]});
			}

			return ret.ToArray();
		}
	}
}
