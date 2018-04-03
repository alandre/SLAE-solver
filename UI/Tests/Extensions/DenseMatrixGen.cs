using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolverCore;

namespace Extensions
{
    
     internal static class DenseMatrixGen
     {
        private static Random rand = new Random();

        public static DenseMatrix DiagonalMatrix(int size)
        {
            double[,] matrix = new double[size, size];

            for (int i = 0; i < size; i++)
                matrix[i, i] = rand.Next(1, 50);
            return new DenseMatrix(matrix);
        }

        public static DenseMatrix LowCondMatrix(int size)
        {
            double[,] matrix = new double[size, size];

            for (int i = 0; i < size; i++)
            {
                for(int j=0;j<size;j++)
                {
                    matrix[i, j] = rand.Next(-10, -1);
                }
                matrix[i, i] = rand.Next(1, 50);

            }
            return new DenseMatrix(matrix);
        }

        public static DenseMatrix HightCondMatrix(int size)
        {
            double[,] matrix = new double[size, size];

            for (int i = 1; i <= size; i++)
            {
                for (int j = 1; j <= size; j++)
                {
                    matrix[i - 1, j - 1] = 1.0 / (i + j - 1);
                }
            }
            return new DenseMatrix(matrix);
        }

        public static DenseMatrix NotDiagonallyDominantMatrix(int size)
        {
            double[,] matrix = new double[size, size];

            for (int i = 0; i < size; i++)
            {
                matrix[i, i] = rand.Next(1, 10);
            }

            for (int i = size * 4 / 5; i < size; i++) 
            {
                for (int j = 0; j < size / 5; j++) 
                    matrix[i, j] = matrix[j, i] = rand.Next(20, 60);
            }
                
            return new DenseMatrix(matrix);
        }
        public static DenseMatrix SingularMatrix(int size)
        {
            double[,] matrix = new double[size, size];

            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                {
                    matrix[i, j] = rand.Next(1, 10);
                }

            for (int i = 0; i < size; i++)
            {
                matrix[0, i] = 0;
            }

            return new DenseMatrix(matrix);
        }


    }
}
