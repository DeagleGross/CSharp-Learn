using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MoreLinq;

#nullable disable

namespace Algorithms.Vectors.KMeansClusterization.Infrastructure;

internal class KMeansImpl
{
	public static Dictionary<Vector, List<Vector>> ClusterVectorsNaiive(
		ICollection<Vector> vectorsToCluster, 
		int numberOfClusters, 
		int numberOfSamplesPerCluster = 50, // not used for now
		int maxIterations = 300) 
	{
		// select random centroids
		Vector[] centroids = vectorsToCluster.RandomSubset(numberOfClusters).ToArray();

		// while true
		// calculate distance from each vector to the each cluster centroid and assign vector to cluster
		// compute new centroids by averaging vectors in each cluster
		// if centroids don't change
		// break

		int currentIteration = 0;

		while (true)
		{
			var resultingClusters = AssignVectorsToClusters(vectorsToCluster, centroids);
			var newCentroids = CalculateNewCentroids(resultingClusters);

			if (currentIteration > maxIterations 
				|| !IsCentroidsChanged(centroids, newCentroids))
			{
				if (currentIteration > maxIterations)
				{
					Console.WriteLine("Max iterations exceeded");
				}

				return resultingClusters;
			}

			centroids = newCentroids;

			currentIteration++;
		}
	}

	private static bool IsCentroidsChanged(Vector[] oldCentroids, Vector[] newCentroids) 
	{
		for (int i = 0; i < oldCentroids.Length; i++)
		{
			if (!oldCentroids[i].Equals(newCentroids[i]))
			{
				return true;
			}
		}

		return false;
	}

	private static Vector[] CalculateNewCentroids(Dictionary<Vector, List<Vector>> clusters)
	{
		Vector[] newCentroids = new Vector[clusters.Count];

		int clusterIndex = 0;
		foreach (var cluster in clusters)
		{
			var clusteredVectors = cluster.Value;
			var clusteredVectorsCount = clusteredVectors.Count;

			Vector meanVector = new(clusteredVectors[0].DimensionsCount);

			foreach (var clusterVector in clusteredVectors)
			{ 
				VectorMath.Add(meanVector, clusterVector);
			}

			VectorMath.DivideEachValue(meanVector, clusteredVectorsCount);

			newCentroids[clusterIndex] = meanVector;

			clusterIndex++;
		}

		return newCentroids;
	}

	private static double CalculateClusterVariance(Vector centroid, IEnumerable<Vector> vectorsInCluster)
	{
		// we can use this function to assess variance difference between iterations
		double sumOfDistanceSquares = 0D;

		int vectorsCount = 1;

		foreach (var vector in vectorsInCluster)
		{
			var distanceToCentroid = VectorMath.CalculateVectorL2Distance(centroid, vector);
			sumOfDistanceSquares += Math.Pow(distanceToCentroid, 2);
			vectorsCount++;
		}

		var variance = sumOfDistanceSquares / vectorsCount;

		return variance;
	}

	private static Dictionary<Vector, List<Vector>> AssignVectorsToClusters(
		ICollection<Vector> vectorsToCluster,
		Vector[] centroids)
	{
		Dictionary<Vector, List<Vector>> ret = new();

		foreach (var vector in vectorsToCluster)
		{
			var distance = double.MaxValue;
			Vector chosenCentroid = null;

			foreach (var centroid in centroids)
			{
				double currentCentroidDistance = VectorMath.CalculateVectorL2Distance(vector, centroid);
				if (currentCentroidDistance < distance)
				{ 
					distance = currentCentroidDistance;
					chosenCentroid = centroid;
				}
			}

			if (chosenCentroid is null)
			{
				throw new InvalidOperationException($"Chosen centroid is null for vector {vector}");
			}

			if (!ret.ContainsKey(chosenCentroid))
			{
				ret[chosenCentroid] = new List<Vector>();
			}

			ret[chosenCentroid].Add(vector);
		}

		return ret;
	}
}
