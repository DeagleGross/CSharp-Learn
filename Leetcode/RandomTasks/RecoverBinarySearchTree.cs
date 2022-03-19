using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

// https://leetcode.com/problems/recover-binary-search-tree/

#nullable disable

namespace LeetCodeSolutions.RandomTasks
{
	[TestClass]
	public class RecoverBinarySearchTree
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

		[TestMethod]
		public void Solve()
		{
			var tree = BuildTree(1, 3, null, null, 2);
			var builtTree = PrintTree(tree);
			Console.WriteLine(builtTree);

			RecoverTree(tree);

			PrintTree(tree).ShouldBe("[3,1,null,null,2]");
		}

		public void RecoverTree(TreeNode root)
		{
			Queue<TreeNode> nodesToVisit = new();
			nodesToVisit.Enqueue(root);

			TreeNode nodeToSwap1 = null;
			TreeNode nodeToSwap2 = null;

			TreeNode previousElement = new(int.MinValue);

			InOrderTraversal(root, ref nodeToSwap1, ref nodeToSwap2, ref previousElement);

			(nodeToSwap1.val, nodeToSwap2.val) = (nodeToSwap2.val, nodeToSwap1.val);
		}

		private void InOrderTraversal(
			TreeNode root,
			ref TreeNode nodeToSwap1,
			ref TreeNode nodeToSwap2,
			ref TreeNode previousElement)
		{
			if (root == null)
			{
				return;
			}

			InOrderTraversal(root.left, ref nodeToSwap1, ref nodeToSwap2, ref previousElement);

			// if firstElement has not been found yet and current node's value has violation,
			// then current one is the first violation
			if (nodeToSwap1 == null
				&& previousElement.val > root.val)
			{
				nodeToSwap1 = previousElement;
			}

			// only two nodes, reverse the order - catch first, catch second one
			if (nodeToSwap1 != null
				&& previousElement.val > root.val)
			{
				// if the firstElment is found and current node's value has violation, then current one is the second violation
				nodeToSwap2 = root;
			}

			previousElement = root;

			InOrderTraversal(root.right, ref nodeToSwap1, ref nodeToSwap2, ref previousElement);
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
			Queue<TreeNode> nodesToVisit = new();
			nodesToVisit.Enqueue(root);

			List<string> values = new();

			values.Add(root.val.ToString());

			while (nodesToVisit.Count > 0)
			{
				var currentNode = nodesToVisit.Dequeue();

				if (currentNode.left == null
					&& currentNode.right == null)
				{
					break;
				}

				values.Add(currentNode.left?.val.ToString() ?? "null");

				if (currentNode.left != null)
				{
					nodesToVisit.Enqueue(currentNode.left);
				}

				values.Add(currentNode.right?.val.ToString() ?? "null");

				if (currentNode.right != null)
				{
					nodesToVisit.Enqueue(currentNode.right);
				}
			}

			return "[" + string.Join(",", values) + "]";
		}
	}
}
