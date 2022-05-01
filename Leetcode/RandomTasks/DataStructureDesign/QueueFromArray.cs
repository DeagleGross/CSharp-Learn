using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

// Implement queue with backing array

namespace LeetCodeSolutions.RandomTasks.DataStructureDesign
{
	[TestClass]
	public class QueueFromArray
	{
		[TestMethod]
		public void Solve()
		{
			var queue = new MyQueue<int>(3);
			queue.Enqueue(1);
			queue.Enqueue(2);
			queue.Enqueue(3);

			var t1= queue.Dequeue();
			t1.ShouldBe(1);

			queue.Enqueue(1);

			var t2 = queue.Dequeue();
			t2.ShouldBe(2);

			var t3 = queue.Dequeue();
			t3.ShouldBe(3);

			queue.Enqueue(1);
			queue.Enqueue(2);
			//queue.Enqueue(3);

			var t4 = queue.Dequeue();
			t4.ShouldBe(1);
		}

		public class MyQueue<T>
		{
			private T[] _backingArray;

			private int _nextInsertIndex;
			private int _tailIndex;

			private int _count;

			public MyQueue(int capacity)
			{
				_backingArray = new T[capacity];
				_nextInsertIndex = 0;
				_tailIndex = 0;
				_count = 0;
			}

			public void Enqueue(T value)
			{
				if (_count + 1 > _backingArray.Length)
				{
					throw new InvalidOperationException("Queue is full");
				}

				_backingArray[_nextInsertIndex] = value;
				_nextInsertIndex += 1;

				if (_nextInsertIndex > _backingArray.Length - 1)
				{
					_nextInsertIndex = 0;
				}

				_count += 1;
			}

			public T Dequeue()
			{
				if (_count == 0)
				{
					// means that queue is empty
					throw new InvalidOperationException("Queue is empty");
				}

				var result = _backingArray[_tailIndex];
				
				_tailIndex += 1;
				if (_tailIndex > _backingArray.Length - 1)
				{
					_tailIndex = 0;
				}

				_count -= 1;

				return result;
			}
		}
	}
}
