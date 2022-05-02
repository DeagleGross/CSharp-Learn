using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// https://leetcode.com/problems/inorder-successor-in-bst-ii/

namespace LeetCodeSolutions.RandomTasks.Trees
{
	[TestClass]
	public class InorderSuccessorInBst2
	{
		public class Node
		{
			public int val;
			public Node left;
			public Node right;
			public Node parent;
		}

		[TestMethod]
		public void Solve()
		{ }

		public Node InorderSuccessor(Node x)
		{
			//There are two possible situations here :
			// 1
			//  Node has a right child, and hence its successor is somewhere lower in the tree.
			//  To find the successor, go to the right once and then as many times to the left as you could.
			// 2
			//  Node has no right child, then its successor is somewhere upper in the tree.
			//  To find the successor, go up till the node that is left child of its parent.
			//  The answer is the parent. Beware that there could be no successor (= null successor) in such a situation.

			/*
			If the node has a right child, and hence its successor is somewhere lower in the tree. 
			Go to the right once and then as many times to the left as you could. Return the node you end up with.
			
			Node has no right child, and hence its successor is somewhere upper in the tree.
			Go up till the node that is left child of its parent. The answer is the parent.
			*/

			if (x.right != null)
			{
				x = x.right;
				while (x.left != null)
				{
					x = x.left;
				}

				return x;
			}

			// the successor is somewhere upper in the tree
			while (x.parent != null && x == x.parent.right)
			{
				x = x.parent;
			}

			return x.parent;
		}

	}
}
