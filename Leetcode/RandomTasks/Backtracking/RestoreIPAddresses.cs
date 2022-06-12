using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// https://leetcode.com/problems/restore-ip-addresses/

namespace LeetCodeSolutions.RandomTasks.Backtracking
{
	[TestClass]
	public class RestoreIpAddresses1
	{
		[TestMethod]
		public void Solve()
		{
			var s = "25525511135";
			var result = RestoreIpAddresses(s);

			result.Count.Should().Be(2);
		}

		[TestMethod]
		public void Solve2()
		{
			var s = "0000";
			var result = RestoreIpAddresses(s);

			result.Count.Should().Be(1);
		}

		[TestMethod]
		public void Solve3()
		{
			var s = "101023";
			var result = RestoreIpAddresses(s);

			result.Count.Should().Be(5);
		}

		private List<string> _ips = new();

		public IList<string> RestoreIpAddresses(string s)
		{
			if (s.Length < 4)
			{
				return new List<string>();
			}

			HashSet<int> pos = new();

			Backtrack(1, s, pos);

			return _ips;
		}

		private void Backtrack(int index, string inputStr, HashSet<int> dotsPositions)
		{
			if (dotsPositions.Count == 3)
			{
				var str = inputStr;
				int dotCount = 0;
				foreach (var pos in dotsPositions)
				{
					str = str.Insert(pos+dotCount, ".");
					dotCount++;
				}

				// means we have composed an ip address
				// if it is valid - add it to results
				if (IPAddress.TryParse(str, out _))
				{
					var parts = str.Split(".");

					if (parts.Any(p => p.Length > 1 && p.StartsWith("0")))
					{
						return;
					}

					_ips.Add(str);
				}
				return;
			}

			if (index >= inputStr.Length)
			{
				return;
			}

			for (int i = index; i < inputStr.Length; i++)
			{
				dotsPositions.Add(i);

				Backtrack(i+1, inputStr, dotsPositions);

				dotsPositions.Remove(i);
			}
		}
	}
}
