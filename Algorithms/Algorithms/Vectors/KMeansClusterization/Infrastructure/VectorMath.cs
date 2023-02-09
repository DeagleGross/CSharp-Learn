using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

using Algorithms.Vectors.KMeansClusterization.Infrastructure.Exceptions;

namespace Algorithms.Vectors.KMeansClusterization.Infrastructure;
internal static class VectorMath
{
	/// <summary>
	/// Calculates vector eucledean distance (L2).
	/// </summary>
	public static double CalculateVectorL2Distance(Vector left, Vector right)
	{
		EnsureVectorSizesEqual(left, right);

		double distance = 0;

		for (int i = 0; i < left.DimensionsCount; i++)
		{
			double oneDimensionDistance = left[i] - right[i];
			distance += oneDimensionDistance * oneDimensionDistance;
		}

		return Math.Sqrt(distance);
	}

	/// <summary>
	/// Calculates vector eucledean distance (L2).
	/// </summary>
	public static double CalculateVectorCosineDistance(Vector left, Vector right)
	{
		EnsureVectorSizesEqual(left, right);

		double distance = 0;
		double norma = 0.0;
		double normb = 0.0;

		for (int i = 0; i < left.DimensionsCount; i++)
		{
			distance += left[i] * right[i];
			norma += left[i] * left[i];
			normb += right[i] * right[i];
		}

		var ret = 1 - (distance / (Math.Sqrt(norma) * Math.Sqrt(normb)));

		return ret;
	}

	/// <summary>
	/// Calculates vector eucledean distance (L2).
	/// </summary>
	public static double CalculateVectorInnerProductDistance(Vector left, Vector right)
	{
		EnsureVectorSizesEqual(left, right);

		double distance = 0;

		for (int i = 0; i < left.DimensionsCount; i++)
		{
			distance += left[i] * right[i];
		}

		return distance;
	}

	public static void EnsureVectorSizesEqual(Vector left, Vector right)
	{
		if (left.DimensionsCount != right.DimensionsCount)
		{
			throw new VectorLengthUnequalException(left.DimensionsCount, right.DimensionsCount);
		}
	}

	/// <summary>
	/// Adds specified <paramref name="vectorToAdd"/> vector to the specified <paramref name="vectorToAddTo"/>.
	/// </summary>
	public static void Add(Vector vectorToAddTo, Vector vectorToAdd) 
	{
		EnsureVectorSizesEqual(vectorToAddTo, vectorToAdd);

		for (int i = 0; i < vectorToAddTo.DimensionsCount; i++)
		{
			vectorToAddTo[i] = vectorToAddTo[i] + vectorToAdd[i];
		}
	}

	public static void DivideEachValue(Vector vectorToDivideEachValueIn, int divisor)
	{
		for (int i = 0; i < vectorToDivideEachValueIn.DimensionsCount; i++)
		{
			double result = vectorToDivideEachValueIn[i] / divisor;
			vectorToDivideEachValueIn[i] = result;
		}
	}
}
