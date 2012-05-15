#region Author © Verein Konstantin, 2012
//KocT9H@gmail.com
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sem_Supervised_Sites_PartB
{
    /// <summary>
    ///     Set of statistics functions
    /// </summary>
    /// 
    /// <remarks>
    ///     This class represents collection of functions used
    ///     in clustering algorithm computations.
    /// </remarks>
    /// 
    public static class Tools
    {
        //public static Dictionary<string, int> dictionary;
        public static int num_of_words_in_dictionary;
        public static string[] words_in_dictionary;

        public static LinkedList<double[,]> mLC;
        public static double[,] pca_result;

        //***************//

        #region UpdateCentroid
        public static double[] updateCentroid(double[,] matrix)
        {
            double sum=0;
            double[] newCentr = new double[matrix.GetLength(1)];
            //i = word
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                //j = site number
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    sum += matrix[j, i];
                }
                newCentr[i] = sum / matrix.GetLength(0);
                sum = 0;
            }
            return newCentr;
        }

        public static double[] updateCentroid(LinkedList<double[]> relatedPoints, int vector_len)
        {
            int i=0;
            double[,] tmp2DimList = new double[relatedPoints.Count,vector_len];
            foreach (double[] t in relatedPoints)
            {
                for (int j = 0; j < vector_len; j++)
                {
                    tmp2DimList[i, j] = t[j];
                }
                i++;
            }
            return updateCentroid(tmp2DimList);
        }
        #endregion

        //***************//

        #region Algebric Operations

        //find min index of array
        public static int MinIndex(double[] temp)
        {
            int min = 0;
            double tmp_val = 999;
            for (int i = 0; i < temp.Length; i++)
                if (temp[i] < tmp_val)
                {
                    tmp_val = temp[i];
                    min = i;
                }
            return min;
        }

        //Subtracts two vectors
        public static double[] Subtract(double[] a, double[] b)
        {
            double[] r = new double[a.Length];

            for (int i = 0; i < a.Length; i++)
                r[i] = a[i] - b[i];

            return r;
        }

        //Multiplies two vectors. Result = Matrix
        public static double[,] Multiply(double[] a, double[] b)
        {
            int m = a.GetLength(0);
           
            double[,] r = new double[m, m];

            for (int i = 0; i < m; i++)
                for (int j = 0; j < m; j++)
                    r[i, j] = a[i] * b[j];

            return r;
        }

        //Multiplies two vectors. Result = Scalar
        public static double Multiply_vec_sc(double[] a, double[] b)
        {
            int m = a.GetLength(0);

            double r=0.0;

            for (int i = 0; i < m; i++)
                r += a[i] * b[i];

            return r;
        }

        //Multiplies vector by matrix. Result = Vector
        public static double[] MultiplyVecMatr(double[] b, double[,] a)
        {
            double[] r = new double[a.GetLength(0)];
            int k=0;
            for (int i = 0; i < b.GetLength(0); i++)
            {
                for (int j = 0; j < b.GetLength(0); j++)
                {
                    r[i] += b[k] * a[i, j];
                    k++;
                }
                k = 0;
            }

            return r;
        }

        //Multiplies a matrix by a scalar.
        public static double[,] Multiply(this double[,] a, double x)
        {
            double[,] r = new double[a.GetLength(0), a.GetLength(1)];

            for (int i = 0; i < a.GetLength(0); i++)
                for (int j = 0; j < a.GetLength(1); j++)
                    r[i, j] = a[i, j] * x;

            return r;
        }

        public static double[,] AssignIMatr(int word_number)
        {
            double[,] IMatrix = new double[word_number, word_number];
            for (int r = 0; r < word_number; r++)
            {
                for (int c = 0; c < word_number; c++)
                    if (r != c)
                        IMatrix[r, c] = 0;
                IMatrix[r, r] = 1;
            }
            return IMatrix;
        }

        public static double[,] AssignZerMatr(int word_number)
        {
            double[,] ZerMatrix = new double[word_number, word_number];
            for (int r = 0; r < word_number; r++)
            {
                for (int c = 0; c < word_number; c++)
                        ZerMatrix[r, c] = 0;
            }
            return ZerMatrix;
        }

        //Calculate matrix determinant
        public static double det(double[,] A,int w)
        {
            int i, j, k, h; 
            double d = 0;
            double[,] m = new double[A.GetLength(0),A.GetLength(0)];
            if (w == 2)
                d = A[0,0]*A[1,1] - A[1,0]*A[0,1];
            else   
            {
                d = 0;
                for (k = 0 ; k < w ; k++)
                {
                    /* Create minor matrix */
                    for (i = 1 ; i < w ; i++)
                    {
                        h = 0;
                        for (j = 0; j < w; j++)
                        {
                            if (j == k)
                                continue;
                            m[i-1,h] = A[i,j];
                            h++;
                        }
                    }
                    /* sum of (+/-)cofactor * minor matrix */
                    d += Math.Pow(-1.0, k) * A[0,k] * det(m, w-1);
                }
            }
        return d;
        }


        //Adds two matrices. Oper 1 = "+", Oper 0 = "-".
        public static double[,] Add_Sub(double[,] a, double[,] b, int oper)
        {
            if (a.GetLength(0) != b.GetLength(0) || a.GetLength(1) != b.GetLength(1))
                throw new Exception("Matrix dimensions must match");

            double[,] r = new double[a.GetLength(0), a.GetLength(1)];

            for (int i = 0; i < a.GetLength(0); i++)
                for (int j = 0; j < a.GetLength(1); j++)
                    if(oper==1)
                        r[i, j] = a[i, j] + b[i, j];
                    else
                        r[i, j] = a[i, j] - b[i, j];
            return r;
        }

        public static bool Equals(double[,] a, double[,] b)
        {
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < b.GetLength(1); j++)
                {
                    if (a[i, j] != b[i, j])
                        return false;
                }
            }
            return true;
        }

        public static bool Equals(double[] a, double[] b)
        {
            for (int i = 0; i < a.GetLength(0); i++)
            {
                if (a[i] != b[i])
                    return false;
            }
            return true;
        }

        #endregion

        //***************//

        
    }
}
