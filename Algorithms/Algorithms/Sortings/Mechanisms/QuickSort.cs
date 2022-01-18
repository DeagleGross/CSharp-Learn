using System;

namespace Algorithms.Sortings.Mechanisms;

public class QuickSort : SortingMechanismBase
{
    public QuickSort(Random random) : base(random)
    {
    }

    protected override void Sort()
    {
        QuickSortAlgo(0, Size - 1);
    }

    private void QuickSortAlgo(int low, int high)
    {
        if (low < high)
        {
            var pi = Partition(low, high);

            QuickSortAlgo(low, pi - 1);
            QuickSortAlgo(pi + 1, high);
        }
    }

    private int Partition(int low, int high)
    {
        var pivot = Values[high];
        var lowest = low - 1;

        for (var i = low; i < high; i++)
        {
            if (Values[i] < pivot)
            {
                // placing current i-th element to left part (to lowest)
                lowest++;
                Swap(i, lowest);
            }
        }

        Swap(lowest + 1, high);
        return lowest + 1;
    }

    private void Swap(int i, int j)
    {
        var tmp = Values[i];
        Values[i] = Values[j];
        Values[j] = tmp;
    }
}