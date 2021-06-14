using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

// ReSharper disable InconsistentNaming

namespace DataStructures.HashTable
{
	[TestClass]
	public class HashTable_Tests
	{
		[TestMethod]
		public void AddOperation()
		{
			var hashTable = new HashTable<int, int>();
			
			hashTable.Add(1, 100);
            hashTable.Add(2, 101);
            hashTable.Add(3, 102);

            hashTable.ContainsKey(1).ShouldBeTrue();
            hashTable.ContainsKey(2).ShouldBeTrue();
            hashTable.ContainsKey(3).ShouldBeTrue();
            
            hashTable.ContainsKey(4).ShouldBeFalse();
        }

        [TestMethod]
        public void DeleteOperation()
        {
            var hashTable = new HashTable<int, int>();

            hashTable.Add(1, 100);
            hashTable.Remove(1);

            hashTable.ContainsKey(1).ShouldBeFalse();
        }
	}
}