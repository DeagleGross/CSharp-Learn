using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trees.Binary
{
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

        }

        private void PreorderTraverse(TreeNode node, IList<T> nodes)
        {
            if (node == null)
            {
                return;
            }

            // root
            nodes.Add(node.Value);

            // left
            PreorderTraverse(node.Left, nodes);

            // right
            PreorderTraverse(node.Right, nodes);
        }
    }
}
