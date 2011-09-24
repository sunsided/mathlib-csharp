using MathLib.Vector;

namespace MathLib.Lines
{
	/// <summary>
	/// Structure for a 2D line segment
	/// </summary>
	public struct LineSegment2D
	{
		public Vector2D Start, End;

		/// <summary>
		/// Constructs a line through two given points
		/// </summary>
		/// <param name="point1">First point ("Origin" of the line)</param>
		/// <param name="point2">Second point</param>
		public LineSegment2D(Vector2D point1, Vector2D point2)
		{
			Start = point1;
			End = point2;
		}

		/// <summary>
		/// Returns the direction of the line
		/// </summary>
		/// <returns>Vector3</returns>
		public Vector2D Direction()
		{
			Vector2D difference = End - Start;
			difference.Normalise();
			return difference;
		}

		/// <summary>
		/// Returns the length of the line
		/// </summary>
		/// <returns>double</returns>
		public double Length()
		{
			Vector2D difference = End - Start;
			return difference.Magnitude();
		}

		/// <summary>
		/// Gets the distance of a point to the line
		/// </summary>
		/// <param name="point">The point</param>
		/// <returns>double</returns>
		public double GetDistance(Vector2D point)
		{
			// calculate direction, get length, normalise
			Vector2D direction = End - Start;
			double length = direction.Magnitude();
			direction.Scale(1f / length);	// normalise

			// calculate t-value
			Vector2D difference = point - Start;
			double d = difference.Dot(direction);

			// test edge points
			if (d <= 0f)
				return difference.Magnitude();
			if (d >= length)
				return (point - End).Magnitude();


			// project point
			Vector2D shadow = Start + d * direction;
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
		public Vector2D Project(Vector2D point)
		{
			// calculate direction, get length, normalise
			Vector2D direction = End - Start;
			double length = direction.Magnitude();
			direction.Scale(1f / length);	// normalise

			// calculate t-value
			Vector2D difference = point - Start;
			double d = difference.Dot(direction);

			// test edge points
			if (d <= 0f) return Start;
			if (d >= length) return End;

			// calculate projection point
			return Start + direction * d;
		}
	}
}