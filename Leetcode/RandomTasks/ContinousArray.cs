using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Shouldly;

namespace LeetCodeSolutions.RandomTasks;

/*https://leetcode.com/problems/contiguous-array/
 Given a binary array nums, return the maximum length of a contiguous subarray with an equal number of 0 and 1.
Input: nums = [0,1]
Output: 2
Explanation: [0, 1] is the longest contiguous subarray with an equal number of 0 and 1.

Input: nums = [0,1,0]
Output: 2
Explanation: [0, 1] (or [1, 0]) is a longest contiguous subarray with equal number of 0 and 1.
 */

[TestClass]
public class ContinousArray
{
	[TestMethod]
	public void Solve()
	{
		var arr = new[] { 
			1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 1, 1, 0, 1, 0, 0,
			1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 1, 0, 0, 0, 
			0, 1, 0, 1, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1, 0, 0, 1, 
			1, 1, 1, 1, 0, 0, 1, 0, 1, 1, 0, 0, 0, 1, 0, 0, 0, 
			1, 1, 1, 0, 1, 1, 0, 1, 0, 0, 1, 1, 0, 1, 0, 0, 1, 
			1, 1, 0, 0, 1, 0, 1, 1, 1, 0, 0, 1, 0, 1, 1};

		var length = FindMaxLength_FromSolution(arr);
		length.ShouldBe(94);
	}

	public int FindMaxLength_BruteForce_SubOptimal(int[] nums)
	{
		int maxLength = 0;

		int startingPoint = 0;

		if (nums.Length == 0 || nums.Length == 1)
		{
			return 0;
		}

		while (startingPoint < nums.Length)
		{
			if (nums.Length - startingPoint <= maxLength)
			{
				// we've already found max length
				break;
			}

			var sum = 0;

			for (int i = startingPoint; i < nums.Length; i++)
			{
				sum += nums[i];
				var subarrayLength = (i - startingPoint) + 1;
				if (subarrayLength % 2 == 0)
				{
					if (sum == subarrayLength / 2)
					{
						if (maxLength < subarrayLength)
						{
							maxLength = subarrayLength;
						}
					}
				}
			}

			startingPoint += 1;
		}

		return maxLength;
	}

	public int FindMaxLength_FromSolution(int[] nums)
	{
		Dictionary<int, int> counts = new();
		counts.Add(0, -1);

		int maxLength = 0;
		int count = 0; 

		// incerement count when we see 1, decrement when we see 0 (or vice-versa - it does not really matter)
		// if at any moment, the countcountcount becomes zero, it implies that we've encountered equal number of zeros and ones from the beginning till the current index of the array (i)
		// if we encounter the same count twice while traversing the array, it means that the number of zeros and ones are equal between the indices corresponding to the equal count values.
		
		for (int i = 0; i < nums.Length; i++)
		{
			if (nums[i] == 1)
			{
				count += 1;
			}

			if (nums[i] == 0)
			{
				count -= 1;
			}

			if (counts.ContainsKey(count))
			{
				maxLength = Math.Max(maxLength, i - counts[count]);
			}
			else
			{
				counts.Add(count, i);
			}
		}

		return maxLength;
	}
}

