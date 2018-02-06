using System;

namespace SolverCore
{
    public class Vector : IVector
    {
        private readonly double[] vector;

        public double this[int index]
        {
            get
            {
                if(index < 0 || index >= vector.Length)
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
        /// <param name="size">размер вектора</param>
        /// <param name="vector">элементы вектора</param>
        /// <exception cref="ArgumentNullException">если аргумент vector == null </exception>
        /// <exception cref="ArgumentException">если vector.Length != size</exception>
        /// <exception cref="ArgumentException">если аргумент size < 0</exception>
        public Vector(int size, double[] vector)
        {
            if(vector == null)
            {
                throw new ArgumentNullException(nameof(vector));
            }

            if (size < 0)
            {
                throw new ArgumentException("Size can't be less then 0", nameof(size));
            }

            if (vector.Length != size)
            {
                throw new ArgumentException($"Size of vector not equals {nameof(size)}", nameof(vector));
            }

            this.vector = new double[size];
            vector.CopyTo(this.vector, 0);
        }

        /// <summary>
        /// вычитание вектора
        /// </summary>
        /// <param name="vector">вычитаемый вектор</param>
        /// <returns>результирующий вектор</returns>
        /// <exception cref="ArgumentNullException">если аргумент vector == null</exception>
        /// <exception cref="ArgumentException">если размер вычитаемого вектора не равен размеру текущего(уменьшаемого) вектора</exception>
        public IVector Minus(IVector vector)
        {
            if(vector == null)
            {
                throw new ArgumentNullException(nameof(vector));
            }

            if(vector.Size != Size)
            {
                throw new ArgumentException("Size of vector must be equals size of current", nameof(vector));
            }

            var result = new double[Size];

            for(int i = 0; i < Size; i++)
            {
                result[i] = this[i] - vector[i];
            }

            return new Vector(Size, result);
        }

        /// <summary>
        /// операция умножения на вектор
        /// </summary>
        /// <param name="vector">множитель</param>
        /// <returns>результирующее число</returns>
        /// <exception cref="ArgumentNullException">если аргумент vector == null</exception>
        /// <exception cref="ArgumentException">если размер вектора множителя не равен размеру текущего вектора</exception>
        public double Multiply(IVector vector)
        {
            if (vector == null)
            {
                throw new ArgumentNullException(nameof(vector));
            }

            if (vector.Size != Size)
            {
                throw new ArgumentException("Size of vector must be equals size of current", nameof(vector));
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

        /// <summary>
        /// операция нахождения суммы векторов
        /// </summary>
        /// <param name="vector">слагаемое</param>
        /// <returns>результирующий вектор</returns>
        /// <exception cref="ArgumentNullException">если аргумент vector == null</exception>
        /// <exception cref="ArgumentException">если размер вектора слагаемого не равен размеру текущего вектора</exception>
        public IVector Plus(IVector vector)
        {
            if (vector == null)
            {
                throw new ArgumentNullException(nameof(vector));
            }

            if (vector.Size != Size)
            {
                throw new ArgumentException("Size of vector must be equals size of current", nameof(vector));
            }

            var result = new double[Size];

            for(int i = 0; i < Size; i++)
            {
                result[i] = this[i] + vector[i];
            }

            return new Vector(Size, result);
        }
    }
}
