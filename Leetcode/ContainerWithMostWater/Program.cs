using System;

namespace ContainerWithMostWater
{
	/// <summary>
	/// https://leetcode.com/problems/container-with-most-water/
	/// </summary>
	
	class Program
	{
		static void Main(string[] args)
		{
			var input = new[] { 1, 8, 6, 2, 5, 4, 8, 3, 7 };
			var area = MaxArea(input);
			Console.WriteLine(area);
		}

		public static int MaxArea(int[] height)
		{
			int s = 0;

			int left = 0;
			int right = height.Length - 1;

			while (right > left)
			{
				var leftHeight = height[left];
				var rightHeight = height[right];

				var newS = Math.Min(leftHeight, rightHeight) * (right - left);

				if (newS > s)
				{
					s = newS;
				}

				if (leftHeight > rightHeight)
				{
					right--;
				}
				else
				{
					left++;
				}
			}

			return s;
		}

		public static int MaxAreaNaiive(int[] height)
		{
			int s = 0;
			for (int i = 0; i < height.Length; i++)
			{
				int left = height[i];
				int right = height[i];

				if (i == height.Length - 1)
				{
					break;
				}

				for (int j = i+1; j < height.Length; j++)
				{
					var newS = (j - i) * (Math.Min(left, height[j]));
					if (newS > s)
					{
						s = newS;
					}
				}
			}

			return s;
		}
	}
}
