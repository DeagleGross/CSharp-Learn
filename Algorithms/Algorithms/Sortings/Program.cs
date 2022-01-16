using System;
using Algorithms.Sortings.Mechanisms;

namespace Algorithms.Sortings
{
	public static class Sortings
	{
		public static void RunAll()
		{
            var random = new Random();
			
			var sortingMechanisms = new SortingMechanismBase[]
			{
				new BubbleSort(random),
				new InsertionSort(random),
				new MergeSort(random),
				new QuickSort(random),
				new HeapSort(random),
				new CountingSort(random),
				new RadixSort(random)
			};

			foreach (var sortingMechanism in sortingMechanisms)
			{
                Console.WriteLine($"Sorting mechanism {sortingMechanism.GetType()}");
				sortingMechanism.RunSortAndGetResults();
                Console.WriteLine($"Finished mechanism {sortingMechanism.GetType()}");
                Console.WriteLine("------");
                Console.WriteLine("");
			}
		}
	}
}