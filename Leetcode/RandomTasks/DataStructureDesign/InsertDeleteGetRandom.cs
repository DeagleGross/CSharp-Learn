using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// https://leetcode.com/problems/insert-delete-getrandom-o1/

namespace LeetCodeSolutions.RandomTasks.DataStructureDesign
{
	[TestClass]
	public class InsertDeleteGetRandom
	{
		[TestMethod]
		public void Solve()
		{
			var set = new RandomizedSet();

			set.Insert(1);
			set.Remove(2);
			set.Insert(2);

			var r1 = set.GetRandom();

			set.Remove(1);
			set.Insert(2);

			var r2 = set.GetRandom();
		}

		[TestMethod]
		public void Solve2()
		{
			var set = new RandomizedSet();

			set.Insert(0);
			set.Insert(1);
			set.Insert(2);

			set.GetRandom();
			set.GetRandom();
			set.GetRandom();
			set.GetRandom();
			set.GetRandom();

			set.GetRandom();
			set.GetRandom();
			set.GetRandom();
			set.GetRandom();
			set.GetRandom();
		}

		public class RandomizedSet
		{
			private Random _rnd = new Random(DateTime.Now.Millisecond);

			private class SetItem
			{
				public int Value;
			}

			private readonly List<SetItem> _values = new();

			// this needs to be updated when value is removed
			private readonly Dictionary<int, SetItem> _valueItems = new();

			public RandomizedSet()
			{

			}

			public bool Insert(int val)
			{
				if (_valueItems.ContainsKey(val))
				{
					return false;
				}

				var setItem = new SetItem()
				{
					Value = val
				};

				_values.Add(setItem);

				_valueItems.Add(val, setItem);

				return true;
			}

			public bool Remove(int val)
			{
				if (!_valueItems.ContainsKey(val))
				{
					return false;
				}

				var setItem = _valueItems[val];

				_values.Remove(setItem);
				_valueItems.Remove(val);

				return true;
			}

			public int GetRandom()
			{
				// Approach with cryptographic RandomNumberGenerator is slow since the generator itself is slow;

				//Span<byte> number = stackalloc byte[4];
				//RandomNumberGenerator.Fill(number);
				//var integer = Math.Abs(BitConverter.ToInt32(number));

				var integer = _rnd.Next(0, _values.Count);

				var index = integer % _values.Count;

				return _values[index].Value;
			}
		}
	}
}
