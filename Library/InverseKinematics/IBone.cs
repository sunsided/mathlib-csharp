using MathLib.Vector;

namespace MathLib.InverseKinematics
{
	public interface IBone : IJoint
	{
		/// <summary>
		/// Gets or sets the origin
		/// </summary>
		Vector3D Origin { get; set; }

		/// <summary>
		/// Gets the Endpoint
		/// </summary>
		Vector3D GetEndpoint();

		/// <summary>
		/// The length of the bone
		/// </summary>
		double Length { get; set; }

		/// <summary>
		/// Gets the current direction of the bone
		/// </summary>
		Vector3D Direction { get; }

		/// <summary>
		/// Gets the basic direction of the bone
		/// </summary>
		Vector3D BaseDirection { get; set; }

		/// <summary>
		/// Rotates the bone towards the target point
		/// </summary>
		/// <param name="target">The target point</param>
		void RotateTowards(Vector3D target);
	}
}