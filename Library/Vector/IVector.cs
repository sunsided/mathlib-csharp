using System;

namespace MathLib.Vector
{
	/// <summary>
	/// Interface for generic vectors
	/// </summary>
	public interface IVector : ICloneable
	{
		/// <summary>
		/// The Number of dimensions
		/// </summary>
		int Dimensions { get; }

		/// <summary>
		/// The field array
		/// </summary>
		double[] Fields { get; }

		/// <summary>
		/// Returns the magnitude (length) of the vector
		/// </summary>
		/// <returns>double</returns>
		double Magnitude();

		/// <summary>
		/// Normalises the vector
		/// </summary>
		void Normalise();

		/// <summary>
		/// Assigns a vector
		/// </summary>
		/// <param name="vector">value to assign</param>
		void Assign(double[] vector);
	}
}