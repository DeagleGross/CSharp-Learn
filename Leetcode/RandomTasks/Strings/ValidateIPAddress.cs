using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// https://leetcode.com/problems/validate-ip-address/

namespace LeetCodeSolutions.RandomTasks.Strings
{
	[TestClass]
	public class ValidateIpAddress
	{
		[TestMethod]
		public void Solve()
		{
			string queryIp = "172.16.254.1";

			var result = ValidIPAddress(queryIp);

			result.Should().Be("IPv4");
		}

		private const string _ipV4 = "IPv4";
		private const string _ipV6 = "IPv6";
		private const string _neither = "Neither";

		public string ValidIPAddress(string queryIP)
		{
			var ip = queryIP.ToLowerInvariant();
			if (ip.Contains('.'))
			{
				return CheckV4(ip);
			}

			if (ip.Contains(':'))
			{
				return CheckV6(ip);
			}

			return _neither;
		}

		private string CheckV4(string ip)
		{
			// "x1.x2.x3.x4"
			var parts = ip.Split('.');
			if (parts.Length != 4)
			{
				return _neither;
			}

			foreach (var part in parts)
			{
				if ((part.Length == 0 || part.Length > 3)
					|| (part.Length > 1
						&& part.StartsWith('0')))
				{
					return _neither;
				}

				var parseSuccess = int.TryParse(
					part,
					NumberStyles.Integer,
					CultureInfo.InvariantCulture,
					out int result);

				if (!parseSuccess || result > 255)
				{
					return _neither;
				}
			}

			return _ipV4;
		}

		private string CheckV6(string ip)
		{
			//"x1:x2:x3:x4:x5:x6:x7:x8"
			var parts = ip.Split(":");
			if (parts.Length != 8)
			{
				return _neither;
			}

			foreach (var part in parts)
			{
				//65535
				if (part.Length == 0
					|| part.Length > 4)
				{
					return _neither;
				}

				var parseSuccess = int.TryParse(
					part,
					NumberStyles.HexNumber,
					CultureInfo.InvariantCulture,
					out int result);

				if (!parseSuccess || result > 65535)
				{
					return _neither;
				}
			}

			return _ipV6;
		}
	}
}
