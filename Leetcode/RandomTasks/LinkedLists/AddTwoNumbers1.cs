using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

// https://leetcode.com/problems/add-two-numbers/

namespace LeetCodeSolutions.RandomTasks.LinkedLists;

[TestClass]
public class AddTwoNumbers1
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

	#endregion


	[TestMethod]
	public void Solve()
	{
		var sum2 = AddTwoNumbers(new ListNode(9, 9, 9, 9, 9, 9, 9), new ListNode(9, 9, 9));
		sum2.ToString().ShouldBe("10000998");
	}

	public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
	{
		ListNode resultHead = new(0);
		ListNode p = l1;
		ListNode q = l2;
		
		var current = resultHead;

		int carry = 0;

		while (p != null || q != null)
		{
			int x = p?.val ?? 0;
			int y = q?.val ?? 0;

			int sum = carry + x + y;

			carry = sum / 10;

			// here we take division remainer by 10 to get only the least significant digit of the resulting sum
			current.next = new ListNode(sum % 10); 

			current = current.next;

			p = p?.next;
			q = q?.next;
		}

		if (carry > 0)
		{
			current.next = new ListNode(carry);
		}

		return resultHead.next;
	}
}
