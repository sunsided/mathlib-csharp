using System;
using System.Collections.Generic;
using System.Text;
using Matrix4D=Library.Matrix.Matrix4D;
using Vector4D=Library.Vector.Vector4D;

namespace ConsoleTest
{
	class Program
	{
		static void Main(string[] args)
		{
			Matrix4D mat = new Matrix4D();
			Vector4D vector = new Vector4D(1f, 0f, 20f, 1f);

			mat.ToScale(0.25f, 1f, 1f);

			Vector4D nv = mat * vector;

			;
		}
	}
}
