using MathLib.Vector;

namespace MathLib
{
	/// <summary>
	/// Interpolation methods
	/// </summary>
	public static class Interpolation
    {
        #region 3D Interpolation

        /// <summary>
		/// Performs a three-dimensional Catmull-Rom spline interpolation through four points
		/// <para>All points will be traversed.</para>
		/// </summary>
		/// <param name="p0">Start point</param>
		/// <param name="p1">First vector to move through</param>
		/// <param name="p2">Second vector to move through</param>
		/// <param name="p3">End point</param>
		/// <param name="t">positional weighting factor</param>
		/// <returns>Interpolated vector</returns>
		public static Vector3D CatmullRom(Vector3D p0, Vector3D p1, Vector3D p2, Vector3D p3, double t)
		{
			double t2 = t * t, t3 = t2 * t;
			Vector3D ret = 0.5f * ((2f * p1) +
							(-p0 + p2) * t +
							(2f * p0 - 5f * p1 + 4f * p2 - p3) * t2 +
							(-p0 + 3f * p1 - 3f * p2 + p3) * t3);
			return ret;
		}

		/// <summary>
		/// Performs a three-dimensional cubic Beziér spline interpolation through four points.
		/// <para>Only the first and last vector will be directly reached. The other two vectors <paramref name="p1"/> and <paramref name="p2"/> serve as
		/// weighting vectors for the interpolation.</para>
		/// </summary>
		/// <param name="p0">Start point</param>
		/// <param name="p1">First weighting vector</param>
		/// <param name="p2">Second weighting vector</param>
		/// <param name="p3">End point</param>
		/// <param name="t">positional weighting factor</param>
		/// <returns>Interpolated vector</returns>
		public static Vector3D CubicBezier(Vector3D p0, Vector3D p1, Vector3D p2, Vector3D p3, double t)
		{
			double t2 = t * t, t3 = t2 * t;
			double b = 1f - t, b2 = b * b, b3 = b2 * b;
			return (p0 * b3 + 3f * p1 * b2 * t + 3f * p2 * b * t2 + p3 * t3);
        }

        #endregion

        #region 2D Interpolation

        /// <summary>
        /// Performs a two-dimensional Catmull-Rom spline interpolation through four points
		/// <para>All points will be traversed.</para>
        /// </summary>
		/// <param name="p0">Start point</param>
		/// <param name="p1">First vector to move through</param>
		/// <param name="p2">Second vector to move through</param>
		/// <param name="p3">End point</param>
        /// <param name="t">positional weighting factor</param>
        /// <returns>Interpolated vector</returns>
        public static Vector2D CatmullRom(Vector2D p0, Vector2D p1, Vector2D p2, Vector2D p3, double t)
        {
            double t2 = t * t, t3 = t2 * t;
            Vector2D ret = 0.5f * ((2f * p1) +
                            (-p0 + p2) * t +
                            (2f * p0 - 5f * p1 + 4f * p2 - p3) * t2 +
                            (-p0 + 3f * p1 - 3f * p2 + p3) * t3);
            return ret;
        }

        /// <summary>
        /// Performs a two-dimensional cubic Beziér spline interpolation through four points
		/// <para>Only the first and last vector will be directly reached. The other two vectors <paramref name="p1"/> and <paramref name="p2"/> serve as
		/// weighting vectors for the interpolation.</para>
        /// </summary>
		/// <param name="p0">Start point</param>
		/// <param name="p1">First weighting vector</param>
		/// <param name="p2">Second weighting vector</param>
		/// <param name="p3">End point</param>
        /// <param name="t">positional weighting factor</param>
        /// <returns>Interpolated vector</returns>
        public static Vector2D CubicBezier(Vector2D p0, Vector2D p1, Vector2D p2, Vector2D p3, double t)
        {
            double t2 = t * t, t3 = t2 * t;
            double b = 1f - t, b2 = b * b, b3 = b2 * b;
            return (p0 * b3 + 3f * p1 * b2 * t + 3f * p2 * b * t2 + p3 * t3);
        }

        #endregion
    }
}
