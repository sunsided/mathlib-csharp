using System;

namespace Library
{
	/// <summary>
	/// Structure for a 3D vector
	/// </summary>
	public class Vector2D
	{
		public float X, Y;

		/// <summary>
		/// Creates a vector with the given components
		/// </summary>
		/// <param name="x">x component</param>
		/// <param name="y">y component</param>
		public Vector2D(float x, float y)
		{
			X = x; Y = y;
		}
		
		/// <summary>
		/// Creates a vector with the given components
		/// </summary>
		/// <param name="vector">vector to copy</param>
		public Vector2D(Vector2D vector)
		{
			this.X = vector.X; this.Y = vector.Y;
		}	

		/// <summary>
		/// Scales a vector
		/// </summary>
		/// <param name="s">The scalar to multiply</param>
		/// <returns>Vector2D</returns>
		public Vector2D Scale(float s)
		{
			X *= s;
			Y *= s;
			return this;
		}

		/// <summary>
		/// Adds a vector
		/// </summary>
		/// <param name="b">The vector to add</param>
		/// <returns>Result</returns>
		public Vector2D Add(Vector2D b)
		{
			X += b.X;
			Y += b.Y;
			return this;
		}

		/// <summary>
		/// Subtracts a vector
		/// </summary>
		/// <param name="b">The vector to subtract</param>
		/// <returns>Result</returns>
		public Vector2D Sub(Vector2D b)
		{
			X -= b.X;
			Y -= b.Y;
			return this;
		}

		/// <summary>
		/// Returns the cross product a cross b
		/// </summary>
		/// <param name="b">The vector to cross</param>
		/// <returns>Cross product</returns>
		public Vector3D Cross(Vector2D b)
		{
			return new Vector3D(0.0f,
								0.0f,
								X * b.Y - Y * b.X);
		}

		/// <summary>
		/// Returns the dot product a dot b
		/// </summary>
		/// <param name="b">The vector to dot</param>
		/// <returns>Float</returns>
		public float Dot(Vector2D b)
		{
			return (X * b.X + Y * b.Y);
		}

		/// <summary>
		/// Returns the distance to b
		/// </summary>
		/// <param name="b">The vector to check</param>
		/// <returns>Float</returns>
		public float GetDistance(Vector2D b)
		{
			return (b - this).Magnitude();
		}

		/// <summary>
		/// Returns the magnitude (length) of the vector
		/// </summary>
		/// <returns>Float</returns>
		public float Magnitude()
		{
			return (float)Math.Sqrt(X * X + Y * Y);
		}

		/// <summary>
		/// Normalizes the vector
		/// </summary>
		public void Normalise()
		{
			float magInverted = 1f / Magnitude();
			X *= magInverted;
			Y *= magInverted;
		}

		/// <summary>
		/// Adds a vector to another
		/// </summary>
		/// <param name="a">Base vector</param>
		/// <param name="b">The vector to add</param>
		/// <returns>Result</returns>
		public static Vector2D operator +(Vector2D a, Vector2D b)
		{
			Vector2D t = new Vector2D(a);
			return t.Add(b);
		}

		/// <summary>
		/// Subtracts a vector from another
		/// </summary>
		/// <param name="a">Base vector</param>
		/// <param name="b">The vector to subtract</param>
		/// <returns>Result</returns>
		public static Vector2D operator -(Vector2D a, Vector2D b)
		{
			Vector2D t = new Vector2D(a);
			return t.Sub(b);
		}

		/// <summary>
		/// Conjugates the vector
		/// </summary>
		/// <param name="a">Base vector</param>
		/// <returns>Result</returns>
		public static Vector2D operator -(Vector2D a)
		{
			Vector2D t = new Vector2D(a);
			t.X = -t.X;
			t.Y = -t.Y;
			return t;
		}

		/// <summary>
		/// Returns the cross product of two vectors
		/// </summary>
		/// <param name="a">First vector</param>
		/// <param name="b">Second vector</param>
		/// <returns>Result</returns>
		public static Vector3D operator %(Vector2D a, Vector2D b)
		{
			Vector2D t = new Vector2D(a);
			return t.Cross(b);
		}

		/// <summary>
		/// Returns the scaled vector
		/// </summary>
		/// <param name="a">Vector</param>
		/// <param name="s">Scalar</param>
		/// <returns>Vector2D</returns>
		public static Vector2D operator *(Vector2D a, float s)
		{
			Vector2D t = new Vector2D(a);
			return t.Scale(s);
		}

		/// <summary>
		/// Returns the scaled vector
		/// </summary>
		/// <param name="a">Vector</param>
		/// <param name="s">Scalar</param>
		/// <returns>Vector2D</returns>
		public static Vector2D operator *(float s, Vector2D a)
		{
			Vector2D t = new Vector2D(a);
			return t.Scale(s);
		}

        /// <summary>
        /// Returns a string representation
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return string.Format("{{{0}; {1}}}", X.ToString(), Y.ToString());
        }

	    /// <summary>
	    /// An invalid, non-existing vector
	    /// </summary>
        public static readonly Vector2D Invalid = new Vector2D(Single.NaN, Single.NaN);
	    
	    /// <summary>
	    /// Checks whether the given vector is valid,
	    /// thus contains no NaN components
	    /// </summary>
	    /// <param name="vector">The vector to check</param>
	    /// <returns>bool</returns>
	    public static bool IsValid( Vector2D vector )
	    {
            return (vector.X != Single.NaN) && (vector.Y != Single.NaN);
	    }

		/// <summary>
		/// Assigns a vector
		/// </summary>
		/// <param name="vector">value to assign</param>
		public void Assign(Vector2D vector)
		{
			X = vector.X;
			Y = vector.Y;
		}
	}
}
