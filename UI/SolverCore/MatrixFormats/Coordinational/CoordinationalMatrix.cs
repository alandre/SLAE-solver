using System;
using System.Collections;
using System.Linq;

namespace SolverCore
{
    public class CoordinationalMatrix : IMatrix, ILinearOperator, ITransposeLinearOperator
    {
        /// <summary>
        /// A - массив элементов матрицы
        /// Ig - массив индексов строк
        /// Jg - массив индексов столбцов
        /// A , Ig, Jg - имеют одинаковый размер
        /// </summary>
        private double[] A;
        private int[] Ig;
        private int[] Jg;

        public double this[int i, int j] {
            get
            {
                try
                {
                    int k = 0;
                    for (; k< Ig.Length; k++)
                    {
                        if (Ig[k] == i)
                            if (Jg[k] == j)
                                break;
                    }
                    return A[k];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new IndexOutOfRangeException();
                }
            }
            set => throw new NotImplementedException();
            //{
            //    try
            //    {
            //        int k = 0;
            //        for (; k < Ig.Length; k++)
            //        {
            //            if (Ig[k] == i)
            //                if (Jg[k] == j)
            //                {
            //                    A[k]=???
            //                    break;
            //                }
            //        }
            //    }
            //    catch (IndexOutOfRangeException)
            //    {
            //        throw new IndexOutOfRangeException();
            //    }
            //}
        }

        /// <summary>
        /// вопрос: это вообще правильно?
        /// </summary>
        public int Size {
            get
            {
                int Size = Ig.Max();
                int maxJg = Jg.Max();
                if (Size < maxJg)
                    Size = maxJg;
                return Size;
            }
        }

        public IVector Diagonal {
            get
            {
                //int Size = (int) Ig.Max();
                //int maxJg = (int) Jg.Max();
                //if (Size < maxJg)
                //    Size = maxJg;

                var diagonal = new Vector(Size);

                int igLength = Ig.GetLength(0);
                int k = 0; 
                for (int i = 0; i < igLength; i++)
                {
                    if (Ig[i] == Jg[i]) {
                        diagonal[k] = A[i];
                        k++;
                    }
                    else if (Ig[i] < Ig[i + 1])
                    {
                        int res= Ig[i + 1]-Ig[i];
                        for (int j=0; j<res;j++)
                            diagonal[k+j-1] = 0;
                    }
                }

                return diagonal;
            }
        }

        public ILinearOperator Transpose => 
            new TransposeMatrix<CoordinationalMatrix> { Matrix = this };

        public CoordinationalMatrix ConvertToCoordinationalMatrix()
        {
            throw new NotImplementedException();
        }

        public void Fill(FillFunc elems)
        {
            if (elems == null)
            {
                throw new ArgumentNullException(nameof(elems));
            }

            int i = 0;
            foreach (var elem in this)
            {
                Ig[i] = elem.row;
                Jg[i] = elem.col;
                A[i] = elem.value;
                i++;
            }
        }

        public System.Collections.Generic.IEnumerator<(double value, int row, int col)> GetEnumerator()
        {
            int igLength = Ig.GetLength(0);
            for (int i = 0; i < igLength; i++)
            {
                yield return (A[i], Ig[i], Jg[i]);
            }
            throw new NotImplementedException();
        }

        public IVector LMult(IVector x, bool UseDiagonal, int diagonalElement = 1)
        {
            if (x == null)
            {
                throw new ArgumentNullException(nameof(x));
            }

            if (x.Size != Size)
            {
                throw new RankException();
            }

            var result = new Vector(Size);
            
            for (int i = 0; i < Size; i++)
            {
                var sum = UseDiagonal ? Diagonal[i] * x[i] : x[i];

                for (int j = 0; Ig[j] <= i && Jg[j] < i; j++)
                {
                    sum += A[j] * x[Jg[j]];
                }

                result[i] = sum;
            }

            return result;
        }

        public IVector LMultTranspose(IVector vector, bool isUseDiagonal, int diagonalElement = 1)
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
                var sum = isUseDiagonal ? Diagonal[i] * vector[i] : vector[i];

                for (int j = 0; Ig[j] <= i && Jg[j] < i; j++)
                {
                    sum += A[j] * vector[Jg[j]];
                }

                result[i] = sum;
            }

            return result;
            //throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public IVector MultiplyTranspose(IVector vector)
        {
            throw new NotImplementedException();
        }

        public IVector UMult(IVector x, bool UseDiagonal, int diagonalElement = 1)
        {
            throw new NotImplementedException();
        }

        public IVector UMultTranspose(IVector vector, bool isUseDiagonal, int diagonalElement = 1)
        {
            throw new NotImplementedException();
        }

        public IVector USolve(IVector x, bool UseDiagonal)
        {
            throw new NotImplementedException();
        }

        public IVector USolveTranspose(IVector vector, bool isUseDiagonal)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
