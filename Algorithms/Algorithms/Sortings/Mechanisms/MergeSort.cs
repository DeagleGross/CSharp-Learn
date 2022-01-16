using System;

namespace Algorithms.Sortings.Mechanisms
{
	public class MergeSort : SortingMechanismBase
	{
		public MergeSort(Random random) : base(random)
		{
		}

		protected override void Sort()
		{
			Sort(0, Size - 1);
		}

		private void Sort(int l, int r)
		{
			if (l < r)
			{
				// middle point
				int m = l + (r - l) / 2;
				
				Sort(l, m); // sorting left part
				Sort(m + 1, r); // sorting right part
				
				Merge(l, m, r); // merging parts of array
			}
		}

		private void Merge(int l, int m, int r)
		{
			var leftSize = m - l + 1;
			var rightSize = r - m;

			var leftArray = new int[leftSize];
			var rightArray = new int[rightSize];

			// copy initial array to two arrays
			for (var x = 0; x < leftSize; x++)
			{
				leftArray[x] = Values[l + x];
			}

			for (var x = 0; x < rightSize; x++)
			{
				rightArray[x] = Values[m + 1 + x];
			}

			var k = l;
			var left = 0;
			var right = 0;

			// merging
			while (left < leftSize && right < rightSize)
			{
				if (leftArray[left] < rightArray[right])
				{
					Values[k++] = leftArray[left++];
				}
				else
				{
					Values[k++] = rightArray[right++];
				}
			}
			
			// copy remaining items in each of arrays
			while (left < leftSize)
			{
				Values[k++] = leftArray[left++];
			}
			
			while (right < rightSize)
			{
				Values[k++] = rightArray[right++];
			}
		}
	}
}