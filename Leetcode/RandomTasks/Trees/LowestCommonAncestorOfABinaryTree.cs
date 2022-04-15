using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

//https://leetcode.com/problems/lowest-common-ancestor-of-a-binary-tree/

namespace LeetCodeSolutions.RandomTasks.Trees;

[TestClass]
public class LowestCommonAncestorOfABinaryTree
{
    [TestMethod]
    public void Solve()
    {
        TreeNode tree = ConstructTree(1, 2);
        TreeNode p = new(1);
        TreeNode q = new(2);

        var ret = LowestCommonAncestor(tree, p, q);
        ret.val.ShouldBe(1);
    }

    public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
    {
        TreeNode found = null;
        Visit(root, p.val, q.val, ref found);

        return found;
    }

    private bool Visit(TreeNode node, int valueLeft, int valueRight, ref TreeNode found)
    {
        if (node == null)
        {
            return false;
        }

        // it's better to use ints than bools since we need "at least two" condition

        int isNodeItself =
            node.val == valueLeft
            || node.val == valueRight ? 1 : 0;


        int foundOnTheLeft = Visit(node.left, valueLeft, valueRight, ref found) ? 1 : 0;
        int foundOnTheRight = Visit(node.right, valueLeft, valueRight, ref found) ? 1 : 0;

        if (isNodeItself + foundOnTheRight + foundOnTheLeft >= 2)
        {
            found = node;
        }

        return isNodeItself + foundOnTheRight + foundOnTheLeft > 0;
    }

    private TreeNode ConstructTree(params int?[] values)
    {
        var root = new TreeNode(values[0]!.Value);

        Queue<TreeNode> nodesToFill = new();

        var nextNode = root;
        bool isLeft = true;

        if (values.Length == 1)
        {
            return root;
        }

        for (int i = 1; i < values.Length; i++)
        {
            var currentValue = values[i];
            bool isSkipNode = currentValue == null;

            if (isLeft)
            {
                // fill up the left node
                if (!isSkipNode)
                {
                    nextNode.left = new(currentValue.Value);
                    nodesToFill.Enqueue(nextNode.left);
                }

                isLeft = false;
            }
            else
            {
                // fill up right node
                if (!isSkipNode)
                {
                    nextNode.right = new(currentValue!.Value);
                    nodesToFill.Enqueue(nextNode.right);
                }

                isLeft = true;
                nextNode = nodesToFill.Dequeue();
            }
        }

        return root;
    }

    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;

        public TreeNode(int x)
        {
            val = x;
        }

        public override string ToString()
        {
            return $"Value={val}; Left={left}; Right={right}";
        }
    }
}