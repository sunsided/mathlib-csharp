using System;
using Library;
using Library.Vector;

namespace Library.InverseKinematics
{
    public class Bone : IBone
    {
		/// <summary>
		/// Creates a Bone
		/// </summary>
		/// <param name="origin">The bone's origin</param>
		/// <param name="direction">The bone's direction</param>
		/// <param name="length">The bone's length</param>
		/// <param name="upMin">Minimum upwards angle</param>
		/// <param name="upMax">Maximum upwards angle</param>
		/// <param name="sideMin">Minimum sidewards angle</param>
		/// <param name="sideMax">Maximum sidewards angle</param>
		/// <param name="torqueMin">Minimum torque</param>
		/// <param name="torqueMax">Maximum torque</param>
		public Bone( Vector3D origin, Vector3D direction, double length, double upMin, double upMax, double sideMin, double sideMax, double torqueMin, double torqueMax)
		{
			joint = new Joint(upMin, upMax, sideMin, sideMax, torqueMin, torqueMax);
			this.length = length;
			this.origin = origin;
			direction_cached = base_direction = direction;
		}

		/// <summary>
		/// Creates a Bone
		/// </summary>
		/// <param name="origin">The bone's origin</param>
		/// <param name="direction">The bone's direction</param>
		/// <param name="length">The bone's length</param>
		public Bone(Vector3D origin, Vector3D direction, double length)
		{
			double angle = 0.5f * (double) Math.PI;
			joint = new Joint(-angle, angle, -angle, angle, -angle, angle);
			this.length = length;
			this.origin = origin;
			direction_cached = base_direction = direction;
		}    	

		/// <summary>
		/// Creates a Bone
		/// </summary>
		/// <param name="origin">The bone's origin</param>
		/// <param name="endpoint">The bone's endpoint</param>
		/// <param name="upMin">Minimum upwards angle</param>
		/// <param name="upMax">Maximum upwards angle</param>
		/// <param name="sideMin">Minimum sidewards angle</param>
		/// <param name="sideMax">Maximum sidewards angle</param>
		/// <param name="torqueMin">Minimum torque</param>
		/// <param name="torqueMax">Maximum torque</param>
		public Bone(Vector3D origin, Vector3D endpoint, double upMin, double upMax, double sideMin, double sideMax, double torqueMin, double torqueMax)
		{
			joint = new Joint(upMin, upMax, sideMin, sideMax, torqueMin, torqueMax);
			this.origin = origin;
			base_direction = endpoint - origin;
			length = base_direction.Magnitude();
			base_direction.Normalise();
			direction_cached = base_direction;
		}

		/// <summary>
		/// Creates a Bone
		/// </summary>
		/// <param name="origin">The bone's origin</param>
		/// <param name="endpoint">The bone's endpoint</param>
		public Bone(Vector3D origin, Vector3D endpoint)
		{
			double angle = 0.5f * (double)Math.PI;
			joint = new Joint(-angle, angle, -angle, angle, -angle, angle);
			this.origin = origin;
			base_direction = endpoint - origin;
			length = base_direction.Magnitude();
			base_direction.Normalise();
			direction_cached = base_direction;
		}    	
    	
		#region Values

    	/// <summary>
    	/// The bone's joint
    	/// </summary>
		protected Joint joint;
    	
		/// <summary>
		/// Gets the origin
		/// </summary>
		protected Vector3D origin;

		/// <summary>
		/// Gets the basic direction of the bone
		/// </summary>
		protected Vector3D base_direction;

		/// <summary>
		/// The cached direction vector
		/// </summary>
		protected Vector3D direction_cached;
    	
		/// <summary>
		/// Gets the Endpoint
		/// </summary>
		protected double length;

		#endregion

		#region Properties

		/// <summary>
    	/// Gets or sets the origin
    	/// </summary>
    	public Vector3D Origin
    	{
    		get
    		{
				return origin;
    		}
    		set
    		{
				origin = value;
    		}
    	}
    	
    	/// <summary>
    	/// Gets the Endpoint
    	/// </summary>
		public Vector3D GetEndpoint()
		{
			return origin + Direction * length;
		}

		/// <summary>
		/// The length of the bone
		/// </summary>
    	public double Length
    	{
    		get { return length; }
    		set { length = value; }
    	}

    	/// <summary>
    	/// Gets the current direction of the bone
    	/// </summary>
    	public Vector3D Direction
    	{
    		get { return direction_cached; }
		}

		/// <summary>
		/// Gets the basic direction of the bone
		/// </summary>
		public Vector3D BaseDirection
		{
			get { return base_direction; }
			set { base_direction = value; }
		}    	
    	
		#endregion

		#region Casts

    	/// <summary>
    	/// Cast to LineSegment3D
    	/// </summary>
    	/// <param name="bone"></param>
    	/// <returns></returns>
    	public static explicit operator LineSegment3D(Bone bone)
    	{
			return new LineSegment3D(bone.Origin, bone.GetEndpoint());
    	}
    	
		#endregion
    	
    	/// <summary>
    	/// Rotates the bone towards the target point
    	/// </summary>
    	/// <param name="target">The target point</param>
    	public void RotateTowards( Vector3D target )
    	{
			throw new NotImplementedException();
    	}

		#region IJoint Members

		public double UpAngle
		{
			get { return joint.UpAngle; }
		}

		public double SideAngle
		{
			get { return joint.SideAngle; }
		}

		public double Torque
		{
			get { return joint.Torque; }
		}

		public double UpAngleMin
		{
			get
			{
				return joint.UpAngleMin;
			}
			set
			{
				joint.UpAngleMin = value;
			}
		}

		public double UpAngleMax
		{
			get
			{
				return joint.UpAngleMax;
			}
			set
			{
				joint.UpAngleMax = value;
			}
		}

		public double SideAngleMin
		{
			get
			{
				return joint.SideAngleMin;
			}
			set
			{
				joint.TorqueMin = value;
			}
		}

		public double SideAngleMax
		{
			get
			{
				return joint.SideAngleMax;
			}
			set
			{
				joint.SideAngleMax = value;
			}
		}

		public double TorqueMin
		{
			get
			{
				return joint.TorqueMin;
			}
			set
			{
				joint.TorqueMin = value;
			}
		}

		public double TorqueMax
		{
			get
			{
				return joint.TorqueMax;
			}
			set
			{
				joint.TorqueMax = value;
			}
		}

		/// <summary>
		/// Rotates the joint
		/// </summary>
		/// <param name="up">Angle around the joint's X axis</param>
		/// <param name="side">Angle around the joint's Y axis</param>
		/// <param name="torque">Angle around the joint's Z axis</param>
		/// <returns></returns>
		public void Rotate(double up, double side, double torque)
		{
			joint.Rotate(up, side, torque);
			direction_cached = joint.GetDirectionVector(base_direction);
		}

		public Vector3D GetDirectionVector()
		{
			return direction_cached;
		}

		#endregion

		public override string ToString()
		{
			return string.Format("{{{0}; {1}}}", Origin.ToString(), GetEndpoint().ToString());
		}
	}
}
