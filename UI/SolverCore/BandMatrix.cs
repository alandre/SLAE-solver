using System;

namespace SolverCore
{
    public class BandMatrix : IMatrixForUser
    {
        private readonly double[,] matrix;

        public double this[int i, int j]
        {
            get
            {
                if(i < 0 || j < 0 || i >= Size || j >= Size)
                {
                    throw new IndexOutOfRangeException();
                }

                return matrix[i, j];
            }
        }

        public int Size
        {
            get
            {
                return matrix.GetLength(0);
            }
        }

        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="matrix"></param>
        /// <exception cref="ArgumentNullException">если matrix == null</exception>
        /// <exception cref="ArgumentException">если передаваемая матрица не квадратна</exception>
        public BandMatrix(double[,] matrix)
        {
            if(matrix == null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }

            if(matrix.GetLength(0) != matrix.GetLength(1))
            {
                throw new ArgumentException("matrix must be square", nameof(matrix));
            }

            var size = matrix.GetLength(0);
            this.matrix = new double[size, size];
            
            //пока сам написал копирование
            for(int i = 0; i < size; i++)
            {
                for(int j = 0; j < size; j++)
                {
                    this.matrix[i, j] = matrix[i, j];
                }
            }
        }

        /// <summary>
        /// умножение матрицы на вектор
        /// </summary>
        /// <param name="vector"> множитель</param>
        /// <returns>результат - вектор</returns>
        /// <exception cref="ArgumentNullException">если vector == null</exception>
        /// <exception cref="RankException">если размерности не соотносятся</exception>
        public IVector Multiply(IVector vector)
        {
            if (vector == null)
            {
                throw new ArgumentNullException(nameof(vector));
            }

            if (vector.Size != Size)
            {
                throw new RankException(nameof(vector));
            }

            var result = new double[Size];

            for(int i = 0; i < Size; i++)
            {
                var sum = 0.0;

                for(int j = 0; j < Size; j++)
                {
                    sum += this[i, j] * vector[i];
                }

                result[i] = sum;
            }

            return new Vector(result);
        }

        public IMatrix ConvertTo(string matrixFormat)
        {
            throw new NotImplementedException();
        }
    }
}
