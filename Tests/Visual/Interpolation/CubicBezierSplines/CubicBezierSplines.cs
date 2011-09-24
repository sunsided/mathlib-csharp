using System;
using System.Drawing;
using System.Windows.Forms;
using MathLib.Vector;

namespace MathLib.Tests.Visual.Interpolation.CubicBezierSplines
{
	public partial class CubicBezierSplines : Form
	{
		public CubicBezierSplines()
		{
			InitializeComponent();

			// Hook on panel to paint
			panelCanvas.Paint += new PaintEventHandler(panelCanvas_Paint);

			// Create points
			pt1 = new Vector3D(10f, panelCanvas.ClientRectangle.Height * 0.5f, 0f);
			pt2 = new Vector3D(panelCanvas.ClientRectangle.Width * 0.3f, panelCanvas.ClientRectangle.Height * 0.5f, 0f);
			pt3 = new Vector3D(panelCanvas.ClientRectangle.Width * 0.7f, panelCanvas.ClientRectangle.Height * 0.5f, 0f);
			pt4 = new Vector3D(panelCanvas.ClientRectangle.Width - 10f, panelCanvas.ClientRectangle.Height * 0.5f, 0f);
		}

		#region Points

		protected Vector3D pt1, pt2, pt3, pt4;

		#endregion

		#region Draw a point

		protected void DrawPoint(Vector3D point, Graphics graphics, Color color)
		{
			// Set Point size
			const float width = 6f;

			// Create point bounds
			RectangleF pointBounds = new RectangleF((float)point.X - 0.5f * width, (float)point.Y - 0.5f * width, width, width);

			// Create Pen
			Brush brush = new SolidBrush(color);

			// Draw point
			graphics.FillEllipse(brush, pointBounds);
		}

		#endregion

		#region Draw a line segment

		protected void DrawLineSegment(Vector3D point1, Vector3D point2, Graphics graphics, Color color, float width)
		{
			// Create Pen
			Pen pen = new Pen(color, width);

			// Create Drawing.Points
			PointF pt1 = new PointF((float)point1.X, (float)point1.Y);
			PointF pt2 = new PointF((float)point2.X, (float)point2.Y);

			// Draw point
			graphics.DrawLine(pen, pt1, pt2);
		}

		#endregion

		#region Draw Spline Segment

		protected void DrawSplineSegment(Vector3D point0, Vector3D point1, Vector3D point2, Vector3D point3, Graphics graphics, Color color, float width)
		{
			// Draw Points in segment pt2..pt3			
			Vector3D[] arr = new Vector3D[50];
			arr[0] = point0;
			for (int i = 1; i < arr.Length; ++i)
			{
				// Berechnen
				arr[i] = MathLib.Interpolation.CubicBezier(point0, point1, point2, point3, (float)i / arr.Length);
				// Zeigen
				if( i > 1 ) DrawLineSegment(arr[i-1], arr[i], graphics, color, width);
			}
		}

		#endregion

		#region Drawing Hook

		void panelCanvas_Paint(object sender, PaintEventArgs e)
		{
			// Set quality mode
			e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

			// Draw Spline Segments
			DrawLineSegment(pt1, pt2, e.Graphics, Color.Goldenrod, 2f);
			DrawSplineSegment(pt1, pt2, pt3, pt4, e.Graphics, Color.Crimson, 2f);
			DrawLineSegment(pt3, pt4, e.Graphics, Color.Goldenrod, 2f);

			// Draw Control Points
			DrawPoint(pt1, e.Graphics, Color.CornflowerBlue);
			DrawPoint(pt2, e.Graphics, Color.CornflowerBlue);
			DrawPoint(pt3, e.Graphics, Color.CornflowerBlue);
			DrawPoint(pt4, e.Graphics, Color.CornflowerBlue);
		}

		#endregion

		private static float time = 0.0f;

		private void timerSinus_Tick(object sender, EventArgs e)
		{
			float sin = (float)Math.Sin(time);
			float cos = (float)Math.Cos(time);

			float cos2 = (float)Math.Sin(time * 2f);
			float sin2 = -(float)Math.Sin(time * 2);

			// Create points	
			pt1.Y = panelCanvas.ClientRectangle.Height * 0.5f + 30f * sin;
			pt2.Y = panelCanvas.ClientRectangle.Height * 0.5f + 70f * cos2;
			pt3.Y = panelCanvas.ClientRectangle.Height * 0.5f + 70f * sin2;
			pt4.Y = panelCanvas.ClientRectangle.Height * 0.5f + 30f * cos;

			// Create points
			pt2.X = panelCanvas.ClientRectangle.Width * 0.3f + 20f * sin;
			pt3.X = panelCanvas.ClientRectangle.Width * 0.7f + 20f * cos;

			// Wrap around
			if (time >= 4f * Math.PI) time = 0;
			time += 0.1f;

			// Invalidate
			panelCanvas.Invalidate();
		}
	}
}