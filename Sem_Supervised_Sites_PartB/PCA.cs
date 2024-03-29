﻿using System;
using System.Collections.Generic;
using System.Linq;



namespace PCA_Logic
{
    /// <summary>
    ///   Principal component analysis (PCA) is a technique used to reduce
    ///   multidimensional data sets to lower dimensions for analysis.
    /// </summary>
    /// <remarks>
    ///   Principal Components Analysis or the Karhunen-Loeve expansion is a
    ///   classical method for dimensionality reduction or exploratory data
    ///   analysis.
    ///  
    ///   Mathematically, PCA is a process that decomposes the covariance matrix of a matrix
    ///   into two parts: eigenvalues and column eigenvectors, whereas Singular Value Decomposition
    ///   (SVD) decomposes a matrix per se into three parts: singular values, column eigenvectors,
    ///   and row eigenvectors. The relationships between PCA and SVD lie in that the eigenvalues 
    ///   are the square of the singular values and the column vectors are the same for both.   
    ///   
    ///   This class uses SVD on the data set which generally gives better numerical accuracy.
    ///</remarks>
    public class PCA
    {
        /*Row=words, Col= Web pages*/

        public double[,] covarianceMatrix;
        public double[] eigenValues;
        public double[,] eigenVectors;
        private double[,] sourceMatrix;
        private double[,] resultMatrix;
        private int dim_num;

        public PCA(double[,] matrix, int dim_num)
        {
            this.sourceMatrix = Transpose(matrix);
            this.dim_num = dim_num;
        }

        public double[,]Transpose(double[,] m)
        {
            double[,] t = new double[m.GetLength(1), m.GetLength(0)];
            for (int i = 0; i < m.GetLength(0); i++)
                for (int j = 0; j < m.GetLength(1); j++)
                    t[j, i] = m[i, j];

            return t;
        }


        //Method activates all PCA computation
        public void compute()
        {
            
            this.covarianceMatrix = CreateCovMatrix(sourceMatrix);
 
            Accord.Math.Decompositions.EigenValueDecomposition evd =
                new Accord.Math.Decompositions.EigenValueDecomposition(this.covarianceMatrix);

            this.eigenValues = evd.RealEigenValues;                      
            //Each column is corresponding eigen vector
            this.eigenVectors = Transpose(evd.EigenVectors);

            this.resultMatrix = multiplyVectorByEigenvector(sourceMatrix);
        }

        #region CreateCovMatr
        public double[,] CreateCovMatrix(double[,] matrix)
        {
            return Covariance(matrix, Mean(matrix));
        }

        public double[,] Covariance(double[,] matrix, double[] mean)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            if (rows == 1)
            {
                throw new ArgumentException("Sample has only one observation.", "matrix");
            }
            double N = cols;
            double[,] cov = new double[rows, rows];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    double c = 0.0;
                    for (int k = 0; k < cols; k++)
                    {
                        c += (matrix[j, k] - mean[j]) * (matrix[i, k] - mean[i]);
                    }
                    c /= N-1;
                    cov[i, j] = c;
                }
            }

            return cov;
        }

        public double[] Mean(double[,] value)
        {
            double[] mean = new double[value.GetLength(0)];
            double col = value.GetLength(1);

            // for each row
            for (int j = 0; j < value.GetLength(0); j++)
            {
                // for each col
                for (int i = 0; i < value.GetLength(1); i++)
                {
                    mean[j] += value[j, i];
                }

                mean[j] = mean[j] / col;
               
            }

            return mean;
        }
       #endregion

        //---------------------------------------------

        #region multiplyVectorByEigenvector

        public double[,] multiplyVectorByEigenvector(double[,] matrix)
        {
            double[,] C = new double[this.dim_num, matrix.GetLength(1)];

            int[] max_ind_arr = Max(this.eigenValues);

            //create result matrix

            for (int i = 0; i < this.dim_num; i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    for (int k = 0; k < matrix.GetLength(0); k++)
                        C[i, j] += matrix[k, j] * this.eigenVectors[max_ind_arr[i], k];
            
            
            C = Transpose(C);

  
            return C;
        }
        
        private int[] Max(double[] numbers)
        {
            int capacity = this.dim_num;
            //array of desired dimension number
            double[] temp_arr = new double[numbers.Length];
            int[] max_ind_arr = new int[capacity];
            for (int i = 0; i < numbers.Length; i++)
                temp_arr[i] = numbers[i];
            //sort biggest eigenvalues
            Array.Sort(temp_arr);
            Array.Reverse(temp_arr);
            for (int j = 0; j < capacity; j++)
                for (int i = 0; i < numbers.Length; i++)
                    //return index of biggest eigenvalue
                    if (temp_arr[j] == numbers[i])
                        max_ind_arr[j] = i;
            return max_ind_arr;
        }

        #endregion

        //---------------------------------------------

        public double[,] Result
        {
            get { return this.resultMatrix; }
            
        }

       
    }
}
