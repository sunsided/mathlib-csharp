namespace Library
{
	public abstract class BasicMatrix
	{
		internal float[,] Cell;

		/// <summary>
		/// Sets the matrix to an identity matrix
		/// </summary>
		public abstract void ToIdentity();

		/// <summary>
		/// Assigns matrix values
		/// </summary>
		/// <param name="value">Value to fill in</param>
		public abstract void Fill(float value);
	}
}