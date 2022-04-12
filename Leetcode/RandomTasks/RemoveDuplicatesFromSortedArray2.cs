using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

// https://leetcode.com/problems/remove-duplicates-from-sorted-array-ii/

namespace LeetCodeSolutions.RandomTasks
{
	[TestClass]
	public class RemoveDuplicatesFromSortedArray2
	{
		[TestMethod]
		public void Solve()
		{
			int[] input = new[] {1, 1, 1, 2, 2, 3};
			var numberOfUniques = RemoveDuplicates(input);

			numberOfUniques.ShouldBe(5);

			Array.Resize(ref input, numberOfUniques);

			input.ShouldBe(new []{ 1, 1, 2, 2, 3 });
		}

		[TestMethod]
		public void Solve2()
		{
			int[] input = new[] {0, 0, 1, 1, 1, 1, 2, 3, 3};
			var numberOfUniques = RemoveDuplicates(input);

			numberOfUniques.ShouldBe(7);

			Array.Resize(ref input, numberOfUniques);

			input.ShouldBe(new []{ 0,0, 1, 1, 2, 3, 3 });
		}

		[TestMethod]
		public void Solve3()
		{
			int[] input = new[] { 1, 1, 1, 1 };
			var numberOfUniques = RemoveDuplicates(input);

			numberOfUniques.ShouldBe(2);

			Array.Resize(ref input, numberOfUniques);

			input.ShouldBe(new[] { 1, 1 });
		}

		public int RemoveDuplicates(int[] nums)
		{
			// returns number of non duplicate values
			int? previousValue = null;
			const int maxDupes = 1;
			int dupesLeft = maxDupes;
			int numberOfUniques = 0;

			int firstFreeCell = -1;

			// sweep

			for (int i = 0; i < nums.Length; i++)
			{
				if (nums[i] != previousValue)
				{
					numberOfUniques++;
					previousValue = nums[i];
					dupesLeft = maxDupes;
					continue;
				}

				if (nums[i] == previousValue)
				{
					if (dupesLeft > 0)
					{
						dupesLeft--;
						previousValue = nums[i];
						numberOfUniques++;
						continue;
					}
					
					nums[i] = int.MinValue;

					if (firstFreeCell == -1)
					{
						firstFreeCell = i;
					}
				}
			}

			// compact

			if (firstFreeCell == -1) // no free cells
			{
				return numberOfUniques;
			}

			for (int i = firstFreeCell; i < nums.Length-1; i++) // we may optimize it a bit
			{
				if (nums[i] != int.MinValue)
				{
					continue;
				}

				var j = i + 1;
				while (j < nums.Length-1)
				{
					if (nums[j] != int.MinValue)
					{
						break;
					}

					j++;
				}

				nums[i] = nums[j];
				nums[j] = Int32.MinValue;

				if (i == numberOfUniques - 1)
				{
					break;
				}
			}

			return numberOfUniques;
		}
	}

	
}
