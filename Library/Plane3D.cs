using MathLib.Lines;
using MathLib.Vector;

namespace MathLib
{
	/// <summary>
	/// Structure for a 3D plane
	/// </summary>
	public struct Plane3D
	{
        public Vector3D Normal;
		public double D;

		/// <summary>
		/// Creates a plane with the given components
		/// </summary>
		/// <param name="x">normal x component</param>
        /// <param name="y">normal y component</param>
        /// <param name="z">normal z component</param>
        /// <param name="d">distance to origin</param>
        public Plane3D(double x, double y, double z, double d)
		{
			Normal = new Vector3D(x, y, z);
            D = d;
		}

        /// <summary>
        /// Creates a plane with the given components
        /// </summary>
        /// <param name="normal">the normal vector of the plane</param>
        /// <param name="d">distance to origin</param>
        public Plane3D(Vector3D normal, double d)
        {
            Normal = normal; this.D = d;
        }

        /// <summary>
        /// Creates a plane with the given components
        /// </summary>
        /// <param name="v0">First point on the plane</param>
        /// <param name="v1">Second point on the plane</param>
        /// <param name="v2">Third point on the plane</param>
        public Plane3D(Vector3D v0, Vector3D v1, Vector3D v2)
        {
            Normal = (v1 - v0).Cross(v2 - v0);
            Normal.Normalise();
            D = -Normal.Dot(v0);
        }

        /// <summary>
        /// Creates a plane with the given components
        /// </summary>
        /// <param name="normal">the normal vector of the plane</param>
        /// <param name="pointOnPlane">Point on the plane</param>
        public Plane3D(Vector3D normal, Vector3D pointOnPlane)
        {
            Normal = normal;
            D = -Normal.Dot(pointOnPlane);
        }

		/// <summary>
		/// Returns the distance to b
		/// </summary>
		/// <param name="b">The vector to check</param>
		/// <returns>double</returns>
		public double GetDistance(Vector3D b)
		{
			return D;
		}

        /// <summary>
        /// Returns the position of the point in relation to the plane
        /// </summary>
        /// <param name="point">The point to test</param>
        /// <returns>Plane3D.PointLocation</returns>
        public PointLocation TestPoint( Vector3D point )
        {
            double dp = point.Dot(Normal) + D;
            if (dp > 0.0005f) return PointLocation.Front;
            if (dp < -0.0005f) return PointLocation.Back;
            return PointLocation.Coplanar;
        }

        /// <summary>
        /// Gets the intersection point of a line with the plane
        /// </summary>
        /// <param name="line">The line to test</param>
        /// <returns>Intersection point</returns>
        public Vector3D GetIntersection(LineSegment3D line)
        {
            double aDot = line.Start.Dot(Normal);
            double bDot = line.End.Dot(Normal);
            double scale = (-D - aDot) / (bDot - aDot);
            return line.Start + scale * (line.End - line.Start);
        }

        /// <summary>
        /// Gets the intersection point of a line with the plane
        /// </summary>
        /// <param name="line">The line to test</param>
        /// <returns>Intersection point</returns>
        public Vector3D GetIntersection(Line3D line)
        {
            double aDot = Normal.Dot(line.Origin) + D;
            double bDot = Normal.Dot(line.Direction);
            double scale = -aDot / bDot;
            return line.Origin + scale * line.Direction;
        }

        /// <summary>
        /// Returns a string representation
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return string.Format("{{{0}; {1}}}", Normal.ToString(), D.ToString());
        }

        /// <summary>
        /// Defines the location of a point in relation to the plane
        /// </summary>
        public enum PointLocation
        {
            /// <summary>
            /// Point is behind the plane
            /// </summary>
            Back = -1,

            /// <summary>
            /// Point is on the plane
            /// </summary>
            Coplanar = 0,

            /// <summary>
            /// Point is in front of the plane
            /// </summary>
            Front = 1,
        }
	}
}
