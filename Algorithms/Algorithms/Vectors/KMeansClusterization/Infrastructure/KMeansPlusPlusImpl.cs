using System;

namespace Algorithms.Vectors.KMeansClusterization.Infrastructure;

internal class ClusteringKmProgram
{
	private static void Run()
	{
		Console.WriteLine("\nBegin k-means++ demo ");

		double[][] data = 
		{
			new[] { 0.65, 0.220 },
			new[] { 0.73, 0.160 },
			new[] { 0.59, 0.110 },
			new[] { 0.61, 0.120 },
			new[] { 0.75, 0.150 },
			new[] { 0.67, 0.240 },
			new[] { 0.68, 0.230 },
			new[] { 0.70, 0.220 },
			new[] { 0.62, 0.130 },
			new[] { 0.66, 0.210 },
			new[] { 0.77, 0.19 },
			new[] { 0.75, 0.180 },
			new[] { 0.74, 0.170 },
			new[] { 0.70, 0.210 },
			new[] { 0.61, 0.110 },
			new[] { 0.58, 0.100 },
			new[] { 0.66, 0.230 },
			new[] { 0.59, 0.120 },
			new[] { 0.68, 0.210 },
			new[] { 0.61, 0.130 }
		};
		
		Console.WriteLine("\nData: ");
		PrintMatrix(
			data,
			new[] { 2, 3 },
			new[] { 5, 8 },
			true);
		Console.WriteLine("\nPress Enter to continue \n");
		Console.ReadLine(); // pause

		int k = 3; // number of clusters
		string initMethod = "plusplus";
		int maxIter = 100; // max (likely less)
		int seed = 0;

		Console.WriteLine("\nSetting k = " + k);
		Console.WriteLine(
			"Setting initMethod = " +
			initMethod);
		Console.WriteLine(
			"Setting maxIter to converge = " +
			maxIter);
		Console.WriteLine("Setting seed = " + seed);
		KMeans km = new(k, data, initMethod, maxIter, seed);

		int trials = 10; // attempts to find best
		Console.WriteLine(
			"\nStarting clustering w/ trials = " +
			trials);
		km.Cluster(trials);
		Console.WriteLine("Done");

		Console.WriteLine("\nBest clustering found: ");
		PrintVector(km.Clustering, 3);

		Console.WriteLine("\nCluster counts: ");
		PrintVector(km.Counts, 4);

		Console.WriteLine("\nThe cluster means: ");
		PrintMatrix(
			km.Means,
			new[] { 4, 4 },
			new[] { 8, 8 },
			true);

		Console.WriteLine(
			"\nTotal within-cluster SS = " +
			km.Wcss.ToString("F4"));

		Console.WriteLine("\nClustered data: ");
		ShowClustered(
			data,
			k,
			km.Clustering,
			new[] { 2, 3 },
			new[] { 8, 10 },
			true);

		//// "knee" technique to find best k
		//Console.WriteLine("\nk WCSS:");
		//for (int k = 1; k <= 9; ++k)
		//{
		//  KMeans km = new KMeans(k, data, "plusplus", 100, k);
		//  km.Cluster(10);
		//  Console.WriteLine(k + " " + km.wcss.ToString("F4"));
		//}

		Console.WriteLine("\nPress any key to exit...");
		Console.ReadKey();
	}

	private static void PrintVector(int[] vec, int wid)
	{
		int n = vec.Length;
		for (int i = 0; i < n; ++i)
		{
			Console.Write(vec[i].ToString().PadLeft(wid));
		}

		Console.WriteLine("");
	}

	private static void PrintMatrix(
		double[][] m,
		int[] dec,
		int[] wid,
		bool isPrintIndices)
	{
		int n = m.Length;
		int iPad = n.ToString().Length + 1;

		for (int i = 0; i < n; ++i)
		{
			if (isPrintIndices)
			{
				Console.Write(
					"[" +
					i.ToString().PadLeft(iPad) + "] ");
			}

			for (int j = 0; j < m[0].Length; ++j)
			{
				double x = m[i][j];
				if (Math.Abs(x) < 1.0e-5)
				{
					x = 0.0;
				}

				Console.Write(
					x.ToString(
						"F" +
						dec[j]).PadLeft(wid[j]));
			}

			Console.WriteLine("");
		}
	}

	private static void ShowClustered(
		double[][] data,
		int K,
		int[] clustering,
		int[] decs,
		int[] wids,
		bool indices)
	{
		int n = data.Length;
		int iPad = n.ToString().Length + 1; // indices
		int numDash = 0;
		for (int i = 0; i < wids.Length; ++i)
		{
			numDash += wids[i];
		}

		for (int i = 0; i < numDash + iPad + 5; ++i)
		{
			Console.Write("-");
		}

		Console.WriteLine("");

		for (int k = 0; k < K; ++k) // display by cluster
		{
			for (int i = 0; i < n; ++i) // each data item
			{
				if (clustering[i] == k) // curr data is in cluster k
				{
					if (indices == true)
					{
						Console.Write(
							"[" +
							i.ToString().PadLeft(iPad) + "]   ");
					}

					for (int j = 0; j < data[i].Length; ++j)
					{
						double x = data[i][j];
						if (x < 1.0e-5)
						{
							x = 0.0; // prevent "-0.00"
						}

						string s = x.ToString("F" + decs[j]);
						Console.Write(s.PadLeft(wids[j]));
					}

					Console.WriteLine("");
				}
			}

			for (int i = 0; i < numDash + iPad + 5; ++i)
			{
				Console.Write("-");
			}

			Console.WriteLine("");
		}
	}
}

internal class KMeans
{
	private readonly int _numberOfClusters; // number clusters (use lower k for indexing)
	private readonly double[][] _data; // data to be clustered
	private readonly int _numberOfDataItems; // number data items
	private readonly int _dim; // number values in each data item
	private readonly string _initMethodName; // "plusplus", "forgy" "random". Only "plusplus" is supported
	private readonly int _maxIterationsCount; // max per single clustering attempt
	private readonly Random _rnd; // for centroids initialization

	public int[] Clustering { set; get; } // final cluster assignments
	public double[][] Means { set; get; } // final cluster means aka centroids
	public double Wcss { set; get; } // final total within-cluster sum of squares (inertia??)
	public int[] Counts { set; get; } // final num items in each cluster

	public KMeans(int numberOfClusters, double[][] data, string initMethodName, int maxIterationsCount, int seed)
	{
		_numberOfClusters = numberOfClusters;
		_data = data; // reference copy
		_initMethodName = initMethodName;
		_maxIterationsCount = maxIterationsCount;

		_numberOfDataItems = data.Length;
		_dim = data[0].Length;
		_rnd = new Random(seed);

		Means = new double[_numberOfClusters][]; // one mean per cluster
		for (int i = 0; i < _numberOfClusters; ++i)
		{
			Means[i] = new double[_dim];
		}

		Clustering = new int[_numberOfDataItems]; // cell val is cluster ID, index is data item
		Counts = new int[_numberOfClusters]; // one cell per cluster
		Wcss = double.MaxValue; // smaller is better
	}

	public void Cluster(int trials)
	{
		for (int trial = 0; trial < trials; ++trial)
		{
			Cluster(); // find a clustering and update bests
		}
	}

	public void Cluster()
	{
		// init clustering[] and means[][] 
		// loop at most maxIter times
		//   update means using curr clustering
		//   update clustering using new means
		// end-loop
		// if clustering is new best, update clustering, means, counts, wcss

		int[] currClustering = new int[_numberOfDataItems]; // [0, 0, 0, 0, .. ]

		double[][] currMeans = new double[_numberOfClusters][];
		for (int k = 0; k < _numberOfClusters; ++k)
		{
			currMeans[k] = new double[_dim];
		}

		if (_initMethodName == "plusplus")
		{
			InitPlusPlus(_data, currClustering, currMeans, _rnd);
		}
		else
		{
			throw new Exception("not supported");
		}

		bool isClusteringChanged; //  result from UpdateClustering (to exit loop)
		int iter = 0;
		while (iter < _maxIterationsCount)
		{
			UpdateMeans(currMeans, _data, currClustering);
			isClusteringChanged = UpdateClustering(
				currClustering,
				_data,
				currMeans);
			if (isClusteringChanged == false)
			{
				break; // need to stop iterating
			}

			++iter;
		}

		double currWcss = ComputeWithinClusterSs(
			_data,
			currMeans,
			currClustering);

		if (currWcss < Wcss) // new best clustering found
		{
			// copy the clustering, means; compute counts; store WCSS
			for (int i = 0; i < _numberOfDataItems; ++i)
			{
				Clustering[i] = currClustering[i];
			}

			for (int k = 0; k < _numberOfClusters; ++k)
			{
				for (int j = 0; j < _dim; ++j)
				{
					Means[k][j] = currMeans[k][j];
				}
			}

			Counts = ComputeCounts(_numberOfClusters, currClustering);
			Wcss = currWcss;
		}
	}

	private static void InitPlusPlus(
		double[][] data,
		int[] clustering,
		double[][] means,
		Random rnd)
	{
		//  k-means++ init using roulette wheel selection
		// clustering[] and means[][] exist
		int dim = data[0].Length;

		// select one data item index at random as 1st meaan
		int idx = rnd.Next(0, data.Length); // [0, N)
		for (int j = 0; j < dim; ++j)
		{
			means[0][j] = data[idx][j];
		}

		for (int k = 1; k < means.Length; ++k) // find each remaining mean
		{
			double[] dSquareds = new double[data.Length]; // from each item to its closest mean

			for (int i = 0; i < data.Length; ++i) // for each data item
			{
				// compute distances from data[i] to each existing mean (to find closest)
				double[] distances = new double[k]; // we currently have k means

				for (int ki = 0; ki < k; ++ki)
				{
					distances[ki] = EucDistance(data[i], means[ki]);
				}

				int mi = ArgMin(distances); // index of closest mean to curr item
											// save the associated distance-squared
				dSquareds[i] = distances[mi] * distances[mi]; // sq dist from item to its closest mean
			}

			// select an item far from its mean using roulette wheel
			// if an item has been used as a mean its distance will be 0
			// so it won't be selected

			int newMeanIdx = ProporSelect(dSquareds, rnd);
			for (int j = 0; j < dim; ++j)
			{
				means[k][j] = data[newMeanIdx][j];
			}
		} // k remaining means

		//Console.WriteLine("");
		//ShowMatrix(means, 4, 10);
		//Console.ReadLine();

		UpdateClustering(clustering, data, means);
	}

	private static int ProporSelect(double[] vals, Random rnd)
	{
		// roulette wheel selection
		// on the fly technique
		// vals[] can't be all 0.0s
		int n = vals.Length;

		double sum = 0.0;
		for (int i = 0; i < n; ++i)
		{
			sum += vals[i];
		}

		double cumP = 0.0; // cumulative prob
		double p = rnd.NextDouble();

		for (int i = 0; i < n; ++i)
		{
			cumP += vals[i] / sum;
			if (cumP > p)
			{
				return i;
			}
		}

		return n - 1; // last index
	}

	private static int[] ComputeCounts(int K, int[] clustering)
	{
		int[] result = new int[K];
		for (int i = 0; i < clustering.Length; ++i)
		{
			int cid = clustering[i];
			++result[cid];
		}

		return result;
	}

	private static void UpdateMeans(
		double[][] means,
		double[][] data,
		int[] clustering)
	{
		// compute the K means using data and clustering
		// assumes no empty clusters in clustering

		int K = means.Length;
		int N = data.Length;
		int dim = data[0].Length;

		int[] counts = ComputeCounts(K, clustering); // needed for means

		for (int k = 0; k < K; ++k) // make sure no empty clusters
		{
			if (counts[k] == 0)
			{
				throw new Exception("empty cluster passed to UpdateMeans()");
			}
		}

		double[][] result = new double[K][]; // new means
		for (int k = 0; k < K; ++k)
		{
			result[k] = new double[dim];
		}

		for (int i = 0; i < N; ++i) // each data item
		{
			int cid = clustering[i]; // which cluster ID?
			for (int j = 0; j < dim; ++j)
			{
				result[cid][j] += data[i][j]; // accumulate
			}
		}

		// divide accum sums by counts to get means
		for (int k = 0; k < K; ++k)
		{
			for (int j = 0; j < dim; ++j)
			{
				result[k][j] /= counts[k];
			}
		}

		// no 0-count clusters so update the means
		for (int k = 0; k < K; ++k)
		{
			for (int j = 0; j < dim; ++j)
			{
				means[k][j] = result[k][j];
			}
		}
	}

	private static bool UpdateClustering(
		int[] clustering,
		double[][] data,
		double[][] means)
	{
		// update existing cluster clustering using data and means
		// proposed clustering would have an empty cluster: return false - no change to clustering
		// proposed clustering would be no change: return false, no change to clustering
		// proposed clustering is different and has no empty clusters: return true, clustering is changed

		int[] result = new int[data.Length]; // proposed new clustering (cluster assignments)
		bool change = false; // is there a change to the existing clustering?
		int[] counts = new int[means.Length]; // check if new clustering makes an empty cluster

		for (int i = 0; i < data.Length; ++i) // make of copy of existing clustering
		{
			result[i] = clustering[i];
		}

		for (int i = 0; i < data.Length; ++i) // each data item
		{
			double[] dists = new double[means.Length]; // dist from curr item to each mean
			for (int k = 0; k < means.Length; ++k)
			{
				dists[k] = EucDistance(data[i], means[k]);
			}

			int cid = ArgMin(dists); // index of the smallest distance
			result[i] = cid;
			if (result[i] != clustering[i])
			{
				change = true; // the proposed clustering is different for at least one item
			}

			++counts[cid];
		}

		if (change == false)
		{
			return false; // no change to clustering -- clustering has converged
		}

		for (int k = 0; k < means.Length; ++k)
		{
			if (counts[k] == 0)
			{
				return false; // no change to clustering because would have an empty cluster
			}
		}

		// there was a change and no empty clusters so update clustering
		for (int i = 0; i < data.Length; ++i)
		{
			clustering[i] = result[i];
		}

		return true; // successful change to clustering so keep looping
	}

	private static double EucDistance(double[] item, double[] mean)
	{
		// Euclidean distance from item to mean
		// used to determine cluster assignments
		double sum = 0.0;
		for (int j = 0; j < item.Length; ++j)
		{
			sum += (item[j] - mean[j]) * (item[j] - mean[j]);
		}

		return Math.Sqrt(sum);
	}

	private static int ArgMin(double[] v)
	{
		int minIdx = 0;
		double minVal = v[0];
		for (int i = 0; i < v.Length; ++i)
		{
			if (v[i] < minVal)
			{
				minVal = v[i];
				minIdx = i;
			}
		}

		return minIdx;
	}

	private static double ComputeWithinClusterSs(
		double[][] data,
		double[][] means,
		int[] clustering)
	{
		// compute total within-cluster sum of squared differences between 
		// cluster items and their cluster means
		// this is actually the objective function, not distance
		double sum = 0.0;
		for (int i = 0; i < data.Length; ++i)
		{
			int cid = clustering[i]; // which cluster does data[i] belong to?
			sum += SumSquared(data[i], means[cid]);
		}

		return sum;
	}

	private static double SumSquared(double[] item, double[] mean)
	{
		// squared distance between vectors
		// surprisingly, k-means minimizes this, not distance
		double sum = 0.0;
		for (int j = 0; j < item.Length; ++j)
		{
			sum += (item[j] - mean[j]) * (item[j] - mean[j]);
		}

		return sum;
	}

	// display functions for debugging

	//private static void ShowVector(int[] vec, int wid)  // debugging use
	//{
	//  int n = vec.Length;
	//  for (int i = 0; i < n; ++i)
	//    Console.Write(vec[i].ToString().PadLeft(wid));
	//  Console.WriteLine("");
	//}

	//private static void ShowMatrix(double[][] m, int dec, int wid)  // debugging
	//{
	//  for (int i = 0; i < m.Length; ++i)
	//  {
	//    for (int j = 0; j < m[0].Length; ++j)
	//    {
	//      double x = m[i][j];
	//      if (Math.Abs(x) < 1.0e-5) x = 0.0;
	//      Console.Write(x.ToString("F" + dec).PadLeft(wid));
	//    }
	//    Console.WriteLine("");
	//  }
	//}

	//private static int[] PickDistinct(int n, int N, Random rnd)  // for Forgy init
	//{
	//  // pick n distict integers from  [0 .. N)
	//  int[] indices = new int[N];
	//  int[] result = new int[n];
	//  for (int i = 0; i < N; ++i)
	//    indices[i] = i;
	//  Shuffle(indices, rnd);
	//  for (int i = 0; i < n; ++i)
	//    result[i] = indices[i];
	//  return result;
	//}
}
