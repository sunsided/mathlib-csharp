// $Id$

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Library.Vector;

namespace Library.Matrix
{
	/// <summary>
	/// 4-dimensional row-major matrix
	/// </summary>
	public sealed class Matrix4D : ICloneable, IEquatable<Matrix4D>
	{
		/// <summary>
		/// The cell values.
		/// The first dimension is the row, the second is the column.
		/// </summary>
		internal double[,] Cell;

		/// <summary>
		/// Accessor
		/// </summary>
		/// <param name="i">Zeile</param>
		/// <param name="j">Spalte</param>
		/// <returns></returns>
		public double this[int i, int j]
		{
			get { return Cell[i, j]; }
			set { Cell[i, j] = value; }
		}

		/// <summary>
		/// Gets the unit matrix
		/// </summary>
		public static readonly Matrix4D Unit = new Matrix4D(
			1.0d, 0.0d, 0.0d, 0.0d,
			0.0d, 1.0d, 0.0d, 0.0d,
			0.0d, 0.0d, 1.0d, 0.0d,
			0.0d, 0.0d, 0.0d, 1.0d);

		/// <summary>
		/// Gets the zero matrix
		/// </summary>
		public static readonly Matrix4D Zero = new Matrix4D(
			0.0d, 0.0d, 0.0d, 0.0d,
			0.0d, 0.0d, 0.0d, 0.0d,
			0.0d, 0.0d, 0.0d, 0.0d,
			0.0d, 0.0d, 0.0d, 0.0d);

		/// <summary>
		/// Gets a test matrix
		/// </summary>
		public static readonly Matrix4D Test = new Matrix4D(
			0.0d, 1.0d, 2.0d, 3.0d,
			0.1d, 1.1d, 2.1d, 3.1d,
			0.2d, 1.2d, 2.2d, 3.2d,
			0.3d, 1.3d, 2.3d, 3.3d);

		/// <summary>
		/// Gets the magic matrix
		/// </summary>
		public static readonly Matrix4D Magic = new Matrix4D(
			16.0d,  2.0d,  3.0d, 13.0d,
			 5.0d, 11.0d, 10.0d,  8.0d,
			 9.0d,  7.0d,  6.0d, 12.0d,
			 4.0d, 14.0d, 15.0d,  1.0d
			);

		#region Konstruktor

		/// <summary>
		/// Constructs a matrix from row vectors
		/// </summary>
		/// <param name="rows"></param>
		/// <returns></returns>
		public static Matrix4D FromRowVectors(IList<Vector4D> rows)
		{
			if (rows == null) throw new ArgumentNullException("rows");
			if (rows.Count != 4) throw new ArgumentOutOfRangeException("rows", "4 rows expected");
			return new Matrix4D(
				rows[0].X, rows[0].Y, rows[0].Z, rows[0].W,
				rows[1].X, rows[1].Y, rows[1].Z, rows[1].W,
				rows[2].X, rows[2].Y, rows[2].Z, rows[2].W,
				rows[3].X, rows[3].Y, rows[3].Z, rows[3].W);
		}

		/// <summary>
		/// Constructs a matrix from column vectors
		/// </summary>
		/// <param name="columns"></param>
		/// <returns></returns>
		public static Matrix4D FromColumnVectors(IList<Vector4D> columns)
		{
			if (columns == null) throw new ArgumentNullException("columns");
			if (columns.Count != 4) throw new ArgumentOutOfRangeException("columns", "4 columns expected");
			return new Matrix4D(
				columns[0].X, columns[1].X, columns[2].X, columns[3].X,
				columns[0].Y, columns[1].Y, columns[2].Y, columns[3].Y,
				columns[0].Z, columns[1].Z, columns[2].Z, columns[3].Z,
				columns[0].W, columns[1].W, columns[2].W, columns[3].W);
		}

		/// <summary>
		/// Creates a new instance of the <see cref="Matrix3D"/> class.
		/// </summary>
		public Matrix4D()
		{
			Cell = new double[4, 4];
		}

		/// <summary>
		/// Creates a new instance of the <see cref="Matrix3D"/> class.
		/// </summary>
		/// <param name="matrix">The 3D matrix to extend</param>
		public Matrix4D(Matrix3D matrix)
			: this()
		{
			Assign(matrix);
		}

		/// <summary>
		/// Creates a new instance of the <see cref="Matrix3D"/> class.
		/// </summary>
		/// <param name="matrix">The 4D matrix to copy</param>
		public Matrix4D(Matrix4D matrix)
			: this()
		{
			Assign(matrix);
		}

		/// <summary>
		/// Creates a new instance of the <see cref="Matrix3D"/> class and assigns values
		/// </summary>
		/// <param name="m11">The field (1,1)</param>
		/// <param name="m12">The field (1,2)</param>
		/// <param name="m13">The field (1,3)</param>
		/// <param name="m14">The field (1,4)</param>
		/// <param name="m21">The field (2,1)</param>
		/// <param name="m22">The field (2,2)</param>
		/// <param name="m23">The field (2,3)</param>
		/// <param name="m24">The field (2,4)</param>
		/// <param name="m31">The field (3,1)</param>
		/// <param name="m32">The field (3,2)</param>
		/// <param name="m33">The field (3,3)</param>
		/// <param name="m34">The field (3,4)</param>
		/// <param name="m41">The field (4,1)</param>
		/// <param name="m42">The field (4,2)</param>
		/// <param name="m43">The field (4,3)</param>
		/// <param name="m44">The field (4,4)</param>
		public Matrix4D(
			double m11, double m12, double m13, double m14,
			double m21, double m22, double m23, double m24,
			double m31, double m32, double m33, double m34,
			double m41, double m42, double m43, double m44)
			: this()
		{
			Cell[0, 0] = m11; Cell[0, 1] = m12; Cell[0, 2] = m13; Cell[0, 3] = m14;
			Cell[1, 0] = m21; Cell[1, 1] = m22; Cell[1, 2] = m23; Cell[1, 3] = m24;
			Cell[2, 0] = m31; Cell[2, 1] = m32; Cell[2, 2] = m33; Cell[2, 3] = m34;
			Cell[3, 0] = m41; Cell[3, 1] = m42; Cell[3, 2] = m43; Cell[3, 3] = m44;
		}

		#endregion

		#region Matrix type functions

		/// <summary>
		/// Sets the matrix to an identity matrix
		/// </summary>
		public void ToIdentity()
		{
			Cell[0, 0] = 1d; Cell[0, 1] = 0d; Cell[0, 2] = 0d; Cell[0, 3] = 0d;
			Cell[1, 0] = 0d; Cell[1, 1] = 1d; Cell[1, 2] = 0d; Cell[1, 3] = 0d;
			Cell[2, 0] = 0d; Cell[2, 1] = 0d; Cell[2, 2] = 1d; Cell[2, 3] = 0d;
			Cell[3, 0] = 0d; Cell[3, 1] = 0d; Cell[3, 2] = 0d; Cell[3, 3] = 1d;
		}

		/// <summary>
		/// Sets the matrix to a translation matrix
		/// </summary>
		/// <param name="translation">Translation component vector</param>
		public void ToTranslation(Vector3D translation)
		{
			Cell[0, 0] = 1d; Cell[0, 1] = 0d; Cell[0, 2] = 0d; Cell[0, 3] = 0d;
			Cell[1, 0] = 0d; Cell[1, 1] = 1d; Cell[1, 2] = 0d; Cell[1, 3] = 0d;
			Cell[2, 0] = 0d; Cell[2, 1] = 0d; Cell[2, 2] = 1d; Cell[2, 3] = 0d;
			Cell[3, 0] = translation.X;
			Cell[3, 1] = translation.Y;
			Cell[3, 2] = translation.Z; 
			Cell[3, 3] = 1d;
		}

		/// <summary>
		/// Sets the matrix to a translation matrix
		/// </summary>
		/// <param name="x">X component</param>
		/// <param name="y">Y component</param>
		/// <param name="z">Z component</param>
		public void ToTranslation(double x, double y, double z)
		{
			Cell[0, 0] = 1d; Cell[0, 1] = 0d; Cell[0, 2] = 0d; Cell[0, 3] = 0d;
			Cell[1, 0] = 0d; Cell[1, 1] = 1d; Cell[1, 2] = 0d; Cell[1, 3] = 0d;
			Cell[2, 0] = 0d; Cell[2, 1] = 0d; Cell[2, 2] = 1d; Cell[2, 3] = 0d;
			Cell[3, 0] = x;  Cell[3, 1] = y;  Cell[3, 2] = z;  Cell[3, 3] = 1d;
		}

		/// <summary>
		/// Sets the matrix to a scale matrix
		/// </summary>
		/// <param name="factors">Vector of scaling factors</param>
		public void ToScaling(Vector3D factors)
		{
			Cell[0, 0] = factors.X; Cell[0, 1] = 0d;		Cell[0, 2] = 0d;		Cell[0, 3] = 0d;
			Cell[1, 0] = 0d;		Cell[1, 1] = factors.Y; Cell[1, 2] = 0d;		Cell[1, 3] = 0d;
			Cell[2, 0] = 0d;		Cell[2, 1] = 0d;		Cell[2, 2] = factors.Z; Cell[2, 3] = 0d;
			Cell[3, 0] = 0d;		Cell[3, 1] = 0d;		Cell[3, 2] = 0d;		Cell[3, 3] = 1d;
		}

		/// <summary>
		/// Sets the matrix to a scale matrix
		/// </summary>
		/// <param name="x">X factor</param>
		/// <param name="y">Y factor</param>
		/// <param name="z">Z factor</param>
		public void ToScaling(double x, double y, double z)
		{
			Cell[0, 0] = x;  Cell[0, 1] = 0d; Cell[0, 2] = 0d; Cell[0, 3] = 0d;
			Cell[1, 0] = 0d; Cell[1, 1] = y;  Cell[1, 2] = 0d; Cell[1, 3] = 0d;
			Cell[2, 0] = 0d; Cell[2, 1] = 0d; Cell[2, 2] = z;  Cell[2, 3] = 0d;
			Cell[3, 0] = 0d; Cell[3, 1] = 0d; Cell[3, 2] = 0d; Cell[3, 3] = 1d;
		}

		#endregion

		#region Vector-Matrix Multiplication

		/// <summary>
		/// Transforms a given vector by a matrix.
		/// </summary>
		/// <param name="matrix">A <see cref="Matrix4D"/> instance.</param>
		/// <param name="vector">A <see cref="Vector4D"/> instance.</param>
		/// <returns>A new <see cref="Vector4D"/> instance containing the result.</returns>
		public static Vector4D operator *(Matrix4D matrix, Vector4D vector)
		{
			return new Vector4D (
				(matrix.Cell[0,0] * vector.X) + (matrix.Cell[1,0] * vector.Y) + (matrix.Cell[2,0] * vector.Z) + (matrix.Cell[3,0] * vector.W),
				(matrix.Cell[0,1] * vector.X) + (matrix.Cell[1,1] * vector.Y) + (matrix.Cell[2,1] * vector.Z) + (matrix.Cell[3,1] * vector.W),
				(matrix.Cell[0,2] * vector.X) + (matrix.Cell[1,2] * vector.Y) + (matrix.Cell[2,2] * vector.Z) + (matrix.Cell[3,2] * vector.W),
				(matrix.Cell[0,3] * vector.X) + (matrix.Cell[1,3] * vector.Y) + (matrix.Cell[2,3] * vector.Z) + (matrix.Cell[3,3] * vector.W));
		}

		/// <summary>
		/// Transforms a given vector by a matrix.
		/// </summary>
		/// <param name="matrix">A <see cref="Matrix4D"/> instance.</param>
		/// <param name="vector">A <see cref="Vector3D"/> instance.</param>
		/// <returns>A new <see cref="Vector3D"/> instance containing the result.</returns>
		public static Vector3D operator *(Matrix4D matrix, Vector3D vector)
		{
			return new Vector3D(
				(matrix.Cell[0, 0] * vector.X) + (matrix.Cell[1, 0] * vector.Y) + (matrix.Cell[2, 0] * vector.Z) + (matrix.Cell[3, 0]),
				(matrix.Cell[0, 1] * vector.X) + (matrix.Cell[1, 1] * vector.Y) + (matrix.Cell[2, 1] * vector.Z) + (matrix.Cell[3, 1]),
				(matrix.Cell[0, 2] * vector.X) + (matrix.Cell[1, 2] * vector.Y) + (matrix.Cell[2, 2] * vector.Z) + (matrix.Cell[3, 2]));
		}

		/// <summary>
		/// Tests if two matrices are identical
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static bool operator ==(Matrix4D a, Matrix4D b)
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
		public static bool operator !=(Matrix4D a, Matrix4D b)
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
		/// Gets or sets field M(1,4)
		/// </summary>
		public double M14
		{
			get { return Cell[0, 3]; }
			set { Cell[0, 3] = value; }
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
		/// Gets or sets field M(2,4)
		/// </summary>
		public double M24
		{
			get { return Cell[1, 3]; }
			set { Cell[1, 3] = value; }
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

		/// <summary>
		/// Gets or sets field M(3,4)
		/// </summary>
		public double M34
		{
			get { return Cell[2, 3]; }
			set { Cell[2, 3] = value; }
		}

		/// <summary>
		/// Gets or sets field M(4,1)
		/// </summary>
		public double M41
		{
			get { return Cell[3, 0]; }
			set { Cell[3, 0] = value; }
		}

		/// <summary>
		/// Gets or sets field M(4,2)
		/// </summary>
		public double M42
		{
			get { return Cell[3, 1]; }
			set { Cell[3, 1] = value; }
		}

		/// <summary>
		/// Gets or sets field M(4,3)
		/// </summary>
		public double M43
		{
			get { return Cell[3, 2]; }
			set { Cell[3, 2] = value; }
		}

		/// <summary>
		/// Gets or sets field M(4,4)
		/// </summary>
		public double M44
		{
			get { return Cell[3, 3]; }
			set { Cell[3, 3] = value; }
		}


		#endregion

		#region Arithmetik

		/// <summary>
		/// Transforms a given vector by a matrix.
		/// </summary>
		public static Matrix4D operator *(Matrix4D a, Matrix4D b)
		{
			return a.Multiply(b);
		}

		/// <summary>
		/// Multiplies the matrix with another one
		/// </summary>
		/// <param name="b">Matrix to concatenate</param>
		/// <returns>Matrix4D</returns>
		public Matrix4D Multiply(Matrix4D b)
		{
			Matrix4D mat = new Matrix4D(

				M11 * b.M11 + M12 * b.M21 + M13 * b.M31 + M14 * b.M41,
				M11 * b.M12 + M12 * b.M22 + M13 * b.M32 + M14 * b.M42,
				M11 * b.M13 + M12 * b.M23 + M13 * b.M33 + M14 * b.M43,
				M11 * b.M14 + M12 * b.M24 + M13 * b.M34 + M14 * b.M44,

				M21 * b.M11 + M22 * b.M21 + M23 * b.M31 + M24 * b.M41,
				M21 * b.M12 + M22 * b.M22 + M23 * b.M32 + M24 * b.M42,
				M21 * b.M13 + M22 * b.M23 + M23 * b.M33 + M24 * b.M43,
				M21 * b.M14 + M22 * b.M24 + M23 * b.M34 + M24 * b.M44,

				M31 * b.M11 + M32 * b.M21 + M33 * b.M31 + M34 * b.M41,
				M31 * b.M12 + M32 * b.M22 + M33 * b.M32 + M34 * b.M42,
				M31 * b.M13 + M32 * b.M23 + M33 * b.M33 + M34 * b.M43,
				M31 * b.M14 + M32 * b.M24 + M33 * b.M34 + M34 * b.M44,

				M41 * b.M11 + M42 * b.M21 + M43 * b.M31 + M44 * b.M41,
				M41 * b.M12 + M42 * b.M22 + M43 * b.M32 + M44 * b.M42,
				M41 * b.M13 + M42 * b.M23 + M43 * b.M33 + M44 * b.M43,
				M41 * b.M14 + M42 * b.M24 + M43 * b.M34 + M44 * b.M44);

			Cell = mat.Cell;
			return this;
		}

		/// <summary>
		/// Inverts the matrix
		/// </summary>
		/// <returns>Matrix4D</returns>
		public Matrix4D GetInverted()
		{
			Matrix4D mat = new Matrix4D();

			// transpose rotation matrix
			mat.Cell[0, 0] = Cell[0, 0];
			mat.Cell[0, 1] = Cell[1, 0];
			mat.Cell[0, 2] = Cell[2, 0];
			mat.Cell[1, 0] = Cell[0, 1];
			mat.Cell[1, 1] = Cell[1, 1];
			mat.Cell[1, 2] = Cell[2, 1];
			mat.Cell[2, 0] = Cell[0, 2];
			mat.Cell[2, 1] = Cell[1, 2];
			mat.Cell[2, 2] = Cell[2, 2];

			// set fourth column
			mat.Cell[0, 3] = 0d;
			mat.Cell[1, 3] = 0d;
			mat.Cell[2, 3] = 0d;
			mat.Cell[3, 3] = 1d;

			// Retrieve new translation vector
			Vector3D loc = new Vector3D(Cell[3, 0], Cell[3, 1], Cell[3, 2]);
			mat.Cell[3, 0] = -(loc.X * Cell[0, 0] + loc.Y * Cell[0, 1] + loc.Z * Cell[0, 2]);
			mat.Cell[3, 1] = -(loc.X * Cell[1, 0] + loc.Y * Cell[1, 1] + loc.Z * Cell[1, 2]);
			mat.Cell[3, 2] = -(loc.X * Cell[2, 0] + loc.Y * Cell[2, 1] + loc.Z * Cell[2, 2]);
			
			return mat;
		}

		#endregion

		#region Assign

		/// <summary>
		/// Assigns matrix values
		/// </summary>
		/// <param name="b">Matrix to copy</param>
		public void Assign(Matrix3D b)
		{
			if (b == null) throw new ArgumentNullException("b");
			Cell[0, 0] = b.Cell[0, 0]; Cell[0, 1] = b.Cell[0, 1]; Cell[0, 2] = b.Cell[0, 2]; Cell[0, 3] = 0;
			Cell[1, 0] = b.Cell[1, 0]; Cell[1, 1] = b.Cell[1, 1]; Cell[1, 2] = b.Cell[1, 2]; Cell[1, 3] = 0;
			Cell[2, 0] = b.Cell[2, 0]; Cell[2, 1] = b.Cell[2, 1]; Cell[2, 2] = b.Cell[2, 2]; Cell[2, 3] = 0;
			Cell[3, 0] =            0; Cell[3, 1] =            0; Cell[3, 2] =            0; Cell[3, 3] = 1;
		}

		/// <summary>
		/// Assigns matrix values
		/// </summary>
		/// <param name="b">Matrix to copy</param>
		/// <param name="lastRow">The last row</param>
		public void Assign(Matrix3D b, Vector4D lastRow)
		{
			if (b == null) throw new ArgumentNullException("b");
			if (lastRow == null) throw new ArgumentNullException("lastRow");
			Cell[0, 0] = b.Cell[0, 0]; Cell[0, 1] = b.Cell[0, 1]; Cell[0, 2] = b.Cell[0, 2]; Cell[0, 3] = 0;
			Cell[1, 0] = b.Cell[1, 0]; Cell[1, 1] = b.Cell[1, 1]; Cell[1, 2] = b.Cell[1, 2]; Cell[1, 3] = 0;
			Cell[2, 0] = b.Cell[2, 0]; Cell[2, 1] = b.Cell[2, 1]; Cell[2, 2] = b.Cell[2, 2]; Cell[2, 3] = 0;
			Cell[3, 0] = lastRow.X; Cell[3, 1] = lastRow.Y; Cell[3, 2] = lastRow.Z; Cell[3, 3] = lastRow.W;
		}

		/// <summary>
		/// Assigns matrix values
		/// </summary>
		/// <param name="b">Matrix to copy</param>
		public void Assign(Matrix4D b)
		{
			if (b == null) throw new ArgumentNullException("b");
			Cell[0, 0] = b.Cell[0, 0]; Cell[0, 1] = b.Cell[0, 1]; Cell[0, 2] = b.Cell[0, 2]; Cell[0, 3] = b.Cell[0, 3];
			Cell[1, 0] = b.Cell[1, 0]; Cell[1, 1] = b.Cell[1, 1]; Cell[1, 2] = b.Cell[1, 2]; Cell[1, 3] = b.Cell[1, 3];
			Cell[2, 0] = b.Cell[2, 0]; Cell[2, 1] = b.Cell[2, 1]; Cell[2, 2] = b.Cell[2, 2]; Cell[2, 3] = b.Cell[2, 3];
			Cell[3, 0] = b.Cell[3, 0]; Cell[3, 1] = b.Cell[3, 1]; Cell[3, 2] = b.Cell[3, 2]; Cell[3, 3] = b.Cell[3, 3];
		}

		/// <summary>
		/// Assigns matrix values
		/// </summary>
		/// <param name="m11">The field (1,1)</param>
		/// <param name="m12">The field (1,2)</param>
		/// <param name="m13">The field (1,3)</param>
		/// <param name="m14">The field (1,4)</param>
		/// <param name="m21">The field (2,1)</param>
		/// <param name="m22">The field (2,2)</param>
		/// <param name="m23">The field (2,3)</param>
		/// <param name="m24">The field (2,4)</param>
		/// <param name="m31">The field (3,1)</param>
		/// <param name="m32">The field (3,2)</param>
		/// <param name="m33">The field (3,3)</param>
		/// <param name="m34">The field (3,4)</param>
		/// <param name="m41">The field (4,1)</param>
		/// <param name="m42">The field (4,2)</param>
		/// <param name="m43">The field (4,3)</param>
		/// <param name="m44">The field (4,4)</param>
		public void Assign(
			double m11, double m12, double m13, double m14,
			double m21, double m22, double m23, double m24,
			double m31, double m32, double m33, double m34,
			double m41, double m42, double m43, double m44)
		{
			Cell[0, 0] = m11; Cell[0, 1] = m12; Cell[0, 2] = m13; Cell[0, 3] = m14;
			Cell[1, 0] = m21; Cell[1, 1] = m22; Cell[1, 2] = m23; Cell[1, 3] = m24;
			Cell[2, 0] = m31; Cell[2, 1] = m32; Cell[2, 2] = m33; Cell[2, 3] = m34;
			Cell[3, 0] = m41; Cell[3, 1] = m42; Cell[3, 2] = m43; Cell[3, 3] = m44;
		}		
		
		/// <summary>
		/// Assigns matrix values
		/// </summary>
		/// <param name="value">Value to fill in</param>
		public void Fill(double value)
		{
			M11 = value; M12 = value; M13 = value; M14 = value;
			M21 = value; M22 = value; M23 = value; M24 = value;
			M11 = value; M32 = value; M33 = value; M34 = value;
			M11 = value; M42 = value; M43 = value; M44 = value;
		}


		#endregion

		#region Statics

		/// <summary>
		/// A 3D rotation matrix for X-axis rotation
		/// </summary>
		/// <param name="theta">The rotation angle</param>
		public static Matrix4D GetRotationX(double theta)
		{
			double cos = Math.Cos(theta);
			double sin = Math.Sin(theta);			
			return new Matrix4D(
				1.0d, 0.0d, 0.0d, 0.0d,
				0.0d, cos, sin, 0.0d,
				0.0d, -sin, cos, 0.0d,
				0.0d, 0.0d, 0.0d, 1.0d);
		}

		/// <summary>
		/// A 3D rotation matrix for X-axis rotation
		/// </summary>
		/// <param name="cosTheta">The cosine of the rotation angle</param>
		/// <param name="sinTheta">The sine of the rotation angle</param>
		public static Matrix4D GetRotationX(double cosTheta, double sinTheta)
		{
			return new Matrix4D(
				1.0d, 0.0d, 0.0d, 0.0d,
				0.0d, cosTheta, sinTheta, 0.0d,
				0.0d, -sinTheta, cosTheta, 0.0d,
				0.0d, 0.0d, 0.0d, 1.0d);
		}

		/// <summary>
		/// A 3D rotation matrix for Y-axis rotation
		/// </summary>
		/// <param name="theta">The rotation angle</param>
		public static Matrix4D GetRotationY(double theta)
		{
			double cos = Math.Cos(theta);
			double sin = Math.Sin(theta);
			return new Matrix4D(
				cos, 0.0d, -sin, 0.0d,
				0.0d, 1.0d, 0.0d, 0.0d,
				-sin, 0.0d, cos, 0.0d,
				0.0d, 0.0d, 0.0d, 1.0d);
		}

		/// <summary>
		/// A 3D rotation matrix for Y-axis rotation
		/// </summary>
		/// <param name="cosTheta">The cosine of the rotation angle</param>
		/// <param name="sinTheta">The sine of the rotation angle</param>
		public static Matrix4D GetRotationY(double cosTheta, double sinTheta)
		{
			return new Matrix4D(
				cosTheta, 0.0d, -sinTheta, 0.0d,
				0.0d, 1.0d, 0.0d, 0.0d,
				-sinTheta, 0.0d, cosTheta, 0.0d,
				0.0d, 0.0d, 0.0d, 1.0d);
		}

		/// <summary>
		/// A 3D rotation matrix for Y-axis rotation
		/// </summary>
		/// <param name="theta">The rotation angle</param>
		public static Matrix4D GetRotationZ(double theta)
		{
			double cos = Math.Cos(theta);
			double sin = Math.Sin(theta);
			return new Matrix4D(
				cos, sin, 0.0d, 0.0d,
				-sin, cos, 0.0d, 0.0d,
				0.0d, 0.0d, 1.0d, 0.0d,
				0.0d, 0.0d, 0.0d, 1.0d);
		}

		/// <summary>
		/// A 3D rotation matrix for Y-axis rotation
		/// </summary>
		/// <param name="cosTheta">The cosine of the rotation angle</param>
		/// <param name="sinTheta">The sine of the rotation angle</param>
		public static Matrix4D GetRotationZ(double cosTheta, double sinTheta)
		{
			return new Matrix4D(
				cosTheta, sinTheta, 0.0d, 0.0d,
				-sinTheta, cosTheta, 0.0d, 0.0d,
				0.0d, 0.0d, 1.0d, 0.0d,
				0.0d, 0.0d, 0.0d, 1.0d);
		}

		/// <summary>
		/// A 3D rotation matrix for axis-angle rotation
		/// </summary>
		/// <param name="axis">The axis to rotate around</param>
		/// <param name="theta">The rotation angle</param>
		public static Matrix4D GetRotationAxisAngle(Vector3D axis, double theta)
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
		public static Matrix4D GetRotationAxisAngle(Vector3D axis, double cosTheta, double sinTheta)
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
			return new Matrix4D(
				xx * (1 - cos) + cos, xy * (1 - cos) + zsin, xz * (1 - cos) - ysin, 0.0d,
				xy * (1 - cos) - zsin, yy * (1 - cos) + cos, yz * (1 - cos) + xsin, 0.0d,
				xz * (1 - cos) + ysin, yz * (1 - cos) + xsin, zz * (1 - cos) + cos, 0.0d,
				0.0d, 0.0d, 0.0d, 1.0d);
			*/

			return new Matrix4D(
				xx - axis.X * xcos + cos, xy - axis.Y * xcos + zsin, xz - axis.Y * xcos - ysin, 0.0d,
				xy - axis.Y * xcos - zsin, yy - axis.Y * ycos + cos, yz - axis.Z * ycos + xsin, 0.0d,
				xz - axis.Z * xcos + ysin, yz - axis.Z * ycos + xsin, zz - zz * cos + cos, 0.0d,
				0.0d, 0.0d, 0.0d, 1.0d);
		}

		/// <summary>
		/// Gets a progressive rotation matrix based on angular speed
		/// </summary>
		/// <param name="deltaX">X rotation delta angle</param>
		/// <param name="deltaY">Y rotation delta angle</param>
		/// <param name="deltaZ">Z rotation delta angle</param>
		public static Matrix4D GetProgressiveRotation(double deltaX, double deltaY, double deltaZ)
		{
			return new Matrix4D(
				0.0d, -deltaZ, deltaY, 0.0d,
				deltaZ, 0.0d, -deltaX, 0.0d,
				-deltaY, deltaX, 0.0d, 0.0d,
				0.0d, 0.0d, 0.0d, 1.0d);
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
			Cell[0, i] = Cell[0, i] - (s * Cell[0, j]);
			Cell[1, i] = Cell[1, i] - (s * Cell[1, j]);
			Cell[2, i] = Cell[2, i] - (s * Cell[2, j]);
			Cell[3, i] = Cell[3, i] - (s * Cell[3, j]);
		}

		/// <summary>
		/// Swaps two rows
		/// </summary>
		/// <param name="row1">The first row</param>
		/// <param name="row2">The second row</param>
		public void SwapRows(int row1, int row2)
		{
			var a = Cell[row1, 0];
			var b = Cell[row1, 1];
			var c = Cell[row1, 2];
			var d = Cell[row1, 3];
			
			Cell[row1, 0] = Cell[row2, 0];
			Cell[row1, 1] = Cell[row2, 1];
			Cell[row1, 2] = Cell[row2, 2];
			Cell[row1, 3] = Cell[row2, 3];

			Cell[row2, 0] = a; 
			Cell[row2, 1] = b; 
			Cell[row2, 2] = c; 
			Cell[row2, 3] = d;
		}

		/// <summary>
		/// Swaps two columns
		/// </summary>
		/// <param name="col1">The first column</param>
		/// <param name="col2">The second column</param>
		public void SwapColumns(int col1, int col2)
		{
			var a = Cell[0, col1];
			var b = Cell[1, col1];
			var c = Cell[2, col1];
			var d = Cell[3, col1];

			Cell[0, col1] = Cell[0, col2];
			Cell[1, col1] = Cell[1, col2];
			Cell[2, col1] = Cell[2, col2];
			Cell[3, col1] = Cell[3, col2];

			Cell[0, col2] = a;
			Cell[1, col2] = b;
			Cell[2, col2] = c;
			Cell[3, col2] = d;
		}
		
		/// <summary>
		/// Returns a transposed matrix
		/// </summary>
		/// <returns>Matrix4D</returns>
		public Matrix4D GetTransposed()
		{
			return new Matrix4D(
				Cell[0, 0], Cell[1, 0], Cell[2, 0], Cell[3, 0],
				Cell[0, 1], Cell[1, 1], Cell[2, 1], Cell[3, 1],
				Cell[0, 2], Cell[1, 2], Cell[2, 2], Cell[3, 2],
				Cell[0, 3], Cell[1, 3], Cell[2, 3], Cell[3, 3]);
		}

		/// <summary>
		/// Transposes this matrix in place
		/// </summary>
		public void Transpose()
		{
			Matrix4D t = GetTransposed();
			Cell = t.Cell;
		}

		/// <summary>
		/// Gets a submatrix, hiding a row and a column
		/// </summary>
		/// <param name="rowIndex">Index of the row to hide</param>
		/// <param name="columnIndex">Index of the column to hide</param>
		/// <returns>Submatrix of a</returns>
		public Matrix3D GetSubmatrix(int rowIndex, int columnIndex)
		{
			Matrix3D c = new Matrix3D();

			int targetRow = 0;
			for (int p = 0; p < 4; p++)
			{
				if (p == rowIndex) continue;

				int targetColumn = 0;
				if (columnIndex != 0) c.Cell[targetRow, targetColumn++] = Cell[p, 0];
				if (columnIndex != 1) c.Cell[targetRow, targetColumn++] = Cell[p, 1];
				if (columnIndex != 2) c.Cell[targetRow, targetColumn++] = Cell[p, 2];
				if (columnIndex != 3) c.Cell[targetRow, targetColumn] = Cell[p, 3];

				targetRow++;
			}

			return (c);
		}
		
		/// <summary>
		/// Gets a row vector
		/// </summary>
		/// <param name="row">The row index</param>
		/// <returns></returns>
		public Vector4D GetRowVector(int row)
		{
			if (row < 0 || row > 3) throw new ArgumentOutOfRangeException("row", row, "row must be in range 0..3");
			return new Vector4D(Cell[row, 0], Cell[row, 1], Cell[row, 2], Cell[row, 3]);
		}

		/// <summary>
		/// Gets a column vector
		/// </summary>
		/// <param name="column">The column index</param>
		/// <returns></returns>
		public Vector4D GetColumnVector(int column)
		{
			if (column < 0 || column > 3) throw new ArgumentOutOfRangeException("column", column, "column must be in range 0..3");
			return new Vector4D(Cell[0, column], Cell[1, column], Cell[2, column], Cell[3, column]);
		}

		/// <summary>
		/// Gets the determinant
		/// </summary>
		/// <returns></returns>
		public double GetDeterminant()
		{
#if NO_UNROLL
			return
				Cell[0, 0]*GetSubDeterminant(0, 0) -
				Cell[0, 1]*GetSubDeterminant(0, 1) +
				Cell[0, 2]*GetSubDeterminant(0, 2) -
				Cell[0, 3]*GetSubDeterminant(0, 3);
#else

			double value1 = Cell[0, 0]*(Cell[1, 1]*Cell[2, 2]*Cell[3, 3] +
			                            Cell[1, 2]*Cell[2, 3]*Cell[3, 1] +
			                            Cell[1, 3]*Cell[2, 1]*Cell[3, 2] -
			                            Cell[1, 3]*Cell[2, 2]*Cell[3, 1] -
			                            Cell[1, 1]*Cell[2, 3]*Cell[3, 2] -
			                            Cell[1, 2]*Cell[2, 1]*Cell[3, 3]);

			double value2 = Cell[0, 1]*(Cell[1, 0]*Cell[2, 2]*Cell[3, 3] +
			                            Cell[1, 2]*Cell[2, 3]*Cell[3, 0] +
			                            Cell[1, 3]*Cell[2, 0]*Cell[3, 2] -
			                            Cell[1, 3]*Cell[2, 2]*Cell[3, 0] -
			                            Cell[1, 0]*Cell[2, 3]*Cell[3, 2] -
			                            Cell[1, 2]*Cell[2, 0]*Cell[3, 3]);

			double value3 = Cell[0, 2]*(Cell[1, 0]*Cell[2, 1]*Cell[3, 3] +
			                            Cell[1, 1]*Cell[2, 3]*Cell[3, 0] +
			                            Cell[1, 3]*Cell[2, 0]*Cell[3, 1] -
			                            Cell[1, 3]*Cell[2, 1]*Cell[3, 0] -
			                            Cell[1, 0]*Cell[2, 3]*Cell[3, 1] -
			                            Cell[1, 1]*Cell[2, 0]*Cell[3, 3]);

			double value4 = Cell[0, 3]*(Cell[1, 0]*Cell[2, 1]*Cell[3, 2] +
			                            Cell[1, 1]*Cell[2, 2]*Cell[3, 0] +
			                            Cell[1, 2]*Cell[2, 0]*Cell[3, 1] -
			                            Cell[1, 2]*Cell[2, 1]*Cell[3, 0] -
			                            Cell[1, 0]*Cell[2, 2]*Cell[3, 1] -
			                            Cell[1, 1]*Cell[2, 0]*Cell[3, 2]);

			return value1 - value2 + value3 - value4;

#endif
		}

		/// <summary>
		/// Gets the adjoint for a given row and column
		/// </summary>
		/// <param name="row">The row index 0..3</param>
		/// <param name="column">The column index 0..3</param>
		/// <returns></returns>
		public static double GetAdjoint(int row, int column)
		{
			double adjoint = 1;
			if ((row & 1) == 1) adjoint *= -1.0D; // Flip sign for every even row
			if ((column & 1) == 1) adjoint *= -1.0D; // Flip sign for every even column
			return adjoint;
		}

		/// <summary>
		/// Gets the 3x3 sub-determinant by ignoring a given row and column.
		/// This is a memory efficient alternative to getting the submatrix and then calculate it's determinant.
		/// </summary>
		/// <param name="row">The row to skip</param>
		/// <param name="column">The column to skip</param>
		/// <returns></returns>
		public double GetSubDeterminant(int row, int column)
		{
			// get target row indices
			int row0 = 0;
			int row1 = 1;
			int row2 = 2;

			// get target column indices
			int col0 = 0;
			int col1 = 1;
			int col2 = 2;

			// adjust for skipped rows
			if (row == 0) { ++row0; ++row1; ++row2; }
			else if (row == 1) { ++row1; ++row2; }
			else if (row == 2) { ++row2; }

			// adjust for skipped columns
			if (column == 0) { ++col0; ++col1; ++col2; }
			else if (column == 1) { ++col1; ++col2; }
			else if (column == 2) { ++col2; }

			// Regel von Sarrus
			// gem. Mathematische Formelsammlung, col9. Auflage, Papula, S. 201
			return Cell[row0, col0] * Cell[row1, col1] * Cell[row2, col2] +
				   Cell[row0, col1] * Cell[row1, col2] * Cell[row2, col0] +
				   Cell[row0, col2] * Cell[row1, col0] * Cell[row2, col1] -
				   Cell[row0, col2] * Cell[row1, col1] * Cell[row2, col0] -
				   Cell[row0, col0] * Cell[row1, col2] * Cell[row2, col1] -
				   Cell[row0, col1] * Cell[row1, col0] * Cell[row2, col2];
		}

		/// <summary>
		/// Returns the matrix sorted by row values
		/// </summary>
		/// <param name="column">The column to sort along</param>
		/// <returns></returns>
		public Matrix4D GetRowSorted(int column)
		{
			List<Vector4D> rows = new List<Vector4D>(4);
			rows.Add(GetRowVector(0));
			rows.Add(GetRowVector(1));
			rows.Add(GetRowVector(2));
			rows.Add(GetRowVector(3));
			rows.Sort((a, b) => a.Fields[column].CompareTo(b.Fields[column]));
			return FromRowVectors(rows);
		}
		
		/// <summary>
		/// Returns the matrix sorted by column values
		/// </summary>
		/// <param name="row">The row to sort along</param>
		/// <returns></returns>
		public Matrix4D GetColumnSorted(int row)
		{
			List<Vector4D> columns = new List<Vector4D>(4);
			columns.Add(GetColumnVector(0));
			columns.Add(GetColumnVector(1));
			columns.Add(GetColumnVector(2));
			columns.Add(GetColumnVector(3));
			columns.Sort((a, b) => a.Fields[row].CompareTo(b.Fields[row]));
			return FromColumnVectors(columns);
		}

		#endregion

		#region Cast operators

		/// <summary>
		/// Converts a matrix to a <see cref="double"/>[]
		/// </summary>
		/// <param name="matrix"></param>
		/// <returns></returns>
		public static implicit operator double[,](Matrix4D matrix)
		{
			return matrix.Cell;
		}

		#endregion

		/// <summary>
		/// Creates a copy of this matrix
		/// </summary>
		/// <returns></returns>
		public Matrix4D Clone()
		{
			return new Matrix4D(this);
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
			if (obj is Matrix4D) return Equals((Matrix4D) obj);
			return base.Equals(obj);
		}

		/// <summary>
		/// Determines if this matrix equals another one
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool Equals(Matrix4D other)
		{
			return
				Cell[0, 0] == other.Cell[0, 0] &&
				Cell[0, 1] == other.Cell[0, 1] &&
				Cell[0, 2] == other.Cell[0, 2] &&
				Cell[0, 3] == other.Cell[0, 3] &&

				Cell[1, 0] == other.Cell[1, 0] &&
				Cell[1, 1] == other.Cell[1, 1] &&
				Cell[1, 2] == other.Cell[1, 2] &&
				Cell[1, 3] == other.Cell[1, 3] &&

				Cell[2, 0] == other.Cell[2, 0] &&
				Cell[2, 1] == other.Cell[2, 1] &&
				Cell[2, 2] == other.Cell[2, 2] &&
				Cell[2, 3] == other.Cell[2, 3] &&

				Cell[3, 0] == other.Cell[3, 0] &&
				Cell[3, 1] == other.Cell[3, 1] &&
				Cell[3, 2] == other.Cell[3, 2] &&
				Cell[3, 3] == other.Cell[3, 3];
		}

		public override int GetHashCode()
		{
			return (Cell != null ? Cell.GetHashCode() : 0);
		}

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append("[");
			for (int j=0; j<4; ++j)
			{
				if (j > 0) builder.Append("; ");
				for(int i=0; i<4; ++i)
				{
					if (i > 0) builder.Append(", ");
					builder.AppendFormat(CultureInfo.InvariantCulture, "{0}", Cell[i, j]);
				}
			}
			builder.Append("]");
			return builder.ToString();
		}
	}
}