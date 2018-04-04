using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;

namespace SolverCore
{
    /// <summary>
    /// Профильный
    /// </summary>
    public class SkylineMatrix : IMatrix, ILinearOperator, ITransposeLinearOperator
    {
        private double[] di; // диагональ
        private double[] al; // массив элементов профиля нижнего треугольника
        private double[] au; // массив элементов профиля верхнего треугольника
        private int[] ia; // целочисленный массив с указателями начала строк профиля 

        /// конструктор
        public SkylineMatrix(double[] di, int[] ia, double[] al, double[] au)
        {
            if (di == null)
            {
                throw new ArgumentNullException(nameof(di));
            }

            if (ia == null)
            {
                throw new ArgumentNullException(nameof(ia));
            }

            if (al == null)
            {
                throw new ArgumentNullException(nameof(al));
            }

            if (au == null)
            {
                throw new ArgumentNullException(nameof(au));
            }

            var size = di.Length;
            this.di = (double[])di.Clone();

            var size1 = ia.Length;
            if (size1 != size + 1)
            {
                throw new RankException();
            }
            this.ia = (int[])ia.Clone();
            if (this.ia[0] == 1) //если массив начинается с 1, то уменьшаем значения всех элементов на 1
            {
                for (int i = 0; i < size1; i++) this.ia[i]--;
            }

            var size2 = al.Length;
            var size3 = au.Length;
            if (this.ia[size1 - 1] != size2)
            {
                throw new RankException();
            }
            if (this.ia[size1 - 1] != size2 || this.ia[size1 - 1] != size3)
            {
                throw new RankException();
            }
            this.al = (double[])al.Clone();

            this.au = (double[])au.Clone();
        }

        public double this[int i, int j]
        {
            get
            {
                try
                {
                    if (i == j) return di[i]; // если диагональ

                    if (i > j)
                    {
                        if (ia[i + 1] - ia[i] == 0) return 0; // если данный элемент не содержится в профиле, значит, он нулевой

                        int k = i - (ia[i + 1] - ia[i]); // индекс столбца первого элемента в профиле
                        return al[ia[i] + j - k];
                    }
                    else
                    {
                        if (ia[j + 1] - ia[j] == 0) return 0; // если данный элемент не содержится в профиле, значит, он нулевой

                        int k = j - (ia[j + 1] - ia[j]); // индекс строки первого элемента в профиле
                        return au[ia[j] + i - k];
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    throw new IndexOutOfRangeException(); //если выходим за рамки размерности матрицы
                }
            }
        }

        /// конструктор
        public SkylineMatrix(int[] ia)
        {
            if (ia == null)
            {
                throw new ArgumentNullException(nameof(ia));
            }

            this.ia = (int[])ia.Clone();
            this.di = new double[ia.Length - 1];
            this.al = new double[ia[ia.Length - 1]];
            this.au = new double[ia[ia.Length - 1]];

            if (this.ia[0] == 1)
            {
                for (int i = 0; i < ia.Length; i++)
                {
                    ia[i]--;
                }
            }
        }

        public SkylineMatrix(CoordinationalMatrix coordinationalMatrix)
        {
            if (coordinationalMatrix == null)
            {
                throw new ArgumentNullException(nameof(coordinationalMatrix));
            }

            var size = coordinationalMatrix.Size;
            var pattern = new SortedSet<int>[size];
            var pattern2 = new Dictionary<(int i, int j), int>();

            for (int i = 0; i < size; i++)
            {
                pattern[i] = new SortedSet<int>();
            }

            foreach (var item in coordinationalMatrix)
            {
                (int i, int j) = item.row > item.col ? (item.row, item.col) : (item.col, item.row);

                if (i != j)
                {
                    pattern[i].Add(j);
                }
            }

            // добавление в шаблон нулей
            for (int i = 1; i < size; i++)
            {
                if (pattern[i].Count != 0)
                {
                    int j = pattern[i].First();

                    pattern2[(i, j)] = 1;

                    if (j < i - 1)
                    {
                        int jj = j + 1;
                        for (; jj < i; jj++)
                        {
                            if (pattern[i].Contains(jj) == false)
                            {
                                pattern[i].Add(jj);
                                pattern2[(i, jj)] = 0;
                            }
                            else
                            {
                                pattern2[(i, jj)] = 1;
                            }
                        }
                    }
                }
            }

            ia = new int[size + 1];

            for (int i = 0; i < size; i++)
            {
                ia[i + 1] = ia[i] + pattern[i].Count;
            }

            var count = ia[size];

            di = new double[size];
            al = new double[count];
            au = new double[count];

            for (int i = 0; i < Size; i++)
            {
                di[i] = coordinationalMatrix[i, i];

                int ia1 = ia[i];
                int ia2 = ia[i + 1];
                int k = i - (ia2 - ia1);
                for (; ia1 < ia2; ia1++, k++)
                {
                    if (pattern2[(i, k)] == 1)
                    {
                        al[ia1] = coordinationalMatrix[i, k];
                        au[ia1] = coordinationalMatrix[k, i];
                    }
                    else
                    {
                        al[ia1] = 0;
                        au[ia1] = 0;
                    }
                }
            }
        }

        public int Size => di.Length;

        public IVector Diagonal => new Vector(di);

        public ILinearOperator Transpose => new TransposeMatrix<SkylineMatrix> { Matrix = this };

        public IEnumerator<(double value, int row, int col)> GetEnumerator()
        {
            for (int i = 0; i < Size; i++)
            {
                yield return (di[i], i, i);

                int ia1 = ia[i];
                int ia2 = ia[i + 1];
                int k = i - (ia2 - ia1);
                for (; ia1 < ia2; ia1++, k++)
                {
                    yield return (al[ia1], i, k);
                    yield return (au[ia1], k, i);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        // заполнение матрицы
        public void Fill(FillFunc elems)
        {
            if (elems == null)
            {
                throw new ArgumentNullException(nameof(elems));
            }

            for (int i = 0; i < Size; i++)
            {
                di[i] = elems(i, i);

                int ia1 = ia[i];
                int ia2 = ia[i + 1];
                int k = i - (ia2 - ia1);

                for (; ia1 < ia2; ia1++, k++)
                {
                    al[ia1] = elems(i, k);
                    au[ia1] = elems(k, i);
                }
            }
        }

        //умножение на нижний треугольник
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
                int k = ia[i];
                for (int j = i - (ia[i + 1] - k); j < i; j++, k++)
                    result[i] += al[k] * vector[j];
            }

            for (int i = 0; i < Size; i++)
                result[i] += isUseDiagonal ? di[i] * vector[i] : (double)diagonalElement * vector[i];

            return result;
        }

        //умножение на нижний треугольник трансп
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
                int k = ia[i];
                for (int j = i - (ia[i + 1] - k); j < i; j++, k++)
                    result[j] += al[k] * vector[i];
            }

            for (int i = 0; i < Size; i++)
                result[i] += isUseDiagonal ? di[i] * vector[i] : (double)diagonalElement * vector[i];

            return result;
        }

        //умножение матрицы на вектор
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
                int k = ia[i];
                for (int j = i - (ia[i + 1] - k); j < i; j++, k++)
                {
                    result[i] += al[k] * vector[j];
                    result[j] += au[k] * vector[i];
                }
                result[i] += di[i] * vector[i];
            }
            return result;
        }

        //умножение матрицы на вектор трансп
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
                int k = ia[i];
                for (int j = i - (ia[i + 1] - k); j < i; j++, k++)
                {
                    result[i] += au[k] * vector[j];
                    result[j] += al[k] * vector[i];
                }
                result[i] += di[i] * vector[i];
            }
            return result;
        }


        //умножение на верхний треугольник
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

            for (int j = 0; j < Size; j++)
            {
                int k = ia[j];
                for (int i = j - (ia[j + 1] - k); i < j; i++, k++)
                    result[i] += au[k] * vector[j];
            }

            for (int i = 0; i < Size; i++)
                result[i] += isUseDiagonal ? di[i] * vector[i] : (double)diagonalElement * vector[i];

            return result;
        }

        //умножение на верхний треугольник трансп
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

            for (int j = 0; j < Size; j++)
            {
                int k = ia[j];
                for (int i = j - (ia[j + 1] - k); i < j; i++, k++)
                    result[j] += au[k] * vector[i];
            }

            for (int i = 0; i < Size; i++)
                result[i] += isUseDiagonal ? di[i] * vector[i] : (double)diagonalElement * vector[i];

            return result;
        }

        //прямой ход
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

            for (int i = 0; i < Size; i++)
            {
                double sum = 0;
                int k = i - (ia[i + 1] - ia[i]);
                for (int j = ia[i]; k < i; j++, k++)
                    sum += al[j] * result[k];
                result[i] = isUseDiagonal ? (vector[i] - sum) / di[i] : vector[i] - sum;
            }

            return result;
        }

        //прямой ход трансп
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
                if (isUseDiagonal) result[i] /= di[i];
                int k = i - (ia[i + 1] - ia[i]);
                for (int j = ia[i + 1] - 1, m = i - 1; m >= k; j--, m--)
                    result[m] -= al[j] * result[i];
            }

            return result;
        }

        //обратный ход
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
                if (isUseDiagonal) result[i] /= di[i];
                int k = i - (ia[i + 1] - ia[i]);
                for (int j = ia[i + 1] - 1, m = i - 1; m >= k; j--, m--)
                    result[m] -= au[j] * result[i];
            }

            return result;
        }

        //обратный ход трансп
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

            for (int i = 0; i < Size; i++)
            {
                double sum = 0;
                int k = i - (ia[i + 1] - ia[i]);
                for (int j = ia[i]; k < i; j++, k++)
                    sum += au[j] * result[k];
                result[i] = isUseDiagonal ? (vector[i] - sum) / di[i] : vector[i] - sum;
            }

            return result;
        }

        public string Serialize(IVector b, IVector x0)
        {
            var obj = new {ia, b, x0, di, al, au};
            return JsonConvert.SerializeObject(obj);

        }

        public string BinarySerialize(IVector b, IVector x0)
        {
            var obj = new { ia, b, x0, di, al, au };
            MemoryStream ms = new MemoryStream();
            using (BsonWriter writer = new BsonWriter(ms))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(writer, obj);
            }
            return Convert.ToBase64String(ms.ToArray());
        }
    }   
}
