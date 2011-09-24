using System;
using MathLib.Lines;
using MathLib.Vector;

namespace MathLib
{
	/// <summary>
	/// Structure for a 3D triangle
	/// </summary>
	public struct Triangle3D
	{
		/// <summary>
		/// First vertex
		/// </summary>
		public Vector3D V0;

		/// <summary>
		/// Second vertex
		/// </summary>
		public Vector3D V1;

		/// <summary>
		/// Third vertex
		/// </summary>
		public Vector3D V2;

		/// <summary>
		/// Creates a triangle with the given components
		/// </summary>
		/// <param name="p0">First point</param>
        /// <param name="p1">Second point</param>
        /// <param name="p2">Third point</param>
        public Triangle3D(Vector3D p0, Vector3D p1, Vector3D p2)
		{
            V0 = p0; 
            V1 = p1;
            V2 = p2;
		}

		/// <summary>
		/// Calculates the area of the triangle
		/// </summary>
		/// <returns>double</returns>
		public double GetArea()
		{
            // Calculate line segments
            LineSegment3D line1 = new LineSegment3D(V0, V1);
            LineSegment3D line2 = new LineSegment3D(V1, V2);
            LineSegment3D line3 = new LineSegment3D(V2, V0);

            // Calculate lengths
            double l1Sq = line1.Length(); l1Sq *= l1Sq;
            double l2Sq = line2.Length(); l2Sq *= l2Sq;
            double l3Sq = line3.Length(); l3Sq *= l3Sq;

            // 16A² = (a²+b²+c²) - 2(a^4+b^4+c^4)
            double area = l1Sq + l2Sq + l3Sq;
            l1Sq *= l1Sq;
            l2Sq *= l2Sq;
            l3Sq *= l3Sq;
            area -= 2 * (l1Sq + l2Sq + l3Sq);
            area *= 0.0625; // division by 16
            
            return Math.Sqrt(area);
		}

        /// <summary>
        /// Test whether the point given lies in the triangle
        /// </summary>
        /// <param name="point">The point to test</param>
        /// <returns>bool</returns>
        public bool TestPoint( Vector3D point )
        {
            // Prepare
            double angle = 0.0;

            // Calculate
            Vector3D v1 = V0 - point;
            Vector3D v2 = V1 - point;
            Vector3D v3 = V2 - point;
            v1.Normalise();
            v2.Normalise();
            v3.Normalise();

            // Add angles
            angle += Math.Acos(v1.Dot(v2));
            angle += Math.Acos(v2.Dot(v3));
            angle += Math.Acos(v3.Dot(v1));

            // check condition
            return Math.Abs(angle - 2 * Math.PI) <= 0.005;
        }

        /// <summary>
        /// Interpolates the value at the given point
        /// </summary>
        /// <param name="point">The point in the triangle</param>
        /// <param name="value0">Value at point 0</param>
        /// <param name="value1">Value at point 1</param>
        /// <param name="value2">Value at point 2</param>
        /// <returns></returns>
        public double Interpolate(Vector3D point, double value0, double value1, double value2)
        {
            LineSegment3D bottom = new LineSegment3D(V1, V2);

            // Shortcut points
            Vector3D top = V0;
            Vector3D left = V1;
            Vector3D right = V2;

            // Create line from top point through p
            Line3D line = new Line3D(top, point);

            // Create 'up' vector
            Vector3D v0 = right - left; v0.Normalise();
            Vector3D v1 = top - left; v1.Normalise();
            Vector3D up = v0.Cross(V1);
            up.Normalise();

            // Create normal for "bottom" plane
            Vector3D normal = up.Cross(v0);
            normal.Normalise();

            // Create plane for bottom line
            Plane3D plane = new Plane3D(normal, left);

            // Intersect line with bottom line
            Vector3D intersection = plane.GetIntersection(line);

            // Get scaling factor of intersection point on bottom line
            double distance = bottom.Start.GetDistance(intersection);
            double scale = distance / bottom.Length();

            // interpolate colors on bottom line
            double final = value1 + scale * (value2 - value1);

            // Get scaling factor of intersection point on intersection line
            LineSegment3D intersectionLine = new LineSegment3D(top, intersection);
            distance = intersection.GetDistance(point);
            scale = distance / intersectionLine.Length();

            // interpolate colors on bottom line
            return (final + scale * (value0 - final));
        }

        /// <summary>
        /// Interpolates the value at the given point
        /// </summary>
        /// <param name="point">The point in the triangle</param>
        /// <param name="value0">Value at point 0</param>
        /// <param name="value1">Value at point 1</param>
        /// <param name="value2">Value at point 2</param>
        /// <returns></returns>
        public Vector3D Interpolate(Vector3D point, Vector3D value0, Vector3D value1, Vector3D value2)
        {
            LineSegment3D bottom = new LineSegment3D(V1, V2);

            // Shortcut points
            Vector3D top = V0;
            Vector3D left = V1;
            Vector3D right = V2;

            // Create line from top point through p
            Line3D line = new Line3D(top, point);

            // Create 'up' vector
            Vector3D v0 = right - left; v0.Normalise();
            Vector3D v1 = top - left; v1.Normalise();
            Vector3D up = v0.Cross(V1);
            up.Normalise();

            // Create normal for "bottom" plane
            Vector3D normal = up.Cross(v0);
            normal.Normalise();

            // Create plane for bottom line
            Plane3D plane = new Plane3D(normal, left);

            // Intersect line with bottom line
            Vector3D intersection = plane.GetIntersection(line);

            // Get scaling factor of intersection point on bottom line
            double distance = bottom.Start.GetDistance(intersection);
            double scale = distance / bottom.Length();

            // interpolate colors on bottom line
            Vector3D final = value1 + scale * (value2 - value1);

            // Get scaling factor of intersection point on intersection line
            LineSegment3D intersectionLine = new LineSegment3D(top, intersection);
            distance = intersection.GetDistance(point);
            scale = distance / intersectionLine.Length();

            // interpolate colors on bottom line
            return (final + scale * (value0 - final));
        }

        /// <summary>
        /// Interpolates the value at the given point
        /// </summary>
        /// <param name="point">The point in the triangle</param>
        /// <param name="value0">Value at point 0</param>
        /// <param name="value1">Value at point 1</param>
        /// <param name="value2">Value at point 2</param>
        /// <returns></returns>
        public Vector4D Interpolate(Vector3D point, Vector4D value0, Vector4D value1, Vector4D value2)
        {
            LineSegment3D bottom = new LineSegment3D(V1, V2);

            // Shortcut points
            Vector3D top = V0;
            Vector3D left = V1;
            Vector3D right = V2;

            // Create line from top point through p
            Line3D line = new Line3D(top, point);

            // Create 'up' vector
            Vector3D v0 = right - left; v0.Normalise();
            Vector3D v1 = top - left; v1.Normalise();
            Vector3D up = v0.Cross(V1);
            up.Normalise();

            // Create normal for "bottom" plane
            Vector3D normal = up.Cross(v0);
            normal.Normalise();

            // Create plane for bottom line
            Plane3D plane = new Plane3D(normal, left);

            // Intersect line with bottom line
            Vector3D intersection = plane.GetIntersection(line);

            // Get scaling factor of intersection point on bottom line
            double distance = bottom.Start.GetDistance(intersection);
            double scale = distance / bottom.Length();

            // interpolate colors on bottom line
            Vector4D final = value1 + scale * (value2 - value1);

            // Get scaling factor of intersection point on intersection line
            LineSegment3D intersectionLine = new LineSegment3D(top, intersection);
            distance = intersection.GetDistance(point);
            scale = distance / intersectionLine.Length();

            // interpolate colors on bottom line
            return (final + scale * (value0 - final));
        }
	}
}
