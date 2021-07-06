using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace ZigZagConversion
{
	/// <summary>
	/// https://leetcode.com/problems/zigzag-conversion/
	/// </summary>
	class Program
	{
		static void Main(string[] args)
		{
			var input = "ABCD";
			var numRows = 2;
			var output = Convert(input, numRows);
			Console.WriteLine(output);

			Console.ReadKey();
		}

		public static string Convert(string s, int numRows)
		{
			List<string> rows = new();
			for (int i = 0; i < numRows; i++)
			{
				rows.Add("");
			}

			int row = 0;

			bool down = false;

			foreach (var c in s)
			{
				rows[row] += c;

				if (row == 0
					|| row == numRows - 1)
				{
					down = !down;
				}

				if (down)
				{
					row += 1;
				}
				else
				{
					row -= 1;
				}
			}

			var agg = rows.Aggregate(
				new StringBuilder(),
				(a, r) =>
				{
					a.Append(r);
					return a;
				});

			return agg.ToString();
		}
	}
}
