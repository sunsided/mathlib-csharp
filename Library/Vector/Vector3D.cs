// $Id$

using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Library.Matrix;

namespace Library.Vector
{
	/// <summary>
	/// Structure for a 3D vector
	/// </summary>
	public struct Vector3D : IEquatable<Vector3D>, IVector
	{
		#region Members

		/// <summary>
		/// The index of the X field
		/// </summary>
		public const int FieldXIndex = 0;

		/// <summary>
		/// The index of the Y field
		/// </summary>
		public const int FieldYIndex = 1;

		/// <summary>
		/// The index of the Z field
		/// </summary>
		public const int FieldZIndex = 2;

		/// <summary>
		/// The vector array
		/// </summary>
		private readonly double[] _field;

		/// <summary>
		/// The Number of dimensions
		/// </summary>
		public int Dimensions { get { return 3; } }

		/// <summary>
		/// The field array
		/// </summary>
		public double[] Fields { get { return _field; } }

		/// <summary>
		/// The X component
		/// </summary>
		public double X
		{
			get { return _field[FieldXIndex]; }
			set { _field[FieldXIndex] = value; }
		}

		/// <summary>
		/// The Y component
		/// </summary>
		public double Y
		{
			get { return _field[FieldYIndex]; }
			set { _field[FieldYIndex] = value; }
		}

		/// <summary>
		/// The Z component
		/// </summary>
		public double Z
		{
			get { return _field[FieldZIndex]; }
			set { _field[FieldZIndex] = value; }
		}

		#endregion

		#region Statics

		/// <summary>
		/// An invalid, non-existing vector
		/// </summary>
		public static readonly Vector3D Invalid = new Vector3D(Double.NaN, Double.NaN, Double.NaN);

		/// <summary>
		/// A vector representing the X axis
		/// </summary>
		public static readonly Vector3D AxisX = new Vector3D(1.0D, 0.0D, 0.0D);

		/// <summary>
		/// A vector representing the Y axis
		/// </summary>
		public static readonly Vector3D AxisY = new Vector3D(0.0D, 1.0D, 0.0D);

		/// <summary>
		/// A vector representing the Z axis
		/// </summary>
		public static readonly Vector3D AxisZ = new Vector3D(0.0D, 0.0D, 1.0D);

		/// <summary>
		/// A vector representing the origin
		/// </summary>
		public static readonly Vector3D Zero = new Vector3D(0.0D, 0.0D, 0.0D);

		/// <summary>
		/// The empty vector
		/// </summary>
		/// <remarks>Equals Zero</remarks>
		public static readonly Vector3D Empty = Zero;

		/// <summary>
		/// A vector that spans the 3-dimensional space (unit vector)
		/// </summary>
		public static readonly Vector3D UnitVector = new Vector3D(1.0D, 1.0D, 1.0D);

		#endregion

		#region ctors

		/// <summary>
		/// Creates a vector with the given components
		/// </summary>
		/// <param name="x">x component</param>
		/// <param name="y">y component</param>
		/// <param name="z">z component</param>
		public Vector3D(double x, double y, double z)
		{
			_field = new double[3];
			_field[FieldXIndex] = x;
			_field[FieldYIndex] = y;
			_field[FieldZIndex] = z;
		}

		/// <summary>
		/// Creates a vector with the given components
		/// </summary>
		/// <param name="vector">vector to copy</param>
		public Vector3D(Vector3D vector)
			: this(vector.X, vector.Y, vector.Z)
		{
		}

		/// <summary>
		/// Creates a vector with the given components
		/// </summary>
		/// <param name="vector">vector to copy</param>
		/// <param name="z">The Z value to set</param>
		public Vector3D(Vector2D vector, double z)
			: this(vector.X, vector.Y, z)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Vector3D"/> struct.
		/// </summary>
		/// <param name="array">The array.</param>
		public Vector3D(double[] array)
		{
			if (array == null) throw new ArgumentNullException("array", "value must not be null");
			if (array.Length != 3) throw new ArgumentException("The value must be an array of size 3", "array");
			_field = new double[3];
			_field[0] = array[0];
			_field[1] = array[1];
			_field[2] = array[2];
		}

		#endregion

		#region operations

		/// <summary>
		/// Scales a vector
		/// </summary>
		/// <param name="s">The scalar to multiply</param>
		/// <returns>Vector3D</returns>
		public Vector3D Scale(double s)
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
		/// <returns>double</returns>
		public double Dot(Vector3D b)
		{
			return (X * b.X + Y * b.Y + Z * b.Z);
		}

		#endregion 

		/// <summary>
		/// Returns the angle towards the given vector
		/// </summary>
		/// <param name="b">The vector</param>
		/// <returns>double</returns>
		public double GetAngle(Vector3D b)
		{
			return Math.Acos(X * b.X + Y * b.Y + Z * b.Z);
		}
		
		/// <summary>
		/// Returns the distance to b
		/// </summary>
		/// <param name="b">The vector to check</param>
		/// <returns>double</returns>
		public double GetDistance(Vector3D b)
		{
			return (b - this).Magnitude();
		}

		/// <summary>
		/// Returns the magnitude (length) of the vector
		/// </summary>
		/// <returns>double</returns>
		public double Magnitude()
		{
			return Math.Sqrt(X * X + Y * Y + Z * Z);
		}

		/// <summary>
		/// Normalises the vector
		/// </summary>
		public void Normalise()
		{
			double magInverted = 1f / Magnitude();
			X *= magInverted;
			Y *= magInverted;
			Z *= magInverted;
		}

		#region operator overloads

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
		/// Normalises the vector
		/// </summary>
		/// <param name="a">Base vector</param>
		/// <returns>Result</returns>
		public static Vector3D operator ~(Vector3D a)
		{
			Vector3D t = new Vector3D(a);
			t.Normalise();
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
		/// Returns the dot product
		/// </summary>
		/// <param name="a">Vector</param>
		/// <param name="b">Vector</param>
		/// <returns>double</returns>
		public static double operator *(Vector3D a, Vector3D b)
		{
			Vector3D t = new Vector3D(a);
			return t.Dot(b);
		}

		/// <summary>
		/// Returns the scaled vector
		/// </summary>
		/// <param name="a">Vector</param>
		/// <param name="s">Scalar</param>
		/// <returns>Vector3D</returns>
		public static Vector3D operator *(Vector3D a, double s)
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
		public static Vector3D operator /(Vector3D a, double s)
		{
			return a*(1.0/s);
		}

		/// <summary>
		/// Returns the scaled vector
		/// </summary>
		/// <param name="a">Vector</param>
		/// <param name="s">Scalar</param>
		/// <returns>Vector3D</returns>
		public static Vector3D operator *(double s, Vector3D a)
		{
			Vector3D t = new Vector3D(a);
			return t.Scale(s);
		}

		/// <summary>
		/// Implements the operator ==.
		/// </summary>
		/// <param name="a">A.</param>
		/// <param name="b">The b.</param>
		/// <returns>The result of the operator.</returns>
		public static bool operator == (Vector3D a, Vector3D b)
		{
			return a.Equals(b);
		}

		/// <summary>
		/// Implements the operator !=.
		/// </summary>
		/// <param name="a">A.</param>
		/// <param name="b">The b.</param>
		/// <returns>The result of the operator.</returns>
		public static bool operator != (Vector3D a, Vector3D b)
		{
			return !(a == b);
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="Library.Vector.Vector3D"/> to <see cref="System.Double"/>[].
		/// </summary>
		/// <param name="a">A.</param>
		/// <returns>The result of the conversion.</returns>
		public static implicit operator double[] (Vector3D a)
		{
			return a._field;
		}

		/// <summary>
		/// Performs an explicit conversion from <see cref="System.Double"/>[] to <see cref="Library.Vector.Vector3D"/>.
		/// </summary>
		/// <param name="array">The array.</param>
		/// <returns>The result of the conversion.</returns>
		public static explicit operator Vector3D(double[] array)
		{
			return new Vector3D(array);
		}

		#endregion

		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <returns>
		/// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
		/// </returns>
		/// <param name="other">An object to compare with this object.</param>
		public bool Equals(Vector3D other)
		{
			return X == other.X && Y == other.Y && Z == other.Z;
		}

		/// <summary>
		/// Indicates whether this instance and a specified object are equal.
		/// </summary>
		/// <param name="obj">Another object to compare to.</param>
		/// <returns>
		/// true if <paramref name="obj"/> and this instance are the same type and represent the same value; otherwise, false.
		/// </returns>
		public override bool Equals(object obj)
		{
			if (obj is Vector3D) return Equals((Vector3D) obj);
			return base.Equals(obj);
		}

		/// <summary>
		/// Returns the hash code for this instance.
		/// </summary>
		/// <returns>
		/// A 32-bit signed integer that is the hash code for this instance.
		/// </returns>
		public override int GetHashCode()
		{
			return X.GetHashCode() + Y.GetHashCode()*37 + Z.GetHashCode()*37*37;
		}

		/// <summary>
		/// Returns a string representation
		/// </summary>
		/// <returns>string</returns>
		public override string ToString()
		{
			return string.Format("{{{0}; {1}; {2}}}", X, Y, Z);
		}

		/// <summary>
		/// Creates a new object that is a copy of the current instance.
		/// </summary>
		/// <returns>
		/// A new object that is a copy of this instance.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public object Clone()
		{
			return new Vector3D(X, Y, Z);
		}

		/// <summary>
		/// Checks whether the given vector is invalid,
		/// thus contains NaN components
		/// </summary>
		/// <param name="vector">The vector to check</param>
		/// <returns>bool</returns>
		public static bool IsInvalid(Vector3D vector)
		{
			return Double.IsNaN(vector.X) || Double.IsNaN(vector.Y) || Double.IsNaN(vector.Z);
		}

		/// <summary>
		/// Checks whether the given vector is valid,
		/// thus contains no NaN components
		/// </summary>
		/// <param name="vector">The vector to check</param>
		/// <returns>bool</returns>
		public static bool IsValid(Vector3D vector)
		{
			return !IsInvalid(vector);
		}

		/// <summary>
		/// Rotates a given vector around it's X axis
		/// </summary>
		/// <param name="vector">The vector to rotate</param>
		/// <param name="theta">The rotation angle in radians</param>
		/// <returns>Rotated vector</returns>
		public static Vector3D RotateX(Vector3D vector, double theta)
		{
			return Matrix4D.GetRotationX(theta) * vector;
		}

		/// <summary>
		/// Rotates a given vector around it's Y axis
		/// </summary>
		/// <param name="vector">The vector to rotate</param>
		/// <param name="theta">The rotation angle in radians</param>
		/// <returns>Rotated vector</returns>
		public static Vector3D RotateY(Vector3D vector, double theta)
		{
			return Matrix4D.GetRotationY(theta) * vector;
		}

		/// <summary>
		/// Rotates a given vector around it's Z axis
		/// </summary>
		/// <param name="vector">The vector to rotate</param>
		/// <param name="theta">The rotation angle in radians</param>
		/// <returns>Rotated vector</returns>
		public static Vector3D RotateZ(Vector3D vector, double theta)
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
		public static Vector3D RotateAxisAngle(Vector3D vector, Vector3D axis, double theta)
		{
			return Matrix4D.GetRotationAxisAngle(axis, theta) * vector;
		}

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
		/// Assigns a vector
		/// </summary>
		/// <param name="vector">value to assign</param>
		public void Assign(double[] vector)
		{
			if (vector == null) throw new ArgumentNullException("vector", "The value must not be null");
			if (vector.Length != 3) throw new ArgumentException("The value must be an array of size 3", "vector");
			_field[FieldXIndex] = vector[FieldXIndex];
			_field[FieldYIndex] = vector[FieldYIndex];
			_field[FieldZIndex] = vector[FieldZIndex];
		}

		/// <summary>
		/// Rotates the vector around it's X axis
		/// </summary>
		/// <param name="theta">The rotation angle in radians</param>
		/// <returns>Rotated vector</returns>
		public void RotateX(double theta)
		{
			Assign(Matrix4D.GetRotationX(theta) * this);
		}

		/// <summary>
		/// Gets the current vector rotated around it's X axis
		/// </summary>
		/// <param name="theta">The rotation angle in radians</param>
		/// <returns>Rotated vector</returns>
		public Vector3D GetRotateX(double theta)
		{
			return Matrix4D.GetRotationX(theta) * this;
		}

		/// <summary>
		/// Rotates the vector around it's Y axis
		/// </summary>
		/// <param name="theta">The rotation angle in radians</param>
		/// <returns>Rotated vector</returns>
		public void RotateY(double theta)
		{
			Vector3D temp = Matrix4D.GetRotationY(theta) * this;
			Assign(temp);
		}

		/// <summary>
		/// Gets the current vector rotated around it's Y axis
		/// </summary>
		/// <param name="theta">The rotation angle in radians</param>
		/// <returns>Rotated vector</returns>
		public Vector3D GetRotateY(double theta)
		{
			return Matrix4D.GetRotationY(theta) * this;
		}		

		/// <summary>
		/// Rotates the vector around it's Z axis
		/// </summary>
		/// <param name="theta">The rotation angle in radians</param>
		/// <returns>Rotated vector</returns>
		public void RotateZ(double theta)
		{
			Assign(Matrix4D.GetRotationZ(theta) * this);
		}

		/// <summary>
		/// Gets the current vector rotated around it's Z axis
		/// </summary>
		/// <param name="theta">The rotation angle in radians</param>
		/// <returns>Rotated vector</returns>
		public Vector3D GetRotateZ(double theta)
		{
			return Matrix4D.GetRotationZ(theta) * this;
		}

		/// <summary>
		/// Rotates the vector around an arbitrary axis
		/// </summary>
		/// <param name="axis">Rotation axis</param>
		/// <param name="theta">The rotation angle in radians</param>
		/// <returns>Rotated vector</returns>
		public void RotateAxisAngle(Vector3D axis, double theta)
		{
			Assign(Matrix4D.GetRotationAxisAngle(axis, theta) * this);
		}

		/// <summary>
		/// Gets the current vector rotated around an arbitrary axis
		/// </summary>
		/// <param name="axis">Rotation axis</param>
		/// <param name="theta">The rotation angle in radians</param>
		/// <returns>Rotated vector</returns>
		public Vector3D GetRotateAxisAngle(Vector3D axis, double theta)
		{
			return Matrix4D.GetRotationAxisAngle(axis, theta) * this;
		}

		/// <summary>
		/// Rotates the vector towards another one
		/// </summary>
		/// <param name="vector">Target vector</param>
		/// <param name="theta">The rotation angle in radians</param>
		/// <returns>Rotated vector</returns>
		public void RotateToward(Vector3D vector, double theta)
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
		public Vector3D GetRotatedToward(Vector3D vector, double theta)
		{
			Vector3D axis = Cross(vector);
			return Matrix4D.GetRotationAxisAngle(axis, theta) * this;
		}

		#region Parsing

		/// <summary>
		/// Tries to parse the input
		/// </summary>
		/// <param name="input">The input.</param>
		/// <param name="vector">The vector.</param>
		/// <returns>true if the parsing was successful</returns>
		public static bool TryParse(string input, out Vector3D vector)
		{
			return TryParse(input, CultureInfo.InvariantCulture, out vector);
		}

		/// <summary>
		/// Tries to parse the input
		/// </summary>
		/// <param name="input">The input.</param>
		/// <param name="formatProvider">The culture format provider</param>
		/// <param name="vector">The vector.</param>
		/// <returns>true if the parsing was successful</returns>
		public static bool TryParse(string input, IFormatProvider formatProvider, out Vector3D vector)
		{
			try
			{
				vector = Parse(input, formatProvider);
				return true;
			}
			catch
			{
				vector = Invalid;
				return false;
			}
		}

		/// <summary>
		/// Parses the specified input.
		/// </summary>
		/// <param name="vector">The input.</param>
		/// <returns>The parsed vector</returns>
		public static Vector3D Parse(string vector)
		{
			return Parse(vector, CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// Parses the specified input.
		/// </summary>
		/// <param name="vector">The input.</param>
		/// <param name="provider">The culture provider to be used.</param>
		/// <returns>The parsed vector</returns>
		public static Vector3D Parse(string vector, IFormatProvider provider)
		{
			if (vector == null) throw new ArgumentNullException("vector", "Input must not be null");
			if (vector.Equals(String.Empty)) throw new ArgumentException("Input must not be empty", "vector");

			Regex regex = new Regex(@"^\{(?'X'.*?);(?'Y'.*?);(?'Z'.*?)\}|(?'X'.*?);(?'Y'.*?);(?'Z'.*?)$", RegexOptions.IgnorePatternWhitespace | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Compiled);
			Match match = regex.Match(vector.Trim());

			Group groupX = match.Groups["X"];
			if (!groupX.Success) throw new ArgumentException("Could not match the X component", "vector");

			Group groupY = match.Groups["Y"];
			if (!groupY.Success) throw new ArgumentException("Could not match the Y component", "vector");

			Group groupZ = match.Groups["Z"];
			if (!groupZ.Success) throw new ArgumentException("Could not match the Z component", "vector");

			double x = Double.Parse(groupX.Value, provider);
			double y = Double.Parse(groupY.Value, provider);
			double z = Double.Parse(groupZ.Value, provider);

			if (x == 0.0D && y == 0.0D && z == 0.0D) return Zero;
			if (x == 1.0D && y == 1.0D && z == 1.0D) return UnitVector;
			if (Double.IsNaN(x) && Double.IsNaN(y) && Double.IsNaN(z)) return Invalid;

			return new Vector3D(x, y, z);
		}

		#endregion
	}
}