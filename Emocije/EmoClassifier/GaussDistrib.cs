using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmoClassifier
{
    public class GaussDistrib 
    {
        public static double[,] Probability (double[,] data, double[,] mu, double[,] sigma)
        {
         
            double len = data.GetLength(0);

            double p1;
            double[,] p2;
            double[,] sub = Matrix.Subtract(data,mu);

            p1 = (Math.Pow(len, 2 * Math.PI)) * Matrix.Det(sigma);

            p2 = Matrix.Multiply(Matrix.Transpose(sub), Matrix.Inverse(sigma));
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
