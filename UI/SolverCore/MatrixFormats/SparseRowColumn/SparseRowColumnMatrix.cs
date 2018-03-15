using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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

        public SparseRowColumnMatrix(CoordinationalMatrix coordinationalMatrix)
        {
            if (coordinationalMatrix == null)
            {
                throw new ArgumentNullException(nameof(coordinationalMatrix));
            }

            var size = coordinationalMatrix.Size;

            ia = new int[size + 1];
            di = new double[size];
            var itemsSymmetricProfile = new Dictionary<(int i, int j), (double al, double au)>();

            foreach (var item in coordinationalMatrix)
            {
                if (item.row != item.col)
                {
                    (int i, int j) = item.row > item.col ? (item.row, item.col) : (item.col, item.row);

                    if (!itemsSymmetricProfile.ContainsKey((i, j)))
                    {
                        itemsSymmetricProfile[(i, j)] = (0, 0);
                    }

                    if (item.row > item.col)
                    {
                        var au = itemsSymmetricProfile[(i, j)].au;
                        itemsSymmetricProfile[(i, j)] = (item.value, au);
                    }
                    else
                    {
                        var al = itemsSymmetricProfile[(i, j)].al;
                        itemsSymmetricProfile[(i, j)] = (al, item.value);
                    }
                }
                else
                {
                    di[item.row] = item.value;
                }
            }

            var orderedItems = itemsSymmetricProfile.OrderBy(x => x.Key.i).ThenBy(x => x.Key.j);
            var count = orderedItems.Count();

            ja = new int[count];
            al = new double[count];
            au = new double[count];

            int k = 0;

            foreach (var item in orderedItems)
            {
                ja[k] = item.Key.j;
                al[k] = item.Value.al;
                au[k] = item.Value.au;

                ia[item.Key.i + 1]++;
                k++;
            }

            for(int i = 0; i < size; i++)
            {
                ia[i + 1] += ia[i];
            }
        }

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
                Sorter.QuickSort(this.ja, this.ia[i], this.ia[i + 1] - 1, this.al, this.au);
                //Array.Sort(this.ja, this.aa, this.ia[i], this.ia[i + 1] - this.ia[i]);
            }
        }

        public SparseRowColumnMatrix(
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
            this.al = new double[ja.Length];
            this.au = new double[ja.Length];

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

                return number >= 0 ? (i > j ? al[number] : au[number]) : 0;
            }
        }

        public IEnumerator<(double value, int row, int col)> GetEnumerator()
        {
            for (int i = 0; i < Size; i++)
            {
                yield return (di[i], i, i);

                for (int j = ia[i]; j < ia[i + 1]; j++)
                {
                    yield return (al[j], i, ja[j]);
                    yield return (au[j], ja[j], i);
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

                for (int j = ia[i]; j < ia[i + 1]; j++)
                {
                    al[j] = elems(i, ja[j]);
                    au[j] = elems(ja[j], i);
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

            for (int i = 0; i < Size; i++)
            {
                result[i] = di[i] * vector[i];

                for (int j = ia[i]; j < ia[i + 1]; j++)
                {
                    var column = ja[j];

                    result[i] += al[j] * vector[column];
                    result[column] += au[j] * vector[i];
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
                result[i] = di[i] * vector[i];

                for (int j = ia[i]; j < ia[i + 1]; j++)
                {
                    var column = ja[j];
                    result[i] += au[j] * vector[column];
                    result[column] += al[j] * vector[i];
                }
            }

            return result;
        }

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

            for (int i = 0; i < Size; i++)
            {
                result[i] = isUseDiagonal ? di[i] * vector[i] : (int)diagonalElement * vector[i];

                for (int j = ia[i]; j < ia[i + 1]; j++)
                {
                    var column = ja[j];
                    result[i] += al[j] * vector[column];
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
                    result[column] += au[j] * vector[i];
                }
            }

            return result;
        }

        public IVector LMultTranspose(IVector vector, bool isUseDiagonal, DiagonalElement diagonalElement = DiagonalElement.One)
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
                    result[column] += al[j] * vector[i];
                }
            }

            return result;
        }

        public IVector UMultTranspose(IVector vector, bool isUseDiagonal, DiagonalElement diagonalElement = DiagonalElement.One)
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
                    result[i] += au[j] * vector[column];
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
                    sum += al[j] * result[column];
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

            for(int i = Size - 1; i >= 0; i--)
            {
                var diagonalElement = result[i] /= di[i];

                for(int j = ia[i]; j < ia[i + 1]; j++)
                {
                    var column = ja[j];
                    result[column] -= au[j] * diagonalElement;
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

            var result = vector.Clone();

            for (int i = Size - 1; i >= 0; i--)
            {
                var diagonalElement = result[i] /= di[i];

                for (int j = ia[i]; j < ia[i + 1]; j++)
                {
                    var column = ja[j];
                    result[column] -= al[j] * diagonalElement;
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

            var result = new Vector(Size);

            for (int i = 0; i < Size; i++)
            {
                var sum = 0.0;

                for (int j = ia[i]; j < ia[i + 1]; j++)
                {
                    var column = ja[j];
                    sum += au[j] * result[column];
                }

                result[i] = vector[i] - sum;
                result[i] /= isUseDiagonal ? di[i] : 1;
            }

            return result;
        }
    }
}
