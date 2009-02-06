using System;
using Library.Vector;

namespace Library.Matrix
{
	/// <summary>
	/// 3-dimensional row-major matrix
	/// </summary>
	public sealed class Matrix3D : BaseMatrix
	{
		/// <summary>
		/// Gets the unit matrix
		/// </summary>
		public static readonly Matrix3D Unit = new Matrix3D(
			1.0f, 0.0f, 0.0f,
			0.0f, 1.0f, 0.0f,
			0.0f, 0.0f, 1.0f);

		#region Konstruktor

		public Matrix3D()
		{
			Cell = new double[3, 3];
		}

		public Matrix3D(
			double M11, double M12, double M13,
			double M21, double M22, double M23,
			double M31, double M32, double M33)
			: this()
		{
			Cell[0, 0] = M11; Cell[0, 1] = M12; Cell[0, 2] = M13;
			Cell[1, 0] = M21; Cell[1, 1] = M22; Cell[1, 2] = M23;
			Cell[2, 0] = M31; Cell[2, 1] = M32; Cell[2, 2] = M33;
		}

		#endregion

		#region Matrix type functions

		/// <summary>
		/// Sets the matrix to an identity matrix
		/// </summary>
		public override void ToIdentity()
		{
			Cell[0, 0] = 1f; Cell[0, 1] = 0f; Cell[0, 2] = 0f;
			Cell[1, 0] = 0f; Cell[1, 1] = 1f; Cell[1, 2] = 0f;
			Cell[2, 0] = 0f; Cell[2, 1] = 0f; Cell[2, 2] = 1f;
		}

		/// <summary>
		/// Sets the matrix to a scale matrix
		/// </summary>
		/// <param name="factors">Vector of scaling factors</param>
		public virtual void ToScale(Vector3D factors)
		{
			Cell[0, 0] = factors.X; Cell[0, 1] = 0f;		Cell[0, 2] = 0f;	
			Cell[1, 0] = 0f;		Cell[1, 1] = factors.Y; Cell[1, 2] = 0f;	
			Cell[2, 0] = 0f;		Cell[2, 1] = 0f;		Cell[2, 2] = factors.Z;
		}

		/// <summary>
		/// Sets the matrix to a scale matrix
		/// </summary>
		/// <param name="x">X factor</param>
		/// <param name="y">Y factor</param>
		/// <param name="z">Z factor</param>
		public virtual void ToScale(double x, double y, double z)
		{
			Cell[0, 0] = x;  Cell[0, 1] = 0f; Cell[0, 2] = 0f;
			Cell[1, 0] = 0f; Cell[1, 1] = y;  Cell[1, 2] = 0f;
			Cell[2, 0] = 0f; Cell[2, 1] = 0f; Cell[2, 2] = z;
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
			Matrix3D mat = new Matrix3D(
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
			M11 = b.Cell[0, 0]; M12 = b.Cell[0, 1]; M13 = b.Cell[0, 2];
			M21 = b.Cell[1, 0]; M22 = b.Cell[1, 1]; M23 = b.Cell[1, 2];
			M11 = b.Cell[2, 0]; M32 = b.Cell[2, 1]; M33 = b.Cell[2, 2];
		}

		/// <summary>
		/// Assigns matrix values
		/// </summary>
		/// <param name="M11">M11</param>
		/// <param name="M12">M12</param>
		/// <param name="M13">M13</param>
		/// <param name="M21">M21</param>
		/// <param name="M22">M22</param>
		/// <param name="M23">M23</param>
		/// <param name="M31">M31</param>
		/// <param name="M32">M32</param>
		/// <param name="M33">M33</param>
		public void Assign(
			double M11, double M12, double M13,
			double M21, double M22, double M23,
			double M31, double M32, double M33)
		{
			Cell[0, 0] = M11; Cell[0, 1] = M12; Cell[0, 2] = M13;
			Cell[1, 0] = M21; Cell[1, 1] = M22; Cell[1, 2] = M23;
			Cell[2, 0] = M31; Cell[2, 1] = M32; Cell[2, 2] = M33;
		}		
		
		/// <summary>
		/// Assigns matrix values
		/// </summary>
		/// <param name="value">Value to fill in</param>
		public override void Fill(double value)
		{
			M11 = value; M12 = value; M13 = value;
			M21 = value; M22 = value; M23 = value;
			M11 = value; M32 = value; M33 = value;
		}


		#endregion

		#region Statics

		/// <summary>
		/// A 3D rotation matrix for X-axis rotation
		/// </summary>
		/// <example>
		public static Matrix3D GetRotationX(double theta)
		{
			double cos = (double)Math.Cos(theta);
			double sin = (double)Math.Sin(theta);			
			return new Matrix3D(
				1.0f, 0.0f, 0.0f,
				0.0f, cos, sin,
				0.0f, -sin, cos);
		}

		/// <summary>
		/// A 3D rotation matrix for Y-axis rotation
		/// </summary>
		public static Matrix3D GetRotationY(double theta)
		{
			double cos = (double)Math.Cos(theta);
			double sin = (double)Math.Sin(theta);
			return new Matrix3D(
				cos, 0.0f, -sin,
				0.0f, 1.0f, 0.0f,
				-sin, 0.0f, cos);
		}

		/// <summary>
		/// A 3D rotation matrix for Y-axis rotation
		/// </summary>
		public static Matrix3D GetRotationZ(double theta)
		{
			double cos = (double)Math.Cos(theta);
			double sin = (double)Math.Sin(theta);
			return new Matrix3D(
				cos, sin, 0.0f,
				-sin, cos, 0.0f,
				0.0f, 0.0f, 1.0f);
		}
		
		/// <summary>
		/// A 3D rotation matrix for axis-angle rotation
		/// </summary>
		public static Matrix3D GetRotationAxisAngle(Vector3D axis, double theta)
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
			return new Matrix3D(
				xx * (1 - cos) + cos, xy * (1 - cos) + zsin, xz * (1 - cos) - ysin, 0.0f,
				xy * (1 - cos) - zsin, yy * (1 - cos) + cos, yz * (1 - cos) + xsin, 0.0f,
				xz * (1 - cos) + ysin, yz * (1 - cos) + xsin, zz * (1 - cos) + cos, 0.0f,
				0.0f, 0.0f, 0.0f, 1.0f);
			*/

			return new Matrix3D(
				xx - axis.X * xcos + cos, xy - axis.Y * xcos + zsin, xz - axis.Y * xcos - ysin,
				xy - axis.Y * xcos - zsin, yy - axis.Y * ycos + cos, yz - axis.Z * ycos + xsin,
				xz - axis.Z * xcos + ysin, yz - axis.Z * ycos + xsin, zz - zz * cos + cos);
		}

		/// <summary>
		/// Gets a progressive rotation matrix based on angular speed
		/// </summary>
		public static Matrix3D GetProgressiveRotation(double deltaX, double deltaY, double deltaZ)
		{
			return new Matrix3D(
				0.0f, -deltaZ, deltaY,
				deltaZ, 0.0f, -deltaX,
				-deltaY, deltaX, 0.0f);
		}
		
		#endregion

		#region Extended Mathematics

		/// <summary>
		/// Subtracts column <para>j</para>*<para>s</para> from column <para>i</para>
		/// </summary>
		/// <param name="i">The column from which to subtract</param>
		/// <param name="j">The column to subtract</param>
		/// <param name="s">The scaling factor of column j</param>
		private static void colsub(Matrix3D m, int i, int j, double s)
		{
			m.Cell[0, i] = m.Cell[0, i] - (s * m.Cell[0, j]);
			m.Cell[1, i] = m.Cell[1, i] - (s * m.Cell[1, j]);
			m.Cell[2, i] = m.Cell[2, i] - (s * m.Cell[2, j]);
		}

		/// <summary>
		/// Transposes a given matrix
		/// </summary>
		/// <param name="m">Matrix to transpose</param>
		/// <returns>Matrix3D</returns>
		private static Matrix3D transpose(Matrix3D m)
		{
			return new Matrix3D(
				m.Cell[0, 0], m.Cell[1, 0], m.Cell[2, 0],
				m.Cell[0, 1], m.Cell[1, 1], m.Cell[2, 1],
				m.Cell[0, 2], m.Cell[1, 2], m.Cell[2, 2]);
		}
		
		#endregion

	}
}