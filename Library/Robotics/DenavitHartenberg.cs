// $Id$

using System;
using Library.Matrix;

namespace Library.Robotics
{
	/// <summary>
	/// Denavit-Hartenberg kinematics
	/// </summary>
	public static class DenavitHartenberg
	{
		/// <summary>
		/// Creates a forward transformation matrix from frame <code>n-1</code> to frame <code>n</code>,
		/// based on the Denavit-Hartenberg link parameters.
		/// </summary>
		/// <param name="a">Length of the common normal (AKA r)</param>
		/// <param name="d">Offset along previous z to the common normal</param>
		/// <param name="alpha">Angle about common normal, from old z axis to new z axis</param>
		/// <param name="theta">Angle about previous z, from old x to new x</param>
		/// <returns>The DH transformation matrix</returns>
		/// <remarks>http://en.wikipedia.org/wiki/Denavit-Hartenberg_Parameters</remarks>
		public static Matrix4D ForwardTransformation(double a, double d, double alpha, double theta)
		{
			Matrix4D matrix = new Matrix4D().ToDenavitHartenberg(a, d, alpha, theta);
			return matrix;
		}

		/// <summary>
		/// Creates a forward transformation matrix from frame <code>n-1</code> to frame <code>n</code>,
		/// based on the Denavit-Hartenberg link parameters.
		/// </summary>
		/// <param name="matrix">The matrix</param>
		/// <param name="a">Length of the common normal (AKA r)</param>
		/// <param name="d">Offset along previous z to the common normal</param>
		/// <param name="alpha">Angle about common normal, from old z axis to new z axis</param>
		/// <param name="theta">Angle about previous z, from old x to new x</param>
		/// <returns>The DH transformation matrix</returns>
		/// <remarks>http://en.wikipedia.org/wiki/Denavit-Hartenberg_Parameters</remarks>
		public static Matrix4D ToDenavitHartenberg(this Matrix4D matrix, double a, double d, double alpha, double theta)
		{
			if (ReferenceEquals(null, matrix)) throw new ArgumentNullException("matrix");

			double cosT = Math.Cos(theta);
			double sinT = Math.Sin(theta);
			double cosA = Math.Cos(alpha);
			double sinA = Math.Sin(alpha);

			matrix.Assign(
				cosT, -sinT * cosA, sinT * sinA, a * cosT,
				sinT, cosT * cosA, -cosT * sinA, a * sinT,
				0, sinA, cosA, d,
				0, 0, 0, 1
				);

			return matrix;
		}
	}
}
