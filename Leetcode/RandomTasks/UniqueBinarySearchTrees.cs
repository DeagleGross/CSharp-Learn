﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace LeetCodeSolutions.RandomTasks
{
	[TestClass]
	public class UniqueBinarySearchTrees
	{
		[TestMethod]
		public void Solve()
		{
			var input = 3;

			var result = NumTrees(input);

			result.ShouldBe(5);
		}

		public int NumTrees(int n)
		{
			int[] G = new int[n + 1];
			G[0] = 1;
			G[1] = 1;

			for (int i = 2; i <= n; ++i)
			{
				for (int j = 1; j <= i; ++j)
				{
					G[i] += G[j - 1] * G[i - j];
				}
			}
			return G[n];
		}
	}
}
