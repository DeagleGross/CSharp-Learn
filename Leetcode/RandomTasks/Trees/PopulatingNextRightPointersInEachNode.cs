using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

#nullable disable

//https://leetcode.com/problems/populating-next-right-pointers-in-each-node/

namespace LeetCodeSolutions.RandomTasks.Trees
{
	public class Node
	{
		public int val;
		public Node left;
		public Node right;
		public Node next;

		public Node() { }

		public Node(int _val)
		{
			val = _val;
		}

		public Node(int _val, Node _left, Node _right, Node _next)
		{
			val = _val;
			left = _left;
			right = _right;
			next = _next;
		}
	}

	[TestClass]
	public class PopulatingNextRightPointersInEachNode
	{
		[TestMethod]
		public void Solve()
		{
			var tree = BuildTree(1, 2, 3, 4, 5, 6, 7);
			var connected = Connect(tree);

			var result = PrintTree(connected);

			result.ShouldBe("[1,#,2,3,#,4,5,6,7,#]");
		}

		[TestMethod]
		public void Solve_Empty()
		{
			var tree = BuildTree(null);
			var connected = Connect(tree);

			var result = PrintTree(connected);

			result.ShouldBe("[1,#,2,3,#,4,5,6,7,#]");
		}

		Dictionary<int, Node> _previousNodes = new ();

		public Node Connect(Node root)
		{
			if (root == null)
			{
				return null;
			}

			ConnectNodeLevel(root,  1);

			return root;
		}

		private void ConnectNodeLevel(Node node, int level)
		{
			if (_previousNodes.ContainsKey(level))
			{
				_previousNodes[level].next = node;
				_previousNodes[level] = node;
			}
			else
			{
				_previousNodes[level] = node;
			}

			if (node.left is null
				&& node.right is null)
			{
				return;
			}

			ConnectNodeLevel(node.left, level + 1);
			ConnectNodeLevel(node.right, level + 1);
		}

		private Node BuildTree(params int?[] nodes)
		{
			if (nodes == null)
			{
				return null;
			}

			IEnumerable<int?> n = nodes;
			using var nodeEnumerator = n.GetEnumerator();
			nodeEnumerator.MoveNext();

			var root = new Node(nodeEnumerator.Current.Value);

			Queue<Node> nodesToFill = new();
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
					currentNode.left = new Node(left.Value);
					nodesToFill.Enqueue(root.left);
				}

				if (!nodeEnumerator.MoveNext())
				{
					break;
				}

				var right = nodeEnumerator.Current;

				if (right.HasValue)
				{
					currentNode.right = new Node(right.Value);
					nodesToFill.Enqueue(root.right);
				}
			}

			return root;
		}

		public string PrintTree(Node root)
		{
			if (root == null)
			{
				return "[]";
			}

			Queue<Node> nodesToVisit = new();
			nodesToVisit.Enqueue(root);

			List<string> values = new();

			while (nodesToVisit.Count > 0)
			{
				var currentNode = nodesToVisit.Dequeue();

				values.Add(currentNode?.val.ToString() ?? "null");

				if (currentNode?.next is null)
				{
					values.Add("#");
				}

				if (currentNode == null)
				{
					continue;
				}

				nodesToVisit.Enqueue(currentNode.left);
				nodesToVisit.Enqueue(currentNode.right);
			}

			var allNodes = string.Join(",", values);
			var firstNull = allNodes.IndexOf(",null", StringComparison.InvariantCultureIgnoreCase);
			var ret = allNodes[0..firstNull];

			return "[" + ret + "]";
		}
	}
}
