using MathLib.Vector;

namespace MathLib
{
	public static class Interpolation
    {
        #region 3D Interpolation

        /// <summary>
		/// Performs a Catmull-Rom Spline interpolation through four points
		/// </summary>
		/// <param name="p0">p0</param>
		/// <param name="p1">p1</param>
		/// <param name="p2">p2</param>
		/// <param name="p3">p3</param>
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
		/// Performs a Cubic Beziér Spline interpolation through four points
		/// </summary>
		/// <param name="p0">p0</param>
		/// <param name="p1">p1</param>
		/// <param name="p2">p2</param>
		/// <param name="p3">p3</param>
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
        /// Performs a Catmull-Rom Spline interpolation through four points
        /// </summary>
        /// <param name="p0">p0</param>
        /// <param name="p1">p1</param>
        /// <param name="p2">p2</param>
        /// <param name="p3">p3</param>
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
        /// Performs a Cubic Beziér Spline interpolation through four points
        /// </summary>
        /// <param name="p0">p0</param>
        /// <param name="p1">p1</param>
        /// <param name="p2">p2</param>
        /// <param name="p3">p3</param>
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
