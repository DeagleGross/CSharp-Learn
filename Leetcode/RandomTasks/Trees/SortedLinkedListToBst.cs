using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

#nullable disable

// https://leetcode.com/problems/convert-sorted-list-to-binary-search-tree/

namespace LeetCodeSolutions.RandomTasks.Trees
{
	[TestClass]
	public class SortedLinkedListToBst
	{
		public class TreeNode
		{
			public int val;
			public TreeNode left;
			public TreeNode right;

			public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
			{
				this.val = val;
				this.left = left;
				this.right = right;
			}
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

			public override string ToString()
			{
				return $"{val}";
			}
		}


		[TestMethod]
		public void Solve()
		{
			var list = BuildList(-10, -3, 0, 5, 9);
			var treeRoot = SortedListToBST(list);

			var testValue = PrintTree(treeRoot);

			testValue.ShouldBe("[0,-3,9,-10,null,5]");
		}

		public TreeNode SortedListToBST(ListNode head)
		{
			if (head == null)
			{
				return null;
			}

			(var center, var preCenter) = FindCenter(head);
			var node = new TreeNode(center.val);

			if (preCenter != null)
			{
				preCenter.next = null;
				node.left = SortedListToBST(head);
			}

			node.right = SortedListToBST(center.next);
			return node;
		}

		private (ListNode center, ListNode preCenter) FindCenter(ListNode listHead)
		{
			ListNode center = listHead;
			ListNode nextNode = listHead;
			ListNode preCenter = null;

			while (nextNode.next != null && nextNode.next.next != null)
			{
				nextNode = nextNode.next.next;
				preCenter = center;
				center = center.next;
			}

			return (center, preCenter);
		}

		private ListNode BuildList(params int[] elements)
		{
			if (elements is null
				|| elements.Length == 0)
			{
				return null;
			}

			ListNode head = new ListNode(elements[0]);
			var current = head;
			foreach (var val in elements.Skip(1))
			{
				var newNode = new ListNode(val);
				current.next = newNode;
				current = newNode;
			}

			return head;
		}

		private TreeNode BuildTree(params int?[] nodes)
		{
			IEnumerable<int?> n = nodes;
			using var nodeEnumerator = n.GetEnumerator();
			nodeEnumerator.MoveNext();

			var root = new TreeNode(nodeEnumerator.Current.Value);

			Queue<TreeNode> nodesToFill = new();
			nodesToFill.Enqueue(root);

			while (nodesToFill.Count > 0)
			{
				var currentNode = nodesToFill.Dequeue();

				if (!nodeEnumerator.MoveNext())
				{
					break;
				}

				var left = nodeEnumerator.Current;

				if (left.HasValue)
				{
					currentNode.left = new TreeNode(left.Value);
					nodesToFill.Enqueue(root.left);
				}

				if (!nodeEnumerator.MoveNext())
				{
					break;
				}

				var right = nodeEnumerator.Current;

				if (right.HasValue)
				{
					currentNode.right = new TreeNode(right.Value);
					nodesToFill.Enqueue(root.right);
				}
			}

			return root;
		}

		public string PrintTree(TreeNode root)
		{
			if (root == null)
			{
				return "[]";
			}

			Queue<TreeNode> nodesToVisit = new();
			nodesToVisit.Enqueue(root);

			List<string> values = new();
			
			while (nodesToVisit.Count > 0)
			{
				var currentNode = nodesToVisit.Dequeue();
				
				values.Add(currentNode?.val.ToString() ?? "null");

				if (currentNode == null)
				{
					continue;
				}

				nodesToVisit.Enqueue(currentNode.left);
				nodesToVisit.Enqueue(currentNode.right);
			}

			return "[" + string.Join(",", values).TrimEnd(',', 'n','u', 'l') + "]";
		}
	}
}
