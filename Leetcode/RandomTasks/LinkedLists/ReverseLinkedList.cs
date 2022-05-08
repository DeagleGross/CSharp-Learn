using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// https://leetcode.com/problems/reverse-linked-list/

namespace LeetCodeSolutions.RandomTasks.LinkedLists
{
	[TestClass]
	public class ReverseLinkedList
	{
		#region Service

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

			public override string ToString()
			{
				return $"Val:{val}, Next: {next?.val}";
			}

			public string ToStringAll()
			{
				int power = 0;
				ListNode current = this;
				int ret = val;
				while (current.next != null)
				{
					power++;
					current = current.next;
					ret += current.val * (int)Math.Pow(10, power);
				}

				return ret.ToString();
			}
		}

		private int ListToInt(ListNode head)
		{
			var ret = 0;

			ListNode current = head;

			while (current != null)
			{
				ret = ret * 10 + current.val;
				current = current.next;
			}

			return ret;
		}

		#endregion

		[TestMethod]
		public void Solve()
		{
			var head = new ListNode(1, 2, 3, 4, 5);

			var result = ReverseList(head);

			var resultInt = ListToInt(result);

			resultInt.Should().Be(54321);
		}

		public ListNode ReverseList(ListNode head)
		{
			ListNode resultHead = null;

			ListNode current = head;

			while (current != null)
			{
				// keep next node for subsequent traversal
				var nextTemp = current.next;

				current.next = resultHead;
				resultHead = current;

				// restore current to the initial next node
				current = nextTemp;
			}

			return resultHead;
		}
	}
}
