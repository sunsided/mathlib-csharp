using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Library;

namespace LineLineIntersection2D
{
    public partial class LineLineIntersection2D : Form
    {
        public LineLineIntersection2D()
        {
            InitializeComponent();

            // Hook on panel to paint
            panelCanvas.Paint += new PaintEventHandler(panelCanvas_Paint);

            // Create line 1
            pt1 = new Vector2D(panelCanvas.ClientRectangle.Width * 0.2f, panelCanvas.ClientRectangle.Height * 0.3f);
            pt2 = new Vector2D(panelCanvas.ClientRectangle.Width * 0.8f, panelCanvas.ClientRectangle.Height * 0.3f);

            // Create line 2
            pt3 = new Vector2D(panelCanvas.ClientRectangle.Width * 0.3f, panelCanvas.ClientRectangle.Height * 0.2f);
            pt4 = new Vector2D(panelCanvas.ClientRectangle.Width * 0.3f, panelCanvas.ClientRectangle.Height * 0.8f);

            // Create line 3
            pt5 = new Vector2D(panelCanvas.ClientRectangle.Width * 0.2f, panelCanvas.ClientRectangle.Height * 0.8f);
            pt6 = new Vector2D(panelCanvas.ClientRectangle.Width * 0.7f, panelCanvas.ClientRectangle.Height * 0.2f);
            
            // Create ..
            pt7 = new Vector2D(panelCanvas.ClientRectangle.Width - 10.0f, panelCanvas.ClientRectangle.Height * 0.75f);
            pt8 = new Vector2D(10.0f, panelCanvas.ClientRectangle.Height * 0.25f);
        }

        #region Points

        protected Vector2D pt1, pt2,
                           pt3, pt4,
                           pt5, pt6,
                           pt7, pt8;

        #endregion

        #region Draw a point

        protected void DrawPoint(Vector2D point, Graphics graphics, Color color)
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

        protected void DrawLineSegment(Vector2D point1, Vector2D point2, Graphics graphics, Color color, float width)
        {
            // Create Pen
            Pen pen = new Pen(color, width);

            // Create Drawing.Points
            PointF p1 = new PointF(point1.X, point1.Y);
            PointF p2 = new PointF(point2.X, point2.Y);

            // Draw point
            graphics.DrawLine(pen, p1, p2);
        }

        #endregion

        #region Drawing Hook

        void panelCanvas_Paint(object sender, PaintEventArgs e)
        {
            // Set quality mode
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            // Draw Lines
            DrawLineSegment(pt1, pt2, e.Graphics, Color.Goldenrod, 2f);
            DrawLineSegment(pt3, pt4, e.Graphics, Color.Goldenrod, 2f);
            DrawLineSegment(pt5, pt6, e.Graphics, Color.Goldenrod, 2f);
            DrawLineSegment(pt7, pt8, e.Graphics, Color.CornflowerBlue, 2f);

            // Create Line Objects
            Line2D a = new Line2D(pt1, pt2);
            Line2D b = new Line2D(pt3, pt4);
            Line2D c = new Line2D(pt5, pt6);
            Line2D d = new Line2D(pt7, pt8);

            // Create line segments
            LineSegment2D aSeg = new LineSegment2D(pt1, pt2);
            LineSegment2D bSeg = new LineSegment2D(pt3, pt4);
            LineSegment2D cSeg = new LineSegment2D(pt5, pt6);
            
            // Get Point
            Vector2D ad = a.GetIntersection(d);
            Vector2D bd = b.GetIntersection(d);
            Vector2D cd = c.GetIntersection(d);
            
            // Colors
            Color aColor = Color.LightGreen;
            Color bColor = Color.LightGreen;
            Color cColor = Color.LightGreen;
            if (SnapToZero(aSeg.GetDistance(ad)) != 0.0f) aColor = Color.Crimson;
            if (SnapToZero(bSeg.GetDistance(bd)) != 0.0f) bColor = Color.Crimson;
            if (SnapToZero(cSeg.GetDistance(cd)) != 0.0f) cColor = Color.Crimson;
            
            // Draw Control Points
            DrawPoint(ad, e.Graphics, aColor);
            DrawPoint(bd, e.Graphics, bColor);
            DrawPoint(cd, e.Graphics, cColor);
        }
        
        /// <summary>
        /// Snaps a value to zero if it is in a given Epsilon
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>float</returns>
        private float SnapToZero( float value )
        {
            const float epsilon = 0.0001f;
            if (-value >= -epsilon) return 0.0f;
            if (value <= epsilon) return 0.0f;
            return value;
        }

        #endregion

        private static float time = 0.0f;

        private void timerSinus_Tick(object sender, EventArgs e)
        {
            const float scale = 0.25f;

            float sin = (float)Math.Sin(time * scale);
            float cos = (float)Math.Cos(time * scale);

            float cos2 = (float)Math.Cos(time * 2.0f * scale);
            float sin2 = -(float)Math.Sin(time * 2.0f * scale);

            // Y
            float halfHeight = panelCanvas.ClientRectangle.Height * 0.5f;
            pt7.Y = halfHeight + 0.8f * halfHeight * sin;
            pt8.Y = halfHeight + 0.8f * halfHeight * cos;

            // X
            /*
            float quarterWidth = panelCanvas.ClientRectangle.Width * 0.25f;
            pt7.X = quarterWidth + 0.8f * quarterWidth * sin2;
            pt8.X = panelCanvas.ClientRectangle.Width * 0.75f + 0.8f * quarterWidth * cos2;
            */

            // Wrap around
            if (time * scale >= 4f * Math.PI) time = 0;
            time += 0.1f;

            // Invalidate
            panelCanvas.Invalidate();
        }
    }
}