using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// https://leetcode.com/problems/distribute-coins-in-binary-tree/

namespace LeetCodeSolutions.RandomTasks.Trees
{
	[TestClass]
	public class DistributeCoinsInBinaryTree
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
			var root = BuildTree(3, 0, 0);

			var result = DistributeCoins(root);

			result.Should().Be(2);
		}
		
		[TestMethod]
		public void Solve2()
		{
			var root = BuildTree(0, 3, 0);

			var result = DistributeCoins(root);

			result.Should().Be(3);
		}

		[TestMethod]
		public void Solve3()
		{
			var root = BuildTree(1, 0, 2);

			var result = DistributeCoins(root);

			result.Should().Be(2);
		}

		public int DistributeCoins(TreeNode root)
		{
			RedistibuteCoins(root);

			return _moves;
		}

		private int _moves;

		/*
		Let dfs(node) be the excess number of coins in the subtree at or below this node: 
		namely, the number of coins in the subtree, minus the number of nodes in the subtree. 
		Then, the number of moves we make from this node to and from its children is 
			abs(dfs(node.left)) + abs(dfs(node.right)). 
		After, we have an excess of 
			node.val + dfs(node.left) + dfs(node.right) - 1 
		coins at this node.
		*/

		private int RedistibuteCoins(TreeNode node)
		{
			if (node == null)
			{
				return 0;
			}
			
			int l = RedistibuteCoins(node.left);
			int r = RedistibuteCoins(node.right);
			
			_moves += Math.Abs(l) + Math.Abs(r);

			return node.val + l + r - 1;
		}
	}
}
