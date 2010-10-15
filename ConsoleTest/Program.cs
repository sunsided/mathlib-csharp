using Library.Matrix;
using Library.Vector;

namespace ConsoleTest
{
	class Program
	{
		static void Main(string[] args)
		{
			Matrix4D mat = new Matrix4D();
			Vector4D vector = new Vector4D(1f, 0f, 20f, 1f);

			mat.ToScaling(0.25f, 1f, 1f);

			Vector4D nv = mat * vector;

			;

			Matrix3D mat3 = Matrix4D.Test.GetSubmatrix(0, 2);
		}
	}
}
