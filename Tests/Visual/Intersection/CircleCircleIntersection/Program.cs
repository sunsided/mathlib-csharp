using System;
using System.Windows.Forms;

namespace MathLib.Tests.Visual.Intersection.CircleCircleIntersection
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.Run(new global::MathLib.Tests.Visual.Intersection.CircleCircleIntersection.CircleCircleIntersection());
		}
	}
}