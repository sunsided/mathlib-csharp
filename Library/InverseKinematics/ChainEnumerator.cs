using System.Collections;
using System.Collections.Generic;

namespace MathLib.InverseKinematics
{
	/// <summary>
	/// Enumerates the bones in a kinematics chain
	/// </summary>
	public class ChainEnumerator : IEnumerator, IEnumerator<IBone>
	{
		public ChainEnumerator(Chain chain)
		{
			this.Chain = chain;
		}

		private Chain chain;


		protected Chain Chain
		{
			get { return chain; }
			set { chain = value; }
		}

		private int index = -1;
		
		protected int CurrentIndex
		{
			get { return index; }
			set { index = value; }
		}		
		
		#region IEnumerator Members
		
		public object Current
		{
			get { return Chain[index]; }
		}

		public bool MoveNext()
		{
			if( index < 0 )
			{
				index = 0;
				return true;
			}

			if (index >= Chain.Length)
			{
				return false;
			}

			index++;
			return true;
		}

		public void Reset()
		{
			index = -1;
		}

		#endregion

		#region IEnumerator<IBone> Members

		IBone IEnumerator<IBone>.Current
		{
			get { return Chain[index]; }
		}

		#endregion

		#region IDisposable Members

		public void Dispose()
		{
		}

		#endregion
	}
}
