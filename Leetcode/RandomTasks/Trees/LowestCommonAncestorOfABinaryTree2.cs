using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// https://leetcode.com/problems/lowest-common-ancestor-of-a-binary-tree-ii/

namespace LeetCodeSolutions.RandomTasks.Trees
{
	[TestClass]
	public class LowestCommonAncestorOfABinaryTree2
	{
		#region Service

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

			public override string ToString()
			{
				return $"val={val}, left={(left?.val.ToString() ?? "null")}, right={(right?.val.ToString() ?? "null")}";
			}
		}

		private TreeNode BuildTree(params int?[] nodes)
		{
			if (nodes == null)
			{
				return null;
			}

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

		#endregion

		[TestMethod]
		public void Solve()
		{
			TreeNode tree = BuildTree(3, 5, 1, 6, 2, 0, 8, null, null, 7, 4);
			TreeNode p = new(5);
			TreeNode q = new(1);

			var ret = LowestCommonAncestor(tree, p, q);
			ret.val.Should().Be(3);
		}

		private TreeNode _ret;

		public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
		{
			Dfs(root, p, q);

			return _ret;
		}

		public bool Dfs(TreeNode root, TreeNode p, TreeNode q)
		{
			if (root == null)
			{
				return false;
			}

			int isNodeItself = 
				(root.val == p.val
				|| root.val == q.val) ? 1 : 0;

			var foundOnTheLeft = Dfs(root.left, p, q) ? 1 : 0;
			var foundOnTheRight = Dfs(root.right, p, q) ? 1: 0;

			if (foundOnTheLeft + foundOnTheRight + isNodeItself >= 2)
			{
				_ret = root;
			}

			return foundOnTheLeft + foundOnTheRight + isNodeItself > 0;
		}
	}
}
