using System;
using System.Collections;

namespace SolverCore
{
    /// <summary>
    /// плотная симметричная
    /// </summary>
    public class SymmetricDenseMatrix : IMatrix
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
