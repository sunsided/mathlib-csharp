// $Id$

using System;
using Library.Vector;

namespace Library.Matrix
{
	/// <summary>
	/// 3-dimensional row-major matrix
	/// </summary>
	public sealed class Matrix3D : IEquatable<Matrix3D>, ICloneable
	{
		/// <summary>
		/// The cell values;
		/// </summary>
		internal double[,] Cell;

		/// <summary>
		/// Gets the unit matrix
		/// </summary>
		public static readonly Matrix3D Unit = new Matrix3D(
			1.0d, 0.0d, 0.0d,
			0.0d, 1.0d, 0.0d,
			0.0d, 0.0d, 1.0d);

		/// <summary>
		/// Gets the unit matrix
		/// </summary>
		public static readonly Matrix3D Zero = new Matrix3D(
			0.0d, 0.0d, 0.0d,
			0.0d, 0.0d, 0.0d,
			0.0d, 0.0d, 0.0d);

		/// <summary>
		/// Gets a test matrix
		/// </summary>
		public static readonly Matrix3D Test = new Matrix3D(
			0.0d, 1.0d, 2.0d,
			0.1d, 1.1d, 2.1d,
			0.2d, 1.2d, 2.2d);

		/// <summary>
		/// Magic values
		/// </summary>
		public static readonly Matrix3D Magic = new Matrix3D(
			8, 1, 6,
			3, 5, 7,
			4, 9, 2);

		#region Konstruktor

		/// <summary>
		/// Creates a new instance of the <see cref="Matrix3D"/> class.
		/// </summary>
		public Matrix3D()
		{
			Cell = new double[3, 3];
		}

		/// <summary>
		/// Creates a new instance of the <see cref="Matrix3D"/> class.
		/// </summary>
		public Matrix3D(Matrix3D matrix)
			: this()
		{
			Assign(matrix);
		}

		/// <summary>
		/// Creates a new instance of the <see cref="Matrix3D"/> class and assigns values.
		/// </summary>
		/// <param name="m11">The field (1,1)</param>
		/// <param name="m12">The field (1,2)</param>
		/// <param name="m13">The field (1,3)</param>
		/// <param name="m21">The field (2,1)</param>
		/// <param name="m22">The field (2,2)</param>
		/// <param name="m23">The field (2,3)</param>
		/// <param name="m31">The field (3,1)</param>
		/// <param name="m32">The field (3,2)</param>
		/// <param name="m33">The field (3,3)</param>
		public Matrix3D(
			double m11, double m12, double m13,
			double m21, double m22, double m23,
			double m31, double m32, double m33)
		: this()
		{
			Cell[0, 0] = m11; Cell[0, 1] = m12; Cell[0, 2] = m13;
			Cell[1, 0] = m21; Cell[1, 1] = m22; Cell[1, 2] = m23;
			Cell[2, 0] = m31; Cell[2, 1] = m32; Cell[2, 2] = m33;
		}

		#endregion

		#region Matrix type functions

		/// <summary>
		/// Sets the matrix to an identity matrix
		/// </summary>
		public void ToIdentity()
		{
			Cell[0, 0] = 1d; Cell[0, 1] = 0d; Cell[0, 2] = 0d;
			Cell[1, 0] = 0d; Cell[1, 1] = 1d; Cell[1, 2] = 0d;
			Cell[2, 0] = 0d; Cell[2, 1] = 0d; Cell[2, 2] = 1d;
		}

		/// <summary>
		/// Sets the matrix to a scale matrix
		/// </summary>
		/// <param name="factors">Vector of scaling factors</param>
		public void ToScaling(Vector3D factors)
		{
			Cell[0, 0] = factors.X; Cell[0, 1] = 0d;		Cell[0, 2] = 0d;	
			Cell[1, 0] = 0d;		Cell[1, 1] = factors.Y; Cell[1, 2] = 0d;	
			Cell[2, 0] = 0d;		Cell[2, 1] = 0d;		Cell[2, 2] = factors.Z;
		}

		/// <summary>
		/// Sets the matrix to a scale matrix
		/// </summary>
		/// <param name="x">X factor</param>
		/// <param name="y">Y factor</param>
		/// <param name="z">Z factor</param>
		public void ToScaling(double x, double y, double z)
		{
			Cell[0, 0] = x;  Cell[0, 1] = 0d; Cell[0, 2] = 0d;
			Cell[1, 0] = 0d; Cell[1, 1] = y;  Cell[1, 2] = 0d;
			Cell[2, 0] = 0d; Cell[2, 1] = 0d; Cell[2, 2] = z;
		}

		#endregion

		#region Vector-Matrix Multiplication

		/// <summary>
		/// Transforms a given vector by a matrix.
		/// </summary>
		/// <param name="matrix">A <see cref="Matrix3D"/> instance.</param>
		/// <param name="vector">A <see cref="Vector4D"/> instance.</param>
		/// <returns>A new <see cref="Vector4D"/> instance containing the result.</returns>
		public static Vector3D operator *(Matrix3D matrix, Vector3D vector)
		{
			return new Vector3D (
				(matrix.Cell[0,0] * vector.X) + (matrix.Cell[1,0] * vector.Y) + (matrix.Cell[2,0] * vector.Z),
				(matrix.Cell[0,1] * vector.X) + (matrix.Cell[1,1] * vector.Y) + (matrix.Cell[2,1] * vector.Z),
				(matrix.Cell[0,2] * vector.X) + (matrix.Cell[1,2] * vector.Y) + (matrix.Cell[2,2] * vector.Z) );
		}

		/// <summary>
		/// Tests if two matrices are identical
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static bool operator ==(Matrix3D a, Matrix3D b)
		{
			if (ReferenceEquals(a, null)) return ReferenceEquals(b, null);
			return a.Equals(b);
		}

		/// <summary>
		/// Tests if two matrices are different
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static bool operator !=(Matrix3D a, Matrix3D b)
		{
			if (ReferenceEquals(a, null)) return !ReferenceEquals(b, null);
			return !a.Equals(b);
		}

		#endregion

		#region Specific Fields

		/// <summary>
		/// Gets or sets field M(1,1)
		/// </summary>
		public double M11
		{
			get { return Cell[0, 0]; }
			set { Cell[0, 0] = value; }
		}

		/// <summary>
		/// Gets or sets field M(1,2)
		/// </summary>
		public double M12
		{
			get { return Cell[0, 1]; }
			set { Cell[0, 1] = value; }
		}

		/// <summary>
		/// Gets or sets field M(1,3)
		/// </summary>
		public double M13
		{
			get { return Cell[0, 2]; }
			set { Cell[0, 2] = value; }
		}

		/// <summary>
		/// Gets or sets field M(2,1)
		/// </summary>
		public double M21
		{
			get { return Cell[1, 0]; }
			set { Cell[1, 0] = value; }
		}

		/// <summary>
		/// Gets or sets field M(2,2)
		/// </summary>
		public double M22
		{
			get { return Cell[1, 1]; }
			set { Cell[1, 1] = value; }
		}

		/// <summary>
		/// Gets or sets field M(2,3)
		/// </summary>
		public double M23
		{
			get { return Cell[1, 2]; }
			set { Cell[1, 2] = value; }
		}

		/// <summary>
		/// Gets or sets field M(3,1)
		/// </summary>
		public double M31
		{
			get { return Cell[2, 0]; }
			set { Cell[2, 0] = value; }
		}

		/// <summary>
		/// Gets or sets field M(3,2)
		/// </summary>
		public double M32
		{
			get { return Cell[2, 1]; }
			set { Cell[2, 1] = value; }
		}

		/// <summary>
		/// Gets or sets field M(3,3)
		/// </summary>
		public double M33
		{
			get { return Cell[2, 2]; }
			set { Cell[2, 2] = value; }
		}

		#endregion

		#region Arithmetik

		/// <summary>
		/// Transforms a given vector by a matrix.
		/// </summary>
		public static Matrix3D operator *(Matrix3D a, Matrix3D b)
		{
			return a.Multiply(b);
		}

		/// <summary>
		/// Multilplies the matrix with another one
		/// </summary>
		/// <param name="b">Matrix to concatenate</param>
		/// <returns>Matrix3D</returns>
		public Matrix3D Multiply(Matrix3D b)
		{
			Matrix3D mat = new Matrix3D( // TODO: Optimize by using cell access
				M11 * b.M11 + M12 * b.M21 + M13 * b.M31,
				M11 * b.M12 + M12 * b.M22 + M13 * b.M32,
				M11 * b.M13 + M12 * b.M23 + M13 * b.M33,
				M21 * b.M11 + M22 * b.M21 + M23 * b.M31,
				M21 * b.M12 + M22 * b.M22 + M23 * b.M32,
				M21 * b.M13 + M22 * b.M23 + M23 * b.M33,
				M31 * b.M11 + M32 * b.M21 + M33 * b.M31,
				M31 * b.M12 + M32 * b.M22 + M33 * b.M32,
				M31 * b.M13 + M32 * b.M23 + M33 * b.M33);

			Assign(mat);
			return this;
		}

		#endregion

		#region Assign

		/// <summary>
		/// Assigns matrix values
		/// </summary>
		/// <param name="b">Matrix to copy</param>
		public void Assign(Matrix3D b)
		{
			Cell[0, 0] = b.Cell[0, 0]; Cell[0, 1] = b.Cell[0, 1]; Cell[0, 2] = b.Cell[0, 2];
			Cell[1, 0] = b.Cell[1, 0]; Cell[1, 1] = b.Cell[1, 1]; Cell[1, 2] = b.Cell[1, 2];
			Cell[2, 0] = b.Cell[2, 0]; Cell[2, 1] = b.Cell[2, 1]; Cell[2, 2] = b.Cell[2, 2];
		}

		/// <summary>
		/// Assigns matrix values
		/// </summary>
		/// <param name="m11">The field (1,1)</param>
		/// <param name="m12">The field (1,2)</param>
		/// <param name="m13">The field (1,3)</param>
		/// <param name="m21">The field (2,1)</param>
		/// <param name="m22">The field (2,2)</param>
		/// <param name="m23">The field (2,3)</param>
		/// <param name="m31">The field (3,1)</param>
		/// <param name="m32">The field (3,2)</param>
		/// <param name="m33">The field (3,3)</param>
		public void Assign(
			double m11, double m12, double m13,
			double m21, double m22, double m23,
			double m31, double m32, double m33)
		{
			Cell[0, 0] = m11; Cell[0, 1] = m12; Cell[0, 2] = m13;
			Cell[1, 0] = m21; Cell[1, 1] = m22; Cell[1, 2] = m23;
			Cell[2, 0] = m31; Cell[2, 1] = m32; Cell[2, 2] = m33;
		}

		/// <summary>
		/// Assigns matrix values
		/// </summary>
		/// <param name="value">Value to fill in</param>
		public void Fill(double value)
		{
			Cell[0, 0] = value; Cell[0, 1] = value; Cell[0, 2] = value;
			Cell[1, 0] = value; Cell[1, 1] = value; Cell[1, 2] = value;
			Cell[2, 0] = value; Cell[2, 1] = value; Cell[2, 2] = value;
		}


		#endregion

		#region Statics

		/// <summary>
		/// A 3D rotation matrix for X-axis rotation
		/// </summary>
		/// <param name="theta">The rotation angle</param>
		public static Matrix3D GetRotationX(double theta)
		{
			double cos = Math.Cos(theta);
			double sin = Math.Sin(theta);			
			return new Matrix3D(
				1.0d, 0.0d, 0.0d,
				0.0d, cos, sin,
				0.0d, -sin, cos);
		}

		/// <summary>
		/// A 3D rotation matrix for X-axis rotation
		/// </summary>
		/// <param name="cosTheta">The cosine of the rotation angle</param>
		/// <param name="sinTheta">The sine of the rotation angle</param>
		public static Matrix3D GetRotationX(double cosTheta, double sinTheta)
		{
			return new Matrix3D(
				1.0d, 0.0d, 0.0d,
				0.0d, cosTheta, sinTheta,
				0.0d, -sinTheta, cosTheta);
		}

		/// <summary>
		/// A 3D rotation matrix for Y-axis rotation
		/// </summary>
		/// <param name="theta">The rotation angle</param>
		public static Matrix3D GetRotationY(double theta)
		{
			double cos = Math.Cos(theta);
			double sin = Math.Sin(theta);
			return new Matrix3D(
				cos, 0.0d, -sin,
				0.0d, 1.0d, 0.0d,
				-sin, 0.0d, cos);
		}

		/// <summary>
		/// A 3D rotation matrix for Y-axis rotation
		/// </summary>
		/// <param name="cosTheta">The cosine of the rotation angle</param>
		/// <param name="sinTheta">The sine of the rotation angle</param>
		public static Matrix3D GetRotationY(double cosTheta, double sinTheta)
		{
			return new Matrix3D(
				cosTheta, 0.0d, -sinTheta,
				0.0d, 1.0d, 0.0d,
				-sinTheta, 0.0d, cosTheta);
		}

		/// <summary>
		/// A 3D rotation matrix for Y-axis rotation
		/// </summary>
		/// <param name="theta">The rotation angle</param>
		public static Matrix3D GetRotationZ(double theta)
		{
			double cos = Math.Cos(theta);
			double sin = Math.Sin(theta);
			return new Matrix3D(
				cos, sin, 0.0d,
				-sin, cos, 0.0d,
				0.0d, 0.0d, 1.0d);
		}

		/// <summary>
		/// A 3D rotation matrix for Y-axis rotation
		/// </summary>
		/// <param name="cosTheta">The cosine of the rotation angle</param>
		/// <param name="sinTheta">The sine of the rotation angle</param>
		public static Matrix3D GetRotationZ(double cosTheta, double sinTheta)
		{
			return new Matrix3D(
				cosTheta, sinTheta, 0.0d,
				-sinTheta, cosTheta, 0.0d,
				0.0d, 0.0d, 1.0d);
		}
		
		/// <summary>
		/// A 3D rotation matrix for axis-angle rotation
		/// </summary>
		/// <param name="axis">The axis to rotate around</param>
		/// <param name="theta">The rotation angle</param>
		public static Matrix3D GetRotationAxisAngle(Vector3D axis, double theta)
		{
			double cos = Math.Cos(theta);
			double sin = Math.Sin(theta);
			return GetRotationAxisAngle(axis, cos, sin);
		}

		/// <summary>
		/// A 3D rotation matrix for axis-angle rotation
		/// </summary>
		/// <param name="axis">The axis to rotate around</param>
		/// <param name="cosTheta">The cosine of the rotation angle</param>
		/// <param name="sinTheta">The sine of the rotation angle</param>
		public static Matrix3D GetRotationAxisAngle(Vector3D axis, double cosTheta, double sinTheta)
		{
			double cos = cosTheta;
			double sin = sinTheta;

			// pre-calculate squared
			double xx = axis.X * axis.X;
			double yy = axis.Y * axis.Y;
			double zz = axis.Z * axis.Z;

			// pre-calculate axis combinations
			double xy = axis.X * axis.Y;
			double xz = axis.X * axis.Z;
			double yz = axis.Y * axis.Z;

			// pre-calculate axes and angle functions
			double xsin = axis.X * sin;
			double ysin = axis.Y * sin;
			double zsin = axis.Z * sin;
			double xcos = axis.X * cos;
			double ycos = axis.Y * cos;

			/*
			return new Matrix3D(
				xx * (1 - cos) + cos, xy * (1 - cos) + zsin, xz * (1 - cos) - ysin, 0.0d,
				xy * (1 - cos) - zsin, yy * (1 - cos) + cos, yz * (1 - cos) + xsin, 0.0d,
				xz * (1 - cos) + ysin, yz * (1 - cos) + xsin, zz * (1 - cos) + cos, 0.0d,
				0.0d, 0.0d, 0.0d, 1.0d);
			*/

			return new Matrix3D(
				xx - axis.X * xcos + cos, xy - axis.Y * xcos + zsin, xz - axis.Y * xcos - ysin,
				xy - axis.Y * xcos - zsin, yy - axis.Y * ycos + cos, yz - axis.Z * ycos + xsin,
				xz - axis.Z * xcos + ysin, yz - axis.Z * ycos + xsin, zz - zz * cos + cos);
		}

		/// <summary>
		/// Gets a progressive rotation matrix based on angular speed
		/// </summary>
		/// <param name="deltaX">X rotation delta angle</param>
		/// <param name="deltaY">Y rotation delta angle</param>
		/// <param name="deltaZ">Z rotation delta angle</param>
		public static Matrix3D GetProgressiveRotation(double deltaX, double deltaY, double deltaZ)
		{
			return new Matrix3D(
				0.0d, -deltaZ, deltaY,
				deltaZ, 0.0d, -deltaX,
				-deltaY, deltaX, 0.0d);
		}
		
		#endregion

		#region Extended Mathematics

		/// <summary>
		/// Subtracts column <para>j</para>*<para>s</para> from column <para>i</para>
		/// </summary>
		/// <param name="i">Index of the column from which to subtract</param>
		/// <param name="j">Index of the column to subtract</param>
		/// <param name="s">The scaling factor of column j</param>
		public void SubtractFromColumn(int i, int j, double s)
		{
#if DEBUG
			if (i < 0 || i > 2) throw new ArgumentOutOfRangeException("i", i, "i must be in range 0..2");
			if (j < 0 || j > 2) throw new ArgumentOutOfRangeException("j", j, "j must be in range 0..2");
#endif

			Cell[0, i] = Cell[0, i] - (s * Cell[0, j]);
			Cell[1, i] = Cell[1, i] - (s * Cell[1, j]);
			Cell[2, i] = Cell[2, i] - (s * Cell[2, j]);
		}

		/// <summary>
		/// Returnes the transposed matrix
		/// </summary>
		/// <returns>Matrix3D</returns>
		public Matrix3D GetTransposed()
		{
			return new Matrix3D(
				Cell[0, 0], Cell[1, 0], Cell[2, 0],
				Cell[0, 1], Cell[1, 1], Cell[2, 1],
				Cell[0, 2], Cell[1, 2], Cell[2, 2]);
		}

		/// <summary>
		/// Transposes this matrix in place
		/// </summary>
		public void Transpose()
		{
			Matrix3D t = GetTransposed();
			Cell = t.Cell;
		}

		/// <summary>
		/// Gets a row vector
		/// </summary>
		/// <param name="row">The row index</param>
		/// <returns></returns>
		public Vector3D GetRowVector(int row)
		{
#if DEBUG
			if (row < 0 || row > 3) throw new ArgumentOutOfRangeException("row", row, "row must be in range 0..2");
#endif
			return new Vector3D(Cell[row, 0], Cell[row, 1], Cell[row, 2]);
		}

		/// <summary>
		/// Gets a column vector
		/// </summary>
		/// <param name="column">The column index</param>
		/// <returns></returns>
		public Vector3D GetColumnVector(int column)
		{
#if DEBUG
			if (column < 0 || column > 3) throw new ArgumentOutOfRangeException("column", column, "column must be in range 0..2");
#endif
			return new Vector3D(Cell[0, column], Cell[1, column], Cell[2, column]);
		}
		
		/// <summary>
		/// Gets the determinant
		/// </summary>
		/// <returns></returns>
		public double GetDeterminant()
		{
			// Regel von Sarrus
			// gem. Mathematische Formelsammlung, 9. Auflage, Papula, S. 201
			return Cell[0, 0]*Cell[1, 1]*Cell[2, 2] +
			       Cell[0, 1]*Cell[1, 2]*Cell[2, 0] +
			       Cell[0, 2]*Cell[1, 0]*Cell[2, 1] -
			       Cell[0, 2]*Cell[1, 1]*Cell[2, 0] -
			       Cell[0, 0]*Cell[1, 2]*Cell[2, 1] -
			       Cell[0, 1]*Cell[1, 0]*Cell[2, 2];
		}

		/// <summary>
		/// Gets the adjoint for a given row and column
		/// </summary>
		/// <param name="row">The row index 0..3</param>
		/// <param name="column">The column index 0..3</param>
		/// <returns></returns>
		public static double GetAdjoint(int row, int column)
		{
			// if both are even or both are odd, the result is positive
			return (row & 1) == (column & 1) ? 1 : -1;
		}

		#endregion

		#region Cast operators

		/// <summary>
		/// Converts a matrix to a <see cref="double"/>[]
		/// </summary>
		/// <param name="matrix"></param>
		/// <returns></returns>
		public static implicit operator double[,](Matrix3D matrix)
		{
			return matrix.Cell;
		}

		#endregion

		/// <summary>
		/// Creates a copy of this matrix
		/// </summary>
		/// <returns></returns>
		public Matrix3D Clone()
		{
			return new Matrix3D(this);
		}

		/// <summary>
		/// Creates a copy of this matrix
		/// </summary>
		/// <returns></returns>
		object ICloneable.Clone()
		{
			return Clone();
		}

		/// <summary>
		/// Determines if this matrix equals another one
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj)
		{
			if (obj is Matrix3D) return Equals((Matrix3D)obj);
			return base.Equals(obj);
		}

		/// <summary>
		/// Determines if this matrix equals another one
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool Equals(Matrix3D other)
		{
			if (ReferenceEquals(null, other)) return false;
			return
				Cell[0, 0] == other.Cell[0, 0] &&
				Cell[0, 1] == other.Cell[0, 1] &&
				Cell[0, 2] == other.Cell[0, 2] &&

				Cell[1, 0] == other.Cell[1, 0] &&
				Cell[1, 1] == other.Cell[1, 1] &&
				Cell[1, 2] == other.Cell[1, 2] &&

				Cell[2, 0] == other.Cell[2, 0] &&
				Cell[2, 1] == other.Cell[2, 1] &&
				Cell[2, 2] == other.Cell[2, 2];
		}

		public override int GetHashCode()
		{
			return (Cell != null ? Cell.GetHashCode() : 0);
		}

	}
}