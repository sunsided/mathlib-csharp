using MathLib.Lines;
using MathLib.Vector;

namespace MathLib
{
	public class Sphere3D
	{
		public Vector3D Center;
		public double Radius;

		/// <summary>
		/// Creates a sphere based on a point and a radius
		/// </summary>
		/// <param name="center">Center point</param>
		/// <param name="radius">Radius</param>
		public Sphere3D(Vector3D center, double radius)
		{
			Center = center;
			Radius = radius;
		}

		/// <summary>
		/// Checks whether the Sphere intersects with a line
		/// </summary>
		/// <param name="line">The line to test</param>
		/// <returns>Boolean</returns>
		public bool Intersects(Line3D line)
		{
			// Get distance to center
			double distance = line.GetDistance(Center);
			// If the distance is smaller than the radius, we are cutting the edge
			if (distance <= Radius) return true;
			return false;
		}

		/// <summary>
		/// Checks whether the Sphere intersects with a line segment
		/// </summary>
		/// <param name="line">The line segment to test</param>
		/// <returns>Boolean</returns>
		public bool Intersects(LineSegment3D line)
		{
			// Get distance to center
			double distance = line.GetDistance(Center);
			// If the distance is smaller than the radius, we are cutting the edge
			if (distance <= Radius) return true;
			return false;
		}

		/// <summary>
		/// Returns the distance to the given sphere
		/// </summary>
		/// <param name="sphere">The sphere to test</param>
		/// <returns>double</returns>
		public double GetDistance(Sphere3D sphere)
		{
			// Get difference vector
			Vector3D difference = sphere.Center - this.Center;
			// Get value
			double distance = difference.Magnitude() - sphere.Radius - this.Radius;
			// Return distance
			if (distance < 0f) return 0f;
			return distance;
		}
	}
}
