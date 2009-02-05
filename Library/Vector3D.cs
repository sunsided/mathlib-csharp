using System;

namespace Library
{
	/// <summary>
	/// Structure for a 3D vector
	/// </summary>
	public class Vector3D
	{
		public float X, Y, Z;

		/// <summary>
		/// Creates a vector with the given components
		/// </summary>
		/// <param name="x">x component</param>
		/// <param name="y">y component</param>
		/// <param name="z">z component</param>
		public Vector3D(float x, float y, float z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		/// <summary>
		/// Creates a vector with the given components
		/// </summary>
		/// <param name="vector">vector to copy</param>
		public Vector3D(Vector3D vector)
		{
			X = vector.X;
			Y = vector.Y;
			Z = vector.Z;
		}

		/// <summary>
		/// Scales a vector
		/// </summary>
		/// <param name="s">The scalar to multiply</param>
		/// <returns>Vector3D</returns>
		public Vector3D Scale(float s)
		{
			X *= s;
			Y *= s;
			Z *= s;
			return this;
		}

		/// <summary>
		/// Adds a vector
		/// </summary>
		/// <param name="b">The vector to add</param>
		/// <returns>Result</returns>
		public Vector3D Add(Vector3D b)
		{
			X += b.X;
			Y += b.Y;
			Z += b.Z;
			return this;
		}

		/// <summary>
		/// Subtracts a vector
		/// </summary>
		/// <param name="b">The vector to subtract</param>
		/// <returns>Result</returns>
		public Vector3D Sub(Vector3D b)
		{
			X -= b.X;
			Y -= b.Y;
			Z -= b.Z;
			return this;
		}

		/// <summary>
		/// Returns the cross product a cross b
		/// </summary>
		/// <param name="b">The vector to cross</param>
		/// <returns>Cross product</returns>
		public Vector3D Cross(Vector3D b)
		{
			return new Vector3D(Y * b.Z - Z * b.Y,
			                    Z * b.X - X * b.Z,
			                    X * b.Y - Y * b.X);
		}

		/// <summary>
		/// Returns the dot product a dot b
		/// </summary>
		/// <param name="b">The vector to dot</param>
		/// <returns>Float</returns>
		public float Dot(Vector3D b)
		{
			return (X * b.X + Y * b.Y + Z * b.Z);
		}

		/// <summary>
		/// Returns the angle towards the given vector
		/// </summary>
		/// <param name="b">The vector</param>
		/// <returns>Float</returns>
		public float GetAngle(Vector3D b)
		{
			return (float) Math.Acos(X * b.X + Y * b.Y + Z * b.Z);
		}
		
		/// <summary>
		/// Returns the distance to b
		/// </summary>
		/// <param name="b">The vector to check</param>
		/// <returns>Float</returns>
		public float GetDistance(Vector3D b)
		{
			return (b - this).Magnitude();
		}

		/// <summary>
		/// Returns the magnitude (length) of the vector
		/// </summary>
		/// <returns>Float</returns>
		public float Magnitude()
		{
			return (float) Math.Sqrt(X * X + Y * Y + Z * Z);
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
		}

		/// <summary>
		/// Adds a vector to another
		/// </summary>
		/// <param name="a">Base vector</param>
		/// <param name="b">The vector to add</param>
		/// <returns>Result</returns>
		public static Vector3D operator +(Vector3D a, Vector3D b)
		{
			Vector3D t = new Vector3D(a);
			return t.Add(b);
		}

		/// <summary>
		/// Subtracts a vector from another
		/// </summary>
		/// <param name="a">Base vector</param>
		/// <param name="b">The vector to subtract</param>
		/// <returns>Result</returns>
		public static Vector3D operator -(Vector3D a, Vector3D b)
		{
			Vector3D t = new Vector3D(a);
			return t.Sub(b);
		}

		/// <summary>
		/// Conjugates the vector
		/// </summary>
		/// <param name="a">Base vector</param>
		/// <returns>Result</returns>
		public static Vector3D operator -(Vector3D a)
		{
			Vector3D t = new Vector3D(a);
			t.X = -t.X;
			t.Y = -t.Y;
			t.Z = -t.Z;
			return t;
		}

		/// <summary>
		/// Returns the cross product of two vectors
		/// </summary>
		/// <param name="a">First vector</param>
		/// <param name="b">Second vector</param>
		/// <returns>Result</returns>
		public static Vector3D operator %(Vector3D a, Vector3D b)
		{
			Vector3D t = new Vector3D(a);
			return t.Cross(b);
		}

		/// <summary>
		/// Returns the scaled vector
		/// </summary>
		/// <param name="a">Vector</param>
		/// <param name="s">Scalar</param>
		/// <returns>Vector3D</returns>
		public static Vector3D operator *(Vector3D a, float s)
		{
			Vector3D t = new Vector3D(a);
			return t.Scale(s);
		}

		/// <summary>
		/// Returns the scaled vector
		/// </summary>
		/// <param name="a">Vector</param>
		/// <param name="s">Scalar</param>
		/// <returns>Vector3D</returns>
		public static Vector3D operator *(float s, Vector3D a)
		{
			Vector3D t = new Vector3D(a);
			return t.Scale(s);
		}

		/// <summary>
		/// Returns a string representation
		/// </summary>
		/// <returns>string</returns>
		public override string ToString()
		{
			return string.Format("{{{0}; {1}; {2}}}", X.ToString(), Y.ToString(), Z.ToString());
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

		#region Statics

		/// <summary>
		/// A vector representing the X axis
		/// </summary>
		public static readonly Vector3D AxisX = new Vector3D(1.0f, 0.0f, 0.0f);

		/// <summary>
		/// A vector representing the Y axis
		/// </summary>
		public static readonly Vector3D AxisY = new Vector3D(0.0f, 1.0f, 0.0f);

		/// <summary>
		/// A vector representing the Z axis
		/// </summary>
		public static readonly Vector3D AxisZ = new Vector3D(0.0f, 0.0f, 1.0f);

		/// <summary>
		/// A vector representing the origin
		/// </summary>
		public static readonly Vector3D Zero = new Vector3D(0.0f, 0.0f, 0.0f);

		/// <summary>
		/// A vector that spans the 3-dimensional space (unit vector)
		/// </summary>
		public static readonly Vector3D UnitVector = new Vector3D(1.0f, 1.0f, 1.0f);

		/// <summary>
		/// Rotates a given vector around it's X axis
		/// </summary>
		/// <param name="vector">The vector to rotate</param>
		/// <param name="theta">The rotation angle in radians</param>
		/// <returns>Rotated vector</returns>
		public static Vector3D RotateX(Vector3D vector, float theta)
		{
			return Matrix4D.GetRotationX(theta) * vector;
		}

		/// <summary>
		/// Rotates a given vector around it's Y axis
		/// </summary>
		/// <param name="vector">The vector to rotate</param>
		/// <param name="theta">The rotation angle in radians</param>
		/// <returns>Rotated vector</returns>
		public static Vector3D RotateY(Vector3D vector, float theta)
		{
			return Matrix4D.GetRotationY(theta) * vector;
		}

		/// <summary>
		/// Rotates a given vector around it's Z axis
		/// </summary>
		/// <param name="vector">The vector to rotate</param>
		/// <param name="theta">The rotation angle in radians</param>
		/// <returns>Rotated vector</returns>
		public static Vector3D RotateZ(Vector3D vector, float theta)
		{
			return Matrix4D.GetRotationZ(theta) * vector;
		}

		/// <summary>
		/// Rotates a given vector around an arbitrary axis
		/// </summary>
		/// <param name="vector">The vector to rotate</param>
		/// <param name="axis">Rotation axis</param>
		/// <param name="theta">The rotation angle in radians</param>
		/// <returns>Rotated vector</returns>
		public static Vector3D RotateAxisAngle(Vector3D vector, Vector3D axis, float theta)
		{
			return Matrix4D.GetRotationAxisAngle(axis, theta) * vector;
		}

		#endregion

		/// <summary>
		/// Assigns a vector
		/// </summary>
		/// <param name="vector">value to assign</param>
		public void Assign(Vector3D vector)
		{
			X = vector.X;
			Y = vector.Y;
			Z = vector.Z;
		}

		/// <summary>
		/// Rotates the vector around it's X axis
		/// </summary>
		/// <param name="theta">The rotation angle in radians</param>
		/// <returns>Rotated vector</returns>
		public void RotateX(float theta)
		{
			Assign(Matrix4D.GetRotationX(theta) * this);
		}

		/// <summary>
		/// Gets the current vector rotated around it's X axis
		/// </summary>
		/// <param name="theta">The rotation angle in radians</param>
		/// <returns>Rotated vector</returns>
		public Vector3D GetRotateX(float theta)
		{
			return Matrix4D.GetRotationX(theta) * this;
		}

		/// <summary>
		/// Rotates the vector around it's Y axis
		/// </summary>
		/// <param name="theta">The rotation angle in radians</param>
		/// <returns>Rotated vector</returns>
		public void RotateY(float theta)
		{
			Vector3D temp = Matrix4D.GetRotationY(theta) * this;
			Assign(temp);
		}

		/// <summary>
		/// Gets the current vector rotated around it's Y axis
		/// </summary>
		/// <param name="theta">The rotation angle in radians</param>
		/// <returns>Rotated vector</returns>
		public Vector3D GetRotateY(float theta)
		{
			return Matrix4D.GetRotationY(theta) * this;
		}		

		/// <summary>
		/// Rotates the vector around it's Z axis
		/// </summary>
		/// <param name="theta">The rotation angle in radians</param>
		/// <returns>Rotated vector</returns>
		public void RotateZ(float theta)
		{
			Assign(Matrix4D.GetRotationZ(theta) * this);
		}

		/// <summary>
		/// Gets the current vector rotated around it's Z axis
		/// </summary>
		/// <param name="theta">The rotation angle in radians</param>
		/// <returns>Rotated vector</returns>
		public Vector3D GetRotateZ(float theta)
		{
			return Matrix4D.GetRotationZ(theta) * this;
		}

		/// <summary>
		/// Rotates the vector around an arbitrary axis
		/// </summary>
		/// <param name="axis">Rotation axis</param>
		/// <param name="theta">The rotation angle in radians</param>
		/// <returns>Rotated vector</returns>
		public void RotateAxisAngle(Vector3D axis, float theta)
		{
			Assign(Matrix4D.GetRotationAxisAngle(axis, theta) * this);
		}

		/// <summary>
		/// Gets the current vector rotated around an arbitrary axis
		/// </summary>
		/// <param name="axis">Rotation axis</param>
		/// <param name="theta">The rotation angle in radians</param>
		/// <returns>Rotated vector</returns>
		public Vector3D GetRotateAxisAngle(Vector3D axis, float theta)
		{
			return Matrix4D.GetRotationAxisAngle(axis, theta) * this;
		}

		/// <summary>
		/// Rotates the vector towards another one
		/// </summary>
		/// <param name="vector">Target vector</param>
		/// <param name="theta">The rotation angle in radians</param>
		/// <returns>Rotated vector</returns>
		public void RotateToward(Vector3D vector, float theta)
		{
			Vector3D axis = Cross(vector);
			axis.Normalise();
			Assign(Matrix4D.GetRotationAxisAngle(axis, theta) * this);
		}

		/// <summary>
		/// Gets the current vector rotated towards another one
		/// </summary>
		/// <param name="vector">Target vector</param>
		/// <param name="theta">The rotation angle in radians</param>
		/// <returns>Rotated vector</returns>
		public Vector3D GetRotatedToward(Vector3D vector, float theta)
		{
			Vector3D axis = Cross(vector);
			return Matrix4D.GetRotationAxisAngle(axis, theta) * this;
		}
		
	}
}