using System;
using System.Collections;


//строчный несимметричный
namespace SolverCore
{
    public class SparseRowMatrix : IMatrix, ILinearOperator, ITransposeLinearOperator
    {
        private double[] A;//значения
        private int[] ja;//положение ненулевых элементов в строке(индекс j)
        private int[] ia;//местоположение первого ненулевого элемента в строке(ia[size+1]-количество ненулевых элементов)

        //конструктор
        public SparseRowMatrix(double[] A, int[] ja, int[] ia)
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

        //получение элемента по индексу
        public double this[int i, int j]
        {
            get
            {
                if (i < 0 || j < 0 || i >= Size || j >= Size)
                {
                    throw new IndexOutOfRangeException();
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
          //  set => throw new NotImplementedException();
        }

        //размер
        public int Size => this.ia.GetLength(0) - 1;
        //диагональ
        public IVector Diagonal
        {
            get
            {
                var diagonal = new Vector(Size);

                for (int i = 0; i < Size; i++)
                {
                    int ia1 = ia[i];
                    int ia2 = ia[i + 1];
                    for (; ia1 < ia2; ia1++)
                    {
                        if (ja[ia1] == i)
                        {
                            diagonal[i] = A[ia1];
                        }
                    }
                }
                return diagonal;
            }
        }
        //транспонирование
        public ILinearOperator Transpose => throw new NotImplementedException();
        
        public CoordinationalMatrix ConvertToCoordinationalMatrix()
        {
            throw new NotImplementedException();
        }
        
        public void Fill(FillFunc elems)
        {
            throw new NotImplementedException();
        }
        //коллекция:элемент, координаты
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
                    int j;
                    for (; ja[ia1] < i && ia1 < ia2; ia1++)
                    {
                         j = ja[ia1];
                        sum += A[ia1] * x[j];
                    }
                   j = ja[ia1];
                    if (j == i && ia1 < ia2)
                    {
                        sum += (double)diagonalElement *A[ia1] * x[j];
                    }
                    result[i] = sum;
                }
            }
            else
            {
                for (int i = 0; i < Size; i++)
                {
                    double sum = 0;
                    var ia1 = ia[i];
                    var ia2 = ia[i + 1];
                    int j;
                    for (; ja[ia1] < i && ia1 < ia2; ia1++)
                    {
                        j = ja[ia1];
                        sum += A[ia1] * x[j];
                    }
                    j = ja[ia1];
                    sum += (double)diagonalElement*x[j];
                    result[i] = sum;
                }
            }
            return  result;
        }
        //умеожение на транспонированный нижний треугольник
        public IVector LMultTranspose(IVector vector, bool isUseDiagonal, int diagonalElement = 1)
        {
            var result = new Vector(Size);
            if (isUseDiagonal)
            {
                for (int i = Size - 1; i >= 0; i--)
                {
                    var ia1 = ia[i];
                    var ia2 = ia[i + 1] - 1;
                    int j;
                    for (; ja[ia2] > i && ia1 <= ia2; ia2--)
                    {
                        j = ja[ia2];
                        result[j] += A[ia2] * vector[i];
                    }
                    j = ja[ia2];
                    if (j == i && ia1 <= ia2)
                    {
                        result[j] += (double)diagonalElement*A[ia2] * vector[j];
                    }
                }
            }
            else
            {
                for (int i = Size - 1; i >= 0; i--)
                {
                    var ia1 = ia[i];
                    var ia2 = ia[i + 1] - 1;
                    int j;
                    for (; ja[ia2] > i && ia1 <= ia2; ia2--)
                    {
                         j = ja[ia2];
                        result[j] += A[ia2] * vector[i];
                    }
                    j = ja[ia2];
                    result[j] += (double)diagonalElement*vector[j];
                }
            }
            return  result;

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
            return  result;

        }
        //прямой ход с транспонированием 
        public IVector LSolveTranspose(IVector vector, bool isUseDiagonal)
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
            return  result;
        }
        //умножение
        public IVector Multiply(IVector x)
        {
            var result = new Vector(Size);
            for (int i = 0; i < Size; i++)
            {
                double sum = 0;
                var ia1 = ia[i];
                var ia2 = ia[i + 1];
                for (; ia1 < ia2; ia1++)
                {
                    int j = ja[ia1];
                    sum += A[ia1] * x[j];
                }
                result[i] = sum;
            }
            return  result;

        }
        //умножение на траснпонированную матрицу
        public IVector MultiplyTranspose(IVector vector)
        {
            for (int i = Size - 1; i >= 0; i--)
            {
                var ia1 = ia[i];
                var ia2 = ia[i + 1] - 1;
                for (; ia1 <= ia2; ia2--)
                {
                    int j = ja[ia2];
                    result[j] += A[ia2] * vector[i];
                }
            }
            return result;
        }
        //умножение на верхний треугольник
        public IVector UMult(IVector x, bool UseDiagonal, int diagonalElement = 1)
        {
            var result = new Vector(Size);
            if (UseDiagonal)
            {
                for (int i = 0; i < Size; i++)
                {
                    double sum = 0;
                    var ia1 = ia[i];
                    var ia2 = ia[i + 1] - 1;
                    var j = ja[ia2];
                    for (; j > i && ia1 < ia2; ia2--)
                    {
                        j = ja[ia2];
                        sum += A[ia2] * x[j];
                    }
                    j = ja[ia2];
                    if (j == i && ia1 <= ia2)
                    {
                        sum += (double)diagonalElement*A[ia2] * x[j];
                    }
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
                    var j = ja[ia2];
                    for (; j > i && ia1 < ia2; ia2--)
                    {
                        j = ja[ia2];
                        sum += A[ia2] * x[j];
                    }
                    j = ja[ia2];
                    sum += (double)diagonalElement * x[j];
                    result[i] = sum;
                }
            }
            return  result;

        }
        //умножение на транспонированный верхний треугольник
        public IVector UMultTranspose(IVector vector, bool isUseDiagonal, int diagonalElement = 1)
        {
            var result = new Vector(Size);
            if (isUseDiagonal)
            {
                for (int i = 0; i < Size; i++)
                {
                    var ia1 = ia[i];
                    var ia2 = ia[i + 1] - 1;
                    var j = ja[ia2];
                    for (; i < j && ia1 < ia2; ia2--)
                    {
                        j = ja[ia2];
                        result[j] += A[ia2] * vector[i];
                    }
                    j = ja[ia2];
                    if (j == i && ia1 <= ia2)
                        result[i] += (double)diagonalElement*A[ia2] * vector[i];
                }
            }
            else
            {
                for (int i = 0; i < Size; i++)
                {
                    var ia1 = ia[i];
                    var ia2 = ia[i + 1] - 1;
                    var j = ja[ia2];
                    for (; i < j && ia1 < ia2; ia2--)
                    {
                        j = ja[ia2];
                        result[j] += A[ia2] * vector[i];
                    }
                    result[i] += (double)diagonalElement * vector[i];
                }
            }
            return  result;
        }
        //обратный ход
        public IVector USolve(IVector x, bool UseDiagonal)
        {
            var result = x.Clone();
            double sum = 0;
            if (UseDiagonal)
            {
                for (int i = Size - 1; i >= 0; i--)
                {
                    var ia1 = ia[i];
                    var ia2 = ia[i + 1] - 1;
                    int j;
                    sum = 0;
                    for (j = ja[ia2]; j > i && ia1 < ia2; ia2--)
                    {
                        j = ja[ia2];
                        sum += result[j] * A[ia2];
                    }
                    j = ja[ia2];
                    if (i == j && ia1 <= ia2)
                    {
                        result[i] = (result[i] - sum) / A[ia2];
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
                    var ia2 = ia[i + 1] - 1;
                    int j;
                    sum = 0;
                    for (j = ja[ia2]; j > i && ia1 < ia2; ia2--)
                    {
                        j = ja[ia2];
                        sum += result[j] * A[ia2];
                    }
                    result[i] -= sum;

                }
            }
            return  result;
        }
        //обратный ход с тарнсонированной матрицей
        public IVector USolveTranspose(IVector vector, bool isUseDiagonal)
        {
            var result = vector.Clone();
            if (isUseDiagonal)
            {
                var di = Diagonal;
                for (int i = 0; i < Size; i++)
                {
                    var ia1 = ia[i];
                    var ia2 = ia[i + 1] - 1;
                    int j;
                    for (; ja[ia2] > i && ia1 < ia2; ia2--)
                    {
                        j = ja[ia2];
                        result[j] -= result[i] * A[ia2] / di[i];//??????
                    }
                    if (i == ja[ia2] && ia1 <= ia2)
                    {
                        result[i] /= di[i];
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
                    var ia2 = ia[i + 1] - 1;
                    int j;
                    for (; ja[ia2] > i && ia1 < ia2; ia2--)
                    {
                        j = ja[ia2];
                        result[j] -= result[i] * A[ia2];//??????
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
