using Library.Vector;
using NUnit.Framework;

namespace Library.Matrix
{
	/// <summary>
	/// Unit tests for the Matrix4D class
	/// </summary>
	[TestFixture]
	public sealed class Matrix4DTests
	{
		/// <summary>
		/// Tests the GetAdjoint() method
		/// </summary>
		[Test]
		public void TestAdjoint()
		{
			Assert.AreEqual(1, Matrix4D.GetAdjoint(0, 0));
			Assert.AreEqual(-1, Matrix4D.GetAdjoint(1, 0));
			Assert.AreEqual(-1, Matrix4D.GetAdjoint(0, 1));
			Assert.AreEqual(1, Matrix4D.GetAdjoint(1, 1));
			Assert.AreEqual(1, Matrix4D.GetAdjoint(2, 2));
			Assert.AreEqual(-1, Matrix4D.GetAdjoint(2, 3));
		}

		/// <summary>
		/// Tests the GetSubDeterminant() method
		/// </summary>
		[Test]
		public void TestGetSubDeterminant()
		{
			Matrix4D mat4 = new Matrix4D(
				8, 1, 6, 0, 
				3, 5, 7, 0, 
				4, 9, 2, 0, 
				0, 0, 0, 1);

			// Get subdeterminant by skipping 3,3
			double det33 = mat4.GetSubDeterminant(3, 3);
			Assert.AreEqual(-360, det33, 0.0001D);

			// Get subdeterminant by skipping 0,0
			double det00 = mat4.GetSubDeterminant(0, 0);
			Assert.AreEqual(-53, det00, 0.0001D);

			// Get actual submatrix and test determinant
			Matrix3D mat33 = mat4.GetSubmatrix(3, 3);
			Assert.AreEqual(det33, mat33.GetDeterminant(), 0.001D);

			// Get actual submatrix and test determinant
			Matrix3D mat00 = mat4.GetSubmatrix(0, 0);
			Assert.AreEqual(det00, mat00.GetDeterminant(), 0.001D);
		}

		/// <summary>
		/// Tests the GetDeterminant() method
		/// </summary>
		[Test]
		public void TestDeterminant()
		{
			// Test 1: First three rows are already zero
			Matrix4D mat = new Matrix4D(
				0, -7, -7, -5,
				0, 14, 11, 4,
				0, -1, -1, 3,
				-3, 4, 3, 2);
			double det = mat.GetDeterminant();
			Assert.AreEqual(234.0D, det, 0.001D);

			// Test 2: First two rows are already zero
			mat = new Matrix4D(
				0, -7, -7, -5,
				0, 14, 11, 4,
				-3, 4, 3, 2,
				0, -1, -1, 3);
			det = mat.GetDeterminant();
			Assert.AreEqual(-234.0D, det, 0.001D);

			// Test 3
			mat = Matrix4D.Magic;
			det = mat.GetDeterminant();
			Assert.AreEqual(0, det, 0.001D);
		}

		/// <summary>
		/// Tests sorting of a matrix by column
		/// </summary>
		[Test]
		public void TestSortRows()
		{
			// The sorted matrix
			Matrix4D mat = Matrix4D.Magic.GetRowSorted(0);

			// The expected matrix
			Matrix4D expected = new Matrix4D(
				4, 14, 15, 1,
				5, 11, 10, 8,
				9, 7, 6, 12,
				16, 2, 3, 13
				);

			Assert.AreEqual(expected, mat);


			// The sorted matrix
			mat = Matrix4D.Magic.GetRowSorted(1);

			// The expected matrix
			expected = new Matrix4D(
				16, 2, 3, 13,
				9, 7, 6, 12,
				5, 11, 10, 8,
				4, 14, 15, 1
				);

			Assert.AreEqual(expected, mat);


			// The sorted matrix
			mat = new Matrix4D(
				0, 0, 0, -3,
				-7, 14, -1, 4,
				-7, 11, -2, 3,
				-5, 4, 3, 2)
				.GetRowSorted(0)
				;

			// The expected matrix
			expected = new Matrix4D(
				-7, 11, -2, 3,
				-7, 14, -1, 4,
				-5, 4, 3, 2,
				0, 0, 0, -3
				);

			Assert.AreEqual(expected, mat);
		}

		/// <summary>
		/// Tests sorting of a matrix by column
		/// </summary>
		[Test]
		public void TestSortColumns()
		{
			// The sorted matrix
			Matrix4D mat = Matrix4D.Magic.GetColumnSorted(0);

			// The expected matrix
			Matrix4D expected = new Matrix4D(
				2, 3, 13, 16,
				11, 10, 8, 5,
				7, 6, 12, 9,
				14, 15, 1, 4
				);

			Assert.AreEqual(expected, mat);


			// The sorted matrix
			mat = Matrix4D.Magic.GetColumnSorted(3);

			// The expected matrix
			expected = new Matrix4D(
				13, 16, 2, 3,
				8, 5, 11, 10,
				12, 9, 7, 6,
				1, 4, 14, 15
				);

			Assert.AreEqual(expected, mat);
		}

		/// <summary>
		/// Tests the handling of row vectors
		/// </summary>
		[Test]
		public void TestRowVectors()
		{
			Matrix4D mat = Matrix4D.Magic;
			Vector4D row1 = new Vector4D(16, 2, 3, 13);
			Vector4D row2 = new Vector4D(5, 11, 10, 8);
			Vector4D row3 = new Vector4D(9, 7, 6, 12);
			Vector4D row4 = new Vector4D(4, 14, 15, 1);

			Assert.AreEqual(row1, mat.GetRowVector(0), "row 1");
			Assert.AreEqual(row2, mat.GetRowVector(1), "row 2");
			Assert.AreEqual(row3, mat.GetRowVector(2), "row 3");
			Assert.AreEqual(row4, mat.GetRowVector(3), "row 4");

			Matrix4D mat2 = Matrix4D.FromRowVectors(new [] {row1, row2, row3, row4});
			Assert.AreEqual(mat, mat2);
		}

		/// <summary>
		/// Tests the handling of column vectors
		/// </summary>
		[Test]
		public void TestColumnVectors()
		{
			Matrix4D mat = Matrix4D.Magic;
			Vector4D col1 = new Vector4D(16, 5, 9, 4);
			Vector4D col2 = new Vector4D(2, 11, 7, 14);
			Vector4D col3 = new Vector4D(3, 10, 6, 15);
			Vector4D col4 = new Vector4D(13, 8, 12, 1);

			Assert.AreEqual(col1, mat.GetColumnVector(0), "column 1");
			Assert.AreEqual(col2, mat.GetColumnVector(1), "column 2");
			Assert.AreEqual(col3, mat.GetColumnVector(2), "column 3");
			Assert.AreEqual(col4, mat.GetColumnVector(3), "column 4");

			Matrix4D mat2 = Matrix4D.FromColumnVectors(new[] { col1, col2, col3, col4 });
			Assert.AreEqual(mat, mat2);
		}
	}
}
