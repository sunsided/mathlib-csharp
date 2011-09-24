using System;
using MathLib.Vector;
using NUnit.Framework;

namespace MathLib.Tests.Vector
{
	/// <summary>
	/// Unit tests for the Vector2D class
	/// </summary>
	[TestFixture]
	public sealed class Vector2DTests
	{
		/// <summary>
		/// Tests the assignment functions
		/// </summary>
		[Test(Description = "Construction and assignment test")]
		public void AssignTest()
		{
			{
				Vector2D vec = new Vector2D(0, 1);
				Assert.AreEqual(0, vec.X);
				Assert.AreEqual(1, vec.Y);
			}

			{
				Vector2D vec = new Vector2D(new double[] {0, 1});
				Assert.AreEqual(0, vec.X);
				Assert.AreEqual(1, vec.Y);

				vec.Assign(new double[] { 3, 4 });
				Assert.AreEqual(3, vec.X);
				Assert.AreEqual(4, vec.Y);
			}

			{
				Vector2D vec = new Vector2D(new double[] { 0, 1 });
				Assert.AreEqual(0, vec.Fields[0]);
				Assert.AreEqual(1, vec.Fields[1]);
			}

			{
				Vector2D vec = new Vector2D(new double[] { 0, 1 });
				Assert.AreEqual(0, vec.Fields[Vector2D.FieldXIndex]);
				Assert.AreEqual(1, vec.Fields[Vector2D.FieldYIndex]);
			}

			{
				Vector2D vec = new Vector2D(new double[] { 0, 1 });
				Assert.AreEqual(2, vec.Fields.Length);
				Assert.AreEqual(2, vec.Dimensions);
			}
		}

		/// <summary>
		/// Tests the normalisation and magnitude functions
		/// </summary>
		[Test(Description = "Tests the mangitude functions")]
		public void MagnitudeAndNormalisationTest()
		{
			{
				Vector2D vec = new Vector2D(10.0D, 0.0D);
				double mag = vec.Magnitude();
				Assert.AreEqual(10.0D, mag);
			}

			{
				Vector2D vec = new Vector2D(1.0D, 0.0D);
				double mag = vec.Magnitude();
				Assert.AreEqual(1.0D, mag);

				Vector2D vec2 = ~new Vector2D(vec);
				vec.Normalise();
				Assert.IsTrue(vec == vec2);
				Assert.AreEqual(1.0D, vec.X);
				Assert.AreEqual(0.0D, vec.Y);
			}

			{
				Vector2D vec = new Vector2D(3.0D, 0.0D);
				double mag = vec.Magnitude();
				Assert.AreEqual(3.0D, mag);

				Vector2D vec2 = ~new Vector2D(vec);
				vec.Normalise();
				Assert.IsTrue(vec == vec2);
				Assert.AreEqual(1.0D, vec.X);
				Assert.AreEqual(0.0D, vec.Y);
			}

			{
				Vector2D vec = new Vector2D(1.0D, 1.0D);
				double mag = vec.Magnitude();
				Assert.AreEqual(1.4142D, mag, 0.0001D);

				Vector2D vec2 = ~new Vector2D(vec);
				vec.Normalise();
				Assert.IsTrue(vec == vec2);
				Assert.AreEqual(0.7071D, vec.X, 0.0001D);
				Assert.AreEqual(0.7071D, vec.Y, 0.0001D);
			}

			{
				Vector2D vec = new Vector2D(20.0D, 13.5D);
				double mag = vec.Magnitude();
				Assert.AreEqual(24.1299, mag, 0.0001D);

				vec.Normalise();
				Assert.AreEqual(0.8288D, vec.X, 0.0001D);
				Assert.AreEqual(0.5595D, vec.Y, 0.0001D);
			}

			{
				Vector2D vec1 = new Vector2D(10.0D, 22.0D);
				Vector2D vec2 = new Vector2D(1.0D, 2.0D);
				double distance = vec1.GetDistance(vec2);
				Assert.AreEqual(21.9317D, distance, 0.0001D);
			}
		}

		/// <summary>
		/// Tests the scaling operators
		/// </summary>
		[Test(Description = "Tests the scaling functions")]
		public void ScalingTest()
		{
			{
				Vector2D vec = new Vector2D(10.0D, 20.0D);
				Vector2D scaled = vec.Scale(10);
				Assert.AreEqual(100.0D, scaled.X);
				Assert.AreEqual(200.0D, scaled.Y);
			}

			{
				Vector2D vec = new Vector2D(10.0D, 40.0D);
				Vector2D scaled = vec * 10;
				Assert.AreEqual(100.0D, scaled.X);
				Assert.AreEqual(400.0D, scaled.Y);

				Vector2D scaled2 = 10 * vec;
				Assert.AreEqual(scaled, scaled2);
			}

			{
				Vector2D vec = new Vector2D(10.0D, 21.0D);
				Vector2D scaled = vec / 10.0D;
				Assert.AreEqual(1.0D, scaled.X);
				Assert.AreEqual(2.1D, scaled.Y);
			}
		}

		/// <summary>
		/// Tests the equality oprators
		/// </summary>
		[Test(Description = "Tests the equality functions")]
		public void EqualityTest()
		{
			Vector2D vec1 = new Vector2D(10.0D, 20.0D);
			Vector2D vec2 = new Vector2D(10.0D, 20.0D);
			Vector2D vec3 = new Vector2D(1.0D, 2.0D);

			Assert.IsTrue(vec1 == vec1);

			Assert.IsTrue(vec1 == vec2);
			Assert.IsTrue(vec2 == vec1);
			
			Assert.IsFalse(vec1 != vec2);
			Assert.IsFalse(vec2 != vec1);

			Assert.IsFalse(vec1 == vec3);		
			Assert.IsTrue(vec1 != vec3);

			Assert.AreEqual(vec1, vec2);
			Assert.AreNotEqual(vec1, vec3);
		}

		/// <summary>
		/// Tests the addition and subtraction functions
		/// </summary>
		[Test(Description = "Tests the addition functions")]
		public void AdditionTest()
		{
			{
				Vector2D vec1 = new Vector2D(10.0D, 20.0D);
				Vector2D vec2 = new Vector2D(10.0D, 20.0D);
				Vector2D result1 = vec1 + vec2;
				Vector2D result2 = new Vector2D(vec1).Add(vec2);

				Assert.IsTrue(result1 == result2);
				Assert.AreEqual(20.0D, result1.X);
				Assert.AreEqual(40.0D, result1.Y);

				Vector2D result3 = vec2 + vec1;
				Assert.AreEqual(result1, result3);
			}

			{
				Vector2D vec1 = new Vector2D(10.0D, 20.0D);
				Vector2D vec2 = new Vector2D(10.0D, 20.0D);
				Vector2D result = vec1 - vec2;
				Assert.AreEqual(0.0D, result.X);
				Assert.AreEqual(0.0D, result.Y);

				Vector2D result2 = vec2 - vec1;
				Assert.AreEqual(result, result2);
			}
		}
		
		/// <summary>
		/// Tests the parsing functions
		/// </summary>
		[Test(Description = "Tests the parsing functions")]
		public void ParseTest()
		{
			{
				Vector2D vec = Vector2D.Parse("{0;0}");
				Assert.AreEqual(Vector2D.Zero, vec);
				Assert.IsTrue(Vector2D.IsValid(vec));
				Assert.IsFalse(Vector2D.IsInvalid(vec));
			}
			{
				Vector2D vec = Vector2D.Parse("{0.0;0.0}");
				Assert.AreEqual(Vector2D.Zero, vec);
				Assert.IsTrue(Vector2D.IsValid(vec));
				Assert.IsFalse(Vector2D.IsInvalid(vec));
			}
			
			{
				Vector2D vec = Vector2D.Parse("0;0");
				Assert.AreEqual(Vector2D.Zero, vec); 
				Assert.IsTrue(Vector2D.IsValid(vec));
				Assert.IsFalse(Vector2D.IsInvalid(vec));
			}
			{
				Vector2D vec = Vector2D.Parse("0.0;0.0");
				Assert.AreEqual(Vector2D.Zero, vec);
				Assert.IsTrue(Vector2D.IsValid(vec));
				Assert.IsFalse(Vector2D.IsInvalid(vec));
			}

			{
				Vector2D vec = Vector2D.Parse("1;2.0");
				Assert.AreEqual(new Vector2D(1, 2), vec);
				Assert.IsTrue(Vector2D.IsValid(vec));
				Assert.IsFalse(Vector2D.IsInvalid(vec));
			}

			{
				Vector2D vec;
				bool success = Vector2D.TryParse("{0;0}", out vec);
				Assert.AreEqual(true, success);
				Assert.AreEqual(Vector2D.Zero, vec);
				Assert.IsTrue(Vector2D.IsValid(vec));
				Assert.IsFalse(Vector2D.IsInvalid(vec));
			}

			{
				Vector2D vec;
				bool success = Vector2D.TryParse("{1;1}", out vec);
				Assert.AreEqual(true, success);
				Assert.AreEqual(Vector2D.UnitVector, vec);
				Assert.IsTrue(Vector2D.IsValid(vec));
				Assert.IsFalse(Vector2D.IsInvalid(vec));
			}

			{
				Vector2D vec;
				bool success = Vector2D.TryParse("{1;s}", out vec);
				Assert.AreEqual(false, success);
				Assert.IsTrue(Vector2D.IsInvalid(vec));
				Assert.IsFalse(Vector2D.IsValid(vec));
			}

			{
				Vector2D vec;
				bool success = Vector2D.TryParse("{1;1;1}", out vec);
				Assert.AreEqual(false, success);
				Assert.IsTrue(Vector2D.IsInvalid(vec));
				Assert.IsFalse(Vector2D.IsValid(vec));
			}
		}

		/// <summary>
		/// Tests a parsing function that fails
		/// </summary>
		[Test(Description = "Tests the parsing functions")]
		[ExpectedException(typeof(ArgumentException))]
		public void ParseFailTest()
		{
			Vector2D.Parse("foo");
		}

		/// <summary>
		/// Tests the cross operator
		/// </summary>
		[Test(Description = "Tests the cross operator")]
		public void CrossOperatorTest()
		{
			Vector2D vec1 = new Vector2D(10, 22);
			Vector2D vec2 = new Vector2D(13, 15);
			Vector3D result1 = vec1.Cross(vec2);
			Vector3D result2 = vec1%vec2;

			Assert.IsTrue(result1 == result2);
			Assert.AreEqual(0, result1.X);
			Assert.AreEqual(0, result1.Y);
			Assert.AreEqual(-136, result1.Z);
		}

		/// <summary>
		/// Tests the dot operator
		/// </summary>
		[Test(Description = "Tests the dot operator")]
		[TestCase(10, 22, 13, 15, Result = 460)]
		[TestCase(1, 2, 3, 4, Result = 11)]
		public double DotOperatorTest(double x1, double y1, double x2, double y2)
		{
			Vector2D vec1 = new Vector2D(x1, y1);
			Vector2D vec2 = new Vector2D(x2, y2);

			double value1 = vec1.Dot(vec2);
			double value2 = vec1 * vec2;

			Assert.AreEqual(value1, value2, 0.000001d);
			return value1;
		}
	}
}
