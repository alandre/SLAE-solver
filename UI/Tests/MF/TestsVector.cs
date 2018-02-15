using System;
using System.Collections;
using System.Collections.Generic;
using SolverCore;
using System.Linq;
using Xunit;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MF_Tests
{
    public class MockVector : IVector
    {
        private double[] vector;
        public MockVector(double[] vector)
        {
            if (vector == null)
            {
                throw new ArgumentNullException(nameof(vector));
            }

            this.vector = (double[])vector.Clone();
        }
        public double this[int index]
        {
            get => throw new IndexOutOfRangeException();

            set => throw new NotImplementedException();
        }

        public int Size => vector.Length;

        public double Norm => throw new NotImplementedException();

        public IVector Add(IVector vector, double multiplier = 1)
        {
            throw new NotImplementedException();
        }

        public IVector Clone()
        {
            return new Vector((double[])vector.Clone());
        }

        public double DotProduct(IVector vector)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<double> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public IVector HadamardProduct(IVector vector)
        {
            throw new NotImplementedException();
        }

        public void SetConst(double value = 0)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
   
    public class TestsVector
    {
        [Fact]
        public void Vector_TestConstructor()
        {
            var array = new[] { 1.0, 2.0, 3.0, 4.0, 5.0 };
            IVector vector = new Vector(array);

            Assert.Equal(array.Length, vector.Size);
            Assert.Equal(array[2], vector[2], 8);  // 8 — количество знаков после запятой 1e-8
        }

        [Fact]
        public void Vector_TestConstructorArgumentException()
        {
            int size = -1;
            IVector vector;
            Exception ex = Record.Exception(() => vector = new Vector(-1));
            Exception exActual = new ArgumentException("size must be more then 0", nameof(size));
            Assert.StartsWith(ex.Message, exActual.Message);
        }

        [Fact]
        public void Vector_TestEnumerator()
        {
            var array = new[] { 1.0, 2.0, 3.0, 4.0, 5.0 };
            IVector vector = new Vector(array);
            foreach (var elem in vector)
                Console.Write(elem);
        }

        [Fact]
        public void Vector_TestClone()
        {
            var array = new[] { 1.0, 2.0, 3.0, 4.0, 5.0 };
            IVector vector = new Vector(array);
            var testVector = vector.Clone();
            Assert.True(vector.SequenceEqual(testVector));
        }

        [Fact]
        public void Vector_TestSetConst()
        {
            var array = new[] { 1.0, 2.0, 3.0, 4.0, 5.0 };
            IVector vector = new Vector(array);
            vector.SetConst();

            double actual = 0;
            for (int i=0;i<vector.Size;i++)
                Assert.Equal(vector[i], actual);
        }

        [Fact]
        public void Vector_TestNegativeIndex()
        {
            var array = new[] { 1.0, 2.0, 3.0, 4.0, 5.0 };
            IVector vector = new Vector(array);

            Assert.Throws<IndexOutOfRangeException>(() => vector[-1]);
           
        }

        [Fact]
        public void Vector_TestIndexOverSize()
        {
            var array = new[] { 1.0, 2.0, 3.0, 4.0, 5.0 };
            IVector vector = new Vector(array);

            Assert.Throws<IndexOutOfRangeException>(() => vector[vector.Size + 1]);
        }

        [Fact]
        public void Vector_TestNullArgumentInConstructor()
        {
            Assert.Throws<ArgumentNullException>(() => { IVector vector = new Vector(null); });
        }
        #region Add Tests



        [Fact]
        public void Vector_TestAdd()
        {
            var array1 = new[] { 1.0, 2.0, 3.0, 4.0, 5.0 };
            IVector vector1 = new Vector(array1);
            var array2 = new[] { 1.0, 3.0, 4.0, 5.0, 6.0 };
            IVector vector2 = new Vector(array2);

            var result = vector1.Add(vector2, 2);

            Assert.Equal(result, new Vector(new double[] { 3, 8, 11, 14, 17 }));
        }

        [Fact]
        public void Vector_TestAddArgumentException()
        {

            var array1 = new[] { 1.0, 2.0, 3.0, 4.0, 5.0 };
            IVector vector1 = new Vector(array1);
            Exception ex = Record.Exception(() => vector1.Add(null));
            Exception exActual = new ArgumentNullException(nameof(vector1));
            Assert.StartsWith(ex.Message, exActual.Message);

        }

        [Fact]
        public void Vector_TestAddRankException()
        {

            var arrayMain = new[] { 1.0, 2.0, 3.0, 4.0, 5.0 };
            IVector vectorMain = new Vector(arrayMain);
            var array2 = new[] { 1.0, 2.0, 3.0, 4.0, 5.0, 6.0 };
            IVector vector2 = new Vector(array2);

            Exception ex = Record.Exception(() => vectorMain.Add(vector2));
            Exception exActual = new RankException();

            Assert.StartsWith(ex.Message, exActual.Message);
        }

        #endregion

        #region Hadamar Tests

        // нужен ли?
        [Fact]
        public void Vector_TestMockHadamardProduct()
        {
            var array1 = new[] { 1.0, 2.0, 3.0, 4.0, 5.0 };
            IVector vector1 = new Vector(array1);
            var array2 = new[] { 1.0, 2.0, 3.0, 4.0, 5.0 };
            IVector vector2 = new MockVector(array2);
            
            Assert.Throws<IndexOutOfRangeException>(() => vector1.HadamardProduct(vector2));
        }

        [Fact]
        public void Vector_TestHadamardProduct()
        {
            var array1 = new[] { 1.0, 2.0, 3.0, 4.0, 5.0 };
            IVector vector1 = new Vector(array1);
            var array2 = new[] { 1.0, 3.0, 4.0, 5.0, 6.0 };
            IVector vector2 = new Vector(array2);
            var result = vector1.HadamardProduct(vector2);
            for (int i = 0; i < vector1.Size; i++)
                Assert.Equal(result[i], array1[i] * array2[i]);
        }

        [Fact]
        public void Vector_TestHadamarArgumentException()
        {

            var array1 = new[] { 1.0, 2.0, 3.0, 4.0, 5.0 };
            IVector vector1 = new Vector(array1);
            Exception ex = Record.Exception(() => vector1.HadamardProduct(null));
            Exception exActual = new ArgumentNullException(nameof(vector1));
            Assert.StartsWith(ex.Message, exActual.Message);

        }

        [Fact]
        public void Vector_TestHadamarRankException()
        {

            var arrayMain = new[] { 1.0, 2.0, 3.0, 4.0, 5.0 };
            IVector vectorMain = new Vector(arrayMain);
            var array2 = new[] { 1.0, 2.0, 3.0, 4.0, 5.0, 6.0 };
            IVector vector2 = new Vector(array2);

            Exception ex = Record.Exception(() => vectorMain.HadamardProduct(vector2));
            Exception exActual = new RankException();

            Assert.StartsWith(ex.Message, exActual.Message);
        }

        #endregion


        #region DotProduct and Norm Test

        [Fact]
        public void Vector_TestNorm()
        {
            var array1 = new[] { 1.0, 2.0, 3.0, 4.0, 5.0 };
            IVector vector1 = new Vector(array1);
            Assert.Equal(vector1.Norm, Math.Sqrt(array1.Sum(x => x*x)));
            
            
        }

        [Fact]
        public void Vector_TestDotProductArgumentException()
        {

            var array1 = new[] { 1.0, 2.0, 3.0, 4.0, 5.0 };
            IVector vector1 = new Vector(array1);
            Exception ex = Record.Exception(() => vector1.DotProduct(null));
            Exception exActual = new ArgumentNullException(nameof(vector1));
            Assert.StartsWith(ex.Message, exActual.Message);

        }

        [Fact]
        public void Vector_TestDotProductRankException()
        {

            var arrayMain = new[] { 1.0, 2.0, 3.0, 4.0, 5.0 };
            IVector vectorMain = new Vector(arrayMain);
            var array2 = new[] { 1.0, 2.0, 3.0, 4.0, 5.0, 6.0 };
            IVector vector2 = new Vector(array2);

            Exception ex = Record.Exception(() => vectorMain.DotProduct(vector2));
            Exception exActual = new RankException();

            Assert.StartsWith(ex.Message, exActual.Message);
        }

#endregion

    }
}
