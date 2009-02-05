using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Library
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
