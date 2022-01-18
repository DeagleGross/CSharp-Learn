using System.Collections.Generic;

namespace DataStructures.BinaryTree;

public class BinaryTree<T>
{
    internal class TreeNode
    {
        public T Value { get; }
            
        public TreeNode Left { get; }
        public TreeNode Right { get; }

        public TreeNode(T value, TreeNode left, TreeNode right)
        {
            Value = value;
            Left = left;
            Right = right;
        }
    }

    internal IList<T> PreorderTraversal(TreeNode root)
    {
        var nodes = new List<T>();

        PreorderTraverseInternal(root, nodes);

        return nodes;
    }

    private static void PreorderTraverseInternal(TreeNode node, IList<T> nodes)
    {
        if (node == null)
        {
            return;
        }

        // root
        nodes.Add(node.Value);

        // left
        PreorderTraverseInternal(node.Left, nodes);

        // right
        PreorderTraverseInternal(node.Right, nodes);
    }
}