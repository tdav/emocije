using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmoClassifier
{
    public class GaussDistrib 
    {
        public static double[,] Probability (double[,] data, double[,] mu, double[,,] sigma, int k)
        {
         
            double[,] mu1 = new double [mu.GetLength(0),1];
            double[,] sigma1 = new double [sigma.GetLength(0),sigma.GetLength(1)];
       
            double p1;
            double[,] p2;

            double len = data.GetLength(0);

            for (int i = 0; i < mu.GetLength(0); i++)
            {
                mu1[i,1] = mu[i,k];
            }

            for (int i = 0; i < sigma.GetLength(0); i++)
            {
                for (int j = 0; j < sigma.GetLength(1); j++)
                {
                    sigma1[i, j] = sigma[i, j, k];
                }
            }

            double[,] sub = Matrix.Subtract(data,mu1);

            p1 = (Math.Pow(len, 2 * Math.PI)) * Matrix.Det(sigma1);

            p2 = Matrix.Multiply(Matrix.Transpose(sub), Matrix.Inverse(sigma1));
            p2 = Matrix.Multiply(p2, sub);
            p2 = Matrix.ScalarMultiply(-0.5,p2);

            int rows = p2.GetLength(0);
            int col = p2.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    p2[i, j] *= Math.Exp(p2[i, j]);
                }
            }

                double [,] prob = Matrix.ScalarDivide(p1,p2);

            return prob;
        }
    }
}
