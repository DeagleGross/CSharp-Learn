namespace DataStructures.HashTable
{
	internal struct Entry<TKey, TValue>
	{
		public TKey Key;
		public TValue Value;
		public int HashCode;
		public int? Next;
	}
}