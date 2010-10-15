using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LinePointIntersection
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.SetCompatibleTextRenderingDefault(true);
			Application.EnableVisualStyles();
			Application.Run(new LinePointIntersection());
		}
	}
}