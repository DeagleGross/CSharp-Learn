using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Algorithms.Vectors.KMeansClusterization.Infrastructure;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithms.Vectors.KMeansClusterization;

[TestClass]
public class KMeansClusterizationTests
{
	[TestMethod]
	public void Test_1D_1()
	{
		var vectors = new List<Vector>() 
		{
			new(new[]{ 1D}),
			new(new[]{ 2D})
		};
		
		var clusters = KMeansImpl.ClusterVectorsNaiive(vectors, 2);

		clusters.Count.Should().Be(2);
	}

	[TestMethod]
	public void Test_1D_2()
	{
		var vectors = new List<Vector>()
		{
			new(new[]{ 1D}),
			new(new[]{ 2D}),
			new(new[]{ 3D}),

			new(new[]{ 10D}),
			new(new[]{ 21D})
		};

		var clusters = KMeansImpl.ClusterVectorsNaiive(vectors, 2);

		clusters.Count.Should().Be(2);
	}

	[TestMethod]
	public void Test_2D_1()
	{
		var vectors = new List<Vector>()
		{
			new(new[]{ 1D, 1D}),
			new(new[]{ 2D, 2D}),
			new(new[]{ 3D, 3D}),

			new(new[]{ 20D, 20D}),
			new(new[]{ 21D, 21D})
		};

		var clusters = KMeansImpl.ClusterVectorsNaiive(vectors, 2);

		clusters.Count.Should().Be(2);
	}
}
