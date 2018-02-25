using System;
using System.Collections;
using System.Collections.Generic;

namespace SolverCore
{
    /// <summary>
    /// Разреженный строчно-столбцовый формат матрицы с симметричным профилем.
    /// </summary>
    public class SparseRowColumnMatrix : IMatrix, ILinearOperator, ITransposeLinearOperator
    {
        private int[] ia;
        private int[] ja;

        private double[] di;
        private double[] al;
        private double[] au;

        public IVector Diagonal => new Vector(di);

        public ILinearOperator Transpose => new TransposeMatrix<SparseRowColumnMatrix>() { Matrix = this };

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public int Size => di.Length;

        public SparseRowColumnMatrix(
            double[] di,
            double[] al,
            double[] au,
            int[] ia,
            int[] ja)
        {
            if (di == null) throw new ArgumentNullException(nameof(di));
            if (al == null) throw new ArgumentNullException(nameof(al));
            if (au == null) throw new ArgumentNullException(nameof(au));
            if (ia == null) throw new ArgumentNullException(nameof(ia));
            if (ja == null) throw new ArgumentNullException(nameof(ja));

            if (ja.Length != ia[ia.Length - 1] - ia[0] || 
                ja.Length != al.Length || 
                ja.Length != au.Length ||
                di.Length != ia.Length - 1)
            {
                throw new RankException();
            }

            this.di = (double[])di.Clone();
            this.al = (double[])al.Clone();
            this.au = (double[])au.Clone();
            this.ia = (int[])ia.Clone();
            this.ja = (int[])ja.Clone();

            if(this.ia[0] == 1)
            {
                for (int i = 0; i < this.ia.Length; i++)
                {
                    this.ia[i]--;
                }

                for (int j = 0; j < this.ja.Length; j++)
                {
                    this.ja[j]--;
                }
            }
        }

        public double this[int i, int j]
        {
            get
            {
                if (i < 0 || j < 0 || i >= Size || j >= Size)
                {
                    throw new IndexOutOfRangeException();
                }

                if (i == j)
                {
                    return di[i];
                }

                (int start, int end, int minIJ) = i > j ? (ia[i], ia[i + 1], j) : (ia[j], ia[j + 1], i);
                var rowElementsCount = end - start;
                var number = Array.IndexOf(ja, minIJ, start, rowElementsCount);

                return number > 0 ? (i > j ? al[number] : au[number]) : 0;
            }
        }

        public IEnumerator<(double value, int row, int col)> GetEnumerator()
        {
            for (int i = 0; i < Size; i++)
            {
                yield return (di[i], i, i);

                var rowColumnElements = ia[i + 1] - ia[i];

                for (int j = 0; j < rowColumnElements; j++)
                {
                    var index = ia[i] + j;

                    yield return (al[index], i, ja[index]);
                    yield return (au[index], ja[index], i);
                }
            }
        }

        public void Fill(FillFunc elems)
        {
            if(elems == null)
            {
                throw new ArgumentNullException(nameof(elems));
            }

            for (int i = 0; i < Size; i++)
            {
                di[i] = elems(i, i);
                var rowColumnElements = ia[i + 1] - ia[i];

                for (int j = 0; j < rowColumnElements; j++)
                {
                    var index = ia[i] + j;

                    al[index] = elems(i, ja[index]);
                    au[index] = elems(ja[index], i);
                }
            }
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
                result[i] += di[i] * vector[i];
                var rowElementsCount = ia[i + 1] - ia[i];

                for (int j = 0; j < rowElementsCount; j++)
                {
                    var index = ia[i] + j;
                    var column = ja[index];

                    result[i] += al[index] * vector[column];
                    result[column] += au[index] * vector[i];
                }
            }

            return result;
        }

        public IVector MultiplyTranspose(IVector vector)
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

            for (int i = 0; i < Size; i++)
            {
                result[i] += di[i] * vector[i];
                var rowElementsCount = ia[i + 1] - ia[i];

                for (int j = 0; j < rowElementsCount; j++)
                {
                    var index = ia[i] + j;
                    var column = ja[index];

                    result[i] += au[index] * vector[column];
                    result[column] += al[index] * vector[i];
                }
            }

            return result;
        }

        public IVector LMult(IVector vector, bool isUseDiagonal, DiagonalElement diagonalElement)
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
                result[i] += isUseDiagonal ? di[i] * vector[i] : (int)diagonalElement * vector[i];
                var rowElementsCount = ia[i + 1] - ia[i];

                for (int j = 0; j < rowElementsCount; j++)
                {
                    var index = ia[i] + j;
                    var column = ja[index];

                    result[i] += al[index] * vector[column];
                }
            }

            return result;
        }

        public IVector UMult(IVector vector, bool isUseDiagonal, DiagonalElement diagonalElement)
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

            for (int i = 0; i < Size; i++)
            {
                result[i] += isUseDiagonal ? di[i] * vector[i] : (int)diagonalElement * vector[i];
                var rowElementsCount = ia[i + 1] - ia[i];

                for (int j = 0; j < rowElementsCount; j++)
                {
                    var index = ia[i] + j;
                    var column = ja[index];

                    result[column] += au[index] * vector[i];
                }
            }

            return result;
        }

        public IVector LMultTranspose(IVector vector, bool isUseDiagonal, DiagonalElement diagonalElement)
        {
            throw new NotImplementedException();
        }

        public IVector LSolve(IVector vector, bool isUseDiagonal)
        {
            throw new NotImplementedException();
        }

        public IVector LSolveTranspose(IVector vector, bool isUseDiagonal)
        {
            throw new NotImplementedException();
        }

        public IVector UMultTranspose(IVector vector, bool isUseDiagonal, DiagonalElement diagonalElement)
        {
            throw new NotImplementedException();
        }

        public IVector USolve(IVector vector, bool isUseDiagonal)
        {
            throw new NotImplementedException();
        }

        public IVector USolveTranspose(IVector vector, bool isUseDiagonal)
        {
            throw new NotImplementedException();
        }

        public CoordinationalMatrix ConvertToCoordinationalMatrix()
        {
            throw new NotImplementedException();
        }
    }
}
