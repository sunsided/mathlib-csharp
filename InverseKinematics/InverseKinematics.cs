using System;
using System.Drawing;
using System.Windows.Forms;
using Library;
using Library.InverseKinematics;

namespace InverseKinematics
{
	public partial class InverseKinematics : Form
	{
		public InverseKinematics()
		{
			InitializeComponent();

			// Hook on panel to paint
			panelCanvas.Paint += new PaintEventHandler(panelCanvas_Paint);

			// Create points
			Bone1 = new Bone(
				new Vector3D(panelCanvas.ClientRectangle.Width * 0.2f, panelCanvas.ClientRectangle.Height * 0.8f, 0f),
				new Vector3D(panelCanvas.ClientRectangle.Width * 0.2f, panelCanvas.ClientRectangle.Height * 0.5f, 0f)
				);

			//Bone1.Rotate(0.0f, 0.0f, -(float)Math.PI * 0.25f/* * cos*/);
		}

		#region Points

		protected IBone Bone1;
		protected Vector3D origin, v1, v2;

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

		protected void DrawLineSegment(Vector3D point1, Vector3D point2, Graphics graphics, Color color, float width)
		{
			// Create Pen
			Pen pen = new Pen(color, width);

			// Create Drawing.Points
			PointF pt1 = new PointF(point1.X, point1.Y);
			PointF pt2 = new PointF(point2.X, point2.Y);

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

		#region Drawing Hook

		void panelCanvas_Paint(object sender, PaintEventArgs e)
		{
			// Set quality mode
			e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

			// Draw Line
			LineSegment3D line = new LineSegment3D(Bone1.Origin, Bone1.GetEndpoint());
			DrawLineSegment(line, e.Graphics, Color.CornflowerBlue, 4.0f);
			
			// Draw Control Points
			DrawPoint(Bone1.Origin, e.Graphics, Color.Crimson);
			DrawPoint(Bone1.GetEndpoint(), e.Graphics, Color.Crimson);
		}

		#endregion

		private static float time = 0.0f;

		private void timerSinus_Tick(object sender, EventArgs e)
		{
			float sin = (float) Math.Sin(time);
			float cos = (float) Math.Cos(time);

			float cos2 = (float) Math.Sin(time * 2f);
			float sin2 = -(float) Math.Sin(time * 2);

			/*
			// Create points
			pt1.Y = panelCanvas.ClientRectangle.Height * 0.5f + 30f * sin;
			pt2.Y = panelCanvas.ClientRectangle.Height * 0.5f + 50f * cos2;
			pt3.Y = panelCanvas.ClientRectangle.Height * 0.5f + 50f * sin2;
			pt4.Y = panelCanvas.ClientRectangle.Height * 0.5f + 30f * cos;

			// Create points
			pt2.X = panelCanvas.ClientRectangle.Width * 0.3f + 20f * sin;
			pt3.X = panelCanvas.ClientRectangle.Width * 0.7f + 20f * cos;
			*/

			// Wrap around
			if(time >= 4f * Math.PI) time = 0;
			time += 0.1f;

			// Invalidate
			panelCanvas.Invalidate();
		}
	}
}