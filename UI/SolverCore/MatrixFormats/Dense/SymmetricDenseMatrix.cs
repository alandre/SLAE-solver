using System;

namespace SolverCore
{
    /// <summary>
    /// плотная симметричная
    /// </summary>
    public class SymmetricDenseMatrix : IMatrixForUser
    {
        private double[][] matrix;

        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="matrix"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="RankException"></exception>
        public SymmetricDenseMatrix(double[][] matrix)
        {
            if(matrix == null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }

            int size = matrix.Length;
            this.matrix = new double[size][];

            for(int i = 0; i < size; i++)
            {
                if (matrix[i].Length != i + 1)
                {
                    throw new RankException(nameof(matrix));
                }

                this.matrix[i] = new double[i + 1];
                matrix[i].CopyTo(this.matrix[i], 0);
            }
        }

        /// <summary>
        /// обращение по индексу
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public double this[int i, int j]
        {
            get
            {
                if(i < 0 || j < 0 || i >= Size || j >= Size)
                {
                    throw new IndexOutOfRangeException();
                }

                return matrix[Math.Max(i, j)][Math.Min(i, j)];
            }
        }

        public int Size
        {
            get
            {
                return matrix.Length;
            }
        }

        /// <summary>
        /// умножение матрицы на вектор
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="RankException"></exception>
        public IVector Multiply(IVector vector)
        {
            if(vector == null)
            {
                throw new ArgumentNullException(nameof(vector));
            }

            if(vector.Size != Size)
            {
                throw new RankException();
            }

            var result = new double[Size];

            for(int i = 0; i < Size; i++)
            {
                var sum = 0.0;

                for(int j = 0; j < Size; j++)
                {
                    sum += matrix[Math.Max(i, j)][Math.Min(i, j)] * vector[j];
                }

                result[i] = sum;
            }

            return new Vector(result);
        }

        public IVector UpperMult(IVector vector)
        {
            throw new NotImplementedException();
        }

        public IVector LowerMult(IVector vector)
        {
            throw new NotImplementedException();
        }

        public IMatrix ConvertTo(string matrixFormat)
        {
            throw new NotImplementedException();
        }
    }
}
