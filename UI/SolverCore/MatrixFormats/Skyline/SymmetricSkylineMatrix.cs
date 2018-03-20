using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SolverCore
{
    public class SymmetricSkylineMatrix : IMatrix, ILinearOperator
    {
        private double[] di; // диагональ
        private double[] al; // массив элементов профиля
        private int[] ia; // целочисленный массив с указателями начала строк профиля [0,0,...]

        //конструктор
        public SymmetricSkylineMatrix(double[] di, int[] ia, double[] al)
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
            if(this.ia[size1 - 1] != size2)
            {
                throw new RankException();
            }
            this.al = (double[])al.Clone();
        }

        public SymmetricSkylineMatrix(int[] ia)
        {
            if (ia == null)
            {
                throw new ArgumentNullException(nameof(ia));
            }
            
            this.ia = (int[])ia.Clone();
            this.di = new double[ia.Length - 1];
            this.al = new double[ia[ia.Length - 1]];

            if (this.ia[0] == 1)
            {
                for (int i = 0; i < this.ia.Length; i++)
                {
                    this.ia[i]--;
                }
            }
        }

        public SymmetricSkylineMatrix(SymmetricCoordinationalMatrix coordinationalMatrix)
        {
            if (coordinationalMatrix == null)
            {
                throw new ArgumentNullException(nameof(coordinationalMatrix));
            }

            var orderedItems = coordinationalMatrix.OrderBy(x => x.row).ThenBy(x => x.col);
            
            var size = coordinationalMatrix.Size;
            ia = new int[size + 1];
            di = new double[size];

            var itemsProfile = new Dictionary<(int i, int j), double>();
            int flag = 1, jj = 0;
            
            foreach (var item in orderedItems)
            {
                if (item.row == item.col)
                {
                    di[item.row] = item.value;
                }
                else
                {
                    (int i, int j) = (item.row, item.col);

                    itemsProfile[(i,j)] = item.value;
                    if (flag < i)
                    {
                        jj = j++;
                        for (; jj < (i - j); jj++) //заполнеие нулями до диагонали
                        {
                            itemsProfile[(i,jj)] = 0; // заполнение нулей портрета по нижнему треугольнику
                        }
                        flag = i;
                    }
                } 
            }
            
            var orderedItems2 = itemsProfile.OrderBy(x => x.Key.i).ThenBy(x => x.Key.j);
            var count = orderedItems2.Count();
            
            al = new double[count];

            int k = 0;
            ia[0] = 0;
            ia[1] = 0;

            foreach (var item in orderedItems2)
            {
                al[k] = item.Value;
                ia[item.Key.i + 1]++;
                k++;
            }

            for(int i = 0; i < size; i++)
            {
                ia[i + 1] += ia[i];
            }
        }

        //Получение значения по индексу
        public double this[int i, int j]
        {
            get
            {
                try
                {
                    if (i == j) return di[i]; // если диагональ

                    if (j > i)
                    {
                        var a = i;
                        i = j;
                        j = a;
                    }

                    if (ia[i + 1] - ia[i] == 0) return 0; // если данный элемент не содержится в профиле, значит, он нулевой

                    int k = i - (ia[i + 1] - ia[i]); // индекс столбца первого элемента в профиле
                    return al[ia[i] + j - k];
                }
                catch(IndexOutOfRangeException)
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }

        public int Size => di.Length;

        public IVector Diagonal => new Vector(di);
        
        public ILinearOperator Transpose => this;

        public IEnumerator<(double value, int row, int col)> GetEnumerator()
        {
            yield return (di[0], 0, 0);

            for (int i = 1; i < Size; i++)
            {
                yield return (di[i], i, i);

                int ia1 = ia[i];
                int ia2 = ia[i + 1];
                int k = i - (ia2 - ia1);
                for ( ; ia1 < ia2; ia1++, k++)
                {
                    yield return (al[ia1], i, k);
                    yield return (al[ia1], k, i);
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

            int i = 0, j = 0, k = 0; 
            ia[0] = 0; 
            ia[1] = 0; 

            foreach (var elem in this) 
            {
                if (elem.col == elem.row)
                {
                    di[i] = elems(elem.row, elem.col);
                }
                else
                if (elem.col < elem.row)
                {
                    al[j] = elems(elem.row, elem.col);
                    j++;
                }

                if (elem.row == i && i > 0)
                {
                    k++; // подсчет количества элементов в профиле
                }    
                else
                {
                    ia[i+1] = k;
                    i++;
                    k = 0;
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
                    result[i] += al[k] * vector[j];
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
                    vector[i] += al[k] * vector[j];
                    vector[j] += al[k] * vector[i];
                }
                vector[i] += di[i] * vector[i];
            }
            return result;
        }

        //прямой ход
        public IVector LSolve(IVector vector, bool isUseDiagonal)
        {
            if(vector == null)
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
                if (isUseDiagonal) result[i] = result[i] / di[i];
                int k = i - (ia[i + 1] - ia[i]);
                for (int j = ia[i + 1] - 1, m = i - 1; m >= k; j--, m--)
                    result[m] -= al[j] * result[i];
            }
            
            return result;
        }

    }
}
