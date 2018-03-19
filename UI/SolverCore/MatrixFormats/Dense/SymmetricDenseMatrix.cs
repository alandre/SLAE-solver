using System;
using System.Collections;
using System.Collections.Generic;

namespace SolverCore
{
    /// <summary>
    /// плотная симметричная
    /// </summary>
    public class SymmetricDenseMatrix : IMatrix, ILinearOperator
    {
        private double[][] matrix;

        public double this[int i, int j]
        {
            get
            {
                try
                {
                    return i > j ? matrix[i][j] : matrix[j][i];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }

        public int Size => matrix.Length;

        public IVector Diagonal
        {
            get
            {
                var result = new Vector(Size);

                for (int i = 0; i < Size; i++)
                {
                    result[i] = matrix[i][i];
                }

                return result;
            }
        }

        public ILinearOperator Transpose => this;

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
                if(matrix[i] == null)
                {
                    throw new ArgumentException("One of the lines is null", nameof(matrix));
                }

                if (matrix[i].Length != i + 1)
                {
                    throw new RankException(nameof(matrix));
                }

                this.matrix[i] = new double[i + 1];
                matrix[i].CopyTo(this.matrix[i], 0);
            }
        }

        public SymmetricDenseMatrix(int size)
        {
            if(size < 0)
            {
                throw new ArgumentException($"{nameof(size)} must be nonnegative");
            }

            AllocateMemory(size);
        }

        public SymmetricDenseMatrix(SymmetricCoordinationalMatrix coordinationalMatrix)
        {
            if(coordinationalMatrix == null)
            {
                throw new ArgumentNullException(nameof(coordinationalMatrix));
            }

            AllocateMemory(coordinationalMatrix.Size);

            foreach (var item in coordinationalMatrix)
            {
                if (item.row >= item.col)
                {
                    matrix[item.row][item.col] = item.value;
                }
            }
        }

        public IEnumerator<(double value, int row, int col)> GetEnumerator()
        {
            for (int i = 0; i < Size; i++)
            {
                yield return (matrix[i][i], i, i);

                for (int j = 0; j < i; j++)
                {
                    yield return (matrix[i][j], i, j);
                    yield return (matrix[i][j], j, i);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IVector LMult(IVector vector, bool isUseDiagonal, DiagonalElement diagonalElement = DiagonalElement.One)
        {
            if(vector == null)
            {
                throw new ArgumentNullException(nameof(vector));
            }

            if(vector.Size != Size)
            {
                throw new RankException();
            }

            var result = new Vector(Size);

            for(int i = 0; i < Size; i++)
            {
                var sum = isUseDiagonal ? matrix[i][i] * vector[i] : (int)diagonalElement * vector[i];

                for(int j = 0; j < i; j++)
                {
                    sum += matrix[i][j] * vector[j];
                }

                result[i] = sum;
            }

            return result;
        }

        public IVector UMult(IVector vector, bool isUseDiagonal, DiagonalElement diagonalElement = DiagonalElement.One)
        {
            if (vector == null)
            {
                throw new ArgumentNullException(nameof(vector));
            }

            if (vector.Size != Size)
            {
                throw new RankException();
            }

            var result = new Vector(Size);

            for(int i = 0; i < Size; i++)
            {
                result[i] += isUseDiagonal ? matrix[i][i] * vector[i] : (int)diagonalElement * vector[i];

                for (int j = 0; j < i; j++)
                {
                    result[j] += matrix[i][j] * vector[i];
                }
            }

            return result;
        }

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

            var result = new Vector(Size);

            for(int i = 0; i < Size; i++)
            {
                result[i] += matrix[i][i] * vector[i];

                for (int j = 0; j < i; j++)
                {
                    result[i] += matrix[i][j] * vector[j];
                    result[j] += matrix[i][j] * vector[i];
                }
            }

            return result;
        }

        public void Fill(FillFunc elems)
        {
            if(elems == null)
            {
                throw new ArgumentNullException(nameof(elems));
            }

            foreach(var elem in this)
            {
                matrix[elem.row][elem.col] = elems(elem.row, elem.col);
            }
        }

        public IVector LSolve(IVector vector, bool isUseDiagonal)
        {
            if (vector == null)
            {
                throw new ArgumentNullException(nameof(vector));
            }

            if (vector.Size != Size)
            {
                throw new RankException();
            }

            var rightPart = vector.Clone();
            var result = new Vector(Size);

            for (int j = 0; j < Size; j++)
            {
                result[j] = isUseDiagonal ? rightPart[j] / matrix[j][j] : rightPart[j];

                for (int i = j + 1; i < Size; i++)
                {
                    rightPart[i] -= matrix[i][j] * result[j];
                }
            }

            return result;
        }

        public IVector USolve(IVector vector, bool isUseDiagonal)
        {
            if (vector == null)
            {
                throw new ArgumentNullException(nameof(vector));
            }

            if (vector.Size != Size)
            {
                throw new RankException();
            }

            var rightPart = vector.Clone();
            var result = new Vector(Size);

            for (int j = Size - 1; j >= 0; j--)
            {
                result[j] = isUseDiagonal ? rightPart[j] / matrix[j][j] : rightPart[j];

                for (int i = j - 1; i >= 0; i--)
                {
                    rightPart[i] -= matrix[j][i] * result[j];
                }
            }

            return result;
        }

        private void AllocateMemory(int size)
        {
            matrix = new double[size][];

            for (int i = 0; i < size; i++)
            {
                matrix[i] = new double[i + 1];
            }
        }
    }
}
