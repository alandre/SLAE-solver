using System;
using System.Collections;
using System.Collections.Generic;
using SolverCore;
using System.Linq;
using Xunit;

namespace MF.VectorTests
{
    public class TestsVector
    {

        [Fact]
        public void Constructor()
        { 
            var array = new[] { 1.0, 2.0, 3.0, 4.0, 5.0 };
            IVector vector = new Vector(array);

            Assert.Equal(array.Length, vector.Size);
            Assert.True(vector.SequenceEqual(array));  // 8 — количество знаков после запятой; 1e-8
        }

        [Fact]
        public void ConstructorArgumentException()
        {
            Assert.Throws<ArgumentException>(() => { IVector vector = new Vector(-1); });
        }

        [Fact]
        public void Enumerator()
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
        public void Clone()
        {
            var array = new[] { 1.0, 2.0, 3.0, 4.0, 5.0 };
            IVector vector = new Vector(array);
            var testVector = vector.Clone();
            Assert.True(vector.SequenceEqual(testVector));
        }

        [Fact]
        public void SetConst()
        {
            var array = new[] { 1.0, 2.0, 3.0, 4.0, 5.0 };
            IVector vector = new Vector(array);
            vector.SetConst();

            double actual = 0;
            for (int i = 0; i < vector.Size; i++)
                Assert.Equal(vector[i], actual, 8);
        }

        [Fact]
        public void NegativeIndex()
        {
            var array = new[] { 1.0, 2.0, 3.0, 4.0, 5.0 };
            IVector vector = new Vector(array);

            Assert.Throws<IndexOutOfRangeException>(() => vector[-1]);

        }

        [Fact]
        public void IndexOverSize()
        {
            var array = new[] { 1.0, 2.0, 3.0, 4.0, 5.0 };
            IVector vector = new Vector(array);

            Assert.Throws<IndexOutOfRangeException>(() => vector[vector.Size + 1]);
        }

        [Fact]
        public void NullArgumentInConstructor()
        {
            Assert.Throws<ArgumentNullException>(() => { IVector vector = new Vector(null); });
        }

        #region Add Tests

        [Fact]
        public void Add()
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
        public void AddArgumentException()
        {
            var array1 = new[] { 1.0, 2.0, 3.0, 4.0, 5.0 };
            IVector vector1 = new Vector(array1);

            Assert.Throws<ArgumentNullException>(() => vector1.Add(null));
        }

        [Fact]
        public void AddRankException()
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
        public void HadamardProduct()
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
        public void HadamarArgumentException()
        {

            var array1 = new[] { 1.0, 2.0, 3.0, 4.0, 5.0 };
            IVector vector1 = new Vector(array1);

            Assert.Throws<ArgumentNullException>(() => vector1.HadamardProduct(null));
        }

        [Fact]
        public void HadamarRankException()
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
        public void Norm()
        {
            var array1 = new[] { 1.0, 2.0, 3.0, 4.0, 5.0 };
            IVector vector1 = new Vector(array1);
            Assert.Equal(vector1.Norm, Math.Sqrt(array1.Sum(x => x * x)));
        }

        [Fact]
        public void DotProductArgumentException()
        {
            var array1 = new[] { 1.0, 2.0, 3.0, 4.0, 5.0 };
            IVector vector1 = new Vector(array1);

            Assert.Throws<ArgumentNullException>(() => vector1.DotProduct(null));
        }

        [Fact]
        public void DotProductRankException()
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
