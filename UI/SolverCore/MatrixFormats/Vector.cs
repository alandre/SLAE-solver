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
            this.vector = vector.Clone() as double[];
        }
        public Vector(int size)
        {
            this.vector = new double[size];
        }

        public double this[int index]
        {
            get
            {
                try
                {
                    return vector[index];
                }
                catch(IndexOutOfRangeException)
                {
                    throw new IndexOutOfRangeException();
                }
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

            var result = new Vector(Size);

            for(int i = 0; i < Size; i++)
            {
                result[i] = this[i] + multiplier * vector[i];
            }
            return result;
        }

        public IVector Clone()
        {
            return new Vector((double[])vector.Clone());
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
            var enumerator = (IEnumerable<double>)vector;
            return enumerator.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IVector HadamardProduct(IVector vector)
        {
            if (vector == null)
            {
                throw new ArgumentNullException(nameof(vector));
            }

            if (vector.Size != Size)
            {
                throw new RankException();
            }

            var result = new double[Size];

            for (int i = 0; i < Size; i++)
            {
                result[i] = this.vector[i] * vector[i];
            }

            return new Vector(result);
        }
    }
}
