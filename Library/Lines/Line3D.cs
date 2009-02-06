using System;
using Library.Matrix;
using Library.Vector;

namespace Library.Lines
{
	/// <summary>
	/// Structure for a 3D line segment
	/// </summary>
	public class Line3D
	{
		public Vector3D Origin, Direction;

		/// <summary>
		/// Constructs a line through two given points
		/// </summary>
		/// <param name="point1">First point ("Origin" of the line)</param>
		/// <param name="point2">Second point</param>
		public Line3D(Vector3D point1, Vector3D point2)
		{
			Direction = point2 - point1;
			Direction.Normalise();
			Origin = point1;
		}

		/// <summary>
		/// Returns the length of the line
		/// </summary>
		/// <returns>double</returns>
		public double Length()
		{
			return double.PositiveInfinity;
		}

		/// <summary>
		/// Gets the distance of a point to the line
		/// </summary>
		/// <param name="point">The point</param>
		/// <returns>double</returns>
		public double GetDistance(Vector3D point)
		{
			Vector3D difference = point - Origin;
			double d = difference.Dot(Direction);
			// projected point
			Vector3D shadow = Origin + d * Direction;
			// line from projected to point
			difference = shadow - point;
			// length of new line
			return difference.Magnitude();
		}

		/// <summary>
		/// Projects a point onto the line
		/// </summary>
		/// <param name="point">The point</param>
		/// <returns>Vector3</returns>
		public Vector3D Project(Vector3D point)
		{
			Vector3D difference = point - Origin;
			double d = difference.Dot(Direction);
			return Origin + Direction * d;
		}

		#region Rotation

		/// <summary>
		/// Rotates the line around the X axis
		/// </summary>
		/// <param name="theta">The rotation angle in radians</param>
		/// <returns>Rotated vector</returns>
		public void RotateX(double theta)
		{
			this.Direction = Matrix4D.GetRotationX(theta) * this.Direction;
		}

		/// <summary>
		/// Rotates the line around the Y axis
		/// </summary>
		/// <param name="theta">The rotation angle in radians</param>
		/// <returns>Rotated vector</returns>
		public void RotateY(double theta)
		{
			this.Direction = Matrix4D.GetRotationY(theta) * this.Direction;
		}

		/// <summary>
		/// Rotates the line around the Z axis
		/// </summary>
		/// <param name="theta">The rotation angle in radians</param>
		/// <returns>Rotated vector</returns>
		public void RotateZ(double theta)
		{
			this.Direction = Matrix4D.GetRotationY(theta) * this.Direction;
		}

		/// <summary>
		/// Rotates the line around an arbitrary axis
		/// </summary>
		/// <param name="axis">Rotation axis</param>
		/// <param name="theta">The rotation angle in radians</param>
		/// <returns>Rotated vector</returns>
		public void RotateAxisAngle(Vector3D axis, double theta)
		{
			this.Direction = Matrix4D.GetRotationAxisAngle(axis, theta) * Direction;
		}		
		
		#endregion
	}
}