using System;
using System.Collections.Generic;
using System.Text;

namespace Library
{
	/// <summary>
	/// Structure for a 3D vector
	/// </summary>
	public class Vector4D
	{
		public float X, Y, Z, W;

		/// <summary>
		/// Creates a vector with the given components
		/// </summary>
		/// <param name="x">x component</param>
		/// <param name="y">y component</param>
		/// <param name="z">z component</param>
        /// <param name="w">w component</param>
		public Vector4D(float x, float y, float z, float w)
		{
			this.X = x; this.Y = y; this.Z = z; this.W = w;
		}
		
		/// <summary>
		/// Creates a vector with the given components
		/// </summary>
		/// <param name="vector">vector to copy</param>
		public Vector4D(Vector4D vector)
		{
			this.X = vector.X; this.Y = vector.Y; this.Z = vector.Z;
			this.W = vector.W;
		}	

		/// <summary>
		/// Creates a vector with the given components
		/// </summary>
		/// <param name="x">x component</param>
		/// <param name="y">y component</param>
		/// <param name="z">z component</param>
		public Vector4D(float x, float y, float z)
		{
			this.X = x; this.Y = y; this.Z = z; this.W = 1f;
		}

		/// <summary>
		/// Creates a vector with the given components
		/// </summary>
		/// <param name="vector">base vector</param>
        /// <param name="w">w component</param>
		public Vector4D(Vector3D vector, float w)
		{
			this.X = vector.X; this.Y = vector.Y; this.Z = vector.Z; this.W = w;
		}

		/// <summary>
		/// Scales a vector
		/// </summary>
		/// <param name="s">The scalar to multiply</param>
		/// <returns>Vector3D</returns>
		public Vector4D Scale(float s)
		{
			X *= s;
			Y *= s;
			Z *= s;
			W *= s;
			return this;
		}

		/// <summary>
		/// Adds a vector
		/// </summary>
		/// <param name="b">The vector to add</param>
		/// <returns>Result</returns>
		public Vector4D Add(Vector4D b)
		{
			X += b.X;
			Y += b.Y;
			Z += b.Z;
			W += b.W;
			return this;
		}

		/// <summary>
		/// Subtracts a vector
		/// </summary>
		/// <param name="b">The vector to subtract</param>
		/// <returns>Result</returns>
		public Vector4D Sub(Vector4D b)
		{
			X -= b.X;
			Y -= b.Y;
			Z -= b.Z;
			W -= b.W;
			return this;
		}

		/// <summary>
		/// Returns the dot product a dot b
		/// </summary>
		/// <param name="b">The vector to dot</param>
		/// <returns>Float</returns>
		public float Dot(Vector4D b)
		{
			return (this.X * b.X + this.Y * b.Y + this.Z * b.Z + this.W * b.W);
		}

		/// <summary>
		/// Returns the magnitude (length) of the vector
		/// </summary>
		/// <returns>Float</returns>
		public float Magnitude()
		{
			return (float)Math.Sqrt(X * X + Y * Y + Z * Z + W * W);
		}

		/// <summary>
		/// Normalises the vector
		/// </summary>
		public void Normalise()
		{
			float magInverted = 1f / Magnitude();
			X *= magInverted;
			Y *= magInverted;
			Z *= magInverted;
			W *= magInverted;
		}

		/// <summary>
		/// Adds a vector to another
		/// </summary>
		/// <param name="a">Base vector</param>
		/// <param name="b">The vector to add</param>
		/// <returns>Result</returns>
		public static Vector4D operator +(Vector4D a, Vector4D b)
		{
			Vector4D t = new Vector4D(a);
			return t.Add(b);
		}

		/// <summary>
		/// Subtracts a vector from another
		/// </summary>
		/// <param name="a">Base vector</param>
		/// <param name="b">The vector to subtract</param>
		/// <returns>Result</returns>
		public static Vector4D operator -(Vector4D a, Vector4D b)
		{
			Vector4D t = new Vector4D(a);
			return t.Sub(b);
		}

		/// <summary>
		/// Conjugates the vector
		/// </summary>
		/// <param name="a">Base vector</param>
		/// <returns>Result</returns>
		public static Vector4D operator -(Vector4D a)
		{
			Vector4D t = new Vector4D(a);
			t.X = -t.X;
			t.Y = -t.Y;
			t.Z = -t.Z;
			t.W = -t.W;
			return t;
		}

		/// <summary>
		/// Returns the scaled vector
		/// </summary>
		/// <param name="a">Vector</param>
		/// <param name="s">Scalar</param>
		/// <returns>Vector4D</returns>
		public static Vector4D operator *(Vector4D a, float s)
		{
			Vector4D t = new Vector4D(a);
			return t.Scale(s);
		}

		/// <summary>
		/// Returns the scaled vector
		/// </summary>
		/// <param name="a">Vector</param>
		/// <param name="s">Scalar</param>
		/// <returns>Vector4D</returns>
		public static Vector4D operator *(float s, Vector4D a)
		{
			Vector4D t = new Vector4D(a);
			return t.Scale(s);
		}

		/// <summary>
		/// Casting Operator
		/// </summary>
		/// <param name="vector">Item to cast</param>
		/// <returns>Casted item</returns>
		public static explicit operator Vector3D(Vector4D vector)
		{
			float invW = 1f / vector.W;
			return new Vector3D(vector.X * invW, vector.Y * invW, vector.Z * invW);
		}

        /// <summary>
        /// Returns a string representation
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return string.Format("{{{0}; {1}; {2}; {3}}}", X.ToString(), Y.ToString(), Z.ToString(), W.ToString());
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
        public static bool IsValid(Vector2D vector)
        {
            return (vector.X != Single.NaN) && (vector.Y != Single.NaN);
        }

		/// <summary>
		/// Assigns a vector
		/// </summary>
		/// <param name="vector">value to assign</param>
		public void Assign(Vector4D vector)
		{
			X = vector.X;
			Y = vector.Y;
			Z = vector.Z;
			W = vector.W;
		}
	}
}
