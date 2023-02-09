using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;

#nullable disable

namespace Algorithms.Vectors.KMeansClusterization.Infrastructure;

internal class Vector : IEquatable<Vector>
{
	public double[] Value { get; set; }

	public int DimensionsCount { get; }

	public Vector(double[] vectorValue) 
	{
		Value = vectorValue;
		DimensionsCount = vectorValue.Length;
	}

	public Vector(int dimensionsCount)
	{
		Value = new double[dimensionsCount];
		DimensionsCount = Value.Length;
	}

	public double this[int valueIndex]
	{
		get
		{
			if (valueIndex > Value.Length - 1)
			{
				throw new IndexOutOfRangeException();
			}

			return Value[valueIndex];
		}

		set
		{
			if (valueIndex > Value.Length - 1)
			{
				throw new IndexOutOfRangeException();
			}

			Value[valueIndex] = value;
		}
	}

	public override bool Equals(object obj)
	{
		if (obj is not Vector right)
		{
			return false;		
		}

		return Equals(right);
	}

	public override int GetHashCode() 
		=> HashCode.Combine(Value);

	public bool Equals(Vector other) 
	{
		VectorMath.EnsureVectorSizesEqual(this, other);

		for (int i = 0; i < this.DimensionsCount; i++)
		{
			if (this[i] != other[i])
			{
				return false;
			}
		}

		return true;
	}

	public override string ToString() => $"[{string.Join(", ", Value)}]";
}
