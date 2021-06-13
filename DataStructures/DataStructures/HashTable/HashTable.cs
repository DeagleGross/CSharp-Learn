using System.Collections;
using System.Collections.Generic;

namespace DataStructures.HashTable
{
	public class HashTable<TKey, TValue> : IDictionary<TKey, TValue>
	{
		private readonly int[] _buckets;
		private readonly Entry<TKey, TValue>[] _entries;

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			throw new System.NotImplementedException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public void Add(KeyValuePair<TKey, TValue> item)
		{
			if (item.Key == null)
			{
				
			}
		}

		public void Clear()
		{
			throw new System.NotImplementedException();
		}

		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			throw new System.NotImplementedException();
		}

		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			throw new System.NotImplementedException();
		}

		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			throw new System.NotImplementedException();
		}

		public int Count { get; }

		public bool IsReadOnly { get; }

		public void Add(TKey key, TValue value)
		{
			throw new System.NotImplementedException();
		}

		public bool ContainsKey(TKey key)
		{
			throw new System.NotImplementedException();
		}

		public bool Remove(TKey key)
		{
			throw new System.NotImplementedException();
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			throw new System.NotImplementedException();
		}

		public TValue this[TKey key]
		{
			get => throw new System.NotImplementedException();
			set => throw new System.NotImplementedException();
		}

		public ICollection<TKey> Keys { get; }

		public ICollection<TValue> Values { get; }
	}
}