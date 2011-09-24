using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MathLib.Vector
{
	/// <summary>
	/// Structure for a 3D vector
	/// </summary>
	public sealed class Vector4D : IEquatable<Vector4D>, IVector
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
		/// The index of the W field
		/// </summary>
		public const int FieldWIndex = 3;

		/// <summary>
		/// The vector array
		/// </summary>
		private readonly double[] _field;

		/// <summary>
		/// The Number of dimensions
		/// </summary>
		public int Dimensions { get { return 4; } }

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

		/// <summary>
		/// The W component
		/// </summary>
		public double W
		{
			get { return _field[FieldWIndex]; }
			set { _field[FieldWIndex] = value; }
		}

		#endregion

		#region Statics

		/// <summary>
		/// A vector representing the X axis
		/// </summary>
		public static readonly Vector4D AxisX = new Vector4D(1.0D, 0.0D, 0.0D, 0.0D);

		/// <summary>
		/// A vector representing the Y axis
		/// </summary>
		public static readonly Vector4D AxisY = new Vector4D(0.0D, 1.0D, 0.0D, 0.0D);

		/// <summary>
		/// A vector representing the Z axis
		/// </summary>
		public static readonly Vector4D AxisZ = new Vector4D(0.0D, 0.0D, 1.0D, 0.0D);

		/// <summary>
		/// A vector representing the W axis
		/// </summary>
		public static readonly Vector4D AxisW = new Vector4D(0.0D, 0.0D, 0.0D, 1.0D);

		/// <summary>
		/// A vector representing the origin
		/// </summary>
		public static readonly Vector4D Zero = new Vector4D(0.0D, 0.0D, 0.0D, 0.0D);

		/// <summary>
		/// The empty vector
		/// </summary>
		/// <remarks>Equals Zero</remarks>
		public static readonly Vector4D Empty = Zero;

		/// <summary>
		/// A vector that spans the 4-dimensional space (unit vector)
		/// </summary>
		public static readonly Vector4D UnitVector = new Vector4D(1.0D, 1.0D, 1.0D, 1.0D);

		#endregion

		#region ctors

		/// <summary>
		/// Creates a vector with the given components
		/// </summary>
		public Vector4D()
		{
			_field = new double[4];
		}	

		/// <summary>
		/// Creates a vector with the given components
		/// </summary>
		/// <param name="x">x component</param>
		/// <param name="y">y component</param>
		/// <param name="z">z component</param>
		/// <param name="w">w component</param>
		public Vector4D(double x, double y, double z, double w)
		{
			_field = new double[4];
			_field[FieldXIndex] = x;
			_field[FieldYIndex] = y;
			_field[FieldZIndex] = z;
			_field[FieldWIndex] = w;
		}
		
		/// <summary>
		/// Creates a vector with the given components
		/// </summary>
		/// <param name="vector">vector to copy</param>
		public Vector4D(Vector4D vector)
			: this(vector.X, vector.Y, vector.Z, vector.W)
		{
		}	

		/// <summary>
		/// Creates a vector with the given components
		/// </summary>
		/// <param name="x">x component</param>
		/// <param name="y">y component</param>
		/// <param name="z">z component</param>
		public Vector4D(double x, double y, double z)
			: this(x, y, z, 0.0D)
		{
		}

		/// <summary>
		/// Creates a vector with the given components
		/// </summary>
		/// <param name="array">The array to assign</param>
		public Vector4D(double[] array)
		{
			if (array == null) throw new ArgumentNullException("array", "value must not be null");
			if (array.Length != 4) throw new ArgumentException("The value must be an array of size 4", "array");
			_field = new double[4];
			_field[0] = array[0];
			_field[1] = array[1];
			_field[2] = array[2];
			_field[3] = array[3];
		}

		/// <summary>
		/// Creates a vector with the given components
		/// </summary>
		/// <param name="vector">base vector</param>
		/// <param name="w">w component</param>
		public Vector4D(Vector3D vector, double w)
			: this(vector.X, vector.Y, vector.Z, w)
		{
		}

		#endregion

		#region operations

		/// <summary>
		/// Scales a vector
		/// </summary>
		/// <param name="s">The scalar to multiply</param>
		/// <returns>Vector3D</returns>
		public Vector4D Scale(double s)
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
		/// <returns>double</returns>
		public double Dot(Vector4D b)
		{
			return (X * b.X + Y * b.Y + Z * b.Z + W * b.W);
		}

		#endregion

		/// <summary>
		/// Returns the distance to b
		/// </summary>
		/// <param name="b">The vector to check</param>
		/// <returns>double</returns>
		public double GetDistance(Vector4D b)
		{
			return (b - this).Magnitude();
		}

		/// <summary>
		/// Returns the magnitude (length) of the vector
		/// </summary>
		/// <returns>double</returns>
		public double Magnitude()
		{
			return Math.Sqrt(X * X + Y * Y + Z * Z + W * W);
		}

		/// <summary>
		/// Normalises the vector
		/// </summary>
		public void Normalise()
		{
			double magInverted = 1.0D / Magnitude();
			X *= magInverted;
			Y *= magInverted;
			Z *= magInverted;
			W *= magInverted;
		}

		/// <summary>
		/// Normalises the vector
		/// </summary>
		public Vector4D GetNormalised()
		{
			double magInverted = 1d / Magnitude();
			return new Vector4D(X * magInverted, Y * magInverted, Z * magInverted, W * magInverted);
		}

		#region operator overloads

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
		/// Normalises the vector
		/// </summary>
		/// <param name="a">Base vector</param>
		/// <returns>Result</returns>
		public static Vector4D operator ~(Vector4D a)
		{
			Vector4D t = new Vector4D(a);
			t.Normalise();
			return t;
		}

		/// <summary>
		/// Returns the scaled vector
		/// </summary>
		/// <param name="a">Vector</param>
		/// <param name="s">Scalar</param>
		/// <returns>Vector4D</returns>
		public static Vector4D operator *(Vector4D a, double s)
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
		public static Vector4D operator *(double s, Vector4D a)
		{
			Vector4D t = new Vector4D(a);
			return t.Scale(s);
		}

		/// <summary>
		/// Returns the scaled vector
		/// </summary>
		/// <param name="a">Vector</param>
		/// <param name="s">Scalar</param>
		/// <returns>Vector3D</returns>
		public static Vector4D operator /(Vector4D a, double s)
		{
			return a * (1.0 / s);
		}

		/// <summary>
		/// Returns the dot product
		/// </summary>
		/// <param name="a">Vector</param>
		/// <param name="b">Vector</param>
		/// <returns>double</returns>
		public static double operator *(Vector4D a, Vector4D b)
		{
			Vector4D t = new Vector4D(a);
			return t.Dot(b);
		}

		/// <summary>
		/// Casting Operator
		/// </summary>
		/// <param name="vector">Item to cast</param>
		/// <returns>Casted item</returns>
		public static explicit operator Vector3D(Vector4D vector)
		{
			double invW = 1f / vector.W;
			return new Vector3D(vector.X * invW, vector.Y * invW, vector.Z * invW);
		}

		/// <summary>
		/// Casting Operator
		/// </summary>
		/// <param name="vector">Item to cast</param>
		/// <returns>Casted item</returns>
		public static implicit operator double[](Vector4D vector)
		{
			return vector._field;
		}

		/// <summary>
		/// Performs an explicit conversion from <see cref="System.Double"/>[] to <see cref="Vector4D"/>.
		/// </summary>
		/// <param name="array">The array.</param>
		/// <returns>The result of the conversion.</returns>
		public static explicit operator Vector4D(double[] array)
		{
			return new Vector4D(array);
		}

		/// <summary>
		/// Implements the operator ==.
		/// </summary>
		/// <param name="a">A.</param>
		/// <param name="b">The b.</param>
		/// <returns>The result of the operator.</returns>
		public static bool operator == (Vector4D a, Vector4D b)
		{
			if (ReferenceEquals(a, null)) return ReferenceEquals(b, null);
			return a.X == b.X && a.Y == b.Y && a.Z == b.Z && a.W == b.W;
		}

		/// <summary>
		/// Implements the operator !=.
		/// </summary>
		/// <param name="a">A.</param>
		/// <param name="b">The b.</param>
		/// <returns>The result of the operator.</returns>
		public static bool operator != (Vector4D a, Vector4D b)
		{
			return !(a == b);
		}

		#endregion

		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <returns>
		/// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
		/// </returns>
		/// <param name="other">An object to compare with this object.</param>
		public bool Equals(Vector4D other)
		{
			return this == other;
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
			if (obj is Vector4D) return Equals((Vector4D) obj);
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
			return X.GetHashCode() ^ Y.GetHashCode() * 37 ^ Z.GetHashCode() * 1369 ^ W.GetHashCode() * 50653;
		}

		/// <summary>
		/// Returns a string representation
		/// </summary>
		/// <returns>string</returns>
		public override string ToString()
		{
			return string.Format("{{{0}; {1}; {2}; {3}}}", X, Y, Z, W);
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
			return new Vector4D(X, Y, Z, W);
		}

		/// <summary>
		/// An invalid, non-existing vector
		/// </summary>
		public static readonly Vector4D Invalid = new Vector4D(Double.NaN, Double.NaN, Double.NaN, Double.NaN);

		/// <summary>
		/// Checks whether the given vector is valid,
		/// thus contains no NaN components
		/// </summary>
		/// <param name="vector">The vector to check</param>
		/// <returns>bool</returns>
		public static bool IsValid(Vector4D vector)
		{
			return !IsInvalid(vector);
		}

		/// <summary>
		/// Checks whether the given vector is invalid,
		/// thus contains NaN components
		/// </summary>
		/// <param name="vector">The vector to check</param>
		/// <returns>bool</returns>
		public static bool IsInvalid(Vector4D vector)
		{
			return Double.IsNaN(vector.X) || Double.IsNaN(vector.Y) || Double.IsNaN(vector.Z) || Double.IsNaN(vector.W);
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

		/// <summary>
		/// Assigns a vector
		/// </summary>
		/// <param name="vector">value to assign</param>
		public void Assign(double[] vector)
		{
			if (vector == null) throw new ArgumentNullException("vector", "The value must not be null");
			if (vector.Length != 4) throw new ArgumentException("The value must be an array of size 4", "vector");
			_field[FieldXIndex] = vector[FieldXIndex];
			_field[FieldYIndex] = vector[FieldYIndex];
			_field[FieldZIndex] = vector[FieldZIndex];
			_field[FieldWIndex] = vector[FieldWIndex];
		}

		#region Parsing

		/// <summary>
		/// Tries to parse the input
		/// </summary>
		/// <param name="input">The input.</param>
		/// <param name="vector">The vector.</param>
		/// <returns>true if the parsing was successful</returns>
		public static bool TryParse(string input, out Vector4D vector)
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
		public static bool TryParse(string input, IFormatProvider formatProvider, out Vector4D vector)
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
		public static Vector4D Parse(string vector)
		{
			return Parse(vector, CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// Parses the specified input.
		/// </summary>
		/// <param name="vector">The input.</param>
		/// <param name="provider">The culture provider to be used.</param>
		/// <returns>The parsed vector</returns>
		public static Vector4D Parse(string vector, IFormatProvider provider)
		{
			if (vector == null) throw new ArgumentNullException("vector", "Input must not be null");
			if (vector.Equals(String.Empty)) throw new ArgumentException("Input must not be empty", "vector");

			Regex regex = new Regex(@"^\{(?'X'.*?);(?'Y'.*?);(?'Z'.*?);(?'W'.*?)\}|(?'X'.*?);(?'Y'.*?);(?'Z'.*?);(?'W'.*?)$", RegexOptions.IgnorePatternWhitespace | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Compiled);
			Match match = regex.Match(vector.Trim());

			Group groupX = match.Groups["X"];	
			if (!groupX.Success) throw new ArgumentException("Could not match the X component", "vector");

			Group groupY = match.Groups["Y"];
			if (!groupY.Success) throw new ArgumentException("Could not match the Y component", "vector");

			Group groupZ = match.Groups["Z"];
			if (!groupZ.Success) throw new ArgumentException("Could not match the Z component", "vector");

			Group groupW = match.Groups["W"];
			if (!groupW.Success) throw new ArgumentException("Could not match the W component", "vector");

			double x = Double.Parse(groupX.Value, provider);
			double y = Double.Parse(groupY.Value, provider);
			double z = Double.Parse(groupZ.Value, provider);
			double w = Double.Parse(groupW.Value, provider);

			if (x == 0.0D && y == 0.0D && z == 0.0D && w == 0.0D) return Zero;
			if (x == 1.0D && y == 1.0D && z == 1.0D && w == 1.0D) return UnitVector;
			if (Double.IsNaN(x) && Double.IsNaN(y) && Double.IsNaN(z) && Double.IsNaN(w)) return Invalid;

			return new Vector4D(x, y, z, w);
		}

		#endregion
	}
}