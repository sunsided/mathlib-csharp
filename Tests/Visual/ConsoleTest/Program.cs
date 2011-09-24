using MathLib.Matrix;
using MathLib.Vector;

namespace ConsoleTest
{
	class Program
	{
		static void Main(string[] args)
		{
			{
				Matrix4D mat = new Matrix4D();
				Vector4D vector = new Vector4D(1f, 0f, 20f, 1f);

				mat.ToScaling(0.25f, 1f, 1f);

				Vector4D nv = mat*vector;

			}

			{
				Matrix4D mat = new Matrix4D(0, -7, -7, -5, 0, 14, 11, 4, 0, -1, -1, 3, -3, 4, 3, 2);
				double det = mat.GetDeterminant();
			}
			
			{
				double det = Matrix3D.Magic.GetDeterminant();
				
				det = new Matrix4D(Matrix3D.Magic).GetSubDeterminant(0, 0);
			}

			{
				Matrix4D mat3 = Matrix4D.Test;
				mat3[2, 3] = 19;
				mat3[0, 0] = 2;
				mat3[0, 1] = 4.8;
				string original = mat3.ToString();


				checked
				{
					for (int i = 0; i < 4; ++i)
					{
						// Bestimmen von R
						for (int j = i; j < 4; ++j)
						{
							for (int k = 0; k < i - 1; ++k)
							{
								mat3[i, j] -= mat3[i, k]*mat3[k, j];
							}
						}

						// Bestimmen von L
						for (int j = i + 1; j < 4; ++j)
						{
							for (int k = 0; k < i - 1; ++k)
							{
								mat3[j, i] -= mat3[j, k]*mat3[k, i];
							}
							mat3[j, i] /= mat3[i, i];
						}
					}
				}

				string gedingst = mat3.ToString();
			}


			/*
			   For i = 1 To n
			   // Bestimmen von R
			   For j = i To n
				   For k = 1 To i-1               
					   A(i,j) -= A(i,k) * A(k,j) 
				   end
			   end    
			   // Bestimmen von L
			   For j = i+1 To n
				   For k = 1 To i-1
					   A(j,i) -= A(j,k) * A(k,i)
				   end
				   A(j,i) /= A(i,i)
			   end
			end
		*/

		}
	}
}
