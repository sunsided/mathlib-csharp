using System;
using System.Collections.Generic;
using System.Text;
using Library.Vector;

namespace Library
{
	/// <summary>
	/// Structure for a 3D triangle
	/// </summary>
	public class Triangle3D
	{
        public Vector3D v0, v1, v2;

		/// <summary>
		/// Creates a triangle with the given components
		/// </summary>
		/// <param name="p0">First point</param>
        /// <param name="p1">Second point</param>
        /// <param name="p2">Third point</param>
        public Triangle3D(Vector3D p0, Vector3D p1, Vector3D p2)
		{
            v0 = p0; 
            v1 = p1;
            v2 = p2;
		}

		/// <summary>
		/// Calculates the area of the triangle
		/// </summary>
		/// <returns>double</returns>
		public double GetArea()
		{
            // Calculate line segments
            LineSegment3D line1 = new LineSegment3D(v0, v1);
            LineSegment3D line2 = new LineSegment3D(v1, v2);
            LineSegment3D line3 = new LineSegment3D(v2, v0);

            // Calculate lengths
            double l1sq = line1.Length(); l1sq *= l1sq;
            double l2sq = line2.Length(); l2sq *= l2sq;
            double l3sq = line3.Length(); l3sq *= l3sq;

            // 16A² = (a²+b²+c²) - 2(a^4+b^4+c^4)
            double area = l1sq + l2sq + l3sq;
            l1sq *= l1sq;
            l2sq *= l2sq;
            l3sq *= l3sq;
            area -= 2 * (l1sq + l2sq + l3sq);
            area *= 0.0625f;
            
            return (double)Math.Sqrt(area);
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
            Vector3D _v1 = v0 - point;
            Vector3D _v2 = v1 - point;
            Vector3D _v3 = v2 - point;
            _v1.Normalise();
            _v2.Normalise();
            _v3.Normalise();

            // Add angles
            angle += Math.Acos(_v1.Dot(_v2));
            angle += Math.Acos(_v2.Dot(_v3));
            angle += Math.Acos(_v3.Dot(_v1));

            // check condition
            if (Math.Abs(angle - 2 * Math.PI) <= 0.005) return true;

            // return condition fail
            return false;
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
            LineSegment3D bottom = new LineSegment3D(v1, v2);

            // Shortcut points
            Vector3D top = v0;
            Vector3D left = v1;
            Vector3D right = v2;

            // Create line from top point through p
            Line3D line = new Line3D(top, point);

            // Create 'up' vector
            Vector3D _v0 = right - left; _v0.Normalise();
            Vector3D _v1 = top - left; _v1.Normalise();
            Vector3D _up = _v0.Cross(v1);
            _up.Normalise();

            // Create normal for "bottom" plane
            Vector3D normal = _up.Cross(_v0);
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
            LineSegment3D bottom = new LineSegment3D(v1, v2);

            // Shortcut points
            Vector3D top = v0;
            Vector3D left = v1;
            Vector3D right = v2;

            // Create line from top point through p
            Line3D line = new Line3D(top, point);

            // Create 'up' vector
            Vector3D _v0 = right - left; _v0.Normalise();
            Vector3D _v1 = top - left; _v1.Normalise();
            Vector3D _up = _v0.Cross(v1);
            _up.Normalise();

            // Create normal for "bottom" plane
            Vector3D normal = _up.Cross(_v0);
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
            LineSegment3D bottom = new LineSegment3D(v1, v2);

            // Shortcut points
            Vector3D top = v0;
            Vector3D left = v1;
            Vector3D right = v2;

            // Create line from top point through p
            Line3D line = new Line3D(top, point);

            // Create 'up' vector
            Vector3D _v0 = right - left; _v0.Normalise();
            Vector3D _v1 = top - left; _v1.Normalise();
            Vector3D _up = _v0.Cross(v1);
            _up.Normalise();

            // Create normal for "bottom" plane
            Vector3D normal = _up.Cross(_v0);
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
