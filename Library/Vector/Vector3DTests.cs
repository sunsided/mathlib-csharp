using System;
using NUnit.Framework;

namespace Library.Vector.Tests
{
	/// <summary>
	/// Unit tests for the Vector3D class
	/// </summary>
	[TestFixture]
	public sealed class Vector3DTests
	{
		/// <summary>
		/// Tests the assignment functions
		/// </summary>
		[Test(Description = "Construction and assignment test")]
		public void AssignTest()
		{
			{
				Vector3D vec = new Vector3D(0, 1, 2);
				Assert.AreEqual(0, vec.X);
				Assert.AreEqual(1, vec.Y);
				Assert.AreEqual(2, vec.Z);
			}

			{
				Vector3D vec = new Vector3D(new double[] {0, 1, 2});
				Assert.AreEqual(0, vec.X);
				Assert.AreEqual(1, vec.Y);
				Assert.AreEqual(2, vec.Z);

				vec.Assign(new double[] { 3, 4, 5 });
				Assert.AreEqual(3, vec.X);
				Assert.AreEqual(4, vec.Y);
				Assert.AreEqual(5, vec.Z);
			}

			{
				Vector3D vec = new Vector3D(new double[] { 0, 1, 2 });
				Assert.AreEqual(0, vec.Fields[0]);
				Assert.AreEqual(1, vec.Fields[1]);
				Assert.AreEqual(2, vec.Fields[2]);
			}

			{
				Vector3D vec = new Vector3D(new double[] { 0, 1, 2 });
				Assert.AreEqual(0, vec.Fields[Vector3D.FieldXIndex]);
				Assert.AreEqual(1, vec.Fields[Vector3D.FieldYIndex]);
				Assert.AreEqual(2, vec.Fields[Vector3D.FieldZIndex]);
			}

			{
				Vector3D vec = new Vector3D(new double[] { 0, 1, 2 });
				Assert.AreEqual(3, vec.Fields.Length);
				Assert.AreEqual(3, vec.Dimensions);
			}
		}

		/// <summary>
		/// Tests the normalisation and magnitude functions
		/// </summary>
		[Test(Description = "Tests the mangitude functions")]
		public void MagnitudeAndNormalisationTest()
		{
			{
				Vector3D vec = new Vector3D(10.0D, 0.0D, 0.0D);
				double mag = vec.Magnitude();
				Assert.AreEqual(10.0D, mag);
			}

			{
				Vector3D vec = new Vector3D(1.0D, 0.0D, 0.0D);
				double mag = vec.Magnitude();
				Assert.AreEqual(1.0D, mag);

				Vector3D vec2 = ~new Vector3D(vec);
				vec.Normalise();
				Assert.IsTrue(vec == vec2);
				Assert.AreEqual(1.0D, vec.X);
				Assert.AreEqual(0.0D, vec.Y);
				Assert.AreEqual(0.0D, vec.Z);
			}

			{
				Vector3D vec = new Vector3D(3.0D, 0.0D, 0.0D);
				double mag = vec.Magnitude();
				Assert.AreEqual(3.0D, mag);

				Vector3D vec2 = ~new Vector3D(vec);
				vec.Normalise();
				Assert.IsTrue(vec == vec2);
				Assert.AreEqual(1.0D, vec.X);
				Assert.AreEqual(0.0D, vec.Y);
				Assert.AreEqual(0.0D, vec.Z);
			}

			{
				Vector3D vec = new Vector3D(1.0D, 1.0D, 1.0D);
				double mag = vec.Magnitude();
				Assert.AreEqual(1.7321D, mag, 0.0001D);

				Vector3D vec2 = ~new Vector3D(vec);
				vec.Normalise();
				Assert.IsTrue(vec == vec2);
				Assert.AreEqual(0.5774D, vec.X, 0.0001D);
				Assert.AreEqual(0.5774D, vec.Y, 0.0001D);
				Assert.AreEqual(0.5774D, vec.Z, 0.0001D);
			}

			{
				Vector3D vec = new Vector3D(20.0D, 7.0D, 13.5D);
				double mag = vec.Magnitude();
				Assert.AreEqual(25.1247D, mag, 0.0001D);

				vec.Normalise();
				Assert.AreEqual(0.7960D, vec.X, 0.0001D);
				Assert.AreEqual(0.2786D, vec.Y, 0.0001D);
				Assert.AreEqual(0.5373D, vec.Z, 0.0001D);
			}

			{
				Vector3D vec1 = new Vector3D(10.0D, 22.0D, 27.0D);
				Vector3D vec2 = new Vector3D(1.0D, 2.0D, 3.0D);
				double distance = vec1.GetDistance(vec2);
				Assert.AreEqual(32.5115D, distance, 0.0001D);
			}
		}

		/// <summary>
		/// Tests the scaling operators
		/// </summary>
		[Test(Description = "Tests the scaling functions")]
		public void ScalingTest()
		{
			{
				Vector3D vec = new Vector3D(10.0D, 20.0D, 30.0D);
				Vector3D scaled = vec.Scale(10);
				Assert.AreEqual(100.0D, scaled.X);
				Assert.AreEqual(200.0D, scaled.Y);
				Assert.AreEqual(300.0D, scaled.Z);
			}

			{
				Vector3D vec = new Vector3D(10.0D, 20.0D, 30.0D);
				Vector3D scaled = vec * 10;
				Assert.AreEqual(100.0D, scaled.X);
				Assert.AreEqual(200.0D, scaled.Y);
				Assert.AreEqual(300.0D, scaled.Z);

				Vector3D scaled2 = 10 * vec;
				Assert.AreEqual(scaled, scaled2);
			}

			{
				Vector3D vec = new Vector3D(10.0D, 20.0D, 30.0D);
				Vector3D scaled = vec / 10.0D;
				Assert.AreEqual(1.0D, scaled.X);
				Assert.AreEqual(2.0D, scaled.Y);
				Assert.AreEqual(3.0D, scaled.Z);
			}
		}

		/// <summary>
		/// Tests the equality oprators
		/// </summary>
		[Test(Description = "Tests the equality functions")]
		public void EqualityTest()
		{
			Vector3D vec1 = new Vector3D(10.0D, 20.0D, 30.0D);
			Vector3D vec2 = new Vector3D(10.0D, 20.0D, 30.0D);
			Vector3D vec3 = new Vector3D(1.0D, 2.0D, 3.0D);

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
				Vector3D vec1 = new Vector3D(10.0D, 20.0D, 30.0D);
				Vector3D vec2 = new Vector3D(10.0D, 20.0D, 30.0D);
				Vector3D result1 = vec1 + vec2;
				Vector3D result2 = new Vector3D(vec1).Add(vec2);

				Assert.IsTrue(result1 == result2);
				Assert.AreEqual(20.0D, result1.X);
				Assert.AreEqual(40.0D, result1.Y);
				Assert.AreEqual(60.0D, result1.Z);

				Vector3D result3 = vec2 + vec1;
				Assert.AreEqual(result1, result3);
			}

			{
				Vector3D vec1 = new Vector3D(10.0D, 20.0D, 30.0D);
				Vector3D vec2 = new Vector3D(10.0D, 20.0D, 30.0D);
				Vector3D result = vec1 - vec2;
				Assert.AreEqual(0.0D, result.X);
				Assert.AreEqual(0.0D, result.Y);
				Assert.AreEqual(0.0D, result.Z);

				Vector3D result2 = vec2 - vec1;
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
				Vector3D vec = Vector3D.Parse("{0;0;0}");
				Assert.AreEqual(Vector3D.Zero, vec);
				Assert.IsTrue(Vector3D.IsValid(vec));
				Assert.IsFalse(Vector3D.IsInvalid(vec));
			}
			{
				Vector3D vec = Vector3D.Parse("{0.0;0.0;0.0}");
				Assert.AreEqual(Vector3D.Zero, vec);
				Assert.IsTrue(Vector3D.IsValid(vec));
				Assert.IsFalse(Vector3D.IsInvalid(vec));
			}
			
			{
				Vector3D vec = Vector3D.Parse("0;0;0");
				Assert.AreEqual(Vector3D.Zero, vec); 
				Assert.IsTrue(Vector3D.IsValid(vec));
				Assert.IsFalse(Vector3D.IsInvalid(vec));
			}
			{
				Vector3D vec = Vector3D.Parse("0.0;0.0;0.0");
				Assert.AreEqual(Vector3D.Zero, vec);
				Assert.IsTrue(Vector3D.IsValid(vec));
				Assert.IsFalse(Vector3D.IsInvalid(vec));
			}

			{
				Vector3D vec = Vector3D.Parse("1;2.0;3.0");
				Assert.AreEqual(new Vector3D(1, 2, 3), vec);
				Assert.IsTrue(Vector3D.IsValid(vec));
				Assert.IsFalse(Vector3D.IsInvalid(vec));
			}

			{
				Vector3D vec;
				bool success = Vector3D.TryParse("{0;0;0}", out vec);
				Assert.AreEqual(true, success);
				Assert.AreEqual(Vector3D.Zero, vec);
				Assert.IsTrue(Vector3D.IsValid(vec));
				Assert.IsFalse(Vector3D.IsInvalid(vec));
			}

			{
				Vector3D vec;
				bool success = Vector3D.TryParse("{1;1;1}", out vec);
				Assert.AreEqual(true, success);
				Assert.AreEqual(Vector3D.UnitVector, vec);
				Assert.IsTrue(Vector3D.IsValid(vec));
				Assert.IsFalse(Vector3D.IsInvalid(vec));
			}

			{
				Vector3D vec;
				bool success = Vector3D.TryParse("{1;s;1}", out vec);
				Assert.AreEqual(false, success);
				Assert.IsTrue(Vector3D.IsInvalid(vec));
				Assert.IsFalse(Vector3D.IsValid(vec));
			}

			{
				Vector3D vec;
				bool success = Vector3D.TryParse("{1;2;1s}", out vec);
				Assert.AreEqual(false, success);
				Assert.IsTrue(Vector3D.IsInvalid(vec));
				Assert.IsFalse(Vector3D.IsValid(vec));
			}
		}

		/// <summary>
		/// Tests a parsing function that fails
		/// </summary>
		[Test(Description = "Tests the parsing functions")]
		[ExpectedException(typeof(ArgumentException))]
		public void ParseFailTest()
		{
			Vector3D.Parse("foo");
		}

		/// <summary>
		/// Tests the cross operator
		/// </summary>
		[Test(Description = "Tests the cross operator")]
		public void CrossOperatorTest()
		{
			Vector3D vec1 = new Vector3D(10, 22, 27);
			Vector3D vec2 = new Vector3D(13, 15, 91);
			Vector3D result1 = vec1.Cross(vec2);
			Vector3D result2 = vec1%vec2;

			Assert.IsTrue(result1 == result2);
			Assert.AreEqual(1597, result1.X);
			Assert.AreEqual(-559, result1.Y);
			Assert.AreEqual(-136, result1.Z);
		}

		/// <summary>
		/// Tests the dot operator
		/// </summary>
		[Test(Description = "Tests the dot operator")]
		public void DotOperatorTest()
		{
			Vector3D vec1 = new Vector3D(10, 22, 27);
			Vector3D vec2 = new Vector3D(13, 15, 91);
			double result1 = vec1.Dot(vec2);
			double result2 = vec1*vec2;

			Assert.IsTrue(result1 == result2);
			Assert.AreEqual(2917, result1);
		}

		/// <summary>
		/// Tests the rotation functions
		/// </summary>
		[Test(Description = "Tests the rotation functions")]
		public void RotationTest()
		{
			Vector3D baseVector = new Vector3D(10, 5, 2);
			double deg90 = 90.0D*Math.PI/180.0D;

			// rotate counter-clockwise around Z
			{
				Vector3D vec1 = new Vector3D(baseVector);
				vec1.RotateZ(deg90);
				Vector3D vec2 = new Vector3D(-5, 10, 2);
				Assert.AreEqual(vec2.X, vec1.X, 0.0001D);
				Assert.AreEqual(vec2.Y, vec1.Y, 0.0001D);
				Assert.AreEqual(vec2.Z, vec1.Z, 0.0001D);
			}

			// rotate clockwise around Z
			{
				Vector3D vec1 = new Vector3D(baseVector);
				vec1.RotateZ(-deg90);
				Vector3D vec2 = new Vector3D(5, -10, 2);
				Assert.AreEqual(vec2.X, vec1.X, 0.0001D);
				Assert.AreEqual(vec2.Y, vec1.Y, 0.0001D);
				Assert.AreEqual(vec2.Z, vec1.Z, 0.0001D);
			}

			// rotate counter-clockwise around X
			{
				Vector3D vec1 = new Vector3D(baseVector);
				vec1.RotateX(deg90);
				Vector3D vec2 = new Vector3D(10, -2, 5);
				Assert.AreEqual(vec2.X, vec1.X, 0.0001D);
				Assert.AreEqual(vec2.Y, vec1.Y, 0.0001D);
				Assert.AreEqual(vec2.Z, vec1.Z, 0.0001D);
			}

			// rotate counter-clockwise around Y
			{
				Vector3D vec1 = new Vector3D(baseVector);
				vec1.RotateY(deg90);
				Vector3D vec2 = new Vector3D(-2, 5, -10);
				Assert.AreEqual(vec2.X, vec1.X, 0.0001D);
				Assert.AreEqual(vec2.Y, vec1.Y, 0.0001D);
				Assert.AreEqual(vec2.Z, vec1.Z, 0.0001D);
			}
		}
	}
}
