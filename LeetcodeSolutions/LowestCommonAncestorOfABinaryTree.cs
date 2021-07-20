using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//https://leetcode.com/problems/lowest-common-ancestor-of-a-binary-tree/

TreeNode tree = ConstructTree(1,2);
TreeNode p = new(1);
TreeNode q = new(2);

var ret = LowestCommonAncestor(tree, p, q);
Console.WriteLine(ret?.Value.ToString() ?? "NULL");

static TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
{
	TreeNode found = null;
	Visit(root, p.Value, q.Value, ref found);

	return found;
}

static bool Visit(TreeNode node, int valueLeft, int valueRight, ref TreeNode found)
{
	if (node == null)
	{
		return false;
	}

	// it's better to use ints than bools since we need "at least two" condition

	int isNodeItself =
		node.Value == valueLeft
		|| node.Value == valueRight ? 1 : 0;
	

	int foundOnTheLeft = Visit(node.Left, valueLeft, valueRight, ref found) ? 1 : 0;
	int foundOnTheRight = Visit(node.Right, valueLeft, valueRight, ref found) ? 1 : 0;

	if (isNodeItself + foundOnTheRight + foundOnTheLeft >= 2)
	{
		found = node;
	}

	return isNodeItself + foundOnTheRight + foundOnTheLeft > 0;
}

static TreeNode ConstructTree(params int?[] values)
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
			if(!isSkipNode)
			{
				nextNode.Left = new(currentValue.Value);
				nodesToFill.Enqueue(nextNode.Left);
			}

			isLeft = false;
		}
		else
		{
			// fill up right node
			if(!isSkipNode)
			{
				nextNode.Right = new(currentValue!.Value);
				nodesToFill.Enqueue(nextNode.Right);
			}

			isLeft = true;
			nextNode = nodesToFill.Dequeue();
		}
	}

	return root;
}

public class TreeNode
{
	public int Value;
	public TreeNode Left;
	public TreeNode Right;

	public TreeNode(int x)
	{
		Value = x;
	}

	public override string ToString()
	{
		return $"Value={Value}; Left={Left}; Right={Right}";
	}
}