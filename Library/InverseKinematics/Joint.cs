using System;
using System.Collections.Generic;
using System.Text;
using Library.Vector;

namespace Library.InverseKinematics
{
	/// <summary>
	/// Joint for an IK system
	/// </summary>
	public class Joint : IJoint
	{
		public Joint()
		{}
		
		/// <summary>
		/// Creates a joint
		/// </summary>
		/// <param name="upMin">Minimum upwards angle</param>
		/// <param name="upMax">Maximum upwards angle</param>
		/// <param name="sideMin">Minimum sidewards angle</param>
		/// <param name="sideMax">Maximum sidewards angle</param>
		/// <param name="torqueMin">Minimum torque</param>
		/// <param name="torqueMax">Maximum torque</param>
		public Joint( double upMin, double upMax, double sideMin, double sideMax, double torqueMin, double torqueMax)
		{
			upAngleMin = upMin;
			upAngleMax = upMax;
			sideAngleMin = sideMin;
			sideAngleMax = sideMax;
			this.torqueMin = torqueMin;
			this.torqueMax = torqueMax;
		}

		#region Values

		/// <summary>
		/// The joint's angle.
		/// </summary>
		protected double upAngle;

		/// <summary>
		/// The joint's minimum angle.
		/// </summary>
		protected double upAngleMin;

		/// <summary>
		/// The joint's maximum angle.
		/// </summary>
		protected double upAngleMax;
		
		/// <summary>
		/// The joint's sidewards angle.
		/// </summary>
		protected double sideAngle;

		/// <summary>
		/// The joint's minimum side angle.
		/// </summary>
		protected double sideAngleMin;

		/// <summary>
		/// The joint's maximum side angle.
		/// </summary>
		protected double sideAngleMax;
		
		/// <summary>
		/// The joint's torque.
		/// </summary>
		protected double torque;

		/// <summary>
		/// The joint's minimum torque.
		/// </summary>
		protected double torqueMin;

		/// <summary>
		/// The joint's maximum torque.
		/// </summary>
		protected double torqueMax;

		#endregion 
		
		#region Properties

		/// <summary>
		/// The joint's angle.
		/// </summary>
		public double UpAngle
		{
			get { return upAngle; }
		}

		/// <summary>
		/// The joint's sidewards angle.
		/// </summary>
		public double SideAngle
		{
			get { return sideAngle; }
		}

		/// <summary>
		/// The joint's torque.
		/// </summary>
		public double Torque
		{
			get { return torque; }
		}

		/// <summary>
		/// The joint's minimum angle.
		/// </summary>
		public double UpAngleMin
		{
			get { return upAngleMin; }
			set { upAngleMin = value; }
		}

		/// <summary>
		/// The joint's maximum angle.
		/// </summary>
		public double UpAngleMax
		{
			get { return upAngleMax; }
			set { upAngleMax = value; }
		}

		/// <summary>
		/// The joint's minimum side angle.
		/// </summary>
		public double SideAngleMin
		{
			get { return sideAngleMin; }
			set { sideAngleMin = value; }
		}

		/// <summary>
		/// The joint's maximum side angle.
		/// </summary>
		public double SideAngleMax
		{
			get { return sideAngleMax; }
			set { sideAngleMax = value; }
		}

		/// <summary>
		/// The joint's minimum torque.
		/// </summary>
		public double TorqueMin
		{
			get { return torqueMin; }
			set { torqueMin = value; }
		}

		/// <summary>
		/// The joint's maximum torque.
		/// </summary>
		public double TorqueMax
		{
			get { return torqueMax; }
			set { torqueMax = value; }
		}

		#endregion

		/// <summary>
		/// Rotates the joint
		/// </summary>
		/// <param name="yaw">Angle around the joint's X (up) axis</param>
		/// <param name="pitch">Angle around the joint's Y (side) axis</param>
		/// <param name="roll">Angle around the joint's Z (torque) axis</param>
		/// <returns></returns>
		public void Rotate( double yaw, double pitch, double roll )
		{
			upAngle += yaw;
			sideAngle += pitch;
			this.torque += roll;
			
			// Check
			upAngle = 
				upAngle > upAngleMax ? upAngleMax :
				upAngle < upAngleMin ? upAngleMin : 
				upAngle;
			sideAngle =
				sideAngle > sideAngleMax ? sideAngleMax :
				sideAngle < sideAngleMin ? sideAngleMin :
				sideAngle;
			this.torque =
				this.torque > torqueMax ? torqueMax :
				this.torque < torqueMin ? torqueMin :
				this.torque;
		}
		
		/// <summary>
		/// Gets the direction vector
		/// Zero degrees in every direction returns an vector equal to the X axis
		/// </summary>
		/// <returns></returns>
		public Vector3D GetDirectionVector()
		{
			Vector3D vec = Vector3D.AxisX;
			vec.RotateX(upAngle);
			vec.RotateY(sideAngle);
			vec.RotateZ(torque);
			vec.Normalise();
			return vec;
		}

		/// <summary>
		/// Gets the direction vector of the joint, modulated to a given vector
		/// Zero degrees in every direction returns an vector equal to the X axis
		/// </summary>
		/// <returns></returns>
		public Vector3D GetDirectionVector(Vector3D baseDirection)
		{
			Vector3D temp = new Vector3D(baseDirection);
			temp.RotateX(upAngle);
			temp.RotateY(sideAngle);
			temp.RotateZ(torque);
			temp.Normalise();
			return temp;
		}
	}
}
