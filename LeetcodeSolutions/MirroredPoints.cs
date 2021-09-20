using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;

/*
2) дан набор точек на координатной плоскости в таком виде:

Class Point 
{
	int x; 
	int y;

	GetHashCode() { ... }
	Equals() { ... } 
}

Набор точек не упорядочен ни по Х, ни по У.

Нужно выяснить, можно ли провести вертикальную прямую линию такую, что она будет разделять точки на 2 группы, которые будут зеркальным отражением друг для друга.

Ну то есть такая картинка 

.  .  | .   .
.     |     .
 */

var ret = CanMirror(
	new Point[]
	{
		new(1,1),
		new (7,1),
		new(2,4),
		new(6,4)
	});

if (ret)
{
	Console.WriteLine("Mirror line exists");
}
else
{
	Console.WriteLine("Mirror line does not exist");
}

static bool CanMirror(params Point[] points)
{
	Dictionary<int, HashSet<int>> pointsByLines = new();

	//int[,] field = new int[100, 100];

	Point? leftmostPoint = null;
	Point? rightmostPoint = null;

	foreach (var point in points) {
		if (!pointsByLines.ContainsKey(point.Y))
		{
			pointsByLines.Add(point.Y, new());
		}

		pointsByLines[point.Y].Add(point.X);

		if (leftmostPoint == null
			&& rightmostPoint == null)
		{
			leftmostPoint = point;
			rightmostPoint = point;
		}
		else
		{
			// both points not null
			if (point.X < leftmostPoint.Value.X)
			{
				leftmostPoint = point;
			}

			if (point.X > rightmostPoint.Value.X)
			{
				rightmostPoint = point;
			}
		}
	}
	Console.WriteLine($"Leftmost point : {leftmostPoint}, Rightmost point: {rightmostPoint}");

	if (leftmostPoint.Value.Y != rightmostPoint.Value.Y)
	{
		return false;
	}

	var distanceCenter = (rightmostPoint.Value.X - leftmostPoint.Value.X) / 2;
	var lineXCoordinate = leftmostPoint.Value.X + distanceCenter;
	Console.WriteLine($"Line X position {lineXCoordinate}");

	foreach (var line in pointsByLines)
	{
		foreach (var pointX in line.Value)
		{
			if (pointX < lineXCoordinate)
			{
				var distanceToCenter = lineXCoordinate - pointX;
				if (!line.Value.Contains(pointX + 2 * distanceToCenter))
				{
					return false;
				}
			}
			else
			{
				// pointx > linecoordinate
				var distanceToCenter = pointX - lineXCoordinate;
				if (!line.Value.Contains(pointX - 2 * distanceToCenter))
				{
					return false;
				}
			}
		}
	}

	return true;
}