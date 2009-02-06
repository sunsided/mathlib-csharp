using System;
using Library.Matrix;
using Library.Vector;

namespace Library.Matrix
{
	/// <summary>
	/// 4-dimensional row-major matrix
	/// </summary>
	public sealed class Matrix4D : BasicMatrix
	{
		/// <summary>
		/// Gets the unit matrix
		/// </summary>
		public static readonly Matrix4D Unit = new Matrix4D(
			1.0f, 0.0f, 0.0f, 0.0f,
			0.0f, 1.0f, 0.0f, 0.0f,
			0.0f, 0.0f, 1.0f, 0.0f,
			0.0f, 0.0f, 0.0f, 1.0f);

		#region Konstruktor

		/// <summary>
		/// Initializes a new instance of the <see cref="Matrix4D"/> class.
		/// </summary>
		public Matrix4D()
		{
			Cell = new double[4, 4];
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Matrix4D"/> class.
		/// </summary>
		public Matrix4D(
			double M11, double M12, double M13, double M14,
			double M21, double M22, double M23, double M24,
			double M31, double M32, double M33, double M34,
			double M41, double M42, double M43, double M44)
			: this()
		{
			Cell[0, 0] = M11; Cell[0, 1] = M12; Cell[0, 2] = M13; Cell[0, 3] = M14;
			Cell[1, 0] = M21; Cell[1, 1] = M22; Cell[1, 2] = M23; Cell[1, 3] = M24;
			Cell[2, 0] = M31; Cell[2, 1] = M32; Cell[2, 2] = M33; Cell[2, 3] = M34;
			Cell[3, 0] = M41; Cell[3, 1] = M42; Cell[3, 2] = M43; Cell[3, 3] = M44;
		}

		#endregion

		#region Matrix type functions

		/// <summary>
		/// Sets the matrix to an identity matrix
		/// </summary>
		public override void ToIdentity()
		{
			Cell[0, 0] = 1f; Cell[0, 1] = 0f; Cell[0, 2] = 0f; Cell[0, 3] = 0f;
			Cell[1, 0] = 0f; Cell[1, 1] = 1f; Cell[1, 2] = 0f; Cell[1, 3] = 0f;
			Cell[2, 0] = 0f; Cell[2, 1] = 0f; Cell[2, 2] = 1f; Cell[2, 3] = 0f;
			Cell[3, 0] = 0f; Cell[3, 1] = 0f; Cell[3, 2] = 0f; Cell[3, 3] = 1f;
		}

		/// <summary>
		/// Sets the matrix to a translation matrix
		/// </summary>
		/// <param name="translation">Translation component vector</param>
		public void ToTranslation(Vector3D translation)
		{
			Cell[0, 0] = 1f; Cell[0, 1] = 0f; Cell[0, 2] = 0f; Cell[0, 3] = 0f;
			Cell[1, 0] = 0f; Cell[1, 1] = 1f; Cell[1, 2] = 0f; Cell[1, 3] = 0f;
			Cell[2, 0] = 0f; Cell[2, 1] = 0f; Cell[2, 2] = 1f; Cell[2, 3] = 0f;
			Cell[3, 0] = translation.X;
			Cell[3, 1] = translation.Y;
			Cell[3, 2] = translation.Z; 
			Cell[3, 3] = 1f;
		}

		/// <summary>
		/// Sets the matrix to a translation matrix
		/// </summary>
		/// <param name="x">X component</param>
		/// <param name="y">Y component</param>
		/// <param name="z">Z component</param>
		public void ToTranslation(double x, double y, double z)
		{
			Cell[0, 0] = 1f; Cell[0, 1] = 0f; Cell[0, 2] = 0f; Cell[0, 3] = 0f;
			Cell[1, 0] = 0f; Cell[1, 1] = 1f; Cell[1, 2] = 0f; Cell[1, 3] = 0f;
			Cell[2, 0] = 0f; Cell[2, 1] = 0f; Cell[2, 2] = 1f; Cell[2, 3] = 0f;
			Cell[3, 0] = x;  Cell[3, 1] = y;  Cell[3, 2] = z;  Cell[3, 3] = 1f;
		}

		/// <summary>
		/// Sets the matrix to a scale matrix
		/// </summary>
		/// <param name="factors">Vector of scaling factors</param>
		public void ToScale(Vector3D factors)
		{
			Cell[0, 0] = factors.X; Cell[0, 1] = 0f;		Cell[0, 2] = 0f;		Cell[0, 3] = 0f;
			Cell[1, 0] = 0f;		Cell[1, 1] = factors.Y; Cell[1, 2] = 0f;		Cell[1, 3] = 0f;
			Cell[2, 0] = 0f;		Cell[2, 1] = 0f;		Cell[2, 2] = factors.Z; Cell[2, 3] = 0f;
			Cell[3, 0] = 0f;		Cell[3, 1] = 0f;		Cell[3, 2] = 0f;		Cell[3, 3] = 1f;
		}

		/// <summary>
		/// Sets the matrix to a scale matrix
		/// </summary>
		/// <param name="x">X factor</param>
		/// <param name="y">Y factor</param>
		/// <param name="z">Z factor</param>
		public void ToScale(double x, double y, double z)
		{
			Cell[0, 0] = x;  Cell[0, 1] = 0f; Cell[0, 2] = 0f; Cell[0, 3] = 0f;
			Cell[1, 0] = 0f; Cell[1, 1] = y;  Cell[1, 2] = 0f; Cell[1, 3] = 0f;
			Cell[2, 0] = 0f; Cell[2, 1] = 0f; Cell[2, 2] = z;  Cell[2, 3] = 0f;
			Cell[3, 0] = 0f; Cell[3, 1] = 0f; Cell[3, 2] = 0f; Cell[3, 3] = 1f;
		}

		#endregion

		#region Vector-Matrix Multiplication

		/// <summary>
		/// Transforms a given vector by a matrix.
		/// </summary>
		/// <param name="matrix">A <see cref="BasicMatrix"/> instance.</param>
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
		/// <param name="matrix">A <see cref="BasicMatrix"/> instance.</param>
		/// <param name="vector">A <see cref="Vector3D"/> instance.</param>
		/// <returns>A new <see cref="Vector3D"/> instance containing the result.</returns>
		public static Vector3D operator *(Matrix4D matrix, Vector3D vector)
		{
			return new Vector3D(
				(matrix.Cell[0, 0] * vector.X) + (matrix.Cell[1, 0] * vector.Y) + (matrix.Cell[2, 0] * vector.Z) + (matrix.Cell[3, 0]),
				(matrix.Cell[0, 1] * vector.X) + (matrix.Cell[1, 1] * vector.Y) + (matrix.Cell[2, 1] * vector.Z) + (matrix.Cell[3, 1]),
				(matrix.Cell[0, 2] * vector.X) + (matrix.Cell[1, 2] * vector.Y) + (matrix.Cell[2, 2] * vector.Z) + (matrix.Cell[3, 2]));
		}

		#endregion

		#region Specific Fields

		public double M11
		{
			get { return Cell[0, 0]; }
			set { Cell[0, 0] = value; }
		}

		public double M12
		{
			get { return Cell[0, 1]; }
			set { Cell[0, 1] = value; }
		}

		public double M13
		{
			get { return Cell[0, 2]; }
			set { Cell[0, 2] = value; }
		}

		public double M14
		{
			get { return Cell[0, 3]; }
			set { Cell[0, 3] = value; }
		}

		public double M21
		{
			get { return Cell[1, 0]; }
			set { Cell[1, 0] = value; }
		}

		public double M22
		{
			get { return Cell[1, 1]; }
			set { Cell[1, 1] = value; }
		}

		public double M23
		{
			get { return Cell[1, 2]; }
			set { Cell[1, 2] = value; }
		}

		public double M24
		{
			get { return Cell[1, 3]; }
			set { Cell[1, 3] = value; }
		}

		public double M31
		{
			get { return Cell[2, 0]; }
			set { Cell[2, 0] = value; }
		}

		public double M32
		{
			get { return Cell[2, 1]; }
			set { Cell[2, 1] = value; }
		}

		public double M33
		{
			get { return Cell[2, 2]; }
			set { Cell[2, 2] = value; }
		}

		public double M34
		{
			get { return Cell[2, 3]; }
			set { Cell[2, 3] = value; }
		}

		public double M41
		{
			get { return Cell[3, 0]; }
			set { Cell[3, 0] = value; }
		}

		public double M42
		{
			get { return Cell[3, 1]; }
			set { Cell[3, 1] = value; }
		}

		public double M43
		{
			get { return Cell[3, 2]; }
			set { Cell[3, 2] = value; }
		}

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

			Assign(mat);
			return this;
		}

		/// <summary>
		/// Inverts the matrix
		/// </summary>
		/// <returns>Matrix4D</returns>
		public Matrix4D Invert()
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
			mat.Cell[0, 3] = 0f;
			mat.Cell[1, 3] = 0f;
			mat.Cell[2, 3] = 0f;
			mat.Cell[3, 3] = 1f;

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
		public void Assign(Matrix4D b)
		{
			M11 = b.Cell[0, 0]; M12 = b.Cell[0, 1]; M13 = b.Cell[0, 2]; M14 = b.Cell[0, 3];
			M21 = b.Cell[1, 0]; M22 = b.Cell[1, 1]; M23 = b.Cell[1, 2]; M24 = b.Cell[1, 3];
			M11 = b.Cell[2, 0]; M32 = b.Cell[2, 1]; M33 = b.Cell[2, 2]; M34 = b.Cell[2, 3];
			M11 = b.Cell[3, 0]; M42 = b.Cell[3, 1]; M43 = b.Cell[3, 2]; M44 = b.Cell[3, 3];
		}

		/// <summary>
		/// Assigns matrix values
		/// </summary>
		/// <param name="M11">M11</param>
		/// <param name="M12">M12</param>
		/// <param name="M13">M13</param>
		/// <param name="M14">M14</param>
		/// <param name="M21">M21</param>
		/// <param name="M22">M22</param>
		/// <param name="M23">M23</param>
		/// <param name="M24">M24</param>
		/// <param name="M31">M31</param>
		/// <param name="M32">M32</param>
		/// <param name="M33">M33</param>
		/// <param name="M34">M34</param>
		/// <param name="M41">M41</param>
		/// <param name="M42">M42</param>
		/// <param name="M43">M43</param>
		/// <param name="M44">M44</param>
		public void Assign(
			double M11, double M12, double M13, double M14,
			double M21, double M22, double M23, double M24,
			double M31, double M32, double M33, double M34,
			double M41, double M42, double M43, double M44)
		{
			Cell[0, 0] = M11; Cell[0, 1] = M12; Cell[0, 2] = M13; Cell[0, 3] = M14;
			Cell[1, 0] = M21; Cell[1, 1] = M22; Cell[1, 2] = M23; Cell[1, 3] = M24;
			Cell[2, 0] = M31; Cell[2, 1] = M32; Cell[2, 2] = M33; Cell[2, 3] = M34;
			Cell[3, 0] = M41; Cell[3, 1] = M42; Cell[3, 2] = M43; Cell[3, 3] = M44;
		}		
		
		/// <summary>
		/// Assigns matrix values
		/// </summary>
		/// <param name="value">Value to fill in</param>
		public override void Fill(double value)
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
		/// <example>
		public static Matrix4D GetRotationX(double theta)
		{
			double cos = (double)Math.Cos(theta);
			double sin = (double)Math.Sin(theta);			
			return new Matrix4D(
				1.0f, 0.0f, 0.0f, 0.0f,
				0.0f, cos, sin, 0.0f,
				0.0f, -sin, cos, 0.0f,
				0.0f, 0.0f, 0.0f, 1.0f);
		}

		/// <summary>
		/// A 3D rotation matrix for Y-axis rotation
		/// </summary>
		public static Matrix4D GetRotationY(double theta)
		{
			double cos = (double)Math.Cos(theta);
			double sin = (double)Math.Sin(theta);
			return new Matrix4D(
				cos, 0.0f, -sin, 0.0f,
				0.0f, 1.0f, 0.0f, 0.0f,
				-sin, 0.0f, cos, 0.0f,
				0.0f, 0.0f, 0.0f, 1.0f);
		}

		/// <summary>
		/// A 3D rotation matrix for Y-axis rotation
		/// </summary>
		public static Matrix4D GetRotationZ(double theta)
		{
			double cos = (double)Math.Cos(theta);
			double sin = (double)Math.Sin(theta);
			return new Matrix4D(
				cos, sin, 0.0f, 0.0f,
				-sin, cos, 0.0f, 0.0f,
				0.0f, 0.0f, 1.0f, 0.0f,
				0.0f, 0.0f, 0.0f, 1.0f);
		}
		
		/// <summary>
		/// A 3D rotation matrix for axis-angle rotation
		/// </summary>
		public static Matrix4D GetRotationAxisAngle(Vector3D axis, double theta)
		{
			double cos = (double)Math.Cos(theta);
			double sin = (double)Math.Sin(theta);
			
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
				xx * (1 - cos) + cos, xy * (1 - cos) + zsin, xz * (1 - cos) - ysin, 0.0f,
				xy * (1 - cos) - zsin, yy * (1 - cos) + cos, yz * (1 - cos) + xsin, 0.0f,
				xz * (1 - cos) + ysin, yz * (1 - cos) + xsin, zz * (1 - cos) + cos, 0.0f,
				0.0f, 0.0f, 0.0f, 1.0f);
			*/

			return new Matrix4D(
				xx - axis.X * xcos + cos, xy - axis.Y * xcos + zsin, xz - axis.Y * xcos - ysin, 0.0f,
				xy - axis.Y * xcos - zsin, yy - axis.Y * ycos + cos, yz - axis.Z * ycos + xsin, 0.0f,
				xz - axis.Z * xcos + ysin, yz - axis.Z * ycos + xsin, zz - zz * cos + cos, 0.0f,
				0.0f, 0.0f, 0.0f, 1.0f);
		}

		/// <summary>
		/// Gets a progressive rotation matrix based on angular speed
		/// </summary>
		public static Matrix4D GetProgressiveRotation(double deltaX, double deltaY, double deltaZ)
		{
			return new Matrix4D(
				0.0f, -deltaZ, deltaY, 0.0f,
				deltaZ, 0.0f, -deltaX, 0.0f,
				-deltaY, deltaX, 0.0f, 0.0f,
				0.0f, 0.0f, 0.0f, 1.0f);
		}
		
		#endregion

		#region Extended Mathematics

		/// <summary>
		/// Subtracts column <para>j</para>*<para>s</para> from column <para>i</para>
		/// </summary>
		/// <param name="i">The column from which to subtract</param>
		/// <param name="j">The column to subtract</param>
		/// <param name="s">The scaling factor of column j</param>
		private static void colsub(Matrix4D m, int i, int j, double s)
		{
			m.Cell[0, i] = m.Cell[0, i] - (s * m.Cell[0, j]);
			m.Cell[1, i] = m.Cell[1, i] - (s * m.Cell[1, j]);
			m.Cell[2, i] = m.Cell[2, i] - (s * m.Cell[2, j]);
			m.Cell[3, i] = m.Cell[3, i] - (s * m.Cell[3, j]);
		}

		/// <summary>
		/// Transposes a given matrix
		/// </summary>
		/// <param name="m">Matrix to transpose</param>
		/// <returns>Matrix4D</returns>
		private static Matrix4D transpose(Matrix4D m)
		{
			return new Matrix4D(
				m.Cell[0, 0], m.Cell[1, 0], m.Cell[2, 0], m.Cell[3, 0],
				m.Cell[0, 1], m.Cell[1, 1], m.Cell[2, 1], m.Cell[3, 1],
				m.Cell[0, 2], m.Cell[1, 2], m.Cell[2, 2], m.Cell[3, 2],
				m.Cell[0, 3], m.Cell[1, 3], m.Cell[2, 3], m.Cell[3, 3]);
		}

		/// <summary>
		/// Gets the submatrix of a, hiding row <para>ro</para> and column <para>co</para>
		/// </summary>
		/// <param name="a">Matrix</param>
		/// <param name="ro">Row to hide</param>
		/// <param name="co">Column to hide</param>
		/// <returns>Submatrix of a</returns>
		private static Matrix3D submat(Matrix4D a, int ro, int co)
		{
			Matrix3D c = new Matrix3D();
			int i = 0;
			for (int p = 0; p < 4; p++)
			{
				int j = 0;
				if (p != ro)
				{
					if (0 != co)
					{
						c.Cell[i, j] = a.Cell[p, 0];
						j += 1;
					} 
					else if (1 != co)
					{
						c.Cell[i, j] = a.Cell[p, 1];
						j += 1;
					}
					else if (2 != co)
					{
						c.Cell[i, j] = a.Cell[p, 2];
						j += 1;
					}
					else if (3 != co)
					{
						c.Cell[i, j] = a.Cell[p, 3];
					}						
					i += 1;
				}
			}

			return (c);
		}
		
		#endregion

	}
}