using System;

namespace Algorithms.Sortings.Mechanisms
{
	public class InsertionSort : SortingMechanismBase
	{
		public InsertionSort(Random random) : base(random)
		{
		}

		protected override void Sort()
		{
			for (var i = 1; i < Size; i++)
			{
				var currentValue = Values[i];
				var compareIndex = i - 1;

				while (compareIndex >= 0 && currentValue < Values[compareIndex])
				{
					Values[compareIndex + 1] = Values[compareIndex]; 
					compareIndex--;
				}

				Values[compareIndex + 1] = currentValue;
			}
		}
	}
}