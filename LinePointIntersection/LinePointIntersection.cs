using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Library.Lines;
using Library.Vector;

namespace LinePointIntersection
{
	public partial class LinePointIntersection : Form
	{
		public LinePointIntersection()
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

			testPoint = new Vector3D();
		}

		#region Points

		protected Line3D line3D;
		protected LineSegment3D lineSegment3D;
		protected Vector3D testPoint;

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

		protected void DrawLineSegment(Vector3D start, Vector3D end, Graphics graphics, Color color, float width)
		{
			// Create Pen
			Pen pen = new Pen(color, width);

			// Create Drawing.Points
			PointF pt1 = new PointF((float)start.X, (float)start.Y);
			PointF pt2 = new PointF((float)end.X, (float)end.Y);

			// Draw point
			graphics.DrawLine(pen, pt1, pt2);
		}

		protected void DrawLineSegment(LineSegment3D line, Graphics graphics, Color color, float width)
		{
			// Create Pen
			Pen pen = new Pen(color, width);

			// Create Drawing.Points
			PointF pt1 = new PointF((float)line.Start.X, (float)line.Start.Y);
			PointF pt2 = new PointF((float)line.End.X, (float)line.End.Y);

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
			PointF pt1 = new PointF((float)line.Origin.X, (float)line.Origin.Y);
			PointF pt2 = new PointF((float)endPoint.X, (float)endPoint.Y);

			// Draw point
			graphics.DrawLine(pen, pt1, pt2);
		}

		#endregion

		#region Drawing Hook

		void panelCanvas_Paint(object sender, PaintEventArgs e)
		{
			// Set quality mode
			e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

			// Set color based on distance
			float distanceBlue = (float)Math.Round(lineSegment3D.GetDistance(testPoint), 0);
			Color color = distanceBlue <= 1f ? Color.LightGreen : Color.CornflowerBlue;
			
			// Draw line segment
			DrawLineSegment(lineSegment3D, e.Graphics, color, 2f);
			DrawPoint(lineSegment3D.Start, e.Graphics, Color.Black);
			DrawPoint(lineSegment3D.End, e.Graphics, Color.Black);

			// Draw projected point: Blue line
			Vector3D projectedBlue = lineSegment3D.Project(testPoint);
			DrawPoint(projectedBlue, e.Graphics, Color.DarkSeaGreen);
			labelDistBlue.Text = distanceBlue.ToString();

			// Set color based on distance
			float distanceRed = (float)Math.Round(line3D.GetDistance(testPoint), 0);
			color = distanceRed <= 1f ? Color.LightGreen : Color.IndianRed;

			// Draw line
			DrawLine(line3D, e.Graphics, color, 2f);
			Vector3D endPoint = line3D.Origin + line3D.Direction * lineSegment3D.Length();
			DrawPoint(line3D.Origin, e.Graphics, Color.Black);
			DrawPoint(endPoint, e.Graphics, Color.Black);

			// Draw projected point: Red line
			Vector3D projectedRed = line3D.Project(testPoint);
			DrawPoint(projectedRed, e.Graphics, Color.DarkSeaGreen);
			labelDistRed.Text = distanceRed.ToString();

			if (distanceBlue < distanceRed)
				DrawLineSegment(projectedBlue, testPoint, e.Graphics, Color.Gold, 1f);
			else if (distanceBlue > distanceRed)
				DrawLineSegment(projectedRed, testPoint, e.Graphics, Color.Gold, 1f);

			// Draw test point
			DrawPoint(testPoint, e.Graphics, Color.Crimson);
		}

		#endregion

		#region Mouse Movement

		void panelCanvas_MouseMove(object sender, MouseEventArgs e)
		{
			// Set the new point position
			testPoint = new Vector3D((float)e.X, (float)e.Y, 0f);

			// Redraw panel
			panelCanvas.Invalidate();
		}		

		#endregion
	}
}