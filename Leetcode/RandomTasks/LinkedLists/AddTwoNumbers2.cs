using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// https://leetcode.com/problems/add-two-numbers-ii/

namespace LeetCodeSolutions.RandomTasks.LinkedLists
{
	[TestClass]
	public class AddTwoNumbers2
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
			var l1 = new ListNode(7, 2, 4, 3);
			var l2 = new ListNode(5, 6, 4);

			var result = AddTwoNumbers(l1, l2);

			var integerResult = ListToInt(result);

			integerResult.Should().Be(7807);
		}

		[TestMethod]
		public void Solve2()
		{
			var l1 = new ListNode(0);
			var l2 = new ListNode(0);

			var result = AddTwoNumbers(l1, l2);

			var integerResult = ListToInt(result);

			integerResult.Should().Be(0);
		}

		public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
		{
			if (l1 == null)
			{
				return l2;
			}

			if (l2 == null)
			{
				return l1;
			}

			var leftLength = 0;
			var rightLength = 0;

			var left = l1;
			var right = l2;

			while (left != null || right != null)
			{
				if (left != null)
				{
					leftLength++;
				}

				if (right != null)
				{
					rightLength++;
				}

				left = left?.next;
				right = right?.next;
			}

			left = l1;
			right = l2;

			var deltaLeft = Math.Abs(leftLength - rightLength);

			Stack<int> leftNumbers = new Stack<int>();
			Stack<int> rightNumbers = new Stack<int>();
			
			while (left != null && right != null)
			{
				var l = (deltaLeft > 0 && leftLength < rightLength)
					? 0
					: left.val;

				var r = (deltaLeft > 0 && rightLength < leftLength)
					? 0
					: right.val;

				leftNumbers.Push(l);
				rightNumbers.Push(r);

				deltaLeft--;

				if (deltaLeft >= 0)
				{
					if (rightLength > leftLength)
					{
						right = right.next;
					}
					else
					{
						left = left.next;
					}
				}
				else
				{
					left = left?.next;
					right = right?.next;
				}
			}

			Stack<int> retNumbers = new();

			int carry = 0;

			while (leftNumbers.Count > 0 
					|| rightNumbers.Count > 0)
			{
				leftNumbers.TryPop(out int l);
				rightNumbers.TryPop(out int r);

				var sum = carry + l + r;

				carry = sum / 10;

				retNumbers.Push(sum % 10);
			}

			if (carry > 0)
			{
				retNumbers.Push(carry);
			}

			ListNode resultHead = new ListNode(0);
			ListNode current = resultHead;

			while (retNumbers.Count > 0)
			{
				current.next = new ListNode(retNumbers.Pop());
				current = current.next;
			}

			return resultHead.next;
		}
	}
}
