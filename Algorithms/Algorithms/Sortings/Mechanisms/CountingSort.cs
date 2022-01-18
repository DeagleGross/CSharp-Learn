using System;

namespace Algorithms.Sortings.Mechanisms;

public class CountingSort : SortingMechanismBase
{
    public CountingSort(Random random, int size = 50) : base(random, size)
    {
    }

    protected override void Sort()
    {
        var output = new int[Size];

        var countSize = TopRange - BotRange + 1;
        var count = new int[countSize];

        // store count of each character
        for (int i = 0; i < Size; i++)
        {
            ++count[Values[i]];
        }

        // Change count[i] so that count[i]
        // now contains actual position of
        // this character in output array
        for (int i = 1; i < countSize; i++)
        {
            count[i] += count[i - 1];
        }

        // Build the output character array
        // To make it stable we are operating in reverse order.
        for (int i = Size - 1; i >= 0; i--)
        {
            output[count[Values[i]] - 1] = Values[i];
            --count[Values[i]];
        }

        // Copy the output array to arr, so
        // that arr now contains sorted
        // characters
        for (int i = 0; i < Size; ++i)
            Values[i] = output[i];
    }
}