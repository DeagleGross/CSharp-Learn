using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

// https://leetcode.com/problems/design-hit-counter/

namespace LeetCodeSolutions.RandomTasks.DataStructureDesign
{
	[TestClass]
	public class DesignHitCounter
	{
		[TestMethod]
		public void Solve()
		{
			HitCounter obj = new();

			obj.Hit(1);
			obj.Hit(2);
			obj.Hit(3);

			var t1 = obj.GetHits(4); // hits at timestamp [4-300, 4] return 3

			t1.ShouldBe(3);

			obj.Hit(300);     // hit at timestamp 300.
			var t2 = obj.GetHits(300); // get hits at timestamp 300, return 4.

			t2.ShouldBe(4);

			var t3 = obj.GetHits(301); // get hits at timestamp 301, return 3.

			t3.ShouldBe(3);
		}

		[TestMethod]
		public void Solve2()
		{
			HitCounter obj = new();

			obj.Hit(1);
			obj.Hit(1);
			obj.Hit(1);
			obj.Hit(300);

			var t1 = obj.GetHits(300); // hits at timestamp [4-300, 4] return 3

			t1.ShouldBe(4);

			obj.Hit(300);

			var t2 = obj.GetHits(300); // get hits at timestamp 300, return 4.
			t2.ShouldBe(5);

			obj.Hit(301);     // hit at timestamp 300.
			
			var t3 = obj.GetHits(301); // get hits at timestamp 301, return 3.

			t3.ShouldBe(3);
		}

		public class HitCounter
		{
			//A key observation here is that all the timestamps that we will encounter are going to be in increasing order.
			//Also as the timestamps' value increases we have to ignore the timestamps that occurred previously
			//(having a difference of 300 or more with the latest timestamp).
			//This is the case of considering the elements in the FIFO manner (First in first out)
			//which is best solved by using the "queue" data structure.
			private readonly Queue<int> _hits;

			public HitCounter()
			{
				_hits = new Queue<int>();
			}

			public void Hit(int timestamp)
			{
				_hits.Enqueue(timestamp);
			}

			public int GetHits(int timestamp)
			{
				var leftBound = timestamp - 300;
				if (leftBound < 0)
				{
					leftBound = 0;
				}

				if (_hits.Count == 0)
				{
					return 0;
				}

				var head = _hits.Peek();

				while (head <= leftBound && _hits.Count > 0)
				{
					_hits.Dequeue();

					if (_hits.Count == 0)
					{
						break;	
					}

					head = _hits.Peek();
				}
				
				return _hits.Count;
			}
		}

		public class HitCounter1
		{
			private readonly Dictionary<int, int> _hits;


			public HitCounter1()
			{
				_hits = new Dictionary<int, int>();
			}

			public void Hit(int timestamp)
			{
				if (!_hits.ContainsKey(timestamp))
				{
					_hits[timestamp] = 1;
				}
				else
				{

					_hits[timestamp] += 1;
				}
			}

			public int GetHits(int timestamp)
			{
				var simpleSum = 0;

				var leftBound = timestamp - 300;
				if (leftBound < 0)
				{
					leftBound = 0;
				}
				// means we are in one interval 
				for (int i = timestamp; i > leftBound; i--)
				{
					if (_hits.ContainsKey(i))
					{
						simpleSum += _hits[i];
					}
				}

				return simpleSum;
			}
		}
	}
}
