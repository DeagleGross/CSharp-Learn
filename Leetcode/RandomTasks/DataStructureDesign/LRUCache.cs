using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

// https://leetcode.com/problems/lru-cache/

namespace LeetCodeSolutions.RandomTasks.DataStructureDesign
{
	[TestClass]
	public class LruCacheImplementation
	{
		[TestMethod]
		public void Solve()
		{
			var cache = new LRUCache(1);

			cache.Put(1,1);
			cache.Put(2,2);


			cache.Get(1).ShouldBe(-1);
		}

		[TestMethod]
		public void Solve2()
		{
			var cache = new LRUCache(2);

			cache.Put(1, 1);
			cache.Put(2, 2);
			cache.Put(1, 1);

			cache.Put(3, 3);

			cache.Get(2).ShouldBe(-1);
		}

		[TestMethod]
		public void Solve3()
		{
			var cache = new LRUCache(2);

			cache.Put(1, 1);
			cache.Put(2, 2);

			var value = cache.Get(1); // Move 1 at the end of LRU

			value.ShouldBe(1);

			cache.Put(3, 3); // evict 2 from lru

			var value1 = cache.Get(2); // no LRU movements since 2 is already evicted
			value1.ShouldBe(-1);

			cache.Put(4, 4); // evict 3

			var value2 = cache.Get(1);
			value2.ShouldBe(-1);

			var value3 = cache.Get(3);

			value3.ShouldBe(3);

			var value4 = cache.Get(4);

			value4.ShouldBe(4);
		}

		public class LRUCache
		{
			private readonly Dictionary<int, CacheEntry> _cache = new ();

			private CacheEntry _lruHead = null;
			private CacheEntry _lruTail = null;

			private class CacheEntry
			{
				public int Key { get; set; }
				public int Value { get; set; }
				public CacheEntry Previous { get; set; }
				public CacheEntry Next { get; set; }
			}

			private readonly int _maxCapacity;

			public LRUCache(int capacity)
			{
				_maxCapacity = capacity;
			}

			public int Get(int key)
			{
				if (_cache.ContainsKey(key))
				{
					var requiredEntry = _cache[key];
					var value= requiredEntry.Value;

					// move entry in linked list

					if (requiredEntry.Next == null)
					{
						// entry was accessed last - no need to move
						return value;
					}

					if (requiredEntry.Previous == null)
					{
						// means the required entry is at the beginning of the list

						requiredEntry.Next.Previous = null; // remove 
						_lruTail.Next = requiredEntry; // moive to the end
						_lruTail = _lruTail.Next;

						return value;
					}

					requiredEntry.Previous.Next = requiredEntry.Next; // remove entry from list
					_lruTail.Next = requiredEntry; // move to the beginning
					_lruTail = _lruTail.Next;

					return value;
				}

				return -1;
			}

			public void Put(int key, int value)
			{
				CheckCapacityAndEvict();

				if (!_cache.ContainsKey(key))
				{
					// new item
					var cacheEntry = new CacheEntry()
					{
						Key = key,
						Value = value,
						Next = null,
						Previous = null
					};

					if (_lruHead == null)
					{
						// first item
						_lruHead = cacheEntry;
						_lruTail = cacheEntry;
					}
					else
					{
						if (_lruTail == _lruHead)
						{
							//only one item 

							_lruTail.Previous = cacheEntry;
							cacheEntry.Next = _lruHead;
							_lruHead = cacheEntry;
						}
						else
						{
							// several lru items

							cacheEntry.Next = _lruHead;
							_lruHead = cacheEntry;
						}
					}

					_cache.Add(key, cacheEntry);
					return;
				}

				// already existing item
				
				var requiredEntry = _cache[key];
				requiredEntry.Value = value;

				// move entry in linked list

				if (requiredEntry.Next == null)
				{
					// entry was accessed last - no need to move
					return;
				}

				if (requiredEntry.Previous == null)
				{
					// means the required entry is at the beginning of the list

					requiredEntry.Next.Previous = null; // remove 
					_lruTail.Next = requiredEntry; // moive to the end
					_lruTail = _lruTail.Next;

					return;
				}

				requiredEntry.Previous.Next = requiredEntry.Next; // remove entry from list
				_lruTail.Next = requiredEntry; // move to the beginning
				_lruTail = _lruTail.Next;
			}

			private void CheckCapacityAndEvict()
			{
				if (_cache.Count == _maxCapacity)
				{
					// evict least recently used item
					var lruItemKey = _lruHead.Key;

					if (object.ReferenceEquals(_lruHead, _lruTail))
					{
						// only one element in lru list
						_lruHead = null;
						_lruTail = null;
					}
					else
					{
						_lruHead = _lruHead.Next;
						_lruHead.Previous = null; // remove head
					}

					_cache.Remove(lruItemKey);
				}
			}
		}
	}
}
