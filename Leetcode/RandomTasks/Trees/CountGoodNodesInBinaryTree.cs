using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

// https://leetcode.com/problems/count-good-nodes-in-binary-tree/

namespace LeetCodeSolutions.RandomTasks.Trees
{
	[TestClass]
	public class CountGoodNodesInBinaryTree
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
			var root = BuildTree(3, 1, 4, 3, null, 1, 5);

			var result = GoodNodes(root);

			result.ShouldBe(4);
		}

		[TestMethod]
		public void Solve2()
		{
			var root = BuildTree(2, null, 4, 10, 8, null, null, 4);

			var result = GoodNodes(root);

			result.ShouldBe(4);
		}

		private Stack<int> _maxValuesSoFar = new Stack<int>();
		private int _goodNodes = 0;

		public int GoodNodes(TreeNode root)
		{
			_maxValuesSoFar.Push(root.val);
			Dfs(root);

			return _goodNodes;
		}

		private void Dfs(TreeNode root)
		{
			if (root == null)
			{
				return;
			}

			var lastMaxValue = _maxValuesSoFar.Peek();
			bool wasPush = false;

			if (root.val >= lastMaxValue)
			{
				_goodNodes++;
			}
			else
			{
				_maxValuesSoFar.Push(root.val);
				wasPush = true;
			}

			Dfs(root.left);
			Dfs(root.right);

			if (wasPush)
			{
				_maxValuesSoFar.Pop();
			}
		}
	}
}
