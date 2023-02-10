using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Sampling;

internal class ReservoirSampling
{
	// A function to randomly select samplesCount
	// items from stream[0..sampledSubsequenceLength-1].
	public static void ReservoirSamplingAlgorithmR(
		int[] stream,
		int sampledSubsequenceLength,
		int samplesCount)
	{
		// index for elements in stream[]
		int i;

		// reservoir[] is the output array.
		// Initialize it with first samplesCount
		// elements from stream[]

		int[] reservoir = new int[samplesCount];

		for (i = 0; i < samplesCount; i++)
		{
			reservoir[i] = stream[i];
		}

		Random r = new();

		// Iterate from the (samplesCount+1)th
		// element to sampledSubsequenceLength'th element
		for (; i < sampledSubsequenceLength; i++)
		{
			// Pick a random index from 0 to i.
			int j = r.Next(i + 1);

			// If the randomly picked index
			// is smaller than samplesCount, then replace
			// the element present at the index
			// with new element from stream
			if (j < samplesCount)
			{
				reservoir[j] = stream[i];
			}
		}

		// print out selected items
		for (i = 0; i < samplesCount; i++)
		{
			Console.Write(reservoir[i] + " ");
		}
	}
}
