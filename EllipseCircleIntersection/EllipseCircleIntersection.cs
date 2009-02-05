using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Library;

namespace EllipseCircleIntersection
{
	public partial class EllipseCircleIntersection : Form
	{
		public EllipseCircleIntersection()
		{
			InitializeComponent();

			// Hook on panel to paint
			panelCanvas.Paint += new PaintEventHandler(panelCanvas_Paint);
			panelCanvas.MouseMove += new MouseEventHandler(panelCanvas_MouseMove);

			// Create fix sphere
			Vector3D center = new Vector3D(120f, panelCanvas.ClientRectangle.Height - 70f, 0f);
			Vector3D radius = new Vector3D(100f, 50f, 0.1f);
			ellipse = new Ellipse3D(center, radius);

			// Test Sphere
			testSphere.Radius = 40f;
		}

		#region Points

		protected Ellipse3D ellipse;
		protected Sphere3D testSphere;

		#endregion

		#region Draw an ellipse

		protected void DrawEllipse(Ellipse3D ellipse, Graphics graphics, Color color)
		{
			// Create point bounds
			RectangleF pointBounds = new RectangleF(ellipse.Center.X - ellipse.Radius.X, ellipse.Center.Y - ellipse.Radius.Y, 2f * ellipse.Radius.X, 2f * ellipse.Radius.Y);

			// Create Pen
			Brush brush = new SolidBrush(color);

			// Draw point
			graphics.FillEllipse(brush, pointBounds);
		}

		#endregion

		#region Draw a sphere

		protected void DrawSphere(Sphere3D sphere, Graphics graphics, Color color)
		{
			// Create point bounds
			RectangleF pointBounds = new RectangleF(sphere.Center.X - sphere.Radius, sphere.Center.Y - sphere.Radius, 2f * sphere.Radius, 2f * sphere.Radius);

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
			// float distanceBlue = (float)Math.Round(ellipse.GetDistance(testSphere), 0);

			// Draw fix sphere
			DrawEllipse(ellipse, e.Graphics, Color.CornflowerBlue);

			// Post distance
			// labelDistBlue.Text = distanceBlue.ToString();

			// Draw Test Sphere
			//Color color = (distanceBlue == 0f) ? Color.FromArgb(200, Color.LightGreen) : Color.Crimson;
			Color color = ellipse.Contains(testSphere.Center) ? Color.FromArgb(200, Color.LightGreen) : Color.Crimson;
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