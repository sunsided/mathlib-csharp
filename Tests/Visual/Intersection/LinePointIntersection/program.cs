using System;
using System.Windows.Forms;

namespace MathLib.Tests.Visual.Intersection.LinePointIntersection
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
			Application.Run(new global::MathLib.Tests.Visual.Intersection.LinePointIntersection.LinePointIntersection());
		}
	}
}