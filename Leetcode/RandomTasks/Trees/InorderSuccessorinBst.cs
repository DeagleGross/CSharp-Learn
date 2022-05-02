using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// https://leetcode.com/problems/inorder-successor-in-bst/

namespace LeetCodeSolutions.RandomTasks.Trees
{
	[TestClass]
	public class InorderSuccessorinBst
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
					nodesToFill.Enqueue(currentNode.left);
				}

				if (!nodeEnumerator.MoveNext())
				{
					break;
				}

				var right = nodeEnumerator.Current;

				if (right.HasValue)
				{
					currentNode.right = new TreeNode(right.Value);
					nodesToFill.Enqueue(currentNode.right);
				}
			}

			return root;
		}

		#endregion

		[TestMethod]
		public void Solve()
		{
			var tree = BuildTree(2, 1, 3);

			TreeNode p = new TreeNode(1);

			var result = InorderSuccessor(tree, p);

			result.val.Should().Be(2);
		}

		[TestMethod]
		public void Solve2()
		{
			var tree = BuildTree(5, 3, 6, 2, 4, null, null, 1);

			TreeNode p = new TreeNode(6);

			var result = InorderSuccessor(tree, p);

			result.Should().BeNull();
		}

		[TestMethod]
		public void Solve3()
		{
			var tree = BuildTree(5, 3, 6, 2, 4, null, null, 1);

			TreeNode p = new TreeNode(2);

			var result = InorderSuccessor(tree, p);

			result.val.Should().Be(3);
		}

		[TestMethod]
		public void Solve4()
		{
			var tree = BuildTree(5, 3, 6, 1, 4, null, null, null, 2);

			TreeNode p = new TreeNode(4);

			var result = InorderSuccessor(tree, p);

			result.val.Should().Be(5);
		}

		[TestMethod]
		public void Solve5()
		{
			var tree = BuildTree(6, 2, 8, 0, 4, 7, 9, null, null, 3, 5);

			TreeNode p = new TreeNode(2);

			var result = InorderSuccessor(tree, p);

			result.val.Should().Be(3);
		}

		private TreeNode _target;
		private TreeNode _successor;
		private bool _foundTarget;

		public TreeNode InorderSuccessor(TreeNode root, TreeNode p)
		{
			_target = p;
			if (root.val == p.val)
			{
				return root.right;
			}

			InOrderTraversal(root);

			return _successor;
		}

		private void InOrderTraversal(TreeNode root)
		{
			if (root == null)
			{
				return;
			}

			_foundTarget = _foundTarget || root.val == _target.val;

			if (_foundTarget && root.val != _target.val)
			{
				var currentValue = root.val;

				if (currentValue > _target.val
					&& currentValue < (_successor?.val ?? Int32.MaxValue))
				{
					_successor = root;
				}

			}
			
			InOrderTraversal(root.left);

			if (_foundTarget && root.val != _target.val)
			{
				var currentValue = root.val;

				if (currentValue > _target.val
					&& currentValue < (_successor?.val ?? Int32.MaxValue))
				{
					_successor = root;
				}

			}

			InOrderTraversal(root.right);
		}
	}
}
