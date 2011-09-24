using System.Windows.Forms;

namespace MathLib.Visual
{
	public class FlickerFreePanel : Panel
	{
		public FlickerFreePanel()
			: base()
		{
			SetStyle(	ControlStyles.OptimizedDoubleBuffer | 
						ControlStyles.AllPaintingInWmPaint | 
						ControlStyles.UserPaint, true);
		}
	}
}
