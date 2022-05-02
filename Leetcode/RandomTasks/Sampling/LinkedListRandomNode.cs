using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// https://leetcode.com/problems/linked-list-random-node/

namespace LeetCodeSolutions.RandomTasks.Sampling
{
	[TestClass]
	public class LinkedListRandomNode
	{

		public class ListNode
		{
			public int val;
			public ListNode next;

			public ListNode(int val = 0, ListNode next = null)
			{
				this.val = val;
				this.next = next;
			}

			public ListNode(params int[] values)
			{
				val = values[0];
				if (values.Length == 1)
				{
					return;
				}

				ListNode tail = this;

				for (int i = 1; i < values.Length; i++)
				{
					var next = new ListNode(values[i]);
					tail.next = next;
					tail = next;
				}
			}
		}

		[TestMethod]
		public void Solve()
		{
			var nodes = new ListNode(new[] {1, 2, 3});

			var t = new Solution(nodes);

			var t1= t.GetRandom();
			var t2 = t.GetRandom();
			var t3 = t.GetRandom();
		}

		public class Solution
		{
			private ListNode _head;
			private Random _rnd = new Random(DateTime.Now.Millisecond);

			public Solution(ListNode head)
			{
				_head = head;
			}

			public int GetRandom()
			{
				int ret = 0;
				var nodesCount = 0;
				var currentNode = _head;

				while (currentNode != null)
				{
					// reservoir sampling
					var rnd = _rnd.Next(0, nodesCount + 1);
					if (rnd == 0)
					{
						ret = currentNode.val;
					}

					currentNode = currentNode.next;
					nodesCount++;
				}

				return ret;
			}
		}
	}
}
