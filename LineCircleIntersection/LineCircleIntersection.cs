using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Library;
using Library.Vector;

namespace LineCircleIntersection
{
	public partial class LineCircleIntersection : Form
	{
		public LineCircleIntersection()
		{
			InitializeComponent();

			// Hook on panel to paint
			panelCanvas.Paint += new PaintEventHandler(panelCanvas_Paint);
			panelCanvas.MouseMove += new MouseEventHandler(panelCanvas_MouseMove);

			// Create points: Line segment
			Vector3D linePoint1 = new Vector3D(50f, 60f, 0f);
			Vector3D linePoint2 = new Vector3D(panelCanvas.ClientSize.Width - 50, panelCanvas.ClientSize.Height - 40, 0f);
			lineSegment3D = new LineSegment3D(linePoint1, linePoint2);

			// Create points: Line
			linePoint1 = new Vector3D(50f, 20f, 0f);
			linePoint2 = new Vector3D(panelCanvas.ClientSize.Width - 50, panelCanvas.ClientSize.Height - 80, 0f);
			line3D = new Line3D(linePoint1, linePoint2);

			// Sphere
			testSphere.Radius = 40f;
		}

		#region Points

		protected Line3D line3D;
		protected LineSegment3D lineSegment3D;
		protected Sphere3D testSphere;

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

		#region Draw a point

		protected void DrawPoint(Vector3D point, Graphics graphics, Color color)
		{
			// Set Point size
			const float width = 6f;
			
			// Create point bounds
			RectangleF pointBounds = new RectangleF(point.X - 0.5f * width, point.Y - 0.5f * width, width, width);

			// Create Pen
			Brush brush = new SolidBrush(color);

			// Draw point
			graphics.FillEllipse(brush, pointBounds);
		}

		#endregion

		#region Draw a line segment

		protected void DrawLineSegment(Vector3D start, Vector3D end, Graphics graphics, Color color, float width)
		{
			// Create Pen
			Pen pen = new Pen(color, width);

			// Create Drawing.Points
			PointF pt1 = new PointF(start.X, start.Y);
			PointF pt2 = new PointF(end.X, end.Y);

			// Draw point
			graphics.DrawLine(pen, pt1, pt2);
		}

		protected void DrawLineSegment(LineSegment3D line, Graphics graphics, Color color, float width)
		{
			// Create Pen
			Pen pen = new Pen(color, width);

			// Create Drawing.Points
			PointF pt1 = new PointF(line.Start.X, line.Start.Y);
			PointF pt2 = new PointF(line.End.X, line.End.Y);

			// Draw point
			graphics.DrawLine(pen, pt1, pt2);
		}

		#endregion

		#region Draw a (infinite) line

		protected void DrawLine(Line3D line, Graphics graphics, Color color, float width)
		{
			// Create Pen
			Pen pen = new Pen(color, width);

			// Create Drawing.Points
			Vector3D endPoint = line.Origin + line.Direction * lineSegment3D.Length();
			PointF pt1 = new PointF(line.Origin.X, line.Origin.Y);
			PointF pt2 = new PointF(endPoint.X, endPoint.Y);

			// Draw point
			graphics.DrawLine(pen, pt1, pt2);
		}

		#endregion

		#region Drawing Hook

		void panelCanvas_Paint(object sender, PaintEventArgs e)
		{
			// Set quality mode
			e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

			//// Set color based on distance
			float distanceBlue = (float)Math.Round(lineSegment3D.GetDistance(testSphere.Center) - testSphere.Radius, 0);
			if (distanceBlue < 0f) distanceBlue = 0f;

			// Draw line segment
			DrawLineSegment(lineSegment3D, e.Graphics, Color.CornflowerBlue, 2f);
			DrawPoint(lineSegment3D.Start, e.Graphics, Color.Black);
			DrawPoint(lineSegment3D.End, e.Graphics, Color.Black);

			// Post distance
			labelDistBlue.Text = distanceBlue.ToString();

			//// Set color based on distance
			float distanceRed = (float)Math.Round(line3D.GetDistance(testSphere.Center) - testSphere.Radius, 0);
			if (distanceRed < 0f) distanceRed = 0f;

			// Draw line
			DrawLine(line3D, e.Graphics, Color.IndianRed, 2f);
			Vector3D endPoint = line3D.Origin + line3D.Direction * lineSegment3D.Length();	// f(x) = mx + n
			DrawPoint(line3D.Origin, e.Graphics, Color.Black);
			DrawPoint(endPoint, e.Graphics, Color.Black);

			// Post distance
			labelDistRed.Text = distanceRed.ToString();

			// Draw Sphere
			Color color = (distanceBlue == 0f) || (distanceRed == 0f) ? Color.FromArgb(200, Color.LightGreen) : Color.Crimson;
			color = (distanceBlue == 0f) && (distanceRed == 0f) ? Color.FromArgb(200, Color.Yellow) : color;
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