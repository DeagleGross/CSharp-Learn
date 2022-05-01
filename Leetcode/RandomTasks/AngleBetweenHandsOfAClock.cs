using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

// https://leetcode.com/problems/angle-between-hands-of-a-clock/

namespace LeetCodeSolutions.RandomTasks
{
	[TestClass]
	public class AngleBetweenHandsOfAClock
	{
		[TestMethod]
		public void Solve()
		{
			var hour = 12;
			var minute = 30;

			var result = AngleClock(hour, minute);

			result.ShouldBe(165);
		}

		[TestMethod]
		public void Solve2()
		{
			var hour = 3;
			var minute = 30;

			var result = AngleClock(hour, minute);

			result.ShouldBe(75);
		}

		[TestMethod]
		public void Solve3()
		{
			var hour = 3;
			var minute = 15;

			var result = AngleClock(hour, minute);

			result.ShouldBe(7.5);
		}


		private int _anglePerHour = 360 / 12;
		private int _anglePerminute = 360 / 60;

		public double AngleClock(int hour, int minutes)
		{
			var h = hour;
			if (hour == 12)
			{
				h = 0;
			}

			double hourHand = h * _anglePerHour;
			var minuteHand = minutes * _anglePerminute;

			// now we need to account for hour hand movement

			double partOfTheCircle = minuteHand / 360.0;

			var hourAdjustment = _anglePerHour * partOfTheCircle;

			hourHand = hourHand + hourAdjustment;

			var delta = Math.Max(hourHand, minuteHand) - Math.Min(hourHand, minuteHand);

			var ret = Math.Min(delta, 360 - delta);

			return ret;
		}
	}
}
