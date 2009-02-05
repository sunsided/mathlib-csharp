using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest
{
	class Program
	{
		static void Main(string[] args)
		{
			Library.Matrix4D mat = new Library.Matrix4D();
			Library.Vector4D vector = new Library.Vector4D(1f, 0f, 20f, 1f);

			mat.ToScale(0.25f, 1f, 1f);

			Library.Vector4D nv = mat * vector;

			;
		}
	}
}
