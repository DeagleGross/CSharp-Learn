using System;
using System.Net.NetworkInformation;
using System.Text;
using Serilog;

namespace Sorting
{
	public abstract class SortingMechanismBase
	{
		protected readonly int[] Values;
		protected readonly int Size;

		protected SortingMechanismBase(Random random, int size = 50)
		{
			Size = size;
			Values = GenerateArrayToSort(random, size);
		}

		protected abstract void Sort();

		public void RunSortAndGetResults()
		{
			Sort();

			if (IsSorted())
			{
				Log.Information($"Is sorted correctly.");
			}
			else
			{
				Log.Error($"Sorting failed, please, review your code.");
			}
			
			var stringBuilder = new StringBuilder();
			foreach (var value in Values)
			{
				stringBuilder.Append($"{value} ");
			}

			Log.Debug($"Result array: {stringBuilder}");
		}

		private bool IsSorted()
		{
			if (Values.Length <= 1)
			{
				return true;
			}
			
			for (var i = 1; i < Values.Length; i++)
			{
				if (Values[i - 1] > Values[i])
				{
					return false;
				}
			}

			return true;
		}

		private static int[] GenerateArrayToSort(Random random, int size)
		{
			var initialSortArray = new int[size];

			for (var i = 0; i < size; i++)
			{
				initialSortArray[i] = random.Next(0, 101);
			}

			return initialSortArray;
		}
	}
}