using System;
using MathLib.Vector;
using NUnit.Framework;

namespace MathLib.Tests.Vector
{
	/// <summary>
	/// Unit tests for the Vector4D class
	/// </summary>
	[TestFixture]
	public sealed class Vector4DTests
	{
		/// <summary>
		/// Tests the assignment functions
		/// </summary>
		[Test(Description = "Construction and assignment test")]
		public void AssignTest()
		{
			{
				Vector4D vec = new Vector4D(0, 1, 2, 3);
				Assert.AreEqual(0, vec.X);
				Assert.AreEqual(1, vec.Y);
				Assert.AreEqual(2, vec.Z);
				Assert.AreEqual(3, vec.W);
			}

			{
				Vector4D vec = new Vector4D(new double[] {0, 1, 2, 3});
				Assert.AreEqual(0, vec.X);
				Assert.AreEqual(1, vec.Y);
				Assert.AreEqual(2, vec.Z);
				Assert.AreEqual(3, vec.W);

				vec.Assign(new double[] { 3, 4, 5, 9 });
				Assert.AreEqual(3, vec.X);
				Assert.AreEqual(4, vec.Y);
				Assert.AreEqual(5, vec.Z);
				Assert.AreEqual(9, vec.W);
			}

			{
				Vector4D vec = new Vector4D(new double[] { 0, 1, 2, 9 });
				Assert.AreEqual(0, vec.Fields[0]);
				Assert.AreEqual(1, vec.Fields[1]);
				Assert.AreEqual(2, vec.Fields[2]);
				Assert.AreEqual(9, vec.Fields[3]);
			}

			{
				Vector4D vec = new Vector4D(new double[] { 0, 1, 2, 7 });
				Assert.AreEqual(0, vec.Fields[Vector4D.FieldXIndex]);
				Assert.AreEqual(1, vec.Fields[Vector4D.FieldYIndex]);
				Assert.AreEqual(2, vec.Fields[Vector4D.FieldZIndex]);
				Assert.AreEqual(7, vec.Fields[Vector4D.FieldWIndex]);
			}

			{
				Vector4D vec = new Vector4D(new double[] { 0, 1, 2, 18 });
				Assert.AreEqual(4, vec.Fields.Length);
				Assert.AreEqual(4, vec.Dimensions);
			}
		}

		/// <summary>
		/// Tests the normalisation and magnitude functions
		/// </summary>
		[Test(Description = "Tests the mangitude functions")]
		public void MagnitudeAndNormalisationTest()
		{
			{
				Vector4D vec = new Vector4D(10.0D, 0.0D, 0.0D, 0.0D);
				double mag = vec.Magnitude();
				Assert.AreEqual(10.0D, mag);
			}

			{
				Vector4D vec = new Vector4D(1.0D, 0.0D, 0.0D, 0.0D);
				double mag = vec.Magnitude();
				Assert.AreEqual(1.0D, mag);

				Vector4D vec2 = ~new Vector4D(vec);
				vec.Normalise();
				Assert.IsTrue(vec == vec2);
				Assert.AreEqual(1.0D, vec.X);
				Assert.AreEqual(0.0D, vec.Y);
				Assert.AreEqual(0.0D, vec.Z);
				Assert.AreEqual(0.0D, vec.W);
			}

			{
				Vector4D vec = new Vector4D(3.0D, 0.0D, 0.0D, 0.0D);
				double mag = vec.Magnitude();
				Assert.AreEqual(3.0D, mag);

				Vector4D vec2 = ~new Vector4D(vec);
				vec.Normalise();
				Assert.IsTrue(vec == vec2);
				Assert.AreEqual(1.0D, vec.X);
				Assert.AreEqual(0.0D, vec.Y);
				Assert.AreEqual(0.0D, vec.Z);
				Assert.AreEqual(0.0D, vec.W);
			}

			{
				Vector4D vec = new Vector4D(1.0D, 1.0D, 1.0D, 1.0D);
				double mag = vec.Magnitude();
				Assert.AreEqual(2.0D, mag);

				Vector4D vec2 = ~new Vector4D(vec);
				vec.Normalise();
				Assert.IsTrue(vec == vec2);
				Assert.AreEqual(0.5D, vec.X);
				Assert.AreEqual(0.5D, vec.Y);
				Assert.AreEqual(0.5D, vec.Z);
				Assert.AreEqual(0.5D, vec.W);
			}

			{
				Vector4D vec = new Vector4D(20.0D, 7.0D, 13.5D, 91.0D);
				double mag = vec.Magnitude();
				Assert.AreEqual(94.4047D, mag, 0.0001D);

				vec.Normalise();
				Assert.AreEqual(0.2119D, vec.X, 0.0001D);
				Assert.AreEqual(0.0741D, vec.Y, 0.0001D);
				Assert.AreEqual(0.1430D, vec.Z, 0.0001D);
				Assert.AreEqual(0.9639D, vec.W, 0.0001D);
			}

			{
				Vector4D vec1 = new Vector4D(10.0D, 22.0D, 27.0D, 3.0D);
				Vector4D vec2 = new Vector4D(1.0D, 2.0D, 3.0D, 81.0D);
				double distance = vec1.GetDistance(vec2);
				Assert.AreEqual(84.5044D, distance, 0.0001D);
			}
		}

		/// <summary>
		/// Tests the scaling operators
		/// </summary>
		[Test(Description = "Tests the scaling functions")]
		public void ScalingTest()
		{
			{
				Vector4D vec = new Vector4D(10.0D, 20.0D, 30.0D, 40.0D);
				Vector4D scaled = vec.Scale(10);
				Assert.AreEqual(100.0D, scaled.X);
				Assert.AreEqual(200.0D, scaled.Y);
				Assert.AreEqual(300.0D, scaled.Z);
				Assert.AreEqual(400.0D, scaled.W);
			}

			{
				Vector4D vec = new Vector4D(10.0D, 20.0D, 30.0D, 40.0D);
				Vector4D scaled = vec * 10;
				Assert.AreEqual(100.0D, scaled.X);
				Assert.AreEqual(200.0D, scaled.Y);
				Assert.AreEqual(300.0D, scaled.Z);
				Assert.AreEqual(400.0D, scaled.W);

				Vector4D scaled2 = 10 * vec;
				Assert.AreEqual(scaled, scaled2);
			}

			{
				Vector4D vec = new Vector4D(10.0D, 20.0D, 30.0D, 40.0D);
				Vector4D scaled = vec / 10.0D;
				Assert.AreEqual(1.0D, scaled.X);
				Assert.AreEqual(2.0D, scaled.Y);
				Assert.AreEqual(3.0D, scaled.Z);
				Assert.AreEqual(4.0D, scaled.W);
			}
		}

		/// <summary>
		/// Tests the equality oprators
		/// </summary>
		[Test(Description = "Tests the equality functions")]
		public void EqualityTest()
		{
			Vector4D vec1 = new Vector4D(10.0D, 20.0D, 30.0D, 40.0D);
			Vector4D vec2 = new Vector4D(10.0D, 20.0D, 30.0D, 40.0D);
			Vector4D vec3 = new Vector4D(1.0D, 2.0D, 3.0D, 7.0D);

			Assert.IsTrue(vec1 == vec1);
			Assert.IsFalse(vec1 != vec1);

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
				Vector4D vec1 = new Vector4D(10.0D, 20.0D, 30.0D, 40.0D);
				Vector4D vec2 = new Vector4D(10.0D, 20.0D, 30.0D, -30.0D);
				Vector4D result1 = vec1 + vec2;
				Vector4D result2 = new Vector4D(vec1).Add(vec2);

				Assert.IsTrue(result1 == result2);
				Assert.AreEqual(20.0D, result1.X);
				Assert.AreEqual(40.0D, result1.Y);
				Assert.AreEqual(60.0D, result1.Z);
				Assert.AreEqual(10.0D, result1.W);

				Vector4D result3 = vec2 + vec1;
				Assert.AreEqual(result1, result3);
			}

			{
				Vector4D vec1 = new Vector4D(10.0D, 20.0D, 30.0D, 40.0D);
				Vector4D vec2 = new Vector4D(10.0D, 20.0D, 30.0D, -30.0D);
				Vector4D result = vec1 - vec2;
				Assert.AreEqual(0.0D, result.X);
				Assert.AreEqual(0.0D, result.Y);
				Assert.AreEqual(0.0D, result.Z);
				Assert.AreEqual(70.0D, result.W);

				Vector4D result2 = vec2 - vec1;
				Assert.AreEqual(result, -result2);
			}
		}

		/// <summary>
		/// Tests the parsing functions
		/// </summary>
		[Test(Description = "Tests the parsing functions")]
		public void ParseTest()
		{
			{
				Vector4D vec = Vector4D.Parse("{0;0;0;0}");
				Assert.AreEqual(Vector4D.Zero, vec);
				Assert.IsTrue(Vector4D.IsValid(vec));
				Assert.IsFalse(Vector4D.IsInvalid(vec));
			}
			{
				Vector4D vec = Vector4D.Parse("{0.0;0.0;0.0;0.0}");
				Assert.AreEqual(Vector4D.Zero, vec);
				Assert.IsTrue(Vector4D.IsValid(vec));
				Assert.IsFalse(Vector4D.IsInvalid(vec));
			}
			
			{
				Vector4D vec = Vector4D.Parse("0;0;0;0");
				Assert.AreEqual(Vector4D.Zero, vec); 
				Assert.IsTrue(Vector4D.IsValid(vec));
				Assert.IsFalse(Vector4D.IsInvalid(vec));
			}
			{
				Vector4D vec = Vector4D.Parse("0.0;0.0;0.0;0.0");
				Assert.AreEqual(Vector4D.Zero, vec);
				Assert.IsTrue(Vector4D.IsValid(vec));
				Assert.IsFalse(Vector4D.IsInvalid(vec));
			}

			{
				Vector4D vec = Vector4D.Parse("1;2.0;3.0;4");
				Assert.AreEqual(new Vector4D(1, 2, 3, 4), vec);
				Assert.IsTrue(Vector4D.IsValid(vec));
				Assert.IsFalse(Vector4D.IsInvalid(vec));
			}

			{
				Vector4D vec;
				bool success = Vector4D.TryParse("{0;0;0;0}", out vec);
				Assert.AreEqual(true, success);
				Assert.AreEqual(Vector4D.Zero, vec);
				Assert.IsTrue(Vector4D.IsValid(vec));
				Assert.IsFalse(Vector4D.IsInvalid(vec));
			}

			{
				Vector4D vec;
				bool success = Vector4D.TryParse("{1;1;1;1}", out vec);
				Assert.AreEqual(true, success);
				Assert.AreEqual(Vector4D.UnitVector, vec);
				Assert.IsTrue(Vector4D.IsValid(vec));
				Assert.IsFalse(Vector4D.IsInvalid(vec));
			}

			{
				Vector4D vec;
				bool success = Vector4D.TryParse("{1;s;1;2}", out vec);
				Assert.AreEqual(false, success);
				Assert.IsTrue(Vector4D.IsInvalid(vec));
				Assert.IsFalse(Vector4D.IsValid(vec));
			}

			{
				Vector4D vec;
				bool success = Vector4D.TryParse("{1;2;1s}", out vec);
				Assert.AreEqual(false, success);
				Assert.IsTrue(Vector4D.IsInvalid(vec));
				Assert.IsFalse(Vector4D.IsValid(vec));
			}

			{
				Vector4D vec;
				bool success = Vector4D.TryParse("{1;2;1}", out vec);
				Assert.AreEqual(false, success);
				Assert.IsTrue(Vector4D.IsInvalid(vec));
				Assert.IsFalse(Vector4D.IsValid(vec));
			}
		}

		/// <summary>
		/// Tests a parsing function that fails
		/// </summary>
		[Test(Description = "Tests the parsing functions")]
		[ExpectedException(typeof(ArgumentException))]
		public void ParseFailTest()
		{
			Vector4D.Parse("foo");
		}
		
		/// <summary>
		/// Tests the dot operator
		/// </summary>
		[Test(Description = "Tests the dot operator")]
		[TestCase(10, 22, 27, 13, 13, 15, 91, 4, Result = 2969)]
		[TestCase(1, 2, 0, 0, 3, 4, 0, 0, Result = 11)]
		public double DotOperatorTest(double x1, double y1, double z1, double w1, double x2, double y2, double z2, double w2)
		{
			Vector4D vec1 = new Vector4D(x1, y1, z1, w1);
			Vector4D vec2 = new Vector4D(x2, y2, z2, w2);

			double value1 = vec1.Dot(vec2);
			double value2 = vec1 * vec2;

			Assert.AreEqual(value1, value2, 0.000001d);
			return value1;
		}
	}
}
