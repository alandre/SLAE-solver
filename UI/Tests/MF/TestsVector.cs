using System;
using System.Collections;
using System.Collections.Generic;
using SolverCore;
using System.Linq;
using Xunit;

namespace MF_VectorTests
{
    public class TestsVector
    {
        [Fact]
        public void Vector_TestConstructor()
        {
            var array = new[] { 1.0, 2.0, 3.0, 4.0, 5.0 };
            IVector vector = new Vector(array);

            Assert.Equal(array.Length, vector.Size);
            Assert.True(vector.SequenceEqual(array));  // 8 — количество знаков после запятой; 1e-8
        }

        [Fact]
        public void Vector_TestConstructorArgumentException()
        {
            Assert.Throws<ArgumentException>(() => { IVector vector = new Vector(-1); });
        }

        [Fact]
        public void Vector_TestEnumerator()
        {
            var array = new[] { 1.0, 2.0, 3.0, 4.0, 5.0 };
            IVector vector = new Vector(array);
            int i = 0;
            foreach (var elem in vector)
            {
                Assert.Equal(elem, array[i], 8);
                i++;
            }
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
            for (int i = 0; i < vector.Size; i++)
                Assert.Equal(vector[i], actual, 8);
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
            var vectorActual = new Vector(new double[] { 3, 8, 11, 14, 17 });

            for (int i = 0; i < result.Size; i++)
                Assert.Equal(result[i], vectorActual[i], 8);
        }

        [Fact]
        public void Vector_TestAddArgumentException()
        {
            var array1 = new[] { 1.0, 2.0, 3.0, 4.0, 5.0 };
            IVector vector1 = new Vector(array1);

            Assert.Throws<ArgumentNullException>(() => vector1.Add(null));
        }

        [Fact]
        public void Vector_TestAddRankException()
        {
            var arrayMain = new[] { 1.0, 2.0, 3.0, 4.0, 5.0 };
            IVector vectorMain = new Vector(arrayMain);
            var array2 = new[] { 1.0, 2.0, 3.0, 4.0, 5.0, 6.0 };
            IVector vector2 = new Vector(array2);

            Assert.Throws<RankException>(() => vectorMain.Add(vector2));
        }

        #endregion

        #region Hadamar Tests

        [Fact]
        public void Vector_TestHadamardProduct()
        {
            var array1 = new[] { 1.0, 2.0, 3.0, 4.0, 5.0 };
            IVector vector1 = new Vector(array1);
            var array2 = new[] { 1.0, 3.0, 4.0, 5.0, 6.0 };
            IVector vector2 = new Vector(array2);

            var result = vector1.HadamardProduct(vector2);
            var arrayActual = new double[] { 1, 6, 12, 20, 30 };

            for (int i = 0; i < vector1.Size; i++)
                Assert.Equal(result[i], arrayActual[i], 8);
        }

        [Fact]
        public void Vector_TestHadamarArgumentException()
        {

            var array1 = new[] { 1.0, 2.0, 3.0, 4.0, 5.0 };
            IVector vector1 = new Vector(array1);

            Assert.Throws<ArgumentNullException>(() => vector1.HadamardProduct(null));
        }

        [Fact]
        public void Vector_TestHadamarRankException()
        {
            var arrayMain = new[] { 1.0, 2.0, 3.0, 4.0, 5.0 };
            IVector vectorMain = new Vector(arrayMain);
            var array2 = new[] { 1.0, 2.0, 3.0, 4.0, 5.0, 6.0 };
            IVector vector2 = new Vector(array2);

            Assert.Throws<RankException>(() => vectorMain.HadamardProduct(vector2));
        }

        #endregion


        #region DotProduct and Norm Test

        [Fact]
        public void Vector_TestNorm()
        {
            var array1 = new[] { 1.0, 2.0, 3.0, 4.0, 5.0 };
            IVector vector1 = new Vector(array1);
            Assert.Equal(vector1.Norm, Math.Sqrt(array1.Sum(x => x * x)));
        }

        [Fact]
        public void Vector_TestDotProductArgumentException()
        {
            var array1 = new[] { 1.0, 2.0, 3.0, 4.0, 5.0 };
            IVector vector1 = new Vector(array1);

            Assert.Throws<ArgumentNullException>(() => vector1.DotProduct(null));
        }

        [Fact]
        public void Vector_TestDotProductRankException()
        {

            var arrayMain = new[] { 1.0, 2.0, 3.0, 4.0, 5.0 };
            IVector vectorMain = new Vector(arrayMain);
            var array2 = new[] { 1.0, 2.0, 3.0, 4.0, 5.0, 6.0 };
            IVector vector2 = new Vector(array2);

            Assert.Throws<RankException>(() => vectorMain.DotProduct(vector2));
        }

        #endregion

    }
}
