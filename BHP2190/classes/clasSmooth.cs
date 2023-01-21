using System;
using System.Collections.Generic;
using System.Text;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Transformations;
using System.IO;

namespace BHP2190.classes
{
    public class clasSmooth
    {
        public double[] AR, BR;
       /* public double[] ChovitzkySmooth(double[] Ys)
        {

           /* int d = (int)Math.Ceiling(Math.Sqrt(Ys.Length));
            double[,] hermitMatrix = new double[d, d];
            double[,] diffRowsMatrix = new double[d, d];
            double[,] mul2Matrix = new double[d, d];
            int len = 0;

            double smoothwidth = 10;
            if (Ys.Length % 2 != 0) len = Ys.Length - 1;
            else len = Ys.Length;

            double[,] matrx = new double[d, d];
            for (int i = 0; i < d; i++)
                for (int k = 0; k < d; k++)
                    matrx[i, k] = Ys[(i * d) + k];

            Matrix x = Matrix.Create(matrx);
            Matrix r = DefhermMatrix(d);
            Matrix y = smoothwidth * MultiplyMatrices(d, x, Matrix.Transpose(x));
            r.Add(y);
          //  Matrix t = r.CholeskyDecomposition.GetL();
            //SingularValueDecomposition xx = new SingularValueDecomposition(x);

            //Matrix a = MultiplyMatrices(d, Matrix.Transpose(t), x.Inverse());
            //r = MultiplyMatrices(d, t, a.Inverse());
            double[] sy = new double[d * d];
            for (int i = 0; i < d; i++)
                for (int j = 0; j < d; j++)
                    sy[(i * d) + j] = t[i, j];
            return sy;
        }*/

        public Matrix DifRows_Matrix(int d, Matrix dif)
        {
            Matrix df = new Matrix(d, d);
            df = dif;
            for (int i = 0; i < d - 1; i++)
                for (int j = 0; j < d; j++)
                    df[i, j] = df[i + 1, j] - df[i, j];
            for (int j = 0; j < d; j++)
                df[d - 1, j] = 0;
            return Matrix.Create(df);
        }

        public Matrix DefhermMatrix(int d)
        {
            double[,] hm = new double[d, d];
            for (int i = 0; i < d; i++)
                for (int j = 0; j < d; j++)
                    if (i == j)
                        hm[i, j] = 1;
                    else
                        hm[i, j] = 0;
            Matrix hmm = Matrix.Create(hm);
            return hmm;
        }

        public Matrix MultiplyMatrices(int D, Matrix m1, Matrix m2)
        {
            Matrix r = new Matrix(D, D);
            for (int i = 0; i < D; i++)
            {
                for (int j = 0; j < D; j++)
                {
                    r[i, j] = 0;
                    for (int k = 0; k < D; k++)
                    {
                        r[i, j] += m1[i, k] * m2[k, j];
                    }
                }
            }
            return r;
        }

        public void ncube_SPLine(double[] x, double[] y)
        {
            int n = x.Length;
            double[] alpha = new double[n];
            double[] l = new double[n];
            double[] u = new double[n];
            double[] z = new double[n];
            u[0] = 0; z[0] = 0; l[0] = 0;
            for (int i = 1; i < n - 1; i++)
                alpha[i - 1] = 3 * ((y[i + 1] * (x[i] - x[i - 1])) - (y[i] * (x[i + 1] - x[i - 1])) + (y[i - 1] * (x[i + 1] - x[i]))) / ((x[i + 1] - x[i]) * (x[i] - x[i - 1]));
            for (int i = 1; i < n - 1; i++)
            {
                l[i] = 2 * (x[i + 1] - x[i - 1]) - (u[i - 1] * (x[i] - x[i - 1]));
                u[i] = (1 / l[i]) * (x[i + 1] - x[i]);
                z[i] = (1 / l[i]) * (alpha[i] - (x[i] - x[i - 1] * z[i - 1]));
            }
            double[] c = new double[n];
            double[] b = new double[n];
            double[] d = new double[n];
            c[n - 1] = z[n - 1];
            z[n - 1] = 0; l[n - 1] = 1;
            for (int j = n - 2; j >= 0; j--)
            {
                c[j] = z[j] - (u[j] * c[+1]);
                b[j] = ((y[j + 1] - y[j]) / (x[j + 1] - x[j])) - (((x[j + 1] - x[j]) * (c[j + 1] + 2 * c[j])) / 3);
                d[j] = (c[j + 1] - c[j]) / (3 * (x[j + 1] - x[j]));
            }
            double[] S = new double[n];
            for (int j = 0; j < n; j++)
                S[j] = y[j] + (b[j] * x[j]) + (c[j] * x[j] * x[j]) + (d[j] * x[j] * x[j] * x[j]);

        }

        public void fourier_OLD(double[] A, double[] B)
        {
            double[] c = new double[A.Length];
            int j = 0, k = 0;


            double[] sumCos = new double[12000];
            double[] sumSin = new double[12000];
            double[] sum = new double[12000];
            double total = 0;
            double[] omegaVal = new double[12000];
            double[] f = new double[12000];

            for (double omega = 0.01; omega < 52; omega += 0.005)
            {
                omegaVal[k] = omega;
                for (j = 0; j < A.Length - 2; j++)
                {
                    sumCos[k] = sumCos[k] + (B[j] * Math.Cos(omega * A[j]) * (A[j + 1] - A[j])) / Math.PI;
                    sumSin[k] = sumSin[k] + (B[j] * Math.Sin(omega * A[j]) * (A[j + 1] - A[j])) / Math.PI;
                }
                sum[k] = Math.Sqrt(Math.Pow(sumCos[k], 2) + Math.Pow(sumSin[k], 2));
                k++;
            }

            for (j = 0; j < A.Length; j++)
            {
                total = 0;
                for (int l = 0; omegaVal[l] < 50; l++)
                    total = total + (omegaVal[l + 1] - omegaVal[l]) * (sumCos[l] * Math.Cos(omegaVal[l] * A[j]) + sumSin[l] * Math.Sin(omegaVal[l] * A[j]));
                f[j] = total;
            }
            AR = A;
            BR = f;
        }
    }
}
