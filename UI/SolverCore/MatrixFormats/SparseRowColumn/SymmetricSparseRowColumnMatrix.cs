using System;
using System.Collections;
using System.Collections.Generic;

namespace SolverCore
{
    public class SymmetricSparseRowColumnMatrix : IMatrix, ILinearOperator
    {
        private int[] ia;
        private int[] ja;

        private double[] di;
        private double[] aa;

        public SymmetricSparseRowColumnMatrix(
            double[] di,
            double[] aa,
            int[] ia,
            int[] ja)
        {
            if (di == null) throw new ArgumentNullException(nameof(di));
            if (aa == null) throw new ArgumentNullException(nameof(aa));
            if (ia == null) throw new ArgumentNullException(nameof(ia));
            if (ja == null) throw new ArgumentNullException(nameof(ja));

            if (ja.Length != ia[ia.Length - 1] - ia[0] ||
                ja.Length != aa.Length ||
                di.Length != ia.Length - 1)
            {
                throw new RankException();
            }

            this.di = (double[])di.Clone();
            this.aa = (double[])aa.Clone();
            this.ia = (int[])ia.Clone();
            this.ja = (int[])ja.Clone();

            if (this.ia[0] == 1)
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

            for (int i = 0; i < Size; i++)
            {
                Array.Sort(this.ja, this.ia[i], this.ia[i + 1] - this.ia[i]);
            }
        }

        public SymmetricSparseRowColumnMatrix(
        int[] ia,
        int[] ja)
        {
            if (ia == null) throw new ArgumentNullException(nameof(ia));
            if (ja == null) throw new ArgumentNullException(nameof(ja));

            if (ja.Length != ia[ia.Length - 1] - ia[0])
            {
                throw new RankException();
            }

            this.ia = (int[])ia.Clone();
            this.ja = (int[])ja.Clone();
            this.di = new double[ia.Length - 1];
            this.aa = new double[ja.Length];

            if (this.ia[0] == 1)
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

            for (int i = 0; i < Size; i++)
            {
                Array.Sort(this.ja, this.ia[i], this.ia[i + 1] - this.ia[i]);
            }
        }

        public double this[int i, int j]
        {
            get
            {
                if (i < 0 || j < 0 || i >= Size || j >= Size) throw new IndexOutOfRangeException();

                if (i == j) return di[i];

                (int start, int end, int minIJ) = i > j ? (ia[i], ia[i + 1], j) : (ia[j], ia[j + 1], i);
                var rowElementsCount = end - start;
                var number = Array.BinarySearch(ja, minIJ, start, rowElementsCount);

                return number >= 0 ? aa[number] : 0;
            }
        }

        public int Size => di.Length;

        public IVector Diagonal => new Vector(di);

        public ILinearOperator Transpose => this;

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<(double value, int row, int col)> GetEnumerator()
        {
            for (int i = 0; i < Size; i++)
            {
                yield return (di[i], i, i);

                for (int j = ia[i]; j < ia[i + 1]; j++)
                {
                    yield return (aa[j], i, ja[j]);
                }
            }
        }

        public void Fill(FillFunc elems)
        {
            if (elems == null)
            {
                throw new ArgumentNullException(nameof(elems));
            }

            for (int i = 0; i < Size; i++)
            {
                di[i] = elems(i, i);

                for (int j = ia[i]; j < ia[i + 1]; j++)
                {
                    aa[j] = elems(i, ja[j]);
                }
            }
        }

        public IVector LMult(IVector vector, bool isUseDiagonal, DiagonalElement diagonalElement = DiagonalElement.One)
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
                result[i] = isUseDiagonal ? di[i] * vector[i] : (int)diagonalElement * vector[i];

                for (int j = ia[i]; j < ia[i + 1]; j++)
                {
                    var column = ja[j];
                    result[i] += aa[j] * vector[column];
                }
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

            for (int i = 0; i < Size; i++)
            {
                result[i] = isUseDiagonal ? di[i] * vector[i] : (int)diagonalElement * vector[i];

                for (int j = ia[i]; j < ia[i + 1]; j++)
                {
                    var column = ja[j];
                    result[column] += aa[j] * vector[i];
                }
            }

            return result;
        }

        public IVector Multiply(IVector vector)
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
                result[i] = di[i] * vector[i];

                for (int j = ia[i]; j < ia[i + 1]; j++)
                {
                    var column = ja[j];

                    result[i] += aa[j] * vector[column];
                    result[column] += aa[j] * vector[i];
                }
            }

            return result;
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

            var result = new Vector(Size);

            for (int i = 0; i < Size; i++)
            {
                var sum = 0.0;

                for (int j = ia[i]; j < ia[i + 1]; j++)
                {
                    var column = ja[j];
                    sum += aa[j] * result[column];
                }

                result[i] = vector[i] - sum;
                result[i] /= isUseDiagonal ? di[i] : 1;
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

            var result = vector.Clone();

            for (int i = Size - 1; i >= 0; i--)
            {
                var diagonalElement = result[i] /= di[i];

                for (int j = ia[i]; j < ia[i + 1]; j++)
                {
                    var column = ja[j];
                    result[column] -= aa[j] * diagonalElement;
                }
            }

            return result;
        }

        public CoordinationalMatrix ConvertToCoordinationalMatrix()
        {
            throw new NotImplementedException();
        }
    }
}
