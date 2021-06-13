using System;

namespace Sorting.Mechanisms
{
	public class HeapSort : SortingMechanismBase
	{
		public HeapSort(Random random, int size = 50) : base(random, size)
		{
		}

		protected override void Sort()
		{
			// build heap
			for (int i = Size / 2 - 1; i >= 0; i--)
			{
				Heapify(Size, i);
			}
				
			// extract root, move it to rightest element and heapify unprocessed part of array
			for (int i = Size - 1; i > 0; i--)
			{
				// move root to end
				var tmp = Values[0];
				Values[0] = Values[i];
				Values[i] = tmp;
				
				Heapify(i, 0);
			}
		}

		private void Heapify(int n, int i)
		{
			int largest = i;

			int left = 2 * i + 1;
			int right = 2 * i + 2;

			if (left < n && Values[largest] < Values[left])
				largest = left;
			
			if (right < n && Values[largest] < Values[right])
				largest = right;

			// if largest index is not of root one
			if (largest != i)
			{
				var tmp = Values[i];
				Values[i] = Values[largest];
				Values[largest] = tmp;
				
				Heapify(n, largest);
			}
		}
	}
}