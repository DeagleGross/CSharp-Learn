using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace LeetCodeSolutions.RandomTasks.Trees
{
	[TestClass]
	public class BinaryTreeZigzagLevelOrderTraversal
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

			var result = ZigzagLevelOrder(root);

			result.Count.ShouldBe(3);
		}

		[TestMethod]
		public void Solve2()
		{
			var root = BuildTree(3, 9, 20, null, null, 15, 7);

			var result = ZigzagLevelOrder(root);

			result.Count.ShouldBe(3);
		}

		private Dictionary<int, List<int>> _levels = new();

		public IList<IList<int>> ZigzagLevelOrder(TreeNode root)
		{
			Dfs(root, 1);

			IList<IList<int>> ret = new List<IList<int>>();

			for (int i = 1; i <= _levels.Keys.Count; i++)
			{
				if (i % 2 == 0)
				{
					_levels[i].Reverse();
				}
				
				ret.Add(_levels[i]);
			}

			return ret;
		}

		public void Dfs(TreeNode root, int level)
		{
			if (root == null)
			{
				return;
			}

			if (!_levels.ContainsKey(level))
			{
				_levels.Add(level, new());
			}

			_levels[level].Add(root.val);

			Dfs(root.left, level+1);
			Dfs(root.right, level+1);
		}
	}
}
