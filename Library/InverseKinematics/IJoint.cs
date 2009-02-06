using Library.Vector;

namespace Library.InverseKinematics
{
	public interface IJoint
	{
		/// <summary>
		/// The joint's angle.
		/// </summary>
		double UpAngle { get; }

		/// <summary>
		/// The joint's sidewards angle.
		/// </summary>
		double SideAngle { get; }

		/// <summary>
		/// The joint's torque.
		/// </summary>
		double Torque { get; }

		/// <summary>
		/// The joint's minimum angle.
		/// </summary>
		double UpAngleMin { get; set; }

		/// <summary>
		/// The joint's maximum angle.
		/// </summary>
		double UpAngleMax { get; set; }

		/// <summary>
		/// The joint's minimum side angle.
		/// </summary>
		double SideAngleMin { get; set; }

		/// <summary>
		/// The joint's maximum side angle.
		/// </summary>
		double SideAngleMax { get; set; }

		/// <summary>
		/// The joint's minimum torque.
		/// </summary>
		double TorqueMin { get; set; }

		/// <summary>
		/// The joint's maximum torque.
		/// </summary>
		double TorqueMax { get; set; }

		/// <summary>
		/// Rotates the joint
		/// </summary>
		/// <param name="up">Angle around the joint's X axis</param>
		/// <param name="side">Angle around the joint's Y axis</param>
		/// <param name="torque">Angle around the joint's Z axis</param>
		/// <returns></returns>
		void Rotate(double up, double side, double torque);

		/// <summary>
		/// Gets the direction vector
		/// Zero degrees in every direction returns an vector equal to the X axis
		/// </summary>
		/// <returns></returns>
		Vector3D GetDirectionVector();
	}
}