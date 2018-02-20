using System;
using System.Collections;
//строчный симметричный
namespace SolverCore
{
    public class SymmetricSparseRowMatrix : IMatrix, ILinearOperator
    {
        private double[] A;//значения
        private int[] ja;//положение ненулевых элементов в строке(индекс j)
        private int[] ia;//местоположение первого ненулевого элемента в строке(ia[size+1]-количество ненулевых элементов)

        //конструктор
        public SymmetricSparseRowMatrix(double[] A, int[] ja, int[] ia)
        {
            if (A == null)
            {
                throw new ArgumentNullException(nameof(A));
            }

            if (ja == null)
            {
                throw new ArgumentNullException(nameof(ja));
            }

            if (ia == null)
            {
                throw new ArgumentNullException(nameof(ia));
            }

            if (A.GetLength(0) != ja.GetLength(0))
            {
                throw new ArgumentNullException("the matrix A or ia is not properly filled", nameof(iоa));
            }

            if (A.GetLength(0) != ia[ia.GetLength(0) - 1])
            {
                throw new ArgumentNullException("A and ja must be of equal size", nameof(ia));
            }

            var size = ia.GetLength(0);

            for (int i = 0; i < size; i++)
            {
                this.ia[i] = ia[i];
            }

            var size1 = ja.GetLength(0);

            for (int i = 0; i < size1; i++)
            {
                this.ja[i] = ja[i];
                this.A[i] = A[i];
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

                if (j > i)
                {
                    var k = i;
                    i = j;
                    j = k;
                }
                double a = 0; // значение искомого элемента
                var ia1 = ia[i];
                var ia2 = ia[i + 1];
                for (; ia1 < ia2; ia1++)
                {
                    if (ja[ia1] == j)
                    {
                        a = A[ia1];
                    }
                }
                return a;
            }
        }
        //размер
        public int Size => this.ia.GetLength(0) - 1;
        //диагональ
        public IVector Diagonal
        {
            get
            {
                var diagonal = new double[Size];

                for (int i = 0; i < Size; i++)
                {
                    int ia2 = ia[i + 1] - 1;
                    if (ja[ia2] == i)
                    {
                        diagonal[i] = A[ia2];
                    }

                }
                return new Vector(diagonal);
            }
        }

        public ILinearOperator Transpose => throw new NotImplementedException();

        public CoordinationalMatrix ConvertToCoordinationalMatrix()
        {
            throw new NotImplementedException();
        }

        public void Fill(FillFunc elems)
        {
            throw new NotImplementedException();
        }
        //коллекция
        public System.Collections.Generic.IEnumerator<(double value, int row, int col)> GetEnumerator()
        {
            for (int i = 0; i < Size; i++)
            {
                var ia1 = ia[i];
                var ia2 = ia[i + 1];
                for (; ia1 < ia2; ia1++)
                {
                    yield return (A[ia1], i, ja[ia1]);
                }
            }
        }

        //умножение на нижний треугольник
        public IVector LMult(IVector x, bool UseDiagonal, int diagonalElement = 1)
        {
            var result = new Vector(Size);
            if (UseDiagonal)
            {
                for (int i = 0; i < Size; i++)
                {
                    double sum = 0;
                    var ia1 = ia[i];
                    var ia2 = ia[i + 1];
                    for (; i < ja[ia1] && ia1 < ia2; ia1++)
                    {
                        var j = ja[ia1];
                        sum += A[ia1] * x[j];
                    }
                    sum += (double)diagonalElement * A[ia1] * x[i];
                    result[i] = sum;
                }
            }
            else
            {
                for (int i = 0; i < Size; i++)
                {
                    double sum = 0;
                    var ia1 = ia[i];
                    var ia2 = ia[i + 1] - 1;
                    int j;
                    for (; i < ja[ia1] && ia1 < ia2; ia1++)
                    {
                        j = ja[ia1];
                        sum +=A[ia1] * x[j];
                    }
                    j = ja[ia1];
                    sum += (double)diagonalElement * x[j];
                    result[i] = sum;
                }
            }
            return result;
        }
        //прямой ход
        public IVector LSolve(IVector x, bool UseDiagonal)
        {
            var result = x.Clone();
            double sum = 0;
            if (UseDiagonal)
            {
                for (int i = 0; i < Size; i++)
                {
                    var ia1 = ia[i];
                    var ia2 = ia[i + 1];
                    int j;
                    sum = 0;
                    for (; ja[ia1] < i && ia1 < ia2; ia1++)
                    {
                        j = ja[ia1];
                        sum += result[j] * A[ia1];
                    }
                    if (i == ja[ia1] && ia1 < ia2)
                    {
                        result[i] = (result[i] - sum) / A[ia1];
                    }
                    else
                    {
                        throw new ArgumentNullException("does not store the diagonal element", nameof(A));
                    }
                }
            }
            else
            {
                for (int i = 0; i < Size; i++)
                {
                    var ia1 = ia[i];
                    var ia2 = ia[i + 1];
                    int j;
                    sum = 0;
                    for (; ja[ia1] < i && ia1 < ia2; ia1++)
                    {
                        j = ja[ia1];
                        sum += result[j] * A[ia1];
                    }
                    result[i] -= sum;
                }
            }
            return result;
        }
        //умножение
        public IVector Multiply(IVector x)
        {
            var result = new Vector(Size);
            for (int i = 0; i < Size; i++)
            {
                var ia1 = ia[i];
                var ia2 = ia[i + 1];
                for (; ia1 < ia2; ia1++)
                {
                    var j = ja[ia1];
                    for (int k = j; k <= i; k++)
                        result[k] += A[ia1] * x[k];
                }
            }
            return result;
        }
        //умножение на верхний треугольник
        public IVector UMult(IVector x, bool UseDiagonal, int diagonalElement = 1)
        {
            var result = new Vector(Size);
            if (isUseDiagonal)
            {
                for (int i = Size - 1; i >= 0; i--)
                {
                    var ia1 = ia[i];
                    var ia2 = ia[i + 1] - 1;
                    result[i] += (double)diagonalElement*A[ia2] * vector[i];
                    for (ia2--; ia1 <= ia2; ia2--)
                    {
                        var j = ja[ia2];
                        result[j] += A[ia2] * vector[i];
                    }
                }
            }
            else
            {
                for (int i = Size - 1; i >= 0; i--)
                {
                    var ia1 = ia[i];
                    var ia2 = ia[i + 1];
                    var j = ja[ia1];
                    result[i] += (double)diagonalElement * vector[i];
                    for (; i > ja[ia1] && ia1 < ia2; ia1++)
                    {
                        j = ja[ia1];
                        result[j] += A[ia1] * vector[i];
                    } 
                }
            }
            return result;
        }
        //обратный ход
        public IVector USolve(IVector x, bool UseDiagonal)
        {
            var result = vector.Clone();
            if (isUseDiagonal)
            {
                var di = Diagonal;
                for (int i = Size - 1; i >= 0; i--)
                {
                    var ia1 = ia[i];
                    var ia2 = ia[i + 1];
                    int j;
                    for (; ja[ia1] < i && ia1 < ia2; ia1++)
                    {
                        j = ja[ia1];
                        result[j] -= result[i] * A[ia1] / di[i];//??????
                    }
                    if (i == ja[ia1] && ia1 < ia2)
                    {
                        result[i] = result[i] / di[i];
                    }
                    else
                    {
                        throw new ArgumentNullException("does not store the diagonal element", nameof(A));
                    }
                }

            }
            else
            {
                for (int i = Size - 1; i >= 0; i--)
                {
                    var ia1 = ia[i];
                    var ia2 = ia[i + 1];
                    int j;
                    for (; ja[ia1] < i && ia1 < ia2; ia1++)
                    {
                        j = ja[ia1];
                        result[j] -= result[i] * A[ia1];
                    }

                }
            }
            return result;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}