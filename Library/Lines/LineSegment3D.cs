using MathLib.Matrix;
using MathLib.Vector;

namespace MathLib.Lines
{
	/// <summary>
	/// Structure for a 3D line segment
	/// </summary>
	public struct LineSegment3D
	{
		public Vector3D Start, End;

		/// <summary>
		/// Constructs a line through two given points
		/// </summary>
		/// <param name="point1">First point ("Origin" of the line)</param>
		/// <param name="point2">Second point</param>
		public LineSegment3D(Vector3D point1, Vector3D point2)
		{
			Start = point1;
			End = point2;
		}

		/// <summary>
		/// Returns the direction of the line
		/// </summary>
		/// <returns>Vector3</returns>
		public Vector3D Direction()
		{
			Vector3D difference = End - Start;
			difference.Normalise();
			return difference;
		}

		/// <summary>
		/// Returns the length of the line
		/// </summary>
		/// <returns>double</returns>
		public double Length()
		{
			Vector3D difference = End - Start;
			return difference.Magnitude();
		}

		/// <summary>
		/// Gets the distance of a point to the line
		/// </summary>
		/// <param name="point">The point</param>
		/// <returns>double</returns>
		public double GetDistance(Vector3D point)
		{
			// calculate direction, get length, normalise
			Vector3D direction = End - Start;
			double length = direction.Magnitude();
			direction.Scale(1f / length);	// normalise

			// calculate t-value
			Vector3D difference = point - Start;
			double d = difference.Dot(direction);

			// test edge points
			if (d <= 0f)
				return difference.Magnitude();
			if (d >= length)
				return (point - End).Magnitude();


			// project point
			Vector3D shadow = Start + d * direction;
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
			// calculate direction, get length, normalise
			Vector3D direction = End - Start;
			double length = direction.Magnitude();
			direction.Scale(1f / length);	// normalise

			// calculate t-value
			Vector3D difference = point - Start;
			double d = difference.Dot(direction);

			// test edge points
			if (d <= 0f) return Start;
			if (d >= length) return End;

			// calculate projection point
			return Start + direction * d;
		}

		#region Rotation

		/// <summary>
		/// Rotates the line around the X axis
		/// </summary>
		/// <param name="theta">The rotation angle in radians</param>
		/// <returns>Rotated vector</returns>
		public void RotateX(double theta)
		{
			Vector3D direction = End - Start;
			direction = Matrix4D.GetRotationX(theta) * direction;
			End = Start + direction;
		}

		/// <summary>
		/// Rotates the line around the Y axis
		/// </summary>
		/// <param name="theta">The rotation angle in radians</param>
		/// <returns>Rotated vector</returns>
		public void RotateY(double theta)
		{
			Vector3D direction = End - Start;
			direction = Matrix4D.GetRotationY(theta) * direction;
			End = Start + direction;
		}

		/// <summary>
		/// Rotates the line around the Z axis
		/// </summary>
		/// <param name="theta">The rotation angle in radians</param>
		/// <returns>Rotated vector</returns>
		public void RotateZ(double theta)
		{
			Vector3D direction = End - Start;
			direction = Matrix4D.GetRotationZ(theta) * direction;
			End = Start + direction;
		}

		/// <summary>
		/// Rotates the line around an arbitrary axis
		/// </summary>
		/// <param name="axis">Rotation axis</param>
		/// <param name="theta">The rotation angle in radians</param>
		/// <returns>Rotated vector</returns>
		public void RotateAxisAngle(Vector3D axis, double theta)
		{
			Vector3D direction = End - Start;
			direction = Matrix4D.GetRotationAxisAngle(axis, theta) * direction;
			End = Start + direction;
		}

		#endregion
	}
}