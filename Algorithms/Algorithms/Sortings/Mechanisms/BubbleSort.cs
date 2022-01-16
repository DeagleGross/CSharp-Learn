using System;

namespace Algorithms.Sortings.Mechanisms
{
	public class BubbleSort : SortingMechanismBase
	{
		public BubbleSort(Random random) : base(random)
		{
		}
		
		protected override void Sort()
		{
			for (var i = 0; i < Size - 1; i++)
			{
				for (var j = 0; j < Size - i - 1; j++)
				{
					if (Values[j] > Values[j + 1])
					{
						var tmp = Values[j];
						Values[j] = Values[j + 1];
						Values[j + 1] = tmp;
					}
				}
			}
		}
	}
}