using System.Collections;
using System.Collections.Generic;

namespace Library.InverseKinematics
{
	public class Chain : IEnumerable
	{
		/// <summary>
		/// Internal list of bones
		/// </summary>
		private List<IBone> bones;

		/// <summary>
		/// The list of all bones
		/// </summary>
		protected List<IBone> BoneTable
		{
			get { return bones; }
			set { bones = value; }
		}
		
		/// <summary>
		/// Attaches a bone to the chain
		/// </summary>
		/// <param name="bone"></param>
		public void AttachBone( IBone bone )
		{
			bones.Add(bone);
		}

		/// <summary>
		/// Detaches the last bone from the chain
		/// </summary>
		public void DetachBone()
		{
			bones.RemoveAt(bones.Count - 1);
		}

		/// <summary>
		/// Gets the length of the cain, thus the number of bones attached
		/// </summary>
		public int Length
		{
			get { return BoneTable.Count;  }
		}
		
		public IBone this[int index]
		{
			get
			{
				return BoneTable[index];
			}
			set
			{
				BoneTable[index] = value;
			}
		}
		
		#region IEnumerable Members

		public IEnumerator GetEnumerator()
		{
			return new ChainEnumerator(this);
		}

		#endregion
	}
}
