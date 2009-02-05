using System;

namespace Library
{
	/// <summary>
	/// Structure for a 2D line segment
	/// </summary>
	public class Line2D
	{
		public Vector2D Origin, Direction;

		/// <summary>
		/// Constructs a line through two given points
		/// </summary>
		/// <param name="point1">First point ("Origin" of the line)</param>
		/// <param name="point2">Second point</param>
		public Line2D(Vector2D point1, Vector2D point2)
		{
			Direction = point2 - point1;
			Direction.Normalise();
			Origin = point1;
		}

		/// <summary>
		/// Returns the length of the line
		/// </summary>
		/// <returns>Float</returns>
		public float Length()
		{
			return float.PositiveInfinity;
		}

		/// <summary>
		/// Gets the distance of a point to the line
		/// </summary>
		/// <param name="point">The point</param>
		/// <returns>Float</returns>
		public float GetDistance(Vector2D point)
		{
			Vector2D difference = point - Origin;
			float d = difference.Dot(Direction);
			// projected point
			Vector2D shadow = Origin + d * Direction;
			// line from projected to point
			difference = shadow - point;
			// length of new line
			return difference.Magnitude();
		}

		/// <summary>
		/// Projects a point onto the line
		/// </summary>
		/// <param name="point">The point</param>
		/// <returns>Vector3</returns>
		public Vector2D Project(Vector2D point)
		{
			Vector2D difference = point - Origin;
			float d = difference.Dot(Direction);
			return Origin + Direction * d;
		}
	    
	    /// <summary>
	    /// Gets the intersection point of two lines
	    /// </summary>
	    /// <param name="b">The second line</param>
	    /// <returns>Intersection point or Vector2S.Invalid if the lines are
	    /// parallel or identical</returns>
	    public Vector2D GetIntersection(Line2D b)
	    {
	        // Get slopes
            float slopeA = this.GetSlope();
            float slopeB = b.GetSlope();

	        // Pre-check condition
	        // This handles every case in which both slopes are equal,
	        // including the Single.PositiveInfinity extreme
            if (slopeA == slopeB) return Vector2D.Invalid;
	        
	        // Get Y-intercept
	        float interceptA = this.GetYIntercept();
	        float interceptB = b.GetYIntercept();


            // Prepare checks
            bool c1 = (slopeA == Single.PositiveInfinity);
            bool c2 = (slopeB == Single.PositiveInfinity);	        
	        
	        // Since a line can be expressed as
	        // f(x) = y := a*x+b
	        // where a is the slope and b is the y-intercept
	        // To find the interception point, we have to set both
	        // lines equal, so that f(x) == g(x)
	        // This gives:
	        //     a1 * x + b1 = a2 * x + b2    | subtract   (a2 * x)
	        //     a1 * x - a2 * x + b1 = b2    | summarize
	        //     (a1 - a2) * x + b1 = b2      | subtract    b1
	        //     (a1 - a2) * x = b2 - b1      | divide by  (a1 - a2)
	        //     x = (b2 - b1) / (a1 - a2)
	        // and
            //     y = a1 * x + b1  =  a2 * x + b2

            float x = 0.0f, y = 0.0f;

            if (!(c1 || c2))
            {
                x = (interceptB - interceptA) / (slopeA - slopeB);
                y = slopeA * x + interceptA;
            }
            else
            {
                x = b.Origin.X;
                if (c1 && !c2)
                {
                    // swap
                    interceptA = interceptB;
                    slopeA = slopeB;
                    x = Origin.X;
                }

                y = slopeA * x + interceptA;
            }

	        // return
            return new Vector2D(x, y);
	    }
	    
	    /// <summary>
	    /// Returns the angle of the line spanned with the x axis
	    /// </summary>
	    /// <returns>Angle</returns>
	    public double GetAngle()
	    {
            return Math.Atan2(Direction.Y, Direction.X);
	    }

        /// <summary>
        /// Returns the slope of the line
        /// </summary>
        /// <returns>Slope or Single.PositiveInfinity if the Line is vertical</returns>
        public float GetSlope()
        {
            if (Direction.X == 0.0f) return Single.PositiveInfinity;
            return Direction.Y / Direction.X;
        }

        /// <summary>
        /// Returns the Y-intercept of the line
        /// </summary>
        /// <returns>Y-intercept or Single.PositiveInfinity if the Line is vertical</returns>	    
	    public float GetYIntercept()
	    {
            if (Direction.X == 0.0f) return Single.PositiveInfinity;
            
            // Since the line can be described as
            // y := a*x + b
            // we can calculate the Y-Intercept (b) using a Point on the
            // line (x|y) and the Slope (a) so that
            // b := y - a*x
            return Origin.Y - GetSlope() * Origin.X;
	    }
	}
}
