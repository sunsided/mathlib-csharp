namespace Library.InverseKinematics
{
	public interface IJoint
	{
		/// <summary>
		/// The joint's angle.
		/// </summary>
		float UpAngle { get; }

		/// <summary>
		/// The joint's sidewards angle.
		/// </summary>
		float SideAngle { get; }

		/// <summary>
		/// The joint's torque.
		/// </summary>
		float Torque { get; }

		/// <summary>
		/// The joint's minimum angle.
		/// </summary>
		float UpAngleMin { get; set; }

		/// <summary>
		/// The joint's maximum angle.
		/// </summary>
		float UpAngleMax { get; set; }

		/// <summary>
		/// The joint's minimum side angle.
		/// </summary>
		float SideAngleMin { get; set; }

		/// <summary>
		/// The joint's maximum side angle.
		/// </summary>
		float SideAngleMax { get; set; }

		/// <summary>
		/// The joint's minimum torque.
		/// </summary>
		float TorqueMin { get; set; }

		/// <summary>
		/// The joint's maximum torque.
		/// </summary>
		float TorqueMax { get; set; }

		/// <summary>
		/// Rotates the joint
		/// </summary>
		/// <param name="up">Angle around the joint's X axis</param>
		/// <param name="side">Angle around the joint's Y axis</param>
		/// <param name="torque">Angle around the joint's Z axis</param>
		/// <returns></returns>
		void Rotate(float up, float side, float torque);

		/// <summary>
		/// Gets the direction vector
		/// Zero degrees in every direction returns an vector equal to the X axis
		/// </summary>
		/// <returns></returns>
		Vector3D GetDirectionVector();
	}
}