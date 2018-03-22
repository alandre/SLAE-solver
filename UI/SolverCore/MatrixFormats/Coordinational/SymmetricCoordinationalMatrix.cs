using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SolverCore
{
    public class SymmetricCoordinationalMatrix : IMatrix, ILinearOperator, ICloneable
    {
        private int count = -1;
        private Dictionary<(int row, int column), double> matrix;

        public SymmetricCoordinationalMatrix(int[] rows, int[] columns, double[] values, int size)
        {
            if (rows == null) throw new ArgumentNullException(nameof(rows));
            if (columns == null) throw new ArgumentNullException(nameof(columns));
            if (values == null) throw new ArgumentNullException(nameof(values));
            if (size < 0) throw new ArgumentException("Size must be nonnegative", nameof(size));
            if (rows.Length != columns.Length || rows.Length != values.Length) throw new RankException();

            Size = size;
            matrix = new Dictionary<(int row, int column), double>();

            for (int i = 0; i < rows.Length; i++)
            {
                (int row, int column) = rows[i] >= columns[i] ? (rows[i], columns[i]) : (columns[i], rows[i]);
                matrix[(row, column)] = values[i];
            }
        }

        public SymmetricCoordinationalMatrix(Dictionary<(int i, int j), double> items, int size)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));
            if (size < 0) throw new ArgumentException("Size must be nonnegative", nameof(size));

            Size = size;
            matrix = new Dictionary<(int row, int column), double>();

            foreach (var item in items)
            {
                (int row, int column) = item.Key.i >= item.Key.j ? (item.Key.i, item.Key.j) : (item.Key.j, item.Key.i);
                matrix[(row, column)] = item.Value;
            }
        }

        public SymmetricCoordinationalMatrix(IEnumerable<(int i, int j, double value)> items, int size)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));
            if (size < 0) throw new ArgumentOutOfRangeException("Size must be nonnegative", nameof(size));

            Size = size;
            matrix = new Dictionary<(int row, int column), double>();

            foreach (var item in items)
            {
                (int row, int column) = item.i >= item.j ? (item.i, item.j) : (item.j, item.i);
                matrix[(row, column)] = item.value;
            }
        }

        public SymmetricCoordinationalMatrix(int size)
        {
            if (size < 0) throw new ArgumentOutOfRangeException("Size must be nonnegative", nameof(size));

            Size = size;
            matrix = new Dictionary<(int row, int column), double>();
        }

        public int Size { get; }

        public double this[int i, int j]
        {
            get
            {
                if (i >= Size || j >= Size || i < 0 || j < 0)
                {
                    throw new IndexOutOfRangeException();
                }

                (int row, int column) = i > j ? (i, j) : (j, i);

                if (matrix.TryGetValue((row, column), out var value))
                {
                    return value;
                }

                return 0;
            }
        }

        public int Count
        {
            get
            {
                if (count < 0)
                {
                    count = matrix.Count + matrix.Where(x => x.Key.column != x.Key.row).Count();
                }

                return count;
            }
        }

        /// <summary>
        /// Устанавливает элемент в матрице, если он уже есть. Иначе -- игнорирует операцию.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="value"></param>
        /// <returns>True если значение установилось, false иначе.</returns>
        public bool Set(int row, int column, double value)
        {
            (int i, int j) = row > column ? (row, column) : (column, row);

            if (i == j || matrix.ContainsKey((i, j)))
            {
                matrix[(i, j)] = value;
                return true;
            }

            return false;
        }

        public object Clone() => new SymmetricCoordinationalMatrix(matrix, Size);

        public IVector Diagonal
        {
            get
            {
                var result = new Vector(Size);

                foreach (var item in matrix)
                {
                    if (item.Key.column == item.Key.row)
                    {
                        result[item.Key.row] = item.Value;
                    }
                }

                return result;
            }
        }

        public ILinearOperator Transpose => this;

        public void Fill(FillFunc elems)
        {
            if (elems == null)
            {
                throw new ArgumentNullException(nameof(elems));
            }

            foreach (var item in matrix.ToDictionary(x => x.Key, x => x.Value))
            {
                matrix[(item.Key.row, item.Key.column)] = elems(item.Key.row, item.Key.column);
            }
        }

        public IEnumerator<(double value, int row, int col)> GetEnumerator()
        {
            foreach (var item in matrix)
            {
                yield return (item.Value, item.Key.row, item.Key.column);

                if (item.Key.column != item.Key.row)
                {
                    yield return (item.Value, item.Key.column, item.Key.row);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IVector LMult(IVector vector, bool isUseDiagonal, DiagonalElement diagonalElement = DiagonalElement.One)
        {
            if (vector == null) throw new ArgumentNullException(nameof(vector));
            if (vector.Size != Size) throw new RankException();

            var result = new Vector(vector.Size);

            foreach (var elem in matrix)
            {
                var key = elem.Key;

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

        public IVector UMult(IVector vector, bool isUseDiagonal, DiagonalElement diagonalElement = DiagonalElement.One)
        {
            if (vector == null) throw new ArgumentNullException(nameof(vector));
            if (vector.Size != Size) throw new RankException();

            var result = new Vector(vector.Size);

            foreach (var elem in matrix)
            {
                var key = elem.Key;

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
            if (vector == null) throw new ArgumentNullException(nameof(vector));
            if (vector.Size != Size) throw new RankException();

            var result = new Vector(vector.Size);

            foreach (var elem in matrix)
            {
                (var i, var j) = elem.Key;

                result[i] += elem.Value * vector[j];

                if (i != j)
                {
                    result[j] += elem.Value * vector[i];
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

            var result = vector.Clone();
            var sum = new Vector(vector.Size);
            foreach (var elem in matrix.OrderBy(x => x.Key.row).ThenBy(x => x.Key.column))
            {
                var key = elem.Key;

                if (key.column == key.row)
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

                if (key.column == key.row)
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
    }
}
