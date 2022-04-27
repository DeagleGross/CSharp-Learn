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
	public class MeetingRooms2
	{
		[TestMethod]
		public void Solve()
		{
			int[][] input = new int[][]
			{
				new []{ 0, 30 },
				new []{ 5, 10 },
				new []{ 15, 20 }
			};

			var result = MinMeetingRooms(input);

			result.ShouldBe(2);
		}

		public int MinMeetingRooms(int[][] intervals)
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

			int ret = 0;

			int currentConfs = 0;

			while (start < starts.Count)
			{
				var currentStart = starts[start];
				var currentEnd = ends[end];

				if (currentStart < currentEnd)
				{
					currentConfs++;
					ret = Math.Max(currentConfs, ret);
					start++;
				}
				else
				{
					currentConfs--;
					end++;
				}
			}

			return ret;
		}
		
	}
}
