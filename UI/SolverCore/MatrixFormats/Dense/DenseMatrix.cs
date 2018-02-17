using System;
using System.Collections;
using System.Collections.Generic;

namespace SolverCore
{
    /// <summary>
    /// Плотный формат
    /// </summary>
    public class DenseMatrix : IMatrix, ILinearOperator, ITransposeLinearOperator
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

        public DenseMatrix(int size)
        {
            matrix = new double[size, size];
        }

        public double this[int i, int j]
        {
            get
            {
                try
                {
                    return matrix[i, j];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }

        public int Size => matrix.GetLength(0);

        public ILinearOperator Transpose => new TransposeMatrix<DenseMatrix> { Matrix = this };

        public IVector Diagonal
        {
            get
            {
                var diagonal = new Vector(Size);

                for(int i = 0; i < Size; i++)
                {
                    diagonal[i] = matrix[i, i];
                }

                return diagonal;
            }
        }

        public IEnumerator<(double value, int row, int col)> GetEnumerator()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    yield return (matrix[i, j], i, j);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IVector LMult(IVector vector, bool isUseDiagonal, int diagonalElement = 1)
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
                var sum = isUseDiagonal ? matrix[i, i] * vector[i] : diagonalElement * vector[i];

                for(int j = 0; j < i; j++)
                {
                    sum += matrix[i, j] * vector[j];
                }

                result[i] = sum;
            }

            return result;
        }

        public IVector UMult(IVector vector, bool isUseDiagonal, int diagonalElement = 1)
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

            for (int i = 0; i < Size; i++)
            {
                var sum = isUseDiagonal ? matrix[i, i] * vector[i] : diagonalElement * vector[i];

                for (int j = i + 1; j < Size; j++)
                {
                    sum += matrix[i, j] * vector[j];
                }

                result[i] = sum;
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

            //можно сделать через LMult и UMult, но хз надо ли
            var result = new Vector(Size);

            for (int i = 0; i < Size; i++)
            {
                var sum = 0.0;

                for (int j = 0; j < Size; j++)
                {
                    sum += matrix[i, j] * vector[j];
                }

                result[i] = sum;
            }

            return result;
        }

        public IVector MultiplyTranspose(IVector vector)
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

            for (int j = 0; j < Size; j++)
            {
                var sum = 0.0;

                for (int i = 0; i < Size; i++)
                {
                    sum += matrix[i, j] * vector[i];
                }

                result[j] = sum;
            }

            return result;
        }

        public IVector LMultTranspose(IVector vector, bool isUseDiagonal, int diagonalElement = 1)
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

            for (int j = 0; j < Size; j++)
            {
                var sum = isUseDiagonal ? matrix[j, j] * vector[j] : diagonalElement * vector[j];

                for (int i = j; i < Size; i++)
                {
                    sum += matrix[i, j] * vector[i];
                }

                result[j] = sum;
            }

            return result;
        }

        public IVector UMultTranspose(IVector vector, bool isUseDiagonal, int diagonalElement = 1)
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

            for (int j = 0; j < Size; j++)
            {
                var sum = isUseDiagonal ? matrix[j, j] * vector[j] : diagonalElement * vector[j];

                for (int i = 0; i < j; i++)
                {
                    sum += matrix[i, j] * vector[i];
                }

                result[j] = sum;
            }

            return result;
        }

        public IVector LSolve(IVector vector, bool isUseDiagonal)
        {
            if(vector == null)
            {
                throw new ArgumentNullException(nameof(vector));
            }

            if(vector.Size != Size)
            {
                throw new RankException();
            }

            var rightPart = vector.Clone();
            var result = new Vector(Size);

            for(int j = 0; j < Size; j++)
            {
                result[j] = isUseDiagonal ? rightPart[j] / matrix[j, j] : rightPart[j];

                for(int i = j + 1; i < Size; i++)
                {
                    rightPart[i] -= matrix[i, j] * result[j];
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
                result[j] = isUseDiagonal ? rightPart[j] / matrix[j, j] : rightPart[j];

                for (int i = j - 1; i >= 0; i--)
                {
                    rightPart[i] -= matrix[i, j] * result[j];
                }
            }

            return result;
        }

        public IVector LSolveTranspose(IVector vector, bool isUseDiagonal)
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

            for (int i = Size - 1; i >= 0; i--)
            {
                result[i] = isUseDiagonal ? rightPart[i] / matrix[i, i] : rightPart[i];

                for (int j = i - 1; j >= 0; j--)
                {
                    rightPart[j] -= matrix[i, j] * result[i];
                }
            }

            return result;
        }

        public IVector USolveTranspose(IVector vector, bool isUseDiagonal)
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

            for(int i = 0; i < Size; i++)
            {
                result[i] = isUseDiagonal ? rightPart[i] / matrix[i, i] : rightPart[i];

                for (int j = i + 1; j < Size; j++)
                {
                    rightPart[j] -= matrix[i, j] * result[i];
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
                matrix[elem.row, elem.col] = elems(elem.row, elem.col);
            }
        }

        public CoordinationalMatrix ConvertToCoordinationalMatrix()
        {
            throw new NotImplementedException();
        }
    }
}
