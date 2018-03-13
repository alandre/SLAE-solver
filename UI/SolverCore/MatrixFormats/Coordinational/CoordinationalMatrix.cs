using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SolverCore
{
    public class CoordinationalMatrix : IMatrix, ILinearOperator, ITransposeLinearOperator
    {
        Dictionary<(int row, int column), double> matrix;

        public CoordinationalMatrix(IEnumerable<(int i, int j, double value)> items, int size)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            if (size < 0)
            {
                throw new ArgumentOutOfRangeException("Size must be nonnegative", nameof(size));
            }

            Size = size;
            matrix = items.ToDictionary(item => (item.i, item.j), item => item.value);
        }

        public CoordinationalMatrix(int size)
        {
            if (size < 0)
            {
                throw new ArgumentOutOfRangeException("Size must be nonnegative", nameof(size));
            }

            Size = size;
            matrix = new Dictionary<(int row, int column), double>();
        }

        public double this[int i, int j]
        {
            get
            {
                if (i >= Size)
                {
                    throw new IndexOutOfRangeException(nameof(i));
                }
                if (j >= Size)
                {
                    throw new IndexOutOfRangeException(nameof(j));
                }

                if (matrix.ContainsKey((i, j)))
                {
                    return matrix[(i, j)];
                }
                return 0;
            }
        }

        public int Size { get; private set; }

        public IVector Diagonal
        {
            get
            {
                var diagonal = new Vector(Size);
                foreach (KeyValuePair<(int i, int j), double> elem in this.matrix)
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

        public ILinearOperator Transpose
            => throw new NotImplementedException();

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
            foreach (KeyValuePair<(int i, int j), double> rcv in matrix)
            {
                (int i, int j) = rcv.Key;
                yield return (rcv.Value, i, j);
            }
        }

        public IVector LMult(IVector x, bool UseDiagonal, DiagonalElement diagonalElement = DiagonalElement.One)
        {
            if (x == null)
            {
                throw new ArgumentNullException(nameof(x));
            }

            var result = new Vector(x.Size);
            result.SetConst(0.0);
            foreach (KeyValuePair<(int i, int j), double> elem in this.matrix)
            {
                (int, int) key = elem.Key;
                if (key.Item2 <= key.Item1)
                {
                    if (key.Item1 == key.Item2)
                        result[key.Item1] += UseDiagonal ? elem.Value * x[key.Item2] : (double)diagonalElement * x[key.Item2];
                    else
                        result[key.Item1] += elem.Value * x[key.Item2];
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

            var result = new Vector(vector.Size);
            result.SetConst(0.0);
            foreach (KeyValuePair<(int i, int j), double> elem in this.matrix)
            {
                (int, int) key = elem.Key;
                if (key.Item2 <= key.Item1)
                {
                    if (key.Item1 == key.Item2)
                        result[key.Item2] += isUseDiagonal ? elem.Value * vector[key.Item2] : (double)diagonalElement * vector[key.Item2];
                    else
                        result[key.Item2] += elem.Value * vector[key.Item2];
                }

            }

            return result;
        }

        public IVector LSolve(IVector x, bool UseDiagonal)
        {
            throw new NotImplementedException();
        }

        public IVector LSolveTranspose(IVector vector, bool isUseDiagonal)
        {
            throw new NotImplementedException();
        }

        public IVector Multiply(IVector x)
        {
            if (x == null)
            {
                throw new ArgumentNullException(nameof(x));
            }

            var result = new Vector(x.Size);
            result.SetConst(0.0);
            foreach (KeyValuePair<(int i, int j), double> elem in this.matrix)
            {
                (int, int) key = elem.Key;
                result[key.Item1] += elem.Value * x[key.Item2];
                if (key.Item1 != key.Item2)
                    result[key.Item2] += elem.Value * x[key.Item1];
            }

            return result;
        }

        public IVector MultiplyTranspose(IVector vector)
        {
            if (vector == null)
            {
                throw new ArgumentNullException(nameof(vector));
            }

            var result = new Vector(vector.Size);
            result.SetConst(0.0);
            foreach (KeyValuePair<(int i, int j), double> elem in this.matrix)
            {
                (int, int) key = elem.Key;
                result[key.Item2] += elem.Value * vector[key.Item1];
                if (key.Item1 != key.Item2)
                    result[key.Item1] += elem.Value * vector[key.Item2];
            }

            return result;
        }

        public IVector UMult(IVector x, bool UseDiagonal, DiagonalElement diagonalElement = DiagonalElement.One)
        {
            if (x == null)
            {
                throw new ArgumentNullException(nameof(x));
            }

            var result = new Vector(x.Size);
            result.SetConst(0.0);
            foreach (KeyValuePair<(int i, int j), double> elem in this.matrix)
            {
                (int, int) key = elem.Key;
                if (key.Item2 >= key.Item1)
                {
                    if (key.Item1 == key.Item2)
                        result[key.Item1] += UseDiagonal ? elem.Value * x[key.Item2] : (double)diagonalElement * x[key.Item2];
                    else
                        result[key.Item1] += elem.Value * x[key.Item2];
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

            var result = new Vector(vector.Size);
            result.SetConst(0.0);
            foreach (KeyValuePair<(int i, int j), double> elem in this.matrix)
            {
                (int, int) key = elem.Key;
                if (key.Item2 >= key.Item1)
                {
                    if (key.Item1 == key.Item2)
                        result[key.Item2] += isUseDiagonal ? elem.Value * vector[key.Item1] : (double)diagonalElement * vector[key.Item1];
                    else
                        result[key.Item2] += elem.Value * vector[key.Item1];
                }
            }
            return result;
        }

        public IVector USolve(IVector x, bool UseDiagonal)
        {
            throw new NotImplementedException();
        }

        public IVector USolveTranspose(IVector vector, bool isUseDiagonal)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
