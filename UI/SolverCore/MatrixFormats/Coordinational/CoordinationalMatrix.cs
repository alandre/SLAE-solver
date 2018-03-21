using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace SolverCore
{
    public class CoordinationalMatrix : IMatrix, ILinearOperator, ITransposeLinearOperator
    {
        private Dictionary<(int row, int column), double> matrix;

        public CoordinationalMatrix(int[] rows, int[] columns, double[] values, int size)
        {
            if (rows == null) throw new ArgumentNullException(nameof(rows));
            if (columns == null) throw new ArgumentNullException(nameof(columns));
            if (values == null) throw new ArgumentNullException(nameof(values));
            if (size < 0) throw new ArgumentException("Size must be nonnegative", nameof(size));
            if (rows.Length != columns.Length || rows.Length != values.Length) throw new RankException();

            Size = size;
            matrix = new Dictionary<(int row, int column), double>();

            for(int i = 0; i < rows.Length; i++)
            {
                matrix[(rows[i], columns[i])] = values[i];
            }
        }

        public CoordinationalMatrix(Dictionary<(int i, int j), double> items, int size)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));
            if (size < 0) throw new ArgumentException("Size must be nonnegative", nameof(size));

            Size = size;
            matrix = items.ToDictionary(item => item.Key, item => item.Value);
        }

        public CoordinationalMatrix(IEnumerable<(int i, int j, double value)> items, int size)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));
            if (size < 0) throw new ArgumentOutOfRangeException("Size must be nonnegative", nameof(size));

            Size = size;
            matrix = new Dictionary<(int row, int column), double>();

            foreach(var item in items)
            {
                matrix[(item.i, item.j)] = item.value;
            }
        }

        public CoordinationalMatrix(int size)
        {
            if (size < 0) throw new ArgumentException("Size must be nonnegative", nameof(size));

            Size = size;
            matrix = new Dictionary<(int row, int column), double>();
        }

        public double this[int i, int j]
        {
            get
            {
                if (i >= Size || j >= Size || i < 0 || j < 0)
                {
                    throw new IndexOutOfRangeException();
                }

                if (matrix.TryGetValue((i, j), out var value))
                {
                    return value;
                }

                return 0;
            }
        }

        public int Size { get; }

        public int Count => matrix.Count;

        public IVector Diagonal
        {
            get
            {
                var diagonal = new Vector(Size);

                foreach (var elem in matrix)
                {
                    (int i, int j) = elem.Key;

                    if (i == j)
                    {
                        diagonal[i] = elem.Value;
                    }
                }

                return diagonal;
            }
        }

        public ILinearOperator Transpose => new TransposeMatrix<CoordinationalMatrix>() { Matrix = this };

        public void Fill(FillFunc elems)
        {
            if (elems == null)
            {
                throw new ArgumentNullException(nameof(elems));
            }

            foreach (var item in this)
            {
                matrix[(item.row, item.col)] = elems(item.row, item.col);
            }
        }

        public IEnumerator<(double value, int row, int col)> GetEnumerator()
        {
            foreach(var item in matrix)
            {
                yield return (item.Value, item.Key.row, item.Key.column);
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IVector LMult(IVector vector, bool isUseDiagonal, DiagonalElement diagonalElement = DiagonalElement.One)
        {
            if (vector == null)
            {
                throw new ArgumentNullException(nameof(vector));
            }

            if(vector.Size != Size)
            {
                throw new RankException();
            }

            var result = new Vector(vector.Size);

            foreach (var elem in matrix)
            {
                var key = elem.Key;

                if (key.row < key.column)
                {
                    continue;
                }

                if (key.column == key.row)
                {
                    result[key.row] += (isUseDiagonal ? elem.Value : (double)diagonalElement) * vector[key.row];
                }
                else
                {
                    result[key.row] += elem.Value * vector[key.column];
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

            var result = new Vector(vector.Size);

            foreach (var elem in matrix)
            {
                var key = elem.Key;

                if (key.row < key.column)
                {
                    continue;
                }

                if (key.column == key.row)
                {
                    result[key.row] += (isUseDiagonal ? elem.Value : (double)diagonalElement) * vector[key.row];
                }
                else
                {
                    result[key.column] += elem.Value * vector[key.row];
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

            var result = new Vector(vector.Size);

            foreach (var elem in matrix)
            {
                var key = elem.Key;

                if (key.row > key.column)
                {
                    continue;
                }

                if (key.column == key.row)
                {
                    result[key.row] += (isUseDiagonal ? elem.Value : (double)diagonalElement) * vector[key.row];
                }
                else
                {
                    result[key.row] += elem.Value * vector[key.column];
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

            var result = new Vector(vector.Size);

            foreach (var elem in matrix)
            {
                var key = elem.Key;

                if (key.row > key.column)
                {
                    continue;
                }

                if (key.column == key.row)
                {
                    result[key.row] += (isUseDiagonal ? elem.Value : (double)diagonalElement) * vector[key.row];
                }
                else
                {
                    result[key.column] += elem.Value * vector[key.row];
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

            var result = new Vector(vector.Size);

            foreach (var elem in matrix)
            {
                (var i, var j) = elem.Key;
                result[i] += elem.Value * vector[j];
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

            var result = new Vector(vector.Size);

            foreach (var elem in matrix)
            {
                (var i, var j) = elem.Key;
                result[j] += elem.Value * vector[i];
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

            var result = vector.Clone();
            var sum = new Vector(vector.Size);
            foreach (var elem in matrix.OrderBy(x => x.Key.row).ThenBy(x => x.Key.column))
            {
                var key = elem.Key;

                if (key.row < key.column)
                {
                    continue;
                }
                else if (key.column == key.row)
                {
                    result[key.row] = isUseDiagonal ? (vector[key.row]-sum[key.row])/elem.Value: vector[key.row] - sum[key.row] ;
                }
                else
                {
                    sum[key.row] += elem.Value * result[key.column];
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
            var sum = new Vector(vector.Size);
            foreach (var elem in matrix.OrderByDescending(x => x.Key.row).ThenByDescending(x => x.Key.column))
            {
                var key = elem.Key;

                if (key.row < key.column)
                {
                    continue;
                }
                else if (key.column == key.row)
                {
                    result[key.row] = isUseDiagonal ? (vector[key.row] - sum[key.row]) / elem.Value : vector[key.row] - sum[key.row];
                }
                else
                {
                    sum[key.column] += elem.Value * result[key.row];
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

            var result = vector.Clone();
            var sum = new Vector(vector.Size);
            foreach (var elem in matrix.OrderByDescending(x => x.Key.row).ThenByDescending(x => x.Key.column))
            {
                var key = elem.Key;

                if (key.row > key.column)
                {
                    continue;
                }
                else if (key.column == key.row)
                {
                    result[key.row] = isUseDiagonal ? (vector[key.row] - sum[key.row]) / elem.Value : vector[key.row] - sum[key.row];
                }
                else
                {
                    sum[key.row] += elem.Value * result[key.column];
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

            var result = vector.Clone();
            var sum = new Vector(vector.Size);
            foreach (var elem in matrix.OrderBy(x => x.Key.row).ThenBy(x => x.Key.column))
            {
                var key = elem.Key;

                if (key.row > key.column)
                {
                    continue;
                }
                else if (key.column == key.row)
                {
                    result[key.row] = isUseDiagonal ? (vector[key.row] - sum[key.row]) / elem.Value : vector[key.row] - sum[key.row];
                }
                else
                {
                    sum[key.column] += elem.Value * result[key.row];
                }
            }

            return result;
        }

        public string Serialize(IVector b, IVector x0)
        {
            var tmpFori = new List<int>();
            var tmpForj = new List<int>();
            var tmpForgg = new List<double>();

            foreach (var elem in matrix)
            {
                tmpFori.Add(elem.Key.column);
                tmpForj.Add(elem.Key.row);
                tmpForgg.Add(elem.Value);
            }
            double[] gg = new double[tmpForgg.Count];
            int[] i = new int[tmpForgg.Count];
            int[] j = new int[tmpForgg.Count];

            for (int k = 0; k < tmpForgg.Count; k++)
            {
                gg[k] = tmpForgg[k];
                i[k] = tmpFori[k];
                j[k] = tmpForj[k];
            }

            var obj = new { b, x0, gg, column = i, row = j };
            return JsonConvert.SerializeObject(obj);
        }
    }
}
