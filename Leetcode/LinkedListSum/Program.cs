using System;

namespace LinkedListSum
{
	public class ListNode
	{
		public int Val;
		public ListNode Next;

		public ListNode(int val = 0, ListNode next = null)
		{
			Val = val;
			Next = next;
		}

		public ListNode(params int[] values)
		{
			Val = values[0];
			if (values.Length == 1)
			{
				return;
			}

			ListNode tail = this;

			for (int i = 1; i < values.Length; i++)
			{
				var next = new ListNode(values[i]);
				tail.Next = next;
				tail = next;
			}
		}

		public override string ToString()
		{
			int power = 0;
			ListNode current = this;
			int ret = Val;
			while (current.Next != null)
			{
				power++;
				current = current.Next;
				ret += current.Val * (int)Math.Pow(10, power);
			}

			return ret.ToString();
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			//var sum = AddTwoNumbers(new ListNode(2, 4, 3), new ListNode(5, 6, 4));
			//Console.WriteLine(sum.ToString());

			var sum2 = AddTwoNumbers(new ListNode(9, 9, 9, 9, 9, 9, 9), new ListNode(9, 9, 9));
			Console.WriteLine(sum2.ToString());
		}

		public static ListNode AddTwoNumbers(ListNode l1, ListNode l2)
		{
			Console.WriteLine( $"{l1}+{l2}");

			ListNode ret = new(-1, null);
			ListNode current = ret;

			int transfer = 0;
			
			ListNode a = l1;
			ListNode b = l2;

			while (true)
			{
				var leftDigit = a?.Val ?? 0;
				var rightDigit = b?.Val ?? 0;

				var sum = leftDigit + rightDigit + transfer;
				transfer = 0;

				if (sum >= 10)
				{
					sum -= 10;
					transfer += 1;
				}
				
				current.Val = sum;
				
				// we can optimize for the cases when transfer == 0 and one of the sequences is over but for 100 nodes it's hardly a difference
				// but it can remove significant memory pressure

				if (a?.Next == null
					&& b?.Next == null)
				{
					// equal length

					if (transfer > 0)
					{
						current.Next = new(transfer);
					}

					break;
				}

				//if (a.Next == null)
				//{
				//	// l2.Next != null
				//	if (transfer > 0)
				//	{
				//		current.Next = new(transfer);
				//		current = current.Next;
				//	}
				//	current.Next = b.Next;
				//	break;
				//}

				//if (b.Next == null)
				//{
				//	// l1.Next != null
				//	if (transfer > 0)
				//	{
				//		current.Next = new(transfer);
				//		current = current.Next;
				//	}
				//	current.Next = a.Next;
				//	break;
				//}

				// both not null

				a = a?.Next;
				b = b?.Next;
				current.Next = new();
				current = current.Next;
			}

			return ret;
		}

	}


}
