using System;

namespace SolverCore.MatrixFormats
{
    /// <summary>
    /// Класс реализует перевод матриц из одного формата в другой
    /// Класс статический
    /// </summary>
    public static class Convert
    {
        public static IMatrix ToDenseMatrix(IMatrix matrix)
        {
            throw new NotImplementedException();
        }

        public static IMatrix ToSymmetricDenseMatrix(IMatrix matrix)
        {
            if (!IsSymmetricMatrix(matrix, 1e-10))
            {
                throw new ArgumentException($"{matrix} is not symmetric matrix", nameof(matrix));
            }

            throw new NotImplementedException();
        }

        public static IMatrix ToCoordinationalMatrix(IMatrix matrix)
        {
            throw new NotImplementedException();
        }

        public static IMatrix ToSymmetricCoordinationalMatrix(IMatrix matrix)
        {
            throw new NotImplementedException();
        }

        public static IMatrix ToSkylineMatrix(IMatrix matrix)
        {
            throw new NotImplementedException();
        }

        public static IMatrix ToSymmetricSkylineMatrix(IMatrix matrix)
        {
            throw new NotImplementedException();
        }

        public static IMatrix ToSparseRowMatrix(IMatrix matrix)
        {
            throw new NotImplementedException();
        }

        public static IMatrix ToSymmetricSparseRowMatrix(IMatrix matrix)
        {
            throw new NotImplementedException();
        }

        public static IMatrix ToSparseRowColumnMatrix(IMatrix matrix)
        {
            throw new NotImplementedException();
        }

        public static IMatrix ToSymmetricSparseRowColumnMatrix(IMatrix matrix)
        {
            throw new NotImplementedException();
        }

        private static bool IsSymmetricMatrix(IMatrix matrix, double eps)
        {
            for (var i = 0; i < matrix.Size; i++)
            {
                for (var j = 0; j < matrix.Size; j++)
                {
                    if (Math.Abs(matrix[i, j] - matrix[j, i]) > eps)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}