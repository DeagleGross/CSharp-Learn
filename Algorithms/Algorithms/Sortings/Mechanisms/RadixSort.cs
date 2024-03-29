﻿using System;
using System.Linq;

namespace Algorithms.Sortings.Mechanisms;

public class RadixSort : SortingMechanismBase
{
    public RadixSort(Random random, int size = 50) : base(random, size)
    {
    }

    protected override void Sort()
    {
        // Find the maximum number to know number of digits
        var max = Values.Max();

        // Do counting sort for every digit. Note that
        // instead of passing digit number, exp is passed.
        // exp is 10^i where i is current digit number
        for (int exp = 1; max / exp > 0; exp *= 10)
        {
            CountSort(Values, Size, exp);
        }
    }

    // A function to do counting sort of arr[] according to
    // the digit represented by exp.
    public static void CountSort(int[] arr, int n, int exp)
    {
        int[] output = new int[n]; // output array
        int i;
        int[] count = new int[10];

        // initializing all elements of count to 0
        for (i = 0; i < 10; i++)
            count[i] = 0;

        // Store count of occurrences in count[]
        for (i = 0; i < n; i++)
            count[(arr[i] / exp) % 10]++;

        // Change count[i] so that count[i] now contains
        // actual position of this digit in output[]
        for (i = 1; i < 10; i++)
            count[i] += count[i - 1];

        // Build the output array
        for (i = n - 1; i >= 0; i--)
        {
            output[count[(arr[i] / exp) % 10] - 1] = arr[i];
            count[(arr[i] / exp) % 10]--;
        }

        // Copy the output array to arr[], so that arr[] now
        // contains sorted numbers according to current
        // digit
        for (i = 0; i < n; i++)
            arr[i] = output[i];
    }
}