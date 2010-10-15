// $Id$

using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Library.Vector
{
	/// <summary>
	/// Structure for a 3D vector
	/// </summary>
	public struct Vector2D : IEquatable<Vector2D>, IVector
	{
		#region Member

		/// <summary>
		/// The index of the X field
		/// </summary>
		public const int FieldXIndex = 0;

		/// <summary>
		/// The index of the Y field
		/// </summary>
		public const int FieldYIndex = 1;

		/// <summary>
		/// The vector array
		/// </summary>
		private readonly double[] _field;

		/// <summary>
		/// The field array
		/// </summary>
		public double[] Fields { get { return _field; } }

		/// <summary>
		/// The Number of dimensions
		/// </summary>
		public int Dimensions { get { return 2; } }

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

		#endregion

		#region Statics

		/// <summary>
		/// A vector representing the X axis
		/// </summary>
		public static readonly Vector2D AxisX = new Vector2D(1.0f, 0.0f);

		/// <summary>
		/// A vector representing the Y axis
		/// </summary>
		public static readonly Vector2D AxisY = new Vector2D(0.0f, 1.0f);

		/// <summary>
		/// The zero vector
		/// </summary>
		public static readonly Vector2D Zero = new Vector2D(0.0D, 0.0D);

		/// <summary>
		/// An empty vector, equals the Zero vector
		/// </summary>
		public static readonly Vector2D Emtpy = Zero;

		/// <summary>
		/// The unit vector
		/// </summary>
		public static readonly Vector2D UnitVector = new Vector2D(1.0D, 1.0D);

		/// <summary>
		/// An invalid, non-existing vector
		/// </summary>
		public static readonly Vector2D Invalid = new Vector2D(Double.NaN, Double.NaN);

		#endregion

		#region ctors

		/// <summary>
		/// Creates a vector with the given components
		/// </summary>
		/// <param name="x">x component</param>
		/// <param name="y">y component</param>
		public Vector2D(double x, double y)
		{
			_field = new double[2];
			_field[FieldXIndex] = x; 
			_field[FieldYIndex] = y;
		}
		
		/// <summary>
		/// Creates a vector with the given components
		/// </summary>
		/// <param name="vector">vector to copy</param>
		public Vector2D(Vector2D vector)
			: this(vector.X, vector.Y)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Vector3D"/> struct.
		/// </summary>
		/// <param name="array">The array.</param>
		public Vector2D(double[] array)
		{
			if (array == null) throw new ArgumentNullException("array", "value must not be null");
			if (array.Length != 2) throw new ArgumentException("The value must be an array of size 2", "array");
			_field = new double[2];
			_field[0] = array[0];
			_field[1] = array[1];
		}

		#endregion

		#region operations

		/// <summary>
		/// Scales a vector
		/// </summary>
		/// <param name="s">The scalar to multiply</param>
		/// <returns>Vector2D</returns>
		public Vector2D Scale(double s)
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
		/// <returns>double</returns>
		public double Dot(Vector2D b)
		{
			return (X * b.X + Y * b.Y);
		}

		#endregion

		/// <summary>
		/// Returns the distance to b
		/// </summary>
		/// <param name="b">The vector to check</param>
		/// <returns>double</returns>
		public double GetDistance(Vector2D b)
		{
			return (b - this).Magnitude();
		}

		/// <summary>
		/// Returns the magnitude (length) of the vector
		/// </summary>
		/// <returns>double</returns>
		public double Magnitude()
		{
			return Math.Sqrt(X * X + Y * Y);
		}

		/// <summary>
		/// Normalizes the vector
		/// </summary>
		public void Normalise()
		{
			double magInverted = 1f / Magnitude();
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
		/// Normalises the vector
		/// </summary>
		/// <param name="a">Base vector</param>
		/// <returns>Result</returns>
		public static Vector2D operator ~(Vector2D a)
		{
			Vector2D t = new Vector2D(a);
			t.Normalise();
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
		public static Vector2D operator *(Vector2D a, double s)
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
		public static Vector2D operator /(Vector2D a, double s)
		{
			Vector2D t = new Vector2D(a);
			return t.Scale(1.0D/s);
		}

		/// <summary>
		/// Returns the scaled vector
		/// </summary>
		/// <param name="a">Vector</param>
		/// <param name="s">Scalar</param>
		/// <returns>Vector2D</returns>
		public static Vector2D operator *(double s, Vector2D a)
		{
			Vector2D t = new Vector2D(a);
			return t.Scale(s);
		}

		/// <summary>
		/// Returns the dot product
		/// </summary>
		/// <param name="a">Vector a</param>
		/// <param name="b">Vector b</param>
		/// <returns>the dot product</returns>
		public static double operator *(Vector2D a, Vector2D b)
		{
			Vector2D t = new Vector2D(a);
			return t.Dot(b);
		}

		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <returns>
		/// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
		/// </returns>
		/// <param name="other">An object to compare with this object.</param>
		public bool Equals(Vector2D other)
		{
			return X == other.X && Y == other.Y;
		}

		/// <summary>
		/// Returns a string representation
		/// </summary>
		/// <returns>string</returns>
		public override string ToString()
		{
			return string.Format("{{{0}; {1}}}", X, Y);
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
			return new Vector2D(X, Y);
		}

		/// <summary>
		/// Checks whether the given vector is valid,
		/// thus contains no NaN components
		/// </summary>
		/// <param name="vector">The vector to check</param>
		/// <returns>bool</returns>
		public static bool IsValid( Vector2D vector )
		{
			return !IsInvalid(vector);
		}

		/// <summary>
		/// Checks whether the given vector is invalid,
		/// thus contains NaN components
		/// </summary>
		/// <param name="vector">The vector to check</param>
		/// <returns>bool</returns>
		public static bool IsInvalid(Vector2D vector)
		{
			return Double.IsNaN(vector.X) || Double.IsNaN(vector.Y);
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

		/// <summary>
		/// Assigns a vector
		/// </summary>
		/// <param name="vector">value to assign</param>
		public void Assign(double[] vector)
		{
			if (vector == null) throw new ArgumentNullException("vector", "The value must not be null");
			if (vector.Length != 2) throw new ArgumentException("The value must be an array of size 2", "vector");
			_field[FieldXIndex] = vector[FieldXIndex];
			_field[FieldYIndex] = vector[FieldYIndex];
		}

		/// <summary>
		/// Implements the operator ==.
		/// </summary>
		/// <param name="a">A.</param>
		/// <param name="b">The b.</param>
		/// <returns>The result of the operator.</returns>
		public static bool operator == (Vector2D a, Vector2D b)
		{
			return a.Equals(b);
		}

		/// <summary>
		/// Implements the operator !=.
		/// </summary>
		/// <param name="a">A.</param>
		/// <param name="b">The b.</param>
		/// <returns>The result of the operator.</returns>
		public static bool operator !=(Vector2D a, Vector2D b)
		{
			return !(a == b);
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="Library.Vector.Vector2D"/> to <see cref="System.Double"/>[].
		/// </summary>
		/// <param name="a">A.</param>
		/// <returns>The result of the conversion.</returns>
		public static implicit operator double[] (Vector2D a)
		{
			return a._field;
		}

		/// <summary>
		/// Performs an explicit conversion from <see cref="System.Double"/>[] to <see cref="Library.Vector.Vector2D"/>.
		/// </summary>
		/// <param name="array">The array.</param>
		/// <returns>The result of the conversion.</returns>
		public static explicit operator Vector2D(double[] array)
		{
			return new Vector2D(array);
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
			if (obj is Vector2D) return Equals((Vector2D) obj);
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
			return X.GetHashCode() ^ Y.GetHashCode()*37;
		}

		#region Parsing

		/// <summary>
		/// Tries to parse the input
		/// </summary>
		/// <param name="input">The input.</param>
		/// <param name="vector">The vector.</param>
		/// <returns>true if the parsing was successful</returns>
		public static bool TryParse(string input, out Vector2D vector)
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
		public static bool TryParse(string input, IFormatProvider formatProvider, out Vector2D vector)
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
		public static Vector2D Parse(string vector)
		{
			return Parse(vector, CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// Parses the specified input.
		/// </summary>
		/// <param name="vector">The input.</param>
		/// <param name="provider">The culture provider to be used.</param>
		/// <returns>The parsed vector</returns>
		public static Vector2D Parse(string vector, IFormatProvider provider)
		{
			if (vector == null) throw new ArgumentNullException("vector", "Input must not be null");
			if (vector.Equals(String.Empty)) throw new ArgumentException("Input must not be empty", "vector");

			Regex regex = new Regex(@"^\{(?'X'.*?);(?'Y'.*?)\}|(?'X'.*?);(?'Y'.*?)$", RegexOptions.IgnorePatternWhitespace | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Compiled);
			Match match = regex.Match(vector.Trim());

			Group groupX = match.Groups["X"];
			if (!groupX.Success) throw new ArgumentException("Could not match the X component", "vector");

			Group groupY = match.Groups["Y"];
			if (!groupY.Success) throw new ArgumentException("Could not match the Y component", "vector");

			double x = Double.Parse(groupX.Value, provider);
			double y = Double.Parse(groupY.Value, provider);

			if (x == 0.0D && y == 0.0D) return Zero;
			if (x == 1.0D && y == 1.0D) return UnitVector;
			if (Double.IsNaN(x) && Double.IsNaN(y)) return Invalid;

			return new Vector2D(x, y);
		}

		#endregion
	}
}