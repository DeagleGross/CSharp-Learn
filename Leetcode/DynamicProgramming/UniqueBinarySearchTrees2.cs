using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace LeetCodeSolutions.DynamicProgramming
{
	[TestClass]
	public class UniqueBinarySearchTrees2
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
			var input = 3;

			var result = GenerateTrees(input);

			result.Count.ShouldBe(5);
		}
		
		public IList<TreeNode> GenerateTrees(int n)
		{
			if (n == 0)
			{
				return new List<TreeNode>();
			}

			return GenerateTreesCore(1, n);
		}

		public List<TreeNode> GenerateTreesCore(int start, int end)
		{
			List<TreeNode> allTrees = new();

			if (start > end)
			{
				allTrees.Add(null);
				return allTrees;
			}

			// pick up a root
			for (int i = start; i <= end; i++)
			{
				// all possible left subtrees if i is choosen to be a root
				List<TreeNode> leftTrees = GenerateTreesCore(start, i - 1);

				// all possible right subtrees if i is choosen to be a root
				List<TreeNode> rightTrees = GenerateTreesCore(i + 1, end);

				// connect left and right trees to the root i
				foreach (TreeNode l in leftTrees)
				{
					foreach (TreeNode r in rightTrees)
					{
						TreeNode currentTree = new(i)
						{
							left = l,
							right = r
						};

						allTrees.Add(currentTree);
					}
				}
			}

			return allTrees;
		}

	}
}
