namespace Library.Matrix
{
	/// <summary>
	/// A matrix base class
	/// </summary>
	public abstract class BaseMatrix
	{
		/// <summary>
		/// The cell array
		/// </summary>
		internal double[,] Cell;

		/// <summary>
		/// Sets the matrix to an identity matrix
		/// </summary>
		public abstract void ToIdentity();

		/// <summary>
		/// Assigns matrix values
		/// </summary>
		/// <param name="value">Value to fill in</param>
		public abstract void Fill(double value);
	}
}