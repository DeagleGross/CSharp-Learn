using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

// https://leetcode.com/problems/boundary-of-binary-tree/

namespace LeetCodeSolutions.RandomTasks.Trees
{
	[TestClass]
	public class BoundaryOfBinaryTree1
	{
		#region

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
			var root = BuildTree(1, null, 2, 3, 4);

			var result = BoundaryOfBinaryTree(root);

			result.ShouldBe(new []{ 1, 3, 4, 2 });
		}

		[TestMethod]
		public void Solve2()
		{
			var root = BuildTree(1, 2, 3, 4, 5, 6, null, null, null, 7, 8, 9, 10);

			var result = BoundaryOfBinaryTree(root);

			result.ShouldBe(new[] { 1, 2, 4, 7, 8, 9, 10, 6, 3 });
		}

		[TestMethod]
		public void Solve3()
		{
			var root = BuildTree(1, 2, 7, 3, 5, null, 6, 4);

			var result = BoundaryOfBinaryTree(root);

			result.ShouldBe(new[] { 1, 2, 3, 4, 5, 6, 7});
		}

		private List<int> _left = new();
		private List<int> _bottom = new();
		private List<int> _right = new();

		public IList<int> BoundaryOfBinaryTree(TreeNode root)
		{
			_left.Add(root.val);


			Dfs(root.left, true, false);
			Dfs(root.right, false, true);

			_right.Reverse();

			return _left.Concat(_bottom).Concat(_right).ToList();
		}

		public void Dfs(TreeNode root, bool leftmostNode, bool rightmostNode)
		{
			if (root == null)
			{
				return;
			}

			if (root.left == null
				&& root.right == null)
			{
				// bottom
				_bottom.Add(root.val);
				return;
			}

			if (leftmostNode)
			{
				_left.Add(root.val);
			}

			if (rightmostNode)
			{
				_right.Add(root.val);
			}

			//

			if (leftmostNode)
			{
				if (root.left != null)
				{
					Dfs(root.left, true, false);
					Dfs(root.right, false, false);
					return;
				}
				else
				{
					Dfs(root.right, true, false);
					return;
				}
			}

			if (rightmostNode)
			{
				if (root.right != null)
				{
					Dfs(root.left, false, false);
					Dfs(root.right, false, true);
					return;
				}
				else
				{
					Dfs(root.left, false, true);
					return;
				}
			}

			if (!rightmostNode
				&& !leftmostNode)
			{
				Dfs(root.left, false, false);
				Dfs(root.right, false, false);
			}
		}
	}
}
