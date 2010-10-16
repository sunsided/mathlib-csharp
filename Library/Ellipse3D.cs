using System;
using System.Collections.Generic;
using System.Text;
using Library.Matrix;
using Library.Vector;

namespace Library
{
	public struct Ellipse3D
	{
		public Vector3D Center;
		public Vector3D Radius;

		/// <summary>
		/// Creates an ellipse based on a point and three radii
		/// </summary>
		/// <param name="center">Center point</param>
		/// <param name="radius">Radius</param>
		public Ellipse3D(Vector3D center, Vector3D radius)
		{
			Center = center;
			Radius = radius;
		}

		/// <summary>
		/// Checks whether the Ellipse contains a point
		/// </summary>
		/// <param name="point">The point to test</param>
		/// <returns>Boolean</returns>
		public bool Contains(Vector3D point)
		{
			// Create world matrix
			Matrix4D world = new Matrix4D();
			world.ToTranslation(Center.X, Center.Y, Center.Z);

			// Get Scaling matrix
			Matrix4D scale = new Matrix4D();
			scale.ToScaling(Radius.X, Radius.Y, Radius.Z);

			// Matrizen verbinden
			Matrix4D final = scale * world;
			final = scale.GetInverted();

			Vector3D center = final * Center;
			Vector3D newPoint = final * point;

			// Get distance to center
			double distance = newPoint.GetDistance(center);
			// If the distance is smaller than the transformed radius, we are cutting the edge
			if (distance <= 1f) return true;
			return false;
		}

		///// <summary>
		///// Checks whether the Sphere intersects with a line
		///// </summary>
		///// <param name="line">The line to test</param>
		///// <returns>Boolean</returns>
		//public bool Intersects(Line3D line)
		//{
		//    // Get distance to center
		//    double distance = line.GetDistance(Center);
		//    // If the distance is smaller than the radius, we are cutting the edge
		//    if (distance <= Radius) return true;
		//    return false;
		//}

		///// <summary>
		///// Checks whether the Sphere intersects with a line segment
		///// </summary>
		///// <param name="line">The line segment to test</param>
		///// <returns>Boolean</returns>
		//public bool Intersects(LineSegment3D line)
		//{
		//    // Get distance to center
		//    double distance = line.GetDistance(Center);
		//    // If the distance is smaller than the radius, we are cutting the edge
		//    if (distance <= Radius) return true;
		//    return false;
		//}

		///// <summary>
		///// Returns the distance to the given sphere
		///// </summary>
		///// <param name="sphere">The sphere to test</param>
		///// <returns>double</returns>
		//public double GetDistance(Sphere3D sphere)
		//{
		//    // Get difference vector
		//    Vector3D difference = sphere.Center - this.Center;

		//    // Get value
		//    double distance = difference.Magnitude() - sphere.Radius - this.Radius;
		//    // Return distance
		//    if (distance < 0f) return 0f;
		//    return distance;
		//}
	}
}
