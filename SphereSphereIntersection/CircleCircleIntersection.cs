using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Library;
using Library.Vector;

namespace CircleCircleIntersection
{
	public partial class CircleCircleIntersection : Form
	{
		public CircleCircleIntersection()
		{
			InitializeComponent();

			// Hook on panel to paint
			panelCanvas.Paint += new PaintEventHandler(panelCanvas_Paint);
			panelCanvas.MouseMove += new MouseEventHandler(panelCanvas_MouseMove);

			// Create fix sphere
			fixSphere = new Sphere3D(new Vector3D(100f, panelCanvas.ClientRectangle.Height - 100f, 0f), 50f);

			// Test Sphere
			testSphere.Radius = 40f;
		}

		#region Points

		protected Sphere3D fixSphere;
		protected Sphere3D testSphere;

		#endregion

		#region Draw a sphere

		protected void DrawSphere(Sphere3D sphere, Graphics graphics, Color color)
		{
			// Create point bounds
			RectangleF pointBounds = new RectangleF((float)(sphere.Center.X - sphere.Radius), (float)(sphere.Center.Y - sphere.Radius), (float)(2.0D * sphere.Radius), (float)(2.0D * sphere.Radius));

			// Create Pen
			Brush brush = new SolidBrush(color);

			// Draw point
			graphics.FillEllipse(brush, pointBounds);
		}

		#endregion

		#region Drawing Hook

		void panelCanvas_Paint(object sender, PaintEventArgs e)
		{
			// Set quality mode
			e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

			//// Set color based on distance
			float distanceBlue = (float)Math.Round(fixSphere.GetDistance(testSphere), 0);

			// Draw fix sphere
			DrawSphere(fixSphere, e.Graphics, Color.CornflowerBlue);

			// Post distance
			labelDistBlue.Text = distanceBlue.ToString();

			// Draw Test Sphere
			Color color = (distanceBlue == 0f) ? Color.FromArgb(200, Color.LightGreen) : Color.Crimson;
			DrawSphere(testSphere, e.Graphics, color);
		}

		#endregion

		#region Mouse Movement

		void panelCanvas_MouseMove(object sender, MouseEventArgs e)
		{
			// Set the new point position
			testSphere.Center = new Vector3D((float)e.X, (float)e.Y, 0f);

			// Redraw panel
			panelCanvas.Invalidate();
		}		

		#endregion
	}
}