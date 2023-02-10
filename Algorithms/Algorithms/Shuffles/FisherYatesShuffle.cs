using System;

namespace Algorithms.Shuffles;

// This is a shuffling algorithm that is used in MoreLinq Shuffle method.
internal class FisherYatesShuffle
{
	public static void GetRandomSubsequence(int[] arrayToShuffle, int subsequenceLength)
	{
		Random r = new();

		for (int i = subsequenceLength - 1; i > 0; i--)
		{
			int j = r.Next(0, i + 1);

			// shuffles array in-place
			// swap arr[i] with the
			// element at random index j
			(arrayToShuffle[i], arrayToShuffle[j]) = 
				(arrayToShuffle[j], arrayToShuffle[i]);
		}

		// Prints the random array
		for (int i = 0; i < subsequenceLength; i++)
		{
			Console.Write(arrayToShuffle[i] + " ");
		}
	}
}
