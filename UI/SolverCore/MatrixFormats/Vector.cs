using System;
using System.Collections;
using System.Collections.Generic;

namespace SolverCore
{
    public class Vector : IVector
    {
        private readonly double[] vector;

        public double this[int index]
        {
            get
            {
                if(index < 0 || index >= Size)
                {
                    throw new IndexOutOfRangeException();
                }

                return vector[index];
            }
        }

        public int Size
        {
            get
            {
                return vector.Length;
            }
        }

        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="vector">элементы вектора</param>
        /// <exception cref="ArgumentNullException">если аргумент vector == null </exception>
        public Vector(double[] vector)
        {
            if(vector == null)
            {
                throw new ArgumentNullException(nameof(vector));
            }

            this.vector = new double[vector.Length];
            vector.CopyTo(this.vector, 0);
        }

        /// <summary>
        /// вычитание вектора
        /// </summary>
        /// <param name="vector">вычитаемый вектор</param>
        /// <returns>результирующий вектор</returns>
        /// <exception cref="ArgumentNullException">если аргумент vector == null</exception>
        /// <exception cref="RankException">если размер вычитаемого вектора не равен размеру текущего(уменьшаемого) вектора</exception>
        public IVector Minus(IVector vector)
        {
            if(vector == null)
            {
                throw new ArgumentNullException(nameof(vector));
            }

            if(vector.Size != Size)
            {
                throw new RankException(nameof(vector));
            }

            var result = new double[Size];

            for(int i = 0; i < Size; i++)
            {
                result[i] = this[i] - vector[i];
            }

            return new Vector(result);
        }

        /// <summary>
        /// операция нахождения суммы векторов
        /// </summary>
        /// <param name="vector">слагаемое</param>
        /// <returns>результирующий вектор</returns>
        /// <exception cref="ArgumentNullException">если аргумент vector == null</exception>
        /// <exception cref="RankException">если размер вектора слагаемого не равен размеру текущего вектора</exception>
        public IVector Plus(IVector vector)
        {
            if (vector == null)
            {
                throw new ArgumentNullException(nameof(vector));
            }

            if (vector.Size != Size)
            {
                throw new RankException(nameof(vector));
            }

            var result = new double[Size];

            for (int i = 0; i < Size; i++)
            {
                result[i] = this[i] + vector[i];
            }

            return new Vector(result);
        }

        /// <summary>
        /// операция умножения на вектор
        /// </summary>
        /// <param name="vector">множитель</param>
        /// <returns>результирующее число</returns>
        /// <exception cref="ArgumentNullException">если аргумент vector == null</exception>
        /// <exception cref="RankException">если размер вектора множителя не равен размеру текущего вектора</exception>
        public double Multiply(IVector vector)
        {
            if (vector == null)
            {
                throw new ArgumentNullException(nameof(vector));
            }

            if (vector.Size != Size)
            {
                throw new RankException(nameof(vector));
            }

            var result = 0.0;

            for(int i = 0; i < Size; i++)
            {
                result += this[i] * vector[i];
            }

            return result;
        }

        /// <summary>
        /// вычисление нормы вектора
        /// </summary>
        /// <returns>результат - число</returns>
        public double Norm() => Math.Sqrt(Multiply(this));

        // так нужно)))
        /// <summary>
        /// метод необходимый чтобы по коллекции можно было пробегать оператором foreach
        /// </summary>
        /// <returns>возвращается перечислитель</returns>
        public IEnumerator<double> GetEnumerator()
        {
            var enumerable = vector as IEnumerable<double>;
            return enumerable.GetEnumerator();
        }

        /// <summary>
        /// метод который просто так нужно сделать (он приватный). Исходя из архитектуры делается так
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
