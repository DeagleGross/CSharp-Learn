using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// https://leetcode.com/problems/copy-list-with-random-pointer/

namespace LeetCodeSolutions.RandomTasks.LinkedLists
{
	[TestClass]
	public class CopyListWithRandomPointer
	{
		#region Service

		public class Node
		{
			public int val;
			public Node next;
			public Node random;

			public Node(int val = 0, Node next = null)
			{
				this.val = val;
				this.next = next;
			}

			public Node(params int?[][] values)
			{
				val = values[0][0]!.Value;
				
				if (values.Length == 1)
				{
					return;
				}

				Dictionary<int, Node> nodeIndexes = new();
				Dictionary<int, int?> randomNodeIndexes = new();
				Dictionary<int, Node> nodes = new();

				nodes[val] = this;
				randomNodeIndexes[val] = values[0][1];
				nodeIndexes[0] = this;

				Node tail = this;

				for (int i = 1; i < values.Length; i++)
				{
					var next = new Node(values[i][0]!.Value);
					
					randomNodeIndexes[next.val] = values[i][1];
					nodes[next.val] = next;
					nodeIndexes[i] = next;

					tail.next = next;
					tail = next;
				}

				foreach (var random in randomNodeIndexes)
				{
					var nodeVal = random.Key;
					var randomNodeIndex = random.Value;

					if (!randomNodeIndex.HasValue)
					{
						continue;
					}

					nodes[nodeVal].random = nodeIndexes[randomNodeIndex.Value];
				}
			}

			public override string ToString()
			{
				return $"Node: {val}, Next: {next?.val}, Random: {random?.val}";
			}
		}

		#endregion

		[TestMethod]
		public void Solve()
		{
			var node = new Node(
				new int?[][]
				{
					new int?[] {7, null},
					new int?[] {13, 0},
					new int?[] {11, 4},
					new int?[] {10, 2},
					new int?[] {1, 0},
				});

			var result = CopyRandomList(node);

			var current = result;
			int nodesCount = 0;
			int randomsCount = 0;

			while (current != null)
			{
				nodesCount++;
				if (current.random is not null)
				{
					randomsCount++;
				}

				current = current.next;
			}

			nodesCount.Should().Be(5);
			randomsCount.Should().Be(4);
		}

		[TestMethod]
		public void Solve2()
		{
			var node = new Node(
				new int?[][]
				{
					new int?[] {1, 1},
					new int?[] {2, 1}
				});

			var result = CopyRandomList(node);

			var current = result;
			int nodesCount = 0;
			int randomsCount = 0;

			while (current != null)
			{
				nodesCount++;
				if (current.random is not null)
				{
					randomsCount++;
				}

				current = current.next;
			}

			nodesCount.Should().Be(2);
			randomsCount.Should().Be(2);
		}

		public Node CopyRandomList(Node head)
		{
			if (head == null)
			{
				return null;
			}

			Node newListHead = new Node(head.val);
			Node newListCurrent = newListHead;

			Dictionary<Node, Node> copiedNodesByOriginalNodes = new();

			copiedNodesByOriginalNodes[head] = newListHead;

			var current = head.next;

			while (current != null)
			{
				var nodeCopy = new Node(current.val);

				copiedNodesByOriginalNodes[current] = nodeCopy;

				newListCurrent.next = nodeCopy;
				newListCurrent = nodeCopy;

				current = current.next;
			}

			current = head;

			while (current is not null)
			{
				var copiedNode = copiedNodesByOriginalNodes[current];
				if (current.random != null)
				{
					var randomCopiedNode = copiedNodesByOriginalNodes[current.random];
					copiedNode.random = randomCopiedNode;
				}

				current = current.next;
			}

			return newListHead;
		}


		public Node CopyRandomList_Optimal(Node head)
		{
			if (head == null)
			{
				return null;
			}

			var current = head.next;

			Node newListHead = head;
			newListHead.next = new Node(head.val);

			Node newListCurrent = newListHead;

			while (current != null)
			{
				newListCurrent.next = current;
				newListCurrent = newListCurrent.next;

				var nodeCopy = new Node(current.val);

				current = current.next;

				newListCurrent.next = nodeCopy;
				newListCurrent = newListCurrent.next;
			}

			// new we have a combined linked list
			// old1 -> copy1 -> old2 -> copy2 ->...

			// what we need to do is to walk resulting list one more time getting old and oldRandom nodes.
			// Then we folow Next pointer on both old and oldRandom to get new node and a corresponding newRandom node
			// during this walk we form the resulting list

			throw new NotImplementedException();
		}
	}
}
