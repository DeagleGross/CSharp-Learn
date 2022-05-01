using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

// https://leetcode.com/problems/remove-nth-node-from-end-of-list/

namespace LeetCodeSolutions.RandomTasks.LinkedLists
{
	[TestClass]
	public class RemoveNthNodeFromEndOfList
	{
		[TestMethod]
		public void Solve()
		{
			int[] head = new int[] { 1 };
			int n = 1;
			var listHead = GetList(head);

			var result = RemoveNthFromEnd(listHead, n);

			BuildString(result).ShouldBe("[]");
		}

		private ListNode GetList(int[] values)
		{
			ListNode head = new(values[0]);
			ListNode current = head;


			foreach (var val in values.Skip(1))
			{
				var nextNode = new ListNode(val);

				current.next = nextNode;
				current = nextNode;
			}

			return head;
		}

		public ListNode RemoveNthFromEnd(ListNode head, int n)
		{
			if (n == 0)
			{
				return head;
			}

			ListNode current = null;

			int stepsToSkip = n;

			while (stepsToSkip > 0)
			{
				if (current == null)
				{
					current = head;
				}
				else
				{
					current = current.next;
				}

				stepsToSkip--;
			}

			if (current == null)
			{
				return head;
			}

			var toRemove = head;
			ListNode previous = null;

			while (true)
			{
				if (current.next == null)
				{
					break;
				}

				current = current.next;
				previous = toRemove;
				toRemove = toRemove.next;
			}

			if (previous == null)
			{
				return head.next;
			}

			previous.next = toRemove.next; // there is a simpler solution that uses previous.next.next and a dummy node ath the beginning of the list

			return head;
		}

		public string BuildString(ListNode head)
		{
			if (head == null)
			{
				return "[]";
			}

			List<int> values = new();
			ListNode current = head;

			while (current != null)
			{
				values.Add(current.val);
				current = current.next;
			}

			return "[" + string.Join(",", values) + "]";
		}

		public class ListNode
		{
			public int val;
			public ListNode next;

			public ListNode(int val = 0, ListNode next = null)
			{
				this.val = val;
				this.next = next;
			}
		}

	}
}
