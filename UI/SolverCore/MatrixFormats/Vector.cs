using System;
using System.Collections;
using System.Collections.Generic;

namespace SolverCore
{
    public class Vector : IVector
    {
        private double[] vector;

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

            set => vector[index] = value;
        }

        public int Size => vector.Length;

        public double Norm => Math.Sqrt(DotProduct(this));

        public void SetConst(double value = 0)
        {
            for(int i = 0; i < Size; i++)
            {
                vector[i] = value;
            }
        }

        public IVector Add(IVector vector, double multiplier = 1)
        {
            if(vector == null)
            {
                throw new ArgumentNullException(nameof(vector));
            }

            if(vector.Size != Size)
            {
                throw new RankException();
            }

            var result = new double[Size];

            for(int i = 0; i < Size; i++)
            {
                result[i] = this[i] + multiplier * vector[i];
            }

            return new Vector(result);
        }

        public IVector Clone()
        {
            var result = new double[Size];
            vector.CopyTo(result, 0);

            return new Vector(result);
        }

        public double DotProduct(IVector vector)
        {
            if(vector == null)
            {
                throw new ArgumentNullException(nameof(vector));
            }

            if(vector.Size != Size)
            {
                throw new RankException();
            }

            var result = 0.0;

            for(int i = 0; i < Size; i++)
            {
                result += this[i] * vector[i];
            }

            return result;
        }

        public IEnumerator<double> GetEnumerator()
        {
            var enumerator = vector as IEnumerable<double>;
            return enumerator.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
