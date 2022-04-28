using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

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

		[TestMethod]
		public void Solve3()
		{
			var set = new RandomizedSet();

			set.Remove(0);
			set.Remove(0);

			set.Insert(0);

			set.GetRandom();

			set.Remove(0);
			var t1= set.Insert(0);

			t1.ShouldBe(true);
		}

		public class RandomizedSet
		{
			private readonly Random _rnd = new Random(DateTime.Now.Millisecond);

			private readonly List<int> _values = new();
			private readonly Dictionary<int, int> _valuePositions = new();

			public RandomizedSet()
			{

			}

			public bool Insert(int val)
			{
				if (_valuePositions.ContainsKey(val))
				{
					return false;
				}

				_values.Add(val);
				_valuePositions.Add(val, _values.Count-1);

				return true;
			}

			public bool Remove(int val)
			{
				/*
				To delete a value at arbitrary index takes linear time. 
				The solution here is to always delete the last value:
				    Swap the element to delete with the last one.
				    Pop the last element out.
				*/

				if (!_valuePositions.ContainsKey(val))
				{
					return false;
				}

				// move the last element to the place idx of the element to delete
				int lastElement = _values[^1];
				int elementToRemovePosition = _valuePositions[val];

				_values[elementToRemovePosition] = lastElement;
				_valuePositions[lastElement] = elementToRemovePosition;

				// delete the last element

				_values.RemoveAt(_values.Count-1);
				_valuePositions.Remove(val);

				return true;
			}

			public int GetRandom()
			{
				// Approach with cryptographic RandomNumberGenerator is slow since the generator itself is slow;

				//Span<byte> number = stackalloc byte[4];
				//RandomNumberGenerator.Fill(number);
				//var integer = Math.Abs(BitConverter.ToInt32(number));
				//var index = integer % _values.Count;

				var integer = _rnd.Next(0, _values.Count);
				
				return _values[integer];
			}
		}
	}
}
