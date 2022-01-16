using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace DataStructures.StackWithMax
{
	[TestClass]
	public class StackWithMaxTests
	{
		[TestMethod]
		public void TestPush()
		{
			var stack = new StackWithMax<int>();

			stack.Push(1);
			stack.Push(2);
			stack.Push(3);

			stack.Length.ShouldBe(3);

			var value1 = stack.Pop();
			var value2 = stack.Pop();
			var value3 = stack.Pop();

			stack.Length.ShouldBe(0);

			value1.ShouldBe(3);
			value2.ShouldBe(2);
			value3.ShouldBe(1);
		}

		[TestMethod]
		public void TestPush_2()
		{
			var stack = new StackWithMax<int>();

			stack.Push(1);
			stack.Pop();

			stack.Push(2);
			stack.Push(3);
			stack.Pop();

			var value3 = stack.Pop();
			
			stack.Length.ShouldBe(0);
			
			value3.ShouldBe(2);
		}

		[TestMethod]
		public void Test_MaxElement()
		{
			var stack = new StackWithMax<int>();

			stack.Push(20);
			stack.Push(-1);
			stack.Push(2);
			stack.Push(10);
			stack.Push(35);
			stack.Push(10);
			stack.Push(0);
			stack.Push(50);

			stack.GetMaxElement().ShouldBe(50);
			stack.Pop(); // pop 50
			stack.GetMaxElement().ShouldBe(35);

			stack.Pop(); // pop 0
			stack.Pop(); // pop 10
			stack.Pop(); // pop 35

			stack.GetMaxElement().ShouldBe(20);
		}

		[TestMethod]
		public void Test_MaxElement_AddAndRemove()
		{
			var stack = new StackWithMax<int>();

			stack.Push(20);
			stack.Push(-1);
			stack.Push(2);
			stack.Push(10);
			stack.Push(35);

			stack.Pop(); // pop 35

			// current max = 20

			stack.GetMaxElement().ShouldBe(20);

			stack.Push(21);

			stack.GetMaxElement().ShouldBe(21);
		}
	}
}
