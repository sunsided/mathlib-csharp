using System;
using System.Windows.Forms;

namespace MathLib.Tests.Visual.Intersection.LineLineIntersection2D
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
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MathLib.Tests.Visual.Intersection.LineLineIntersection2D.LineLineIntersection2D());
        }
    }
}