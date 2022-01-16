using System;
using System.Collections.Generic;

namespace DataStructures.TrieTree
{
    public class Trie
    {
        private class TrieNode
        {
            private const int N = 26;
            public TrieNode[] Children { get; } = new TrieNode[N];
        }

        private Dictionary<char, TrieNode> _roots = new ();

        public void Add(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentNullException();
            }

            /*
                1. Initialize: cur = root
                2. for each char c in target string S:
                3.      if cur does not have a child c:
                4.          cur.children[c] = new Trie node
                5.      cur = cur.children[c]
                6. cur is the node which represents the string S
             */
        }
    }
}
