using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Vectors.KMeansClusterization.Infrastructure.Exceptions;
internal class VectorLengthUnequalException : Exception
{
	public VectorLengthUnequalException(int leftDimensionsCount, int rightDimensionsCount)
		: base($"Two vectors dimensions count is not the same. Dimensions counts for vectors left: {leftDimensionsCount}, right : {rightDimensionsCount}")
	{ }
}
