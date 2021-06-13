using System;
using Serilog;
using Sorting.Mechanisms;

namespace Sorting
{
	public static class Program
	{
		public static void Main()
		{
			Log.Logger = new LoggerConfiguration()
				.WriteTo.Console()
				.MinimumLevel.Debug()
				.CreateLogger();
			
			var random = new Random();
			
			var sortingMechanisms = new SortingMechanismBase[]
			{
				new BubbleSort(random),
				new InsertionSort(random),
				new MergeSort(random),
				new QuickSort(random),
				new HeapSort(random)
			};

			foreach (var sortingMechanism in sortingMechanisms)
			{
				Log.Information($"Sorting mechanism {sortingMechanism.GetType()}");
				sortingMechanism.RunSortAndGetResults();
				Log.Information($"Finished mechanism {sortingMechanism.GetType()}");
				Log.Information("------");
				Log.Information("");
			}
		}
	}
}