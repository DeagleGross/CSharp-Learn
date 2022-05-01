using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

// https://leetcode.com/problems/find-the-celebrity/

namespace LeetCodeSolutions.RandomTasks.GraphTheory
{
	[TestClass]
	public class FindTheCelebrity
	{
		// test cases can't be performed since we don't know Knows method from base class on LeetCode
		public void Solve()
		{
			int[][] graph = new int[][]
			{
				new int[] {1, 1, 0},
				new int[] {0, 1, 0},
				new int[] {1, 1, 1},
			};

			var n = graph.Length;

			var result = FindCelebrity(n);

			result.ShouldBe(1);
		}

		// This method is defined in solution base class on LeetCode
		bool Knows(int a, int b)
		{
			return true;
		}

		public int FindCelebrity(int n)
		{
			Dictionary<int, HashSet<int>> whomPersonKnows = new();

			var probableCelebrity = 0;

			HashSet<int> notCelebrities = new HashSet<int>();

			while (notCelebrities.Count < n)
			{
				if (notCelebrities.Contains(probableCelebrity))
				{
					probableCelebrity++;
					continue;
				}

				int peopleWhoKnowThisPerson = 0;

				for (int i = 0; i < n; i++)
				{
					if (i == probableCelebrity)
					{
						continue;
					}

					if (!whomPersonKnows.ContainsKey(probableCelebrity))
					{
						whomPersonKnows[probableCelebrity] = new();
					}
					
					if (Knows(probableCelebrity, i))
					{
						// probableCelebrity is not a celebrity since he knows someone
						// but i is maybe a celebrity since at leat one person knows him - we don't check it here but we might as well have

						whomPersonKnows[probableCelebrity].Add(i);
						notCelebrities.Add(probableCelebrity);

						break;
					}
					else
					{
						// probableCelebrity does not know i, check whether i knows probableCelebrity;

						if (whomPersonKnows.ContainsKey(i)
							&& whomPersonKnows[i].Contains(probableCelebrity))
						{
							// this is possibly a celebrity - continue check
							peopleWhoKnowThisPerson++;
							continue;
						}

						if (!Knows(i, probableCelebrity))
						{
							// probableCelebrity is not a celebrity since i does not know probableCelebrity
							notCelebrities.Add(probableCelebrity);
							break;
						}
						else
						{
							// i knows probable celebrity - continue check

							peopleWhoKnowThisPerson++;

							if (!whomPersonKnows.ContainsKey(i))
							{
								whomPersonKnows.Add(i, new());
							}

							whomPersonKnows[i].Add(probableCelebrity);
						}
					}
				}

				// if we get here - it means that probable celebrity does not know anyone
				if (peopleWhoKnowThisPerson == n - 1)
				{
					return probableCelebrity;
				}
				else
				{
					notCelebrities.Add(probableCelebrity);
				}

				// not everyone knnows probableCelebrity - it is not a celebrity

				probableCelebrity++;
			}

			return -1;
		}

		#region From solution - alternative variant + add some caching

		public int FindCelebrity_Alt(int n)
		{
			int celebrityCandidate = 0;
			for (int i = 0; i < n; i++)
			{
				if (Knows(celebrityCandidate, i))
				{
					// celebrityCandidate is not a celebrity since hi knows someone
					// but i might be a celebrity since at least celebrityCandidate knows him
					celebrityCandidate = i;
				}
			}

			if (IsCelebrity(celebrityCandidate, n))
			{
				return celebrityCandidate;
			}
			return -1;
		}

		private bool IsCelebrity(int i, int numberOfPeople)
		{
			for (int j = 0; j < numberOfPeople; j++)
			{
				if (i == j)
				{
						continue; // Don't ask if they know themselves.
				}
				if (Knows(i, j) || !Knows(j, i))
				{
					return false;
				}
			}
			return true;
		}

		#endregion

	}
}
