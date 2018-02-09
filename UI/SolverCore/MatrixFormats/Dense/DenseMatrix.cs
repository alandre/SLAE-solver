using System;
using System.Collections;

namespace SolverCore
{
    /// <summary>
    /// Плотный формат
    /// </summary>
    public class DenseMatrix : IMatrix
    {
        private double[,] matrix;

        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="matrix"></param>
        /// <exception cref="ArgumentNullException">если matrix == null</exception>
        /// <exception cref="ArgumentException">если передаваемая матрица не квадратна</exception>
        public DenseMatrix(double[,] matrix)
        {
            if (matrix == null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }

            if (matrix.GetLength(0) != matrix.GetLength(1))
            {
                throw new ArgumentException("matrix must be square", nameof(matrix));
            }

            var size = matrix.GetLength(0);
            this.matrix = new double[size, size];

            //пока сам написал копирование
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    this.matrix[i, j] = matrix[i, j];
                }
            }
        }

        public double this[int i, int j] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int Size => throw new NotImplementedException();

        public IVector Diagonal => throw new NotImplementedException();

        public ILinearOperator Transpose => throw new NotImplementedException();

        public System.Collections.Generic.IEnumerator<(double value, int row, int col)> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public IVector LMult(IVector x, bool UseDiagonal)
        {
            throw new NotImplementedException();
        }

        public IVector LSolve(IVector x, bool UseDiagonal)
        {
            throw new NotImplementedException();
        }

        public IVector Multiply(IVector x)
        {
            throw new NotImplementedException();
        }

        public IVector UMult(IVector x, bool UseDiagonal)
        {
            throw new NotImplementedException();
        }

        public IVector USolve(IVector x, bool UseDiagonal)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
