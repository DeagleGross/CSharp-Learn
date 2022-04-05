using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

// https://leetcode.com/problems/binary-tree-level-order-traversal/

#nullable disable

namespace LeetCodeSolutions.RandomTasks.Trees
{
	[TestClass]
	public class BinaryTreeLevelOrderTraversal
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

			public override string ToString()
			{
				return $"val={val}, left={(left?.val.ToString() ?? "null")}, right={(right?.val.ToString() ?? "null")}";
			}
		}

		[TestMethod]
		public void Solve()
		{
			var tree = BuildTree(3, 9, 20, null, null, 15, 7);
			var builtTree = PrintTree(tree);
			Console.WriteLine(builtTree);

			var levelOrder = LevelOrder(tree);
			FlattenResult(levelOrder).ShouldBe("[[3],[9,20],[15,7]]");
		}

		private readonly Dictionary<int, IList<int>> _treeLevels = new();

		public IList<IList<int>> LevelOrder(TreeNode root)
		{
			if (root == null)
			{
				return new List<IList<int>>();
			}

			PrintTreeLevel(root, 1);

			List<IList<int>> ret = new();

			foreach (var level in _treeLevels.OrderBy(kv=>kv.Key))
			{
				ret.Add(level.Value);
			}

			return ret;
		}

		private void PrintTreeLevel(TreeNode root, int level)
		{
			if (root == null)
			{
				return;
			}

			if (!_treeLevels.ContainsKey(level))
			{
				_treeLevels.Add(level, new List<int>());
			}

			_treeLevels[level].Add(root.val);

			PrintTreeLevel(root.left, level+1);
			PrintTreeLevel(root.right, level+1);
		}
		
		private string FlattenResult(IList<IList<int>> result)
		{
			List<string> levels = new(result.Count);

			foreach (var level in result)
			{
				var levelNodes = string.Join(",", level);
				levels.Add("["+levelNodes+"]");
			}

			var ret = "[" + string.Join(",", levels) + "]";

			return ret;
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

		public string PrintTree(TreeNode root)
		{
			if (root == null)
			{
				return "[]";
			}

			Queue<TreeNode> nodesToVisit = new();
			nodesToVisit.Enqueue(root);

			List<string> values = new()
			{
				root.val.ToString()
			};

			while (nodesToVisit.Count > 0)
			{
				var currentNode = nodesToVisit.Dequeue();

				if ((currentNode.left == null
					&& currentNode.right == null)
					&& nodesToVisit.Count != 1)
				{
					continue;
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
