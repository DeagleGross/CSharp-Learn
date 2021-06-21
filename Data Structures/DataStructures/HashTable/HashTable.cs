using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures.HashTable
{
	public class HashTable<TKey, TValue> : IDictionary<TKey, TValue>
	{
		/// <summary>
		/// Defines structure containing key and value of dictionary
		/// </summary>
        private struct Entry<TKey, TValue>
        {
            public TKey Key;
            public TValue Value;

            // if hashcode is null, then entry is not defined - this is a marker for entry
            public int? HashCode;
            public int? Next;
        }

        private const int InitialSize = 8;

		private int[] _buckets;
		private Entry<TKey, TValue>[] _entries;

		/// <summary>
		/// Index of first free entry
		/// </summary>
        private int _freeEntry;

        private int _nextInsertedEntry;

        private int _count;

        public int Count => _count;

        public HashTable(bool isReadOnly = false)
        {
            IsReadOnly = isReadOnly;
            _count = 0;
            
            _freeEntry = -1;
            _nextInsertedEntry = 0;

            _buckets = new int[InitialSize];
            _entries = new Entry<TKey, TValue>[InitialSize];
        }

        public void Add(KeyValuePair<TKey, TValue> item)
		{
            Add(item.Key, item.Value);
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key);
        }

        public void Add(TKey key, TValue value)
		{
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key), "Key can not be null");
            }

            var hashCode = key.GetHashCode() % _buckets.Length;

            // using free entry list to add new item
            if (_freeEntry != -1)
            {
                _buckets[hashCode] = _freeEntry;

                // setting free entry for next deleted item
                _freeEntry = _entries[_freeEntry].Next ?? -1;

                // saving element
                _entries[_freeEntry].HashCode = hashCode;
                _entries[_freeEntry].Next = null;
                _entries[_freeEntry].Key = key;
                _entries[_freeEntry].Value = value;

                _count++;
                return;
            }

            // resize if there is no place to insert
            if (_entries.Length == _count)
            {
                Array.Resize(ref _entries, _entries.Length * 2);
            }

            // set next entries-chain
            if (_buckets[hashCode] >= 0 && _buckets[hashCode] < _entries.Length)
            {
                var entry = _entries[_buckets[hashCode]];
                while (entry.HashCode != null && entry.Next != null)
                {
                    entry = _entries[entry.Next.Value];
                }

                entry.Next = _nextInsertedEntry;
            }

            // insert new entry
            _buckets[hashCode] = _nextInsertedEntry;
            _entries[_nextInsertedEntry].HashCode = hashCode;
            _entries[_nextInsertedEntry].Next = null;
            _entries[_nextInsertedEntry].Key = key;
            _entries[_nextInsertedEntry].Value = value;

            _nextInsertedEntry++;
            _count++;
        }

		public bool ContainsKey(TKey key)
		{
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key), "Key can not be null");
            }

            var hashCode = key.GetHashCode() % _buckets.Length;

            if (_buckets[hashCode] >= 0 && _buckets[hashCode] < _entries.Length)
            {
                var entry = _entries[_buckets[hashCode]];
                while (entry.HashCode != null)
                {
                    if (entry.HashCode == hashCode)
                    {
                        return true;
                    }

                    if (entry.Next == null)
                    {
                        break;
                    }

                    entry = _entries[entry.Next.Value];
                }
            }

            return false;
        }

		public bool Remove(TKey key)
		{
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key), "Key can not be null");
            }

            var hashCode = key.GetHashCode() % _buckets.Length;

            if (_buckets[hashCode] >= 0 && _buckets[hashCode] < _entries.Length)
            {
                Entry<TKey, TValue>? previousEntry = null;
                var entry = _entries[_buckets[hashCode]];

                while (entry.HashCode != null)
                {
                    if (entry.HashCode == hashCode)
                    {
                        // found exact entry to remove
                        if (_freeEntry != -1)
                        {
                            var freeEntry = _entries[_freeEntry];

                            while (freeEntry.HashCode != null && freeEntry.Next != null)
                            {
                                freeEntry = _entries[freeEntry.Next.Value];
                            }

                            // fixing chain
                            if (previousEntry.HasValue)
                            {
                                var previousEntry1 = previousEntry.Value;
                                previousEntry1.Next = entry.Next;
                                freeEntry.Next = previousEntry1.Next;
                            }
                        }
                        else
                        {
                            _freeEntry = _buckets[entry.HashCode.Value];
                        }

                        // setting entry as null
                        _entries[_buckets[entry.HashCode.Value]].Next = null;
                        _entries[_buckets[entry.HashCode.Value]].HashCode = null;

                        return true;
                    }

                    if (entry.Next == null)
                    {
                        break;
                    }

                    previousEntry = entry;
                    entry = _entries[entry.Next.Value];
                }
            }

            return false;
        }

        #region Not Implemented

        public bool IsReadOnly { get; }

        public ICollection<TKey> Keys => GetEntries().Select(entry => entry.Key).ToArray();

        public ICollection<TValue> Values => GetEntries().Select(entry => entry.Value).ToArray();

        private ICollection<Entry<TKey, TValue>> GetEntries()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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

        public bool TryGetValue(TKey key, out TValue value)
        {
            throw new System.NotImplementedException();
        }

        public TValue this[TKey key]
        {
            get => throw new System.NotImplementedException();
            set => throw new System.NotImplementedException();
        }

        #endregion
    }
}